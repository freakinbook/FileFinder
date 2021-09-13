using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileFinder
{
	
	public partial class Form1 : Form
	{
		private long cumulativeSize = 0;
		private string selectedDirectory;
		private int fileCount = 0;

		private IntHolder intHolder = new IntHolder();

		private delegate void textBoxDelegate(string text);
		private delegate void buttonDelegate(object sender);
		private delegate void labelDelegate(int fileCount);

		CancellationToken cancellationToken;
		CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


		public Form1()
		{
			InitializeComponent();
		}

		private void ExecuteSearch(string directory, CancellationToken token)
		{
			Task task = Task.Factory.StartNew(() =>
			{
				Search(selectedDirectory, token);
			}, TaskCreationOptions.None);
			task.Wait();
			var d = new textBoxDelegate(Write);
			filePathsTextBox.Invoke(d, $"-----------------------\r\nCumulative size of all found files is {cumulativeSize} bytes.\r\n");
			var bd = new buttonDelegate(ChangeButton);
			searchButton.Invoke(bd, false);
		}

		private void Search(string dirPath, CancellationToken token)
		{
			string[] subDirs = Directory.GetDirectories(dirPath);
			Task task = Task.Factory.StartNew(() =>
			{
				GetFiles(dirPath, token);
			}, TaskCreationOptions.AttachedToParent);

			foreach (string subDir in subDirs)
			{
				if (token.IsCancellationRequested)
				{
					return;
				}
				Task task1 = Task.Factory.StartNew(() =>
				{
					Search(subDir, token);
				}, TaskCreationOptions.AttachedToParent);
			}
		}

		private void GetFiles(string dirPath, CancellationToken token)
		{
			string[] filePaths = Directory.GetFiles(dirPath);
			foreach (string filePath in filePaths)
			{
				if (token.IsCancellationRequested)
				{
					return;
				}
				long fileSize = new FileInfo(filePath).Length;
				cumulativeSize += fileSize;
				Interlocked.Increment(ref intHolder.Value);
				var ld = new labelDelegate(UpdateLabel);
				countLabel.Invoke(ld, intHolder.Value);
				var d = new textBoxDelegate(Write);
				filePathsTextBox.Invoke(d, filePath);
			}
		}

		private void chooseFolderButton_Click(object sender, EventArgs e)
		{
			DialogResult dr = folderBrowserDialog1.ShowDialog();
			if (dr == DialogResult.OK)
			{
				folderPathTextBox.Clear();
				selectedDirectory = folderBrowserDialog1.SelectedPath;
				folderPathTextBox.AppendText(selectedDirectory);
			}
		}

		private void searchButton_Click(object sender, EventArgs e)
		{
			if (selectedDirectory == null)
			{
				MessageBox.Show("Please choose a directory");
				return;
			}
			Reset(sender);
			ChangeButton(sender);
			Task.Run(() => ExecuteSearch(selectedDirectory, cancellationToken));
		}

		private void stopButton_Click(object sender, EventArgs e)
		{
			ChangeButton(sender);
			cancellationTokenSource.Cancel();
		}


		private void ChangeButton(object sender)
		{
			if ((sender as Button) == searchButton)
			{
				searchButton.Visible = false;
				stopButton.Visible = true;
				chooseFolderButton.Enabled = false;
			}
			if ((sender as Button) == stopButton)
			{
				searchButton.Visible = true;
				stopButton.Visible = false;
				chooseFolderButton.Enabled = true;
			}
		}

		private void Reset(object sender)
		{
			filePathsTextBox.Clear();
			ChangeButton(sender);
			cumulativeSize = 0;
			cancellationTokenSource = new CancellationTokenSource();
			cancellationToken = cancellationTokenSource.Token;
		}

		private void Write(string text)
		{
			filePathsTextBox.AppendText(text + "\n");
		}

		private void UpdateLabel(int fileCount)
		{
			countLabel.Text = fileCount.ToString();
		}
	}
}
