using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExperimentExcelDataReader
{
    class Program
    {
        static void Main(string[] args)
        {
            var ds = ReadExcelData(@"E:\MAT_DataExpert\Project\181217_MissingInventorNames\201904_JPBIBREM_INVENTOR_REQ.xlsx");

            DataTable tbl = ds.Tables["Inventor B1&B2"];

            foreach (DataRow row in tbl.Select("Type='CD'"))
            {
                for (var i = 0; i < tbl.Columns.Count; i++)
                {
                    Console.Write("{0} ", row[i]);
                }
                Console.WriteLine("");
            }
        }

        static DataSet ReadExcelData(string path)
        {
            DataSet ds = null;
            var ext = Path.GetExtension(path);

            try
            {
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        var conf = new ExcelDataSetConfiguration
                        {
                            ConfigureDataTable = _ => new ExcelDataTableConfiguration
                            {
                                UseHeaderRow = true
                            }
                        };

                        ds = reader.AsDataSet(conf);
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }

            return ds;
        }
    }
}
