using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;


namespace CSVMergeTool
{

	public static class CSVHelpers
	{
		public static void CombineCsvFiles(string sourceFolder, string destinationFile, string searchPattern = "*.csv")
		{
				// Specify wildcard search to match CSV files that will be combined
				string[] filePaths = Directory.GetFiles(sourceFolder, searchPattern);

				CombineCsvFiles(filePaths, destinationFile);
			
		}

		public static void CombineCsvFiles(string[] filePaths, string destinationFile)
		{
			StreamWriter fileDest = new StreamWriter(destinationFile, true);

			int i;
			for (i = 0; i < filePaths.Length; i++)
			{
				string file = filePaths[i];

				string[] lines = File.ReadAllLines(file);

				if (i > 0)
				{
					lines = lines.Skip(1).ToArray(); // Skip header row for all but first file
				}

				foreach (string line in lines)
				{
					fileDest.WriteLine(line);
				}
			}

			fileDest.Close();
		}
	}

	public static class DataTableHelper
	{
		public static DataTable ToDataTable(this Dictionary<string, List<object>> dict)
		{
			DataTable dataTable = new DataTable();

			dataTable.Columns.AddRange(dict.Keys.Select(c => new DataColumn(c)).ToArray());

			for (int i = 0; i < dict.Values.Max(item => item.Count()); i++)
			{
				DataRow dataRow = dataTable.NewRow();

				foreach (var key in dict.Keys)
				{
					if (dict[key].Count > i)
						dataRow[key] = dict[key][i];
				}
				dataTable.Rows.Add(dataRow);
			}

			return dataTable;
		}

		public static void ToCSV(this DataTable dt, string destinationfile)
		{
			StringBuilder sb = new StringBuilder();

			IEnumerable<string> columnNames = dt.Columns.Cast<DataColumn>().
											  Select(column => column.ColumnName);
			sb.AppendLine(string.Join(",", columnNames));

			foreach (DataRow row in dt.Rows)
			{
				IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString());
				sb.AppendLine(string.Join(",", fields));
			}

			File.WriteAllText(destinationfile, sb.ToString());
		}
	}

}
