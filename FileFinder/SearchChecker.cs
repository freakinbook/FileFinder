using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileFinder
{

	// I made this class to check why my unit test doesn't always work
	// turned out that it's because of a PathTooLongException
	// I am using .NET 4.6.1 (and unable to change it) and max Path length was increased to 32767 characters in .NET 4.6.2
	class SearchChecker : FileNameReceiver
	{
		List<string> fileNames = new List<string>();
		public Searcher s;
		string dirPath;
		StringBuilder sb = new StringBuilder();
		object obj = new object();

		public SearchChecker(string dirPath)
		{
			this.s = new Searcher(this);
			this.dirPath = dirPath;
		}

		// without lock, objects get lost
		public void Print(string fileName)
		{
			lock (obj)
			{
				sb.Append(fileName);
			}
			
		}

		public void Check()
		{
			string[] files = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);
			string[] allFiles = sb.ToString().Replace("\n","").Split('\r');
			IEnumerable<string> difference = files.Except<string>(allFiles);
			foreach (var dif in difference)
			{
				Console.WriteLine(dif);
			}
		}

		public void ShowCount(int fileCount)
		{
			
		}

		public void StopReceiving()
		{
			s.CancelSearch();
		}
	}
}
