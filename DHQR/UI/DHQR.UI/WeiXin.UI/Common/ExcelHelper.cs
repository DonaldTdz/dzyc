using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace DHQR.UI.Common
{
    public class ExcelHelper
    {
        #region Excel导出

        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        public static void Export<T>(IList<T> list, string headerText, string filePath)
        {
            DataTable dataTable = ToDataTable(list);
            Export(dataTable, headerText, filePath);
        }

        public static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Title = strHeaderText;
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-MM-dd hh:mm:ss");

            //取得列宽
            var arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    {
                        IRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.CENTER;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count));
                    }


                    {
                        IRow headerRow = sheet.CreateRow(1);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.CENTER;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;
                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1)*256);
                        }

                    }
                    rowIndex = 2;
                }


                IRow dataRow = sheet.CreateRow(rowIndex);
                dataRow.Height = 400;
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);
                    newCell.CellStyle.Alignment = HorizontalAlignment.CENTER;
                    newCell.CellStyle.VerticalAlignment = VerticalAlignment.CENTER;
                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String": //字符串类型
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime": //日期类型
                            DateTime dateV;
                            DateTime.TryParse(drValue, out dateV);
                            if (dateV > DateTime.MinValue)
                            {
                                newCell.SetCellValue(dateV.ToString("yyyy-MM-dd hh:mm:ss"));
                                newCell.CellStyle = dateStyle; //格式化显示
                            }
                            break;
                        case "System.Boolean": //布尔型
                            bool boolV;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16": //整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal": //浮点型
                        case "System.Double":
                            double doubV;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.Single": //空值处理
                            Single SingleV;
                            Single.TryParse(drValue, out SingleV);
                            newCell.SetCellValue(SingleV);
                            break;
                        case "System.DBNull": //空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue(drValue);
                            break;
                    }

                }
                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }


        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                if (row != null)
                {
                    DataRow dataRow = dt.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }

                    dt.Rows.Add(dataRow);
                }
            }
            return dt;
        }

        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable(list, null);
        }

        public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof (T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (T item in list)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    if (!string.IsNullOrEmpty(prop.Description))
                    {
                        row[prop.Description] = prop.GetValue(item);
                    }
                }
                table.Rows.Add(row);
            }
            return table;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof (T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
            foreach (PropertyDescriptor prop in properties)
            {
                if (!string.IsNullOrEmpty(prop.Description))
                {
                    table.Columns.Add(prop.Description, prop.PropertyType);
                    //table.Columns.Add(prop.Description, prop.ComponentType.BaseType);
                }
            }
            return table;
        }

        #endregion

        #region  Excel导入   时读取excel表内容为datatable

        /// <summary>  
        /// 读取Excel文件到table中  
        /// </summary>  
        /// <param name="path">excel文件路径</param>  
        /// <returns></returns>  
        public static List<DataTable> ReadExcel(string path)
        {
            List<DataTable> dtList = ImportExcelFile(path);
            return dtList;
        }

        public static List<DataTable> ImportExcelFile(string filePath)
        {
            HSSFWorkbook hssfworkbook;

            #region//初始化信息

            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            #endregion

            var number = hssfworkbook.Workbook.NumSheets;
            List<DataTable> tbs = new List<DataTable>();
            for (int k = 0; k < number; k++)
            {
                NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(k);
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                DataTable dt = new DataTable();
                rows.MoveNext();
                HSSFRow row = (HSSFRow) rows.Current;
                for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
                {
                    //dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());  
                    //将第一列作为列表头  
                    dt.Columns.Add(row.GetCell(j).ToString());
                }
                while (rows.MoveNext())
                {
                    row = (HSSFRow) rows.Current;
                    DataRow dataRow = dt.NewRow();
                    for (int i = 0; i < row.LastCellNum; i++)
                    {
                        ICell cell = row.GetCell(i);
                        if (cell.CellType == CellType.NUMERIC)
                        {
                            //NPOI中数字和日期都是NUMERIC类型的，这里对其进行判断是否是日期类型
                            
                            //if (HSSFDateUtil.IsCellDateFormatted(cell)) //日期类型
                            if(DateUtil.IsCellDateFormatted(cell))
                            {
                                dataRow[i] = cell.DateCellValue;
                            }
                            else //其他数字类型
                            {
                                dataRow[i] = cell.NumericCellValue;
                            }
                        }
                        else if (cell.CellType == CellType.BLANK) //空数据类型
                        {
                            dataRow[i] = "";
                        }
                        else //其他类型都按字符串类型来处理
                        {
                            dataRow[i] = cell.StringCellValue;
                        }

                        //NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                        //if (cell == null)
                        //{
                        //    dr[i] = null;
                        //}
                        //else
                        //{
                        //    dr[i] = cell.ToString();
                        //}
                    }
                    dt.Rows.Add(dataRow);
                }
                tbs.Add(dt);
            }
            return tbs;
        }

    #endregion

        #region  DataTable转IList

        /// <summary>
        /// DataTable转IList
        /// </summary>
        /// <typeparam name="T">待返回的list集合实体</typeparam>
        /// <param name="table">待传入的datatable</param>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }



        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

                foreach (DataColumn column in row.Table.Columns)
                {
                    foreach (PropertyDescriptor prop in properties)
                    {
                        if (prop.Description == column.ColumnName)
                        {
                            PropertyInfo props = obj.GetType().GetProperty(prop.Name);
                            try
                            {
                                var value = row[column.Caption].ToString();
                                switch (props.PropertyType.ToString())
                                {
                                    case "System.String"://字符串类型
                                        props.SetValue(obj, value, null);
                                        //newCell.SetCellValue(drValue);
                                        break;
                                    case "System.DateTime"://日期类型
                                        DateTime dateV;
                                        DateTime.TryParse(value, out dateV);
                                        if (dateV > DateTime.MinValue)
                                        {
                                            props.SetValue(obj, dateV, null);
                                        }
                                        break;
                                    case "System.Boolean"://布尔型
                                        bool boolV;
                                        bool.TryParse(value, out boolV);
                                        props.SetValue(obj, boolV, null);
                                        break;
                                    case "System.Int16"://整型
                                    case "System.Int32":
                                    case "System.Int64":
                                    case "System.Byte":
                                        int intV;
                                        int.TryParse(value, out intV);
                                        props.SetValue(obj, intV, null);
                                        break;
                                    case "System.Decimal"://浮点型
                                        decimal decimalV;
                                        decimal.TryParse(value, out decimalV);
                                        props.SetValue(obj, decimalV, null);
                                        break;
                                    case "System.Double":
                                        double doubV;
                                        double.TryParse(value, out doubV);
                                        props.SetValue(obj, doubV, null);
                                        break;
                                    case "System.Single"://空值处理
                                        Single SingleV;
                                        Single.TryParse(value, out SingleV);
                                        props.SetValue(obj, SingleV, null);
                                        break;
                                    case "System.DBNull"://空值处理
                                        props.SetValue(obj, "", null);
                                        break;
                                    default:
                                        props.SetValue(obj, "", null);
                                        break;
                                }
                            }
                            catch
                            {
                                // You can log something here  
                                throw;
                            }
                            break;
                        }
                    }
                }
            }
            return obj;
        }
        #endregion
    }
}