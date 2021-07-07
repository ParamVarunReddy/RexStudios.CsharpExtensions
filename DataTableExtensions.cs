using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RexStudios.CsharpExtensions
{
    public static class DataTableExtensions
    {
        public static void ToCSV(this DataTable table, string delimiter, bool includeHeader)
        {
            StringBuilder result = new StringBuilder();
            if (includeHeader)
            {

                foreach (DataColumn column in table.Columns)
                {
                    result.Append(column.ColumnName);
                    result.Append(delimiter);
                }
                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }

            foreach (DataRow row in table.Rows)
            {
                foreach (object item in row.ItemArray)
                {
                    if (item is System.DBNull)
                        result.Append(delimiter);
                    else
                    {
                        string itemAsString = item.ToString();
                        // Double up all embedded double quotes
                        itemAsString = itemAsString.Replace("\"", "\"\"");
                        // To keep things simple, always delimit with double-quotes
                        // so we don't have to determine in which cases they're necessary
                        // and which cases they're not.
                        itemAsString = "\"" + itemAsString + "\"";
                        result.Append(itemAsString + delimiter);
                    }

                }
                result.Remove(--result.Length, 0);
                result.Append(Environment.NewLine);
            }
            using (StreamWriter writer = new StreamWriter(@"C:\log.csv", true))
            {
                writer.Write(result.ToString());
            }
        }
    }
}
