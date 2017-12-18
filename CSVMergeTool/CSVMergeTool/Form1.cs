using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSVMergeTool
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

		}

		private void button1_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderDlg = new FolderBrowserDialog();
			folderDlg.ShowNewFolderButton = true;
			// Show the FolderBrowserDialog.
			DialogResult result = folderDlg.ShowDialog();
			if (result == DialogResult.OK)
			{
				textBox1.Text = folderDlg.SelectedPath;
				Environment.SpecialFolder root = folderDlg.RootFolder;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			var srcPath = textBox1.Text;
			var destinationFile = textBox1.Text + "/CombinedCSV" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";

			if (string.IsNullOrEmpty(srcPath)){
				MessageBox.Show("Select Folder path for CSV files", "Alert");
				return;
			}

			try
			{
				CSVHelpers.CombineCsvFiles(srcPath, destinationFile);
				MessageBox.Show("CSV Files merged successfully", "Success");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error");
				return;
			}
		}
	}
}
