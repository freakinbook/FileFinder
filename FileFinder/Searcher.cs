using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileFinder
{
	public class Searcher
	{
		private long cumulativeSize = 0;
		private IntHolder intHolder = new IntHolder();
		public int Count { get { return intHolder.Value; } }

		CancellationTokenSource cts;
		FileNameReceiver receiver;


		public Searcher(FileNameReceiver receiver)
		{
			this.receiver = receiver;
		}

		public Task ExecuteSearch(string directory)
		{
			Reset();
			// the task that recursively searches for files is started and awaited by its .ContinueWith
			return Task.Factory.StartNew(() =>
			{
				GetSubDirectories(directory);
			}, cts.Token)
			// ContinueWith task is showing whether operation was cancelled and shows the cumulative size of all found files 
			.ContinueWith(result =>
			{
				if (cts.Token.IsCancellationRequested)
				{
					receiver.Print("\r\n-----------------------\r\nOperation cancelled");
				}
				receiver.Print($"\r\n-----------------------\r\nCumulative size of all found files is {cumulativeSize} bytes.\r\n");
				receiver.StopReceiving();
			});
		}

		// takes the name of a directory and gets its files and subdirectories
		// for every subdir it repeats recursively
		private void GetSubDirectories(string dirPath)
		{
			try
			{
				string[] subDirs;
				try
				{
					subDirs = Directory.GetDirectories(dirPath);
				}
				catch (UnauthorizedAccessException uae)
				{
					receiver.Print(uae.Message + "\r\n");
					return;
				}
				catch (DirectoryNotFoundException dnfe)
				{
					receiver.Print(dnfe.Message + "\r\n");
					return;
				}
				Task task = Task.Factory.StartNew(() =>
				{
					GetFiles(dirPath);
				}, cts.Token, TaskCreationOptions.AttachedToParent, TaskScheduler.Current);
				foreach (string subDir in subDirs)
				{
					Task task1 = Task.Factory.StartNew(() =>
					{
						GetSubDirectories(subDir);
					}, cts.Token, TaskCreationOptions.AttachedToParent, TaskScheduler.Current);
				}
			}
			catch
			{
				receiver.ShowCount(intHolder.Value);
				throw;
			}
		}

		//takes a directory path as a string and finds all the files in it
		private void GetFiles(string dirPath)
		{
			StringBuilder buffer = new StringBuilder();
			try
			{
				string[] filePaths = Directory.GetFiles(dirPath);
				foreach (string filePath in filePaths)
				{
					long fileSize = new FileInfo(filePath).Length;
					// add file size to cumulative size
					Interlocked.Add(ref cumulativeSize, fileSize);
					// increment counter after found file thread-safely 
					Interlocked.Increment(ref intHolder.Value);
					buffer.Append(filePath + "\r\n");
				}
				// without Thread.Sleep(10) gui becomes unresponsive
				// my guess is that it writes to textBox too often and doesn't give time to other controls
				Thread.Sleep(10);
				receiver.Print(buffer.ToString());
				receiver.ShowCount(intHolder.Value);
			}
			catch
			{
				receiver.Print(buffer.ToString());
				receiver.ShowCount(intHolder.Value);
				//TODO: handle System.IO.FileNotFoundException 
				throw;
			}
		}

		public void CancelSearch()
		{
			cts.Cancel();
		}

		// resetting the variables
		public void Reset()
		{
			cumulativeSize = 0;
			intHolder.Value = 0;
			receiver.ShowCount(intHolder.Value);
			cts = new CancellationTokenSource();
		}
	}
}
