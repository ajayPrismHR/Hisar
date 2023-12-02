using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop;
using OfficeExcel = Microsoft.Office.Interop.Excel;

namespace ComplaintTracker.ExcelLib
{
    public class ExcelMachine
    {

        public static void GenerateMonthlyReport(DataSet dataSet, string fileName)
        {

            Microsoft.Office.Interop.Excel.Application application = new Microsoft.Office.Interop.Excel.Application();
            application.Application.Workbooks.Add(Type.Missing);
            application.Columns.AutoFit();

            for (int i = 0; i< dataSet.Tables[0].Columns.Count;i++)
            {
                Microsoft.Office.Interop.Excel.Range xlRange = (Microsoft.Office.Interop.Excel.Range)application.Cells[1, i];
                xlRange.Font.Bold = -1;
                xlRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                xlRange.Borders.Weight = 1d;
                xlRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                application.Cells[1, i] = dataSet.Tables[0].Columns[i - 1].ColumnName;
            }

            for(int i=0; i < dataSet.Tables[0].Rows.Count;i++)
            {

                for (int j = 0; j < dataSet.Tables[0].Columns.Count; j++)
                {
                    if (dataSet.Tables[0].Rows[i][j] !=null)
                    {

                        Microsoft.Office.Interop.Excel.Range xlRange = (Microsoft.Office.Interop.Excel.Range)application.Cells[1, i];
                        xlRange.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        xlRange.Borders.Weight = 1d;
                        application.Cells[i + 2, j + 1] = dataSet.Tables[0].Rows[i][j].ToString();

                    }
                }

            }

            application.Columns.AutoFit();
            
            application.ActiveWorkbook.SaveCopyAs();

            
            







        }


        /// <summary>
        /// Fuction to export dataset to excel
        /// </summary>
        /// <param name="ds"></param>
        internal static void ExportDataSetToExcel(DataSet ds, string strPath)
        {
            //Summary of Monthly Complaint Report
            // Division — Hissar , SubDivision —  Sub Div Civil Lines, For the Month of November


            int inHeaderLength = 3, inColumn = 0, inRow = 0;

            System.Reflection.Missing Default = System.Reflection.Missing.Value;
            //Create Excel File
            strPath += @"\monthly" + DateTime.Now.ToString().Replace(':', '-') + ".xlsx";
            OfficeExcel.Application excelApp = new OfficeExcel.Application();
            OfficeExcel.Workbook excelWorkBook = excelApp.Workbooks.Add(1);
            foreach (DataTable dtbl in ds.Tables)
            {
                //Create Excel WorkSheet
                OfficeExcel.Worksheet excelWorkSheet = excelWorkBook.Sheets.Add(Default, excelWorkBook.Sheets[excelWorkBook.Sheets.Count], 1, Default);
                excelWorkSheet.Name = dtbl.TableName;//Name worksheet

                //Write Column Name
                for (int i = 0; i < dtbl.Columns.Count; i++)
                    excelWorkSheet.Cells[inHeaderLength + 1, i + 1] = dtbl.Columns[i].ColumnName.ToUpper();

                //Write Rows
                for (int m = 0; m < dtbl.Rows.Count; m++)
                {
                    for (int n = 0; n < dtbl.Columns.Count; n++)
                    {
                        inColumn = n + 1;
                        inRow = inHeaderLength + 2 + m;
                        excelWorkSheet.Cells[inRow, inColumn] = dtbl.Rows[m].ItemArray[n].ToString();
                        //if (m % 2 == 0)
                        //    excelWorkSheet.get_Range("A" + inRow.ToString(), "AH" + inRow.ToString()).Interior.Color = System.Drawing.ColorTranslator.FromHtml("#009dd1");
                    }
                }

                //Excel Header
                OfficeExcel.Range cellRang = excelWorkSheet.get_Range("A1", "AH1");
                cellRang.Merge(false);
                cellRang.Interior.Color = System.Drawing.Color.White;
                cellRang.Font.Color = System.Drawing.Color.Gray;
                cellRang.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                cellRang.VerticalAlignment = OfficeExcel.XlVAlign.xlVAlignCenter;
                cellRang.Font.Size = 18;
                excelWorkSheet.Cells[1, 1] = "Summary of Monthly Complaint Report";


                //OfficeExcel.Range cellRang2 = excelWorkSheet.get_Range("A2", "AH2");
                //cellRang2.Merge(false);
                //cellRang2.Interior.Color = System.Drawing.Color.White;
                //cellRang2.Font.Color = System.Drawing.Color.Red;
                //cellRang2.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignCenter;
                //cellRang2.VerticalAlignment = OfficeExcel.XlVAlign.xlVAlignCenter;
                //cellRang2.Font.Size = 16;
                //excelWorkSheet.Cells[1, 2] = "Division — Hissar , SubDivision —  Sub Div Civil Lines, For the Month of November";



                //Style table column names
                cellRang = excelWorkSheet.get_Range("A2", "AH2");
                cellRang.Font.Bold = true;
                cellRang.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
                cellRang.Interior.Color = System.Drawing.ColorTranslator.FromHtml("#009dd1");
                excelWorkSheet.get_Range("B2").EntireColumn.HorizontalAlignment = OfficeExcel.XlHAlign.xlHAlignRight;
                //Formate price column
                //excelWorkSheet.get_Range("F5").EntireColumn.NumberFormat = "0.00";
                //Auto fit columns
                excelWorkSheet.Columns.AutoFit();
            }

            //Delete First Page
            excelApp.DisplayAlerts = false;
            Microsoft.Office.Interop.Excel.Worksheet lastWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];
            lastWorkSheet.Delete();
            excelApp.DisplayAlerts = true;

            //Set Defualt Page
            (excelWorkBook.Sheets[1] as OfficeExcel._Worksheet).Activate();

            excelWorkBook.SaveAs(strPath, Default, Default, Default, false, Default, OfficeExcel.XlSaveAsAccessMode.xlNoChange, Default, Default, Default, Default, Default);
            excelWorkBook.Close();
            excelApp.Quit();

        }
    }
}