using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinder
{
	public interface FileNameReceiver
	{
		void Print(string fileName);
		void ShowCount(int fileCount);
		void StopReceiving();
	}
}
