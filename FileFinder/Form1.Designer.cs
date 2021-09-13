namespace FileFinder
{
	partial class Form1
	{
		/// <summary>
		/// Обязательная переменная конструктора.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Освободить все используемые ресурсы.
		/// </summary>
		/// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Код, автоматически созданный конструктором форм Windows

		/// <summary>
		/// Требуемый метод для поддержки конструктора — не изменяйте 
		/// содержимое этого метода с помощью редактора кода.
		/// </summary>
		private void InitializeComponent()
		{
			this.chooseFolderButton = new System.Windows.Forms.Button();
			this.searchButton = new System.Windows.Forms.Button();
			this.folderPathTextBox = new System.Windows.Forms.TextBox();
			this.stopButton = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.filePathsTextBox = new System.Windows.Forms.TextBox();
			this.countLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// chooseFolderButton
			// 
			this.chooseFolderButton.Location = new System.Drawing.Point(81, 12);
			this.chooseFolderButton.Name = "chooseFolderButton";
			this.chooseFolderButton.Size = new System.Drawing.Size(109, 27);
			this.chooseFolderButton.TabIndex = 0;
			this.chooseFolderButton.Text = "Choose folder...";
			this.chooseFolderButton.UseVisualStyleBackColor = true;
			this.chooseFolderButton.Click += new System.EventHandler(this.chooseFolderButton_Click);
			// 
			// searchButton
			// 
			this.searchButton.Location = new System.Drawing.Point(96, 109);
			this.searchButton.Name = "searchButton";
			this.searchButton.Size = new System.Drawing.Size(79, 27);
			this.searchButton.TabIndex = 1;
			this.searchButton.Text = "Search";
			this.searchButton.UseVisualStyleBackColor = true;
			this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
			// 
			// folderPathTextBox
			// 
			this.folderPathTextBox.Location = new System.Drawing.Point(10, 45);
			this.folderPathTextBox.Name = "folderPathTextBox";
			this.folderPathTextBox.ReadOnly = true;
			this.folderPathTextBox.Size = new System.Drawing.Size(250, 20);
			this.folderPathTextBox.TabIndex = 2;
			// 
			// stopButton
			// 
			this.stopButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.stopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.stopButton.Location = new System.Drawing.Point(51, 83);
			this.stopButton.Name = "stopButton";
			this.stopButton.Size = new System.Drawing.Size(169, 78);
			this.stopButton.TabIndex = 3;
			this.stopButton.Text = "Stop it!";
			this.stopButton.UseVisualStyleBackColor = false;
			this.stopButton.Visible = false;
			this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
			// 
			// filePathsTextBox
			// 
			this.filePathsTextBox.Location = new System.Drawing.Point(11, 180);
			this.filePathsTextBox.Multiline = true;
			this.filePathsTextBox.Name = "filePathsTextBox";
			this.filePathsTextBox.ReadOnly = true;
			this.filePathsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.filePathsTextBox.Size = new System.Drawing.Size(248, 208);
			this.filePathsTextBox.TabIndex = 4;
			// 
			// countLabel
			// 
			this.countLabel.AutoSize = true;
			this.countLabel.Location = new System.Drawing.Point(10, 395);
			this.countLabel.Name = "countLabel";
			this.countLabel.Size = new System.Drawing.Size(14, 13);
			this.countLabel.TabIndex = 5;
			this.countLabel.Text = "#";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(272, 414);
			this.Controls.Add(this.countLabel);
			this.Controls.Add(this.filePathsTextBox);
			this.Controls.Add(this.searchButton);
			this.Controls.Add(this.stopButton);
			this.Controls.Add(this.folderPathTextBox);
			this.Controls.Add(this.chooseFolderButton);
			this.Name = "Form1";
			this.Text = "File Finder v.0.1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button chooseFolderButton;
		private System.Windows.Forms.Button searchButton;
		private System.Windows.Forms.TextBox folderPathTextBox;
		private System.Windows.Forms.Button stopButton;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.TextBox filePathsTextBox;
		private System.Windows.Forms.Label countLabel;
	}
}

