using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FileFinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileFinderUnitTest
{
	[TestClass]
	public class UnitTest1
	{
		// sometimes method gives a false-negative result
		// I found out that it is because of a System.IO.PathTooLongException
		// max path length for .NET before 4.6.2 is 260 characters
		// in newer .NET builds it should always pass  
		[TestMethod]
		public async Task TestCount()
		{
			string dirPath = @"I:\";
			Console.WriteLine(dirPath);
			int fileCountTest = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories).Length;
			SimpleReceiver sr = new SimpleReceiver();
			await sr.searcher.ExecuteSearch(dirPath);
			Assert.AreEqual<int>(fileCountTest, sr.searcher.Count);
		}
	}

	public class SimpleReceiver : FileFinder.FileNameReceiver
	{
		public Searcher searcher { get; }
		

		public SimpleReceiver()
		{
			this.searcher = new Searcher(this);
		}
		public void Print(string fileName)
		{
			Console.WriteLine(fileName);
		}

		public void ShowCount(int fileCount)
		{
			Console.WriteLine(fileCount);
		}

		public void StopReceiving()
		{
			searcher.CancelSearch();
		}
	}
}
