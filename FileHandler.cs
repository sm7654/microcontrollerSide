using microcontrollerSide;
using Excel = Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System.IO;

namespace microcontrollerSide
{
    static class FileHandler
    {

        public static void EncryptExcelCells()
        {
            string path = "C:\\Users\\user\\Desktop\\Book1.xlsx";


            Excel.Application ExcelAPp = new Excel.Application();

            Excel.Workbook Workbook = ExcelAPp.Workbooks.Open(path);
            Excel.Worksheet worksheet = Workbook.Worksheets[1];

            ListObject resultTable = worksheet.ListObjects["resultsTable"];
            
        
            foreach (Excel.Range row in resultTable.DataBodyRange.Rows) 
            {
                byte[] EncryptedValue = Encoding.UTF8.GetBytes(Convert.ToString(row.Cells[1, 1].Value));
                EncryptedValue = AesEncryption.EncryptedData(EncryptedValue);
                row.Cells[1, 1].Value = Convert.ToBase64String(EncryptedValue);

                Console.WriteLine();
            }
            Workbook.Save();
            Workbook.Close();
            
        }



        public static byte[] GetFileBytes(string path)
        {
            try
            {
                return File.ReadAllBytes(path);
            }
            catch (Exception e) { return null; }
        }

        public static byte[] SaveExcelDataToFile(byte[] data)
        {

            string Name = @"C:\Users\user\Test.xlsx"; // excel file
            using (BinaryWriter Writer = new BinaryWriter(File.OpenWrite(Name)))
            {
                File.WriteAllBytes(Name, new byte[10]); // reset file content
                Writer.Write(data); // write the binary 
                Writer.Flush();
                Writer.Close();
            }
            return null;
        }

    }
}
