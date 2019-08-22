using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace 工件界面Demo.Public
{
    public static class FStreamHelper
    {
        /// <summary>
        /// 锁(防止操作同一文件产生错误)
        /// </summary>
        static readonly object filelock = new object();

        /// <summary>
        /// 写入txt
        /// </summary>
        /// <param name="para">要写入的字符</param>
        /// <param name="filepath">文件名</param>
        /// <param name="append">是否追加</param>
        /// <returns></returns>
        public static string WriteTxT(string para,string filepath,bool append)
        {
            try
            {
                string success = "失败";
                lock (filelock)
                {
                    using (StreamWriter sr = new StreamWriter(filepath, append))
                    {
                        sr.WriteLine($"{DateTime.Now}----{para}");
                        success = "成功";
                    }
                    return success;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        /// <summary>
        /// 读取txt
        /// </summary>
        /// <param name="filepath">文件路径</param>
        /// <returns></returns>
        public static string GetTxT(string filepath)
        {
            try
            {
                string str = string.Empty;
                lock (filelock)
                {
                    using (StreamReader sr = new StreamReader(filepath))
                    {
                        str = sr.ReadToEnd();
                    }
                }
                return str;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="table">数据表</param>
        /// <param name="_title">标题</param>
        /// <param name="_sheetName">表明</param>
        /// <param name="_filePath">路径</param>
        /// <returns></returns>
        public static bool ToExcel(DataTable table,string _title, string _sheetName, string _filePath)
        {
            lock (filelock)
            {
                FileStream fs = new FileStream(_filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                IWorkbook workBook = new HSSFWorkbook();
                _sheetName = string.IsNullOrEmpty(_sheetName) ? "sheet1" : _sheetName;
                ISheet sheet = workBook.CreateSheet(_sheetName);

                ////处理表格标题
                IRow row = sheet.CreateRow(0);
                //row.CreateCell(0).SetCellValue(_title);
                //sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, table.Columns.Count - 1));
                //row.Height = 500;

                //ICellStyle cellStyle = workBook.CreateCellStyle();
                //IFont font = workBook.CreateFont();
                //font.FontName = "微软雅黑";
                //font.FontHeightInPoints = 17;
                //cellStyle.SetFont(font);
                //cellStyle.VerticalAlignment = VerticalAlignment.Center;
                //cellStyle.Alignment = HorizontalAlignment.Center;
                //row.Cells[0].CellStyle = cellStyle;

                //处理表格列头
                row = sheet.CreateRow(0);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(table.Columns[i].ColumnName);
                    row.Height = 350;
                    sheet.AutoSizeColumn(i);
                }

                //处理数据内容
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    row = sheet.CreateRow(2 + i);
                    row.Height = 250;
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(table.Rows[i][j].ToString());
                        sheet.SetColumnWidth(j, 256 * 15);
                    }
                }

                //写入数据流
                workBook.Write(fs);
                fs.Flush();
                fs.Close();

                return true;
            }
        }

        /// <summary>
        /// Excel转换成DataTable（.xls）
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            lock (filelock)
            {
                var dt = new DataTable();
                FDataHelper.CreateDtColumns(ref dt);
                using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    var hssfworkbook = new HSSFWorkbook(file);
                    var sheet = hssfworkbook.GetSheetAt(0);
                    int lastcolumn = sheet.GetRow(0).LastCellNum;
                    for (var j = 5; j < lastcolumn; j++)
                    {
                        dt.Columns.Add("data" + (j - 4));
                    }
                    var rows = sheet.GetRowEnumerator();
                    while (rows.MoveNext())
                    {
                        var row = (HSSFRow)rows.Current;
                        var dr = dt.NewRow();
                        for (var i = 0; i < row.LastCellNum; i++)
                        {
                            var cell = row.GetCell(i);
                            if (cell == null)
                            {
                                dr[i] = null;
                            }
                            else
                            {
                                switch (cell.CellType)
                                {
                                    case CellType.Blank:
                                        dr[i] = "[null]";
                                        break;
                                    case CellType.Boolean:
                                        dr[i] = cell.BooleanCellValue;
                                        break;
                                    case CellType.Numeric:
                                        dr[i] = cell.ToString();
                                        break;
                                    case CellType.String:
                                        dr[i] = cell.StringCellValue;
                                        break;
                                    case CellType.Error:
                                        dr[i] = cell.ErrorCellValue;
                                        break;
                                    case CellType.Formula:
                                        try
                                        {
                                            dr[i] = cell.NumericCellValue;
                                        }
                                        catch
                                        {
                                            dr[i] = cell.StringCellValue;
                                        }
                                        break;
                                    default:
                                        dr[i] = "=" + cell.CellFormula;
                                        break;
                                }
                            }
                        }
                        dt.Rows.Add(dr);
                    }
                }
                return dt;
            }
        }

        /// <summary>
        /// Excel转换成DataSet（.xlsx/.xls）
        /// </summary>
        /// <param name="filePath">Excel文件路径</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath, out string strMsg)
        {
            lock (filelock)
            {
                strMsg = "";
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                string fileType = Path.GetExtension(filePath).ToLower();
                string fileName = Path.GetFileName(filePath).ToLower();
                try
                {
                    ISheet sheet = null;
                    int sheetNumber = 0;
                    FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    if (fileType == ".xlsx")
                    {
                        // 2007版本
                        XSSFWorkbook workbook = new XSSFWorkbook(fs);
                        sheetNumber = workbook.NumberOfSheets;
                        for (int i = 0; i < sheetNumber; i++)
                        {
                            string sheetName = workbook.GetSheetName(i);
                            sheet = workbook.GetSheet(sheetName);
                            if (sheet != null)
                            {
                                dt = GetSheetDataTable(sheet, out strMsg);
                                if (dt != null)
                                {
                                    dt.TableName = sheetName.Trim();
                                    ds.Tables.Add(dt);
                                }
                                else
                                {
                                    MsgBoxHelper.AlertMsgBox("Sheet数据获取失败，原因：" + strMsg, "提示");
                                }
                            }
                        }
                    }
                    else if (fileType == ".xls")
                    {
                        // 2003版本
                        HSSFWorkbook workbook = new HSSFWorkbook(fs);
                        sheetNumber = workbook.NumberOfSheets;
                        for (int i = 0; i < sheetNumber; i++)
                        {
                            string sheetName = workbook.GetSheetName(i);
                            sheet = workbook.GetSheet(sheetName);
                            if (sheet != null)
                            {
                                dt = GetSheetDataTable(sheet, out strMsg);
                                if (dt != null)
                                {
                                    dt.TableName = sheetName.Trim();
                                    ds.Tables.Add(dt);
                                }
                                else
                                {
                                    MsgBoxHelper.AlertMsgBox("Sheet数据获取失败，原因：" + strMsg, "提示");
                                }
                            }
                        }
                    }
                    return ds;
                }
                catch (Exception ex)
                {
                    strMsg = ex.Message;
                    return null;
                }
            }
        }

        /// <summary>
        /// 获取sheet表对应的DataTable
        /// </summary>
        /// <param name="sheet">Excel工作表</param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        private static DataTable GetSheetDataTable(ISheet sheet, out string strMsg)
        {
            lock (filelock)
            {
                strMsg = "";
                DataTable dt = new DataTable();
                FDataHelper.CreateDtColumns(ref dt);
                string sheetName = sheet.SheetName;
                int startIndex = 0;// sheet.FirstRowNum;
                int lastIndex = sheet.LastRowNum;
                //最大列数
                int cellCount = 0;
                IRow maxRow = sheet.GetRow(0);
                for (int i = startIndex; i <= lastIndex; i++)
                {
                    IRow row = sheet.GetRow(i);
                    if (row != null && cellCount < row.LastCellNum)
                    {
                        cellCount = row.LastCellNum;
                        maxRow = row;
                    }
                }
                //列名设置
                try
                {
                    for (int i = 5; i < maxRow.LastCellNum; i++)//maxRow.FirstCellNum
                    {
                        dt.Columns.Add("data" + (i - 4));
                    }
                }
                catch
                {
                    strMsg = "工作表" + sheetName + "中无数据";
                    return null;
                }
                //数据填充
                for (int i = startIndex; i <= lastIndex; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow drNew = dt.NewRow();
                    if (row != null)
                    {
                        for (int j = row.FirstCellNum; j < row.LastCellNum; ++j)
                        {
                            if (row.GetCell(j) != null)
                            {
                                ICell cell = row.GetCell(j);
                                switch (cell.CellType)
                                {
                                    case CellType.Blank:
                                        drNew[j] = "";
                                        break;
                                    case CellType.Numeric:
                                        short format = cell.CellStyle.DataFormat;
                                        //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                        if (format == 14 || format == 31 || format == 57 || format == 58)
                                            drNew[j] = cell.DateCellValue;
                                        else
                                            drNew[j] = cell.NumericCellValue;
                                        if (cell.CellStyle.DataFormat == 177 || cell.CellStyle.DataFormat == 178 || cell.CellStyle.DataFormat == 188)
                                            drNew[j] = cell.NumericCellValue.ToString("#0.00");
                                        break;
                                    case CellType.String:
                                        drNew[j] = cell.StringCellValue;
                                        break;
                                    case CellType.Formula:
                                        try
                                        {
                                            drNew[j] = cell.NumericCellValue;
                                            if (cell.CellStyle.DataFormat == 177 || cell.CellStyle.DataFormat == 178 || cell.CellStyle.DataFormat == 188)
                                                drNew[j] = cell.NumericCellValue.ToString("#0.00");
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                drNew[j] = cell.StringCellValue;
                                            }
                                            catch { }
                                        }
                                        break;
                                    default:
                                        drNew[j] = cell.StringCellValue;
                                        break;
                                }
                            }
                        }
                    }
                    dt.Rows.Add(drNew);
                }
                return dt;
            }
        }
    }
}
