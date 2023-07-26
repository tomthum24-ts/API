using BaseCommon.Common.Report.Models;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Barcode;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BaseCommon.Common.Report.Infrastructures
{
    public static class ExcelReportHelper
    {
        #region Avariable

        private const string TMP_ROW = "[TMP]";
        private const string TMP_ROW2 = "[TMP2]";
        private const string TMP_SHEET = "TMP";
        private static ExcelEngine _engine;
        private static List<int> _lstColumnsHide = null;
        private static List<int> _lstColumnsOutToSize = null;
        private static double? _leftMargin = null;

        // Replace

        private const string R_THU = "%R_Thu";
        private const string R_DAY = "%R_Ngay";
        private const string R_MONTH = "%R_Thang";
        private const string R_YEAR = "%R_Nam";
        private const string R_PLACE = "%R_DiaDiem";
        private const string R_USER = "%R_NguoiLap";

        private const int ROW_MAXIMUM = 200;
        private const int COL_MAXIMUM = 256;

        private const string FONT_NAME = "Arial";
        private const int HEADER_FONT_SIZE = 16;
        private const int SUBHEADER_FONT_SIZE = 13;
        private const int CAPTION_FONT_SIZE = 10;
        private const int CONTENT_FONT_SIZE = 10;

        #endregion Avariable

        #region Main Function
        public static IWorkbook AutoExportToExcel(byte[] noiDungBieuMau, string title, List<Dictionary<object, object>> contentData, Dictionary<string, RawDataInfo> headerData)
        {
            int rowStart = 2;
            int colStart = 2;

            int rowIndex = rowStart;
            int colIndex = colStart;

            int colNum = headerData.Count;

            // Get template stream
            MemoryStream stream = GetTemplateStream(noiDungBieuMau);

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workbook = engine.Excel.Workbooks.Open(stream);
            IWorksheet worksheet;

            worksheet = workbook.Worksheets[0];
            worksheet.Range[1, 1].ColumnWidth = 3;

            #region ---- Title ----

            worksheet.Range[rowIndex + 1, colIndex, rowIndex + 1, colIndex + colNum - 1].Merge();
            worksheet.Range[rowIndex + 1, colIndex, rowIndex + 1, colIndex + colNum - 1].Text = headerData == null || !headerData.Any() ? title : headerData.First().Value.TieuDeBieuMau;
            worksheet.Range[rowIndex + 1, colIndex, rowIndex + 1, colIndex + colNum - 1].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

            CellStyle(worksheet, rowIndex + 1, colIndex, rowIndex + 1, colIndex + 6, "Times New Roman", 16, true, false);

            #endregion ---- Title ----

            #region Heade
            // Su dung cho title
            rowIndex += 3;

            // Write header
            WriteColumHeader(worksheet, rowIndex, colStart, headerData.Values.ToList());
            CellStyleBackGround(worksheet, rowIndex, colStart, rowIndex, colStart + colNum - 1);
            #endregion Header

            #region Content

            int stt = 0;
            string columnName = string.Empty;
            object objValue = null;
            int? canhLeStt = headerData["stt"].CanhLe;
            // Remove stt to avoid confusion with data's stt field
            headerData.Remove("stt");

            // Write record
            foreach (Dictionary<object, object> record in contentData)
            {
                rowIndex++;
                colIndex = colStart;

                // STT
                stt++;
                worksheet.Range[rowIndex, colIndex, rowIndex, colIndex].Text = stt.ToString();
                worksheet.Range[rowIndex, colIndex, rowIndex, colIndex].CellStyle.HorizontalAlignment = (ExcelHAlign)canhLeStt;


                // Write cell
                foreach (var header in headerData)
                {
                    colIndex++;
                    // Get value in cell
                    objValue = record[header.Key];
                    if (header.Value.KieuDuLieu == "datetime") objValue = (objValue as string).Replace(" 00:00:00", "");
                    worksheet.Range[rowIndex, colIndex, rowIndex, colIndex].Text = objValue != null ? objValue.ToString() : string.Empty;
                    worksheet.Range[rowIndex, colIndex, rowIndex, colIndex].CellStyle.HorizontalAlignment = (ExcelHAlign)header.Value.CanhLe;
                }
            }

            #endregion Content

            // Draw border
            DrawTableBorder(worksheet, rowStart + 3, colStart, rowIndex, colStart + colNum - 1, ExcelLineStyle.Thin);

            // Hide grid line
            worksheet.IsGridLinesVisible = false;

            return workbook;

        }


        public static IWorkbook OutputCustomMutipleSheet(
            byte[] noiDungBieuMau,
            List<RequestExcelSimpleReport> simpleRequests,
            List<RequestExcelGroupDataReport> groupDataRequests,
            List<RequestExcelGroupTwoLevelDataReport> groupDataTwoLevels)
        {
            // Get template stream
            MemoryStream stream = GetTemplateStream(noiDungBieuMau);

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);

            if (simpleRequests != null)
            {
                foreach (var simpleReport in simpleRequests)
                {
                    workBook = CreateWorkbookSimplePosition(
                                workBook,
                                simpleReport.PositionOfSheet,
                                simpleReport.DataSource,
                                simpleReport.MaBieuMau,
                                AddReplaceValueDefault(simpleReport.ReplaceSameValues),
                                false,
                                simpleReport.ColumnDelete);
                }
            }

            if (groupDataRequests != null)
            {
                foreach (var groupDataReport in groupDataRequests)
                {
                    workBook = CreateWorkBookGroupPosition(
                       workBook,
                       groupDataReport.PositionOfSheet,
                       groupDataReport.GroupData,
                       AddReplaceValueDefault(groupDataReport.ReplaceSameValues),
                       groupDataReport.GroupBox,
                       groupDataReport.MaBieuMau,
                       groupDataReport.GroupName,
                       dataInGroups: groupDataReport.DataInGroups);
                }
            }

            if (groupDataTwoLevels != null)
            {
                foreach (var groupDataTwoLevel in groupDataTwoLevels)
                {
                    workBook = ExportGroupTwoLevelExcelDataPosition(
                       workBook,
                       groupDataTwoLevel.PositionOfSheet,
                       groupDataTwoLevel.DataSource,
                       groupDataTwoLevel.ReplaceSameValues,
                       groupDataTwoLevel.ColumnDelete,
                       groupDataTwoLevel.GroupBox1,
                       groupDataTwoLevel.GroupBox2,
                       groupDataTwoLevel.GroupName1,
                       groupDataTwoLevel.GroupName2,
                       groupDataTwoLevel.MaBieuMau,
                       groupDataTwoLevel.IsFindAll,
                       groupDataTwoLevel.DataInGroups);
                }
            }

            // Close
            stream.Close();
            stream.Dispose();

            return workBook;
        }

        public static IWorkbook OutSimpleReport<T>(List<T> dataSource, byte[] noidungBieuMau, string maBieuMau, Dictionary<string, string> replaceValues = null, bool addCommonReplace = false,
                                                   List<string> arrCotCanXoa = null)
        {
            string fileName = string.Empty;

            // Get template stream
            MemoryStream stream = GetTemplateStream(noidungBieuMau);

            // Check if data is null
            if (stream == null)
            {
                return null;
            }

            IWorkbook workbook = CreateWorkbook(dataSource, maBieuMau, replaceValues, stream, addCommonReplace, arrCotCanXoa);
            stream.Close();
            stream.Dispose();
            return workbook;
        }

        /// <summary>
        /// Outs the group report.
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="groupData">The group data.</param>
        /// <param name="replaceValues">The replace values.</param>
        /// <param name="groupBox">The group box.</param>
        /// <param name="maBieuMau">Name of the view.</param>
        /// <param name="isPrintPreview">if set to <c>true</c> [is print preview].</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        public static IWorkbook OutGroupReport<K, T>(List<IGrouping<K, T>> groupData, byte[] noidungBieuMau, Dictionary<string, string> replaceValues,
                                        string groupBox, string maBieuMau, string groupName, List<T> objSum = null, List<string> valuesColumnDeleted = null, bool isFindAll = false)
        {
            // Get template stream
            MemoryStream stream = GetTemplateStream(noidungBieuMau);

            // Check if data is null
            if (stream == null)
            {
                return null;
            }

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);

            // Get sheets
            IWorksheet workSheet = workBook.Worksheets[0];
            IWorksheet tmpSheet = workBook.Worksheets.Create(TMP_SHEET);
            if (replaceValues == null)
            {
                replaceValues = new Dictionary<string, string>();
            }
            AddCommonReplaceValue(replaceValues);
            // Copy template of group to temporary sheet
            IRange range = workSheet.Range[groupBox];
            int rowCount = range.Rows.Count();
            IRange tmpRange = tmpSheet.Range[groupBox];
            range.CopyTo(tmpRange, ExcelCopyRangeOptions.All);
            // Fill object sum
            if (objSum != null)
            {
                // Fill object
                ITemplateMarkersProcessor markProcessObject = workSheet.CreateTemplateMarkersProcessor();
                markProcessObject.MarkerPrefix = "$";
                markProcessObject.AddVariable("XXX", objSum);
                markProcessObject.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
            }

            // Replace value
            FindAndReplace(workSheet, replaceValues);

            // Delete column
            if (valuesColumnDeleted != null && valuesColumnDeleted.Count > 0)
            {
                if (!isFindAll)
                {
                    foreach (string columValue in valuesColumnDeleted)
                    {
                        IRange localtion = workSheet.FindFirst(columValue, ExcelFindType.Text);

                        if (localtion != null)
                        {
                            workSheet.DeleteColumn(localtion.Column, valuesColumnDeleted.Count);

                            break;
                        }
                    }
                }
                else
                {
                    foreach (string columValue in valuesColumnDeleted)
                    {
                        IRange[] localtions = workSheet.FindAll(columValue, ExcelFindType.Text);

                        if (localtions != null)
                        {
                            for (int i = localtions.Count() - 1; i >= 0; i--)
                            {
                                if (localtions[i] != null)
                                {
                                    workSheet.DeleteColumn(localtions[i].Column, 1);
                                }
                            }
                        }
                    }
                }
            }

            // Find row

            bool isDeleteRow = false;
            if (groupData != null && groupData.Count > 0)
            {
                // Loop data
                for (int i = groupData.Count - 1; i >= 0; i--)
                {
                    IGrouping<K, T> group = groupData[i];
                    List<T> listMember = group.ToList();

                    // Create template maker
                    ITemplateMarkersProcessor markProcess = workSheet.CreateTemplateMarkersProcessor();

                    // Fill data into templates
                    if (listMember.Count > 0)
                    {
                        markProcess.AddVariable(groupName, group.Key == null ? "" : group.Key.ToString());
                        markProcess.AddVariable(maBieuMau, listMember);
                        markProcess.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
                    }
                    else
                    {
                        markProcess.AddVariable(groupName, string.Empty);
                        markProcess.ApplyMarkers(UnknownVariableAction.Skip);
                    }

                    // Insert template rows
                    if (i > 0)
                    {
                        workSheet.InsertRow(range.Row, rowCount, ExcelInsertOptions.FormatAsAfter);
                        tmpRange.CopyTo(workSheet.Range[groupBox], ExcelCopyRangeOptions.All);
                    }
                }
            }
            else
            {
                // Delete
                IRange[] rowSet2 = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);
                if (rowSet2 != null)
                {
                    range = rowSet2[0];
                    workSheet.DeleteRow(range.Row);
                    workSheet.DeleteRow(range.Row - 1);
                    workSheet.DeleteRow(range.Row - 2);
                    isDeleteRow = true;
                }
            }
            if (groupData != null && groupData.Count > 0)
            {
                IRange[] rowSet = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);
                // Check if it has empty rows
                if (rowSet != null)
                {
                    // Delete row
                    for (int i = rowSet.Count() - 1; i >= 0; i--)
                    {
                        range = rowSet[i];

                        // Delete
                        if (range != null)
                        {
                            workSheet.DeleteRow(isDeleteRow ? range.Row - 2 : range.Row);
                        }
                    }
                }
            }

            string BRK_ROW = "[BRK]";

            // Remove temporary sheet
            workSheet.Replace(BRK_ROW, string.Empty);
            workBook.Worksheets.Remove(tmpSheet);
            workSheet.Replace("NULL", string.Empty);

            // Close
            stream.Close();
            stream.Dispose();

            return workBook;
        }

        /// <summary>
        /// Hàm dùng chung dành cho các báo cáo 2 cấp
        /// </summary>
        public static IWorkbook ExportGroupTwoLevelExcelData<T>(List<Dictionary<string, List<Dictionary<string, List<T>>>>> pDataSource, Dictionary<string, string> pReplaceValues, List<string> pColumnsDelete,
                                                byte[] noidungBieuMau, string pGroupBox1, string pGroupBox2, string pGroupName1, string pGroupName2, string maBieuMau, bool pIsFindAll = false)
        {
            //Tạo luồng bộ nhớ lấy mẫu theo tên pViewName
            MemoryStream stream = GetTemplateStream(noidungBieuMau);

            //Kiểm tra luồng có tồn tại không (Kiểm tra mẫu có tồn tại hay không để thông báo cho người dùng)
            if (stream == null)
            {
                return null;
            }

            //Tạo công cụ xử lý file excel - Mở file
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);

            //Lấy workSheet đầu tiên
            IWorksheet workSheet = workBook.Worksheets[0];
            IWorksheet tmpSheet = workBook.Worksheets.Create(TMP_SHEET);

            //Sao chép group vào mẫu tạm
            IRange range1 = workSheet.Range[pGroupBox1];
            IRange range2 = workSheet.Range[pGroupBox2];
            int rowCount1 = range1.Rows.Count();
            int rowCount2 = range2.Rows.Count();

            IRange tmpRange1 = tmpSheet.Range[pGroupBox1];
            IRange tmpRange2 = tmpSheet.Range[pGroupBox2];
            range1.CopyTo(tmpRange1, ExcelCopyRangeOptions.All);

            //Thay thế giá trị trên sheet
            FindAndReplace(workSheet, pReplaceValues);

            #region Delete column

            if (pColumnsDelete != null && pColumnsDelete.Count > 0)
            {
                if (!pIsFindAll)
                {
                    foreach (string columValue in pColumnsDelete)
                    {
                        IRange localtion = workSheet.FindFirst(columValue, ExcelFindType.Text);

                        if (localtion != null)
                        {
                            workSheet.DeleteColumn(localtion.Column, pColumnsDelete.Count);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (string columValue in pColumnsDelete)
                    {
                        IRange[] localtions = workSheet.FindAll(columValue, ExcelFindType.Text);

                        if (localtions != null)
                        {
                            for (int i = localtions.Count() - 1; i >= 0; i--)
                            {
                                if (localtions[i] != null)
                                {
                                    workSheet.DeleteColumn(localtions[i].Column, 1);
                                }
                            }
                        }
                    }
                }
            }

            #endregion Delete column

            #region Replacer Values

            foreach (var dicTieuChiCap1 in pDataSource)
            {
                var listDicTieuChiCap1 = dicTieuChiCap1.SelectMany(o => o.Value).ToList();
                //Có thể sử dụng
                //var listDicTieuChiCap1 = dicTieuChiCap1.Values.SelectMany(o => o).ToList();
                //Không thể sử dụng
                //var listDicTieuChiCap1 = dicTieuChiCap1.Values.ToList();

                Replace(workSheet, pGroupName1, dicTieuChiCap1.Keys.FirstOrDefault());

                foreach (var dicTieuChiCap2 in listDicTieuChiCap1)
                {
                    var lstDicTieuChiCap2 = dicTieuChiCap2.SelectMany(o => o.Value).ToList();

                    // Create template maker
                    ITemplateMarkersProcessor markProcess = workSheet.CreateTemplateMarkersProcessor();

                    //Fill data into templates
                    if (lstDicTieuChiCap2.Count > 0)
                    {
                        Replace(workSheet, pGroupName2, dicTieuChiCap2.Keys.FirstOrDefault());
                        markProcess.AddVariable(maBieuMau, lstDicTieuChiCap2);
                    }
                    else
                    {
                        Replace(workSheet, pGroupName1, string.Empty);
                    }

                    markProcess.ApplyMarkers(UnknownVariableAction.ReplaceBlank);

                    // Insert template rows - Chèn dòng tạm nếu không phải là danh sách cuối cùng
                    if (listDicTieuChiCap1.IndexOf(dicTieuChiCap2) != listDicTieuChiCap1.Count - 1)
                    {
                        workSheet.InsertRow(range2.Row, rowCount2, ExcelInsertOptions.FormatAsAfter);
                        tmpRange2.CopyTo(workSheet.Range[pGroupBox2], ExcelCopyRangeOptions.All);
                    }
                }

                //Chèn dòng tạm nếu không phải là danh sách cuối cùng
                if (pDataSource.IndexOf(dicTieuChiCap1) != pDataSource.Count - 1)
                {
                    workSheet.InsertRow(range1.Row, rowCount1, ExcelInsertOptions.FormatAsAfter);
                    tmpRange1.CopyTo(workSheet.Range[pGroupBox1], ExcelCopyRangeOptions.All);
                }
            }

            #endregion Replacer Values

            #region Find [TMP] & Delete Row

            // Delete 1 Row [TMP]
            if (pDataSource != null && pDataSource.Count > 0)
            {
                // Find row
                IRange[] rowSet = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);

                // Check if it has empty rows
                if (rowSet != null)
                {
                    // Delete row
                    for (int i = rowSet.Count() - 1; i >= 0; i--)
                    {
                        range1 = rowSet[i];

                        // Delete
                        if (range1 != null)
                        {
                            workSheet.DeleteRow(range1.Row);
                        }
                    }
                }
            }
            // Delete 4 Row [TMP]
            else
            {
                // Delete
                IRange[] rowSet2 = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);
                if (rowSet2 != null)
                {
                    range1 = rowSet2[0];
                    workSheet.DeleteRow(range1.Row);
                    workSheet.DeleteRow(range1.Row - 1);
                    workSheet.DeleteRow(range1.Row - 2);
                    workSheet.DeleteRow(range1.Row - 3);
                    workSheet.DeleteRow(range1.Row - 4);
                }
            }

            IRange[] range_Temp = workSheet.FindAll(TMP_ROW2, ExcelFindType.Text);
            if (range_Temp != null)
            {
                // Delete row
                for (int k = range_Temp.Count() - 1; k >= 0; k--)
                {
                    range1 = range_Temp[k];

                    // Delete
                    if (range1 != null)
                    {
                        workSheet.DeleteRow(range1.Row);
                    }
                }
            }

            #endregion Find [TMP] & Delete Row

            string BRK_ROW = "[BRK]";

            // Remove temporary sheet
            workSheet.Replace(BRK_ROW, string.Empty);
            workBook.Worksheets.Remove(tmpSheet);

            // Close
            stream.Close();
            stream.Dispose();

            return workBook;
        }

        /// <summary>
        /// ReportMutiSheet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <param name="replaceValues"></param>
        /// <param name="countSheet"></param>
        /// <param name="noidungBieuMau"></param>
        /// <param name="maBieuMau"></param>
        /// <param name="rangeExcel"></param>
        /// <param name="sameReplace"></param>
        /// <param name="sheetName"></param>
        /// <param name="afterFill"></param>
        /// <param name="isHinhAnh"></param>
        /// <returns></returns>
        public static IWorkbook OutSimpleReportMultiSheet<T>(List<List<T>> dataSource, List<Dictionary<string, string>> replaceValues, int countSheet
            , byte[] noidungBieuMau, string maBieuMau, Dictionary<string, string> sameReplace = null, List<string> sheetName = null,
            Action<IWorkbook> afterFill = null, bool isHinhAnh = false)
        {
            // Get template stream
            MemoryStream stream = GetTemplateStream(noidungBieuMau);

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);
            IWorksheet workSheet = workBook.Worksheets[0];

            if (sameReplace != null && sameReplace.Count > 0)
            {
                // Find and replace values
                FindAndReplace(workSheet, sameReplace);
            }

            string SheetName = "Sheet";

            for (int i = 0; i < countSheet; i++)
            {
                workSheet = workBook.Worksheets[0];
                IWorksheet tmpNewSheet = workBook.Worksheets.AddCopy(workSheet);
                if (sheetName != null && sheetName.Count > 0)
                {
                    tmpNewSheet.Name = sheetName[i];
                }
                else
                {
                    tmpNewSheet.Name = SheetName + (i + "x" + 1).ToString();
                }

                workSheet = workBook.Worksheets[i + 1];

                // Replace value
                if (replaceValues != null && replaceValues.Count > 0)
                {
                    if (replaceValues[i] != null && replaceValues[i].Count > 0)
                    {
                        // Find and replace values
                        FindAndReplace(workSheet, replaceValues[i]);
                    }
                }

                // Gán ngược lại worksheet để tránh bị lỗi bộ nhớ
                workSheet = workBook.Worksheets[i + 1];
                ITemplateMarkersProcessor markProcessor = workSheet.CreateTemplateMarkersProcessor();
                // Fill variables
                markProcessor.AddVariable(maBieuMau, dataSource[i]);

                // End template
                try
                {
                    markProcessor.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
                }
                catch
                {
                    markProcessor.ApplyMarkers(UnknownVariableAction.Skip);
                }
                // Delete temporary row
                IRange range = workSheet.FindFirst(TMP_ROW, ExcelFindType.Text);

                // Delete
                if (range != null)
                {
                    workSheet.DeleteRow(range.Row);
                }
                workSheet.Replace("NULL", string.Empty);
                //img sữ dụng sau
                if (isHinhAnh == true)
                {
                    //var lst = dataSource[i].ToList().FirstOrDefault();
                    //if (lst != null)
                    //{
                    //    var idNhanSu = lst.GetValue<object>("IDNhanSu").ParseInt();
                    //    var dataNhanSu = LayerCommon.Cache.Context.NS_NhanSu.Where(o => o.IDNhanSu == idNhanSu).FirstOrDefault();
                    //    if (dataNhanSu != null && dataNhanSu.HinhNhanSu != null)
                    //    {
                    //        MemoryStream ms = new MemoryStream(dataNhanSu.HinhNhanSu);
                    //        Image image = Image.FromStream(ms, true, true);
                    //        //nhiều dạng hình ảnh đuôi khác nhau thì sẻ có kích thước khác nhau nên convert về 1 dạng giống nhau
                    //        Image imageResize = ResizeImage(300, 300, image);
                    //        AddLogoWithText(workSheet, pStartRow, pStartCol, pWidth, pHeight, (Bitmap)imageResize);
                    //    }
                    //}
                }
            }
            if (countSheet > 0)
            {
                workBook.Worksheets.Remove(0);
            }
            else
            {
                #region Null Data

                // Null Data
                Dictionary<string, string> replaceValue = new Dictionary<string, string>();
                var vn = new System.Globalization.CultureInfo("vi-VN");
                replaceValue.Add("%TenCoQuan", "");
                replaceValue.Add("%NguoiXuatBan", "");
                replaceValue.Add("%Thu", vn.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));
                replaceValue.Add("%Ngay", DateTime.Now.Day.ToString().PadLeft(2, '0'));
                replaceValue.Add("%Thang", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                replaceValue.Add("%Nam", DateTime.Now.Year.ToString());
                replaceValue.Add("%Gio", DateTime.Now.Hour.ToString());
                replaceValue.Add("%Phut", DateTime.Now.Minute.ToString());
                replaceValue.Add("%FooterSystem", $"In từ phần mềm: , lúc {DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy")}");
                FindAndReplace(workSheet, replaceValue);

                // Delete temporary row
                IRange range = workSheet.FindFirst(TMP_ROW, ExcelFindType.Text);

                // Delete
                if (range != null)
                {
                    workSheet.DeleteRow(range.Row);
                    workSheet.DeleteRow(range.Row - 1);
                }
                workSheet.Replace("NULL", string.Empty);

                return workBook;

                #endregion Null Data
            }

            if (afterFill != null) afterFill(workBook);

            // Close
            stream.Close();
            stream.Dispose();
            return workBook;
        }

        /// <summary>
        /// OutGroupReportMultiSheet
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="groupData"></param>
        /// <param name="replaceValues"></param>
        /// <param name="countSheet"></param>
        /// <param name="noidungBieuMau"></param>
        /// <param name="maBieuMau"></param>
        /// <param name="rangeExcel"></param>
        /// <param name="isPrintPreview"></param>
        /// <param name="fileName"></param>
        /// <param name="groupBox"></param>
        /// <param name="groupName"></param>
        /// <param name="sameReplace"></param>
        /// <returns></returns>
        public static IWorkbook OutGroupReportMultiSheet<K, T>(List<List<IGrouping<K, T>>> groupData, List<Dictionary<string, string>> replaceValues, int countSheet, byte[] noidungBieuMau, string maBieuMau
            , string groupBox, string groupName, Dictionary<string, string> sameReplace = null)
        {
            // Get template stream
            MemoryStream stream = GetTemplateStream(noidungBieuMau);

            // Create excel engine
            ExcelEngine engine = new ExcelEngine();
            IWorkbook workBook = engine.Excel.Workbooks.Open(stream);
            IWorksheet tmpSheet = workBook.Worksheets.Create(TMP_SHEET);
            IWorksheet workSheet = workBook.Worksheets[0];

            if (sameReplace != null && sameReplace.Count > 0)
            {
                // Find and replace values
                FindAndReplace(workSheet, sameReplace);
            }
            // Lưu lại file dữ liệu xuất ra
            workSheet = workBook.Worksheets[0];
            string SheetName = "Sheet";

            for (int i = 0; i < countSheet; i++)
            {
                IWorksheet tmpNewSheet = workBook.Worksheets.AddCopy(workSheet);
                string nameSheet = SheetName + (i + "x" + 1).ToString();
                tmpNewSheet.Name = nameSheet;
                // Copy template of group to temporary sheet
                IRange range = tmpNewSheet.Range[groupBox];
                int rowCount = range.Rows.Count();
                IRange tmpRange = tmpSheet.Range[groupBox];
                range.CopyTo(tmpRange, ExcelCopyRangeOptions.All);

                // Replace value
                if (replaceValues != null && replaceValues.Count > 0 && replaceValues[i] != null && replaceValues[i].Count > 0)
                {
                    // Find and replace values
                    FindAndReplace(tmpNewSheet, replaceValues[i]);
                }

                // Loop data
                for (int j = groupData[i].Count - 1; j >= 0; j--)
                {
                    IGrouping<K, T> group = groupData[i][j];
                    List<T> listMember = group.ToList();

                    // Gán ngược lại worksheet để tránh bị lỗi bộ nhớ
                    tmpNewSheet = workBook.Worksheets[nameSheet];

                    ITemplateMarkersProcessor markProcess = tmpNewSheet.CreateTemplateMarkersProcessor();
                    // Fill data into templates
                    if (listMember.Count > 0)
                    {
                        markProcess.AddVariable(groupName, group.Key.ToString());
                        markProcess.AddVariable(maBieuMau, listMember);
                        markProcess.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
                    }
                    else
                    {
                        markProcess.AddVariable(groupName, string.Empty);
                        markProcess.ApplyMarkers(UnknownVariableAction.Skip);
                    }

                    // Insert template rows
                    if (j > 0)
                    {
                        tmpNewSheet.InsertRow(range.Row, rowCount, ExcelInsertOptions.FormatAsAfter);
                        tmpRange.CopyTo(tmpNewSheet.Range[groupBox], ExcelCopyRangeOptions.All);
                    }
                }

                IRange[] range1 = tmpNewSheet.FindAll(TMP_ROW, ExcelFindType.Text);
                if (range1 != null)
                {
                    // Delete row
                    for (int k = range1.Count() - 1; k >= 0; k--)
                    {
                        range = range1[k];

                        // Delete
                        if (range != null)
                        {
                            tmpNewSheet.DeleteRow(range.Row);
                        }
                    }
                }
                //Thêm vào để xử lý cho trường họp sum group
                IRange[] range_Temp = tmpNewSheet.FindAll("[TMP_TEMP]", ExcelFindType.Text);
                if (range_Temp != null)
                {
                    // Delete row
                    for (int k = range_Temp.Count() - 1; k >= 0; k--)
                    {
                        range = range_Temp[k];

                        // Delete
                        if (range != null)
                        {
                            tmpNewSheet.DeleteRow(range.Row);
                        }
                    }
                }

                tmpNewSheet.Replace("NULL", string.Empty);
            }

            workBook.Worksheets.Remove(0);
            workBook.Worksheets.Remove(tmpSheet);

            // Close
            stream.Close();
            stream.Dispose();

            return workBook;
        }

        #endregion Main Function

        #region Support function

        private static Dictionary<string, string> AddReplaceValueDefault(Dictionary<string, string> replaceSameValues)
        {
            var vn = new System.Globalization.CultureInfo("vi-VN");
            if (!replaceSameValues.ContainsKey("%Thu") && !replaceSameValues.ContainsKey("%Thang") && !replaceSameValues.ContainsKey("%Gio"))
            {
                replaceSameValues.Add("%Thu", vn.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));
                replaceSameValues.Add("%Ngay", DateTime.Now.Day.ToString().PadLeft(2, '0'));
                replaceSameValues.Add("%Thang", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                replaceSameValues.Add("%Nam", DateTime.Now.Year.ToString());
                replaceSameValues.Add("%Gio", DateTime.Now.Hour.ToString());
                replaceSameValues.Add("%Phut", DateTime.Now.Hour.ToString());
            }

            if (!replaceSameValues.ContainsKey("%FooterSystem"))
                replaceSameValues.Add("%FooterSystem", $"In từ phần mềm: , lúc {DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy")}");
            return replaceSameValues;
        }

        public static bool IsFileOpenOrReadOnly(string fileName)
        {
            try
            {
                // Check if file is not existed
                if (!File.Exists(fileName))
                {
                    return false;
                }

                // First make sure it's not a read only file
                if ((File.GetAttributes(fileName) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
                {
                    // First we open the file with a FileStream
                    using (FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                    {
                        try
                        {
                            stream.ReadByte();
                            return false;
                        }
                        catch (IOException)
                        {
                            return true;
                        }
                        finally
                        {
                            stream.Close();
                            stream.Dispose();
                        }
                    }
                }
                else
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return true;
            }
        }

        /// <summary>
        /// Finds the and replace.
        /// </summary>
        /// <param name="workSheet">The work sheet.</param>
        /// <param name="replaceValues">The replace values.</param>
        private static void FindAndReplace(IWorksheet workSheet, Dictionary<string, string> replaceValues)
        {
            // Replace value
            if (replaceValues != null && replaceValues.Count > 0)
            {
                int loop = 0;
                Dictionary<string, int> lstValue = new Dictionary<string, int>();
                // Find and replace values
                foreach (KeyValuePair<string, string> replacer in replaceValues)
                {
                    loop = 1;
                    string val = replacer.Value ?? string.Empty;
                    if (lstValue.ContainsKey(val))
                    {
                        loop = lstValue[val];
                        for (int i = 0; i <= loop; i++)
                        {
                            val += " ";
                        }
                        lstValue[val] = loop + 1;
                    }
                    else
                    {
                        lstValue.Add(val, loop);
                    }
                    Replace(workSheet, replacer.Key, val);
                }
            }
        }

        /// <summary>
        /// Replace the specified value in work sheet.
        /// </summary>
        /// <param name="workSheet">The work sheet.</param>
        /// <param name="findValue">The find value.</param>
        /// <param name="replaceValue">The replace value.</param>
        public static void Replace(IWorksheet workSheet, string findValue, string replaceValue, int loop = 1)
        {
            // Find and replace
            if (workSheet != null && !string.IsNullOrEmpty(findValue))
            {
                // Get current cells
                IRange[] cells = workSheet.Range.Cells;
                IRange range = null;
                int countLoop = 0;

                // Loop cells to replace
                for (int i = 0; i < cells.Count(); i++)
                {
                    // Current cell
                    range = cells[i];

                    // Find and replace values
                    if (range != null && range.DisplayText.Contains(findValue))
                    {
                        if (range.HasRichText)
                        {
                            int idx = range.DisplayText.IndexOf(findValue);
                            IFont replaceFont = range.RichText.GetFont(idx);
                            List<IFont> dsF = new List<IFont>();

                            for (int x = 0; x < range.RichText.Text.Length;)
                            {
                                if (x == idx)
                                {
                                    x += findValue.Length;
                                    continue;
                                }
                                dsF.Add(range.RichText.GetFont(x));
                                x++;
                            }

                            foreach (char c in replaceValue)
                            {
                                if (dsF.Count - 1 < idx)
                                {
                                    dsF.Add(replaceFont);
                                }
                                else
                                {
                                    dsF.Insert(idx, replaceFont);
                                }
                            }

                            range.RichText.Text = range.RichText.Text.Replace(findValue, replaceValue);

                            for (int x = 0; x < range.RichText.Text.Length; x++)
                            {
                                range.RichText.SetFont(x, x, dsF[x]);
                            }
                        }
                        else
                        {
                            decimal result = 0;
                            if (decimal.TryParse(Convert.ToString(replaceValue), out result))
                            {
                                range.Value = range.Text.Replace(findValue, replaceValue);
                            }
                            else
                            {
                                range.Text = range.Text.Replace(findValue, replaceValue);
                            }
                        }

                        countLoop++;

                        //if (loop == countLoop)
                        //{
                        //    break;
                        //}
                    }
                }
            }
        }

        private static MemoryStream GetTemplateStream(byte[] noiDungBieuMau)
        {
            var ms = new MemoryStream(noiDungBieuMau);
            return ms;
        }

        public static IWorkbook CreateWorkBookGroupPosition(IWorkbook workBook, int positionSheet, List<IGrouping<string, object>> groupData, Dictionary<string, string> replaceValues,
                                string groupBox, string maBieuMau, string groupName, List<object> objSum = null, List<string> valuesColumnDeleted = null, bool isFindAll = false,
                                List<RequestExcelDataInGroup> dataInGroups = null)
        {
            // Get sheets
            IWorksheet workSheet = workBook.Worksheets[positionSheet];
            IWorksheet tmpSheet = workBook.Worksheets.Create(TMP_SHEET);
            if (replaceValues == null)
            {
                replaceValues = new Dictionary<string, string>();
            }

            //AddCommonReplaceValue(replaceValues);
            // Copy template of group to temporary sheet
            IRange range = workSheet.Range[groupBox];
            int rowCount = range.Rows.Count();
            IRange tmpRange = tmpSheet.Range[groupBox];
            range.CopyTo(tmpRange, ExcelCopyRangeOptions.All);
            // Fill object sum
            if (objSum != null)
            {
                // Fill object
                ITemplateMarkersProcessor markProcessObject = workSheet.CreateTemplateMarkersProcessor();
                markProcessObject.MarkerPrefix = "$";
                markProcessObject.AddVariable("XXX", objSum);
                markProcessObject.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
            }

            // Replace value
            FindAndReplace(workSheet, replaceValues);

            // Delete column
            if (valuesColumnDeleted != null && valuesColumnDeleted.Count > 0)
            {
                if (!isFindAll)
                {
                    foreach (string columValue in valuesColumnDeleted)
                    {
                        IRange localtion = workSheet.FindFirst(columValue, ExcelFindType.Text);

                        if (localtion != null)
                        {
                            workSheet.DeleteColumn(localtion.Column, valuesColumnDeleted.Count);

                            break;
                        }
                    }
                }
                else
                {
                    foreach (string columValue in valuesColumnDeleted)
                    {
                        IRange[] localtions = workSheet.FindAll(columValue, ExcelFindType.Text);

                        if (localtions != null)
                        {
                            for (int i = localtions.Count() - 1; i >= 0; i--)
                            {
                                if (localtions[i] != null)
                                {
                                    workSheet.DeleteColumn(localtions[i].Column, 1);
                                }
                            }
                        }
                    }
                }
            }

            // Find row

            bool isDeleteRow = false;
            if (groupData != null && groupData.Count > 0)
            {
                // Loop data
                for (int i = groupData.Count - 1; i >= 0; i--)
                {
                    IGrouping<string, object> group = groupData[i];
                    List<object> listMember = group.ToList();

                    // Create template maker
                    ITemplateMarkersProcessor markProcess = workSheet.CreateTemplateMarkersProcessor();

                    // Fill data into templates
                    if (listMember.Count > 0)
                    {
                        markProcess.AddVariable(groupName, group.Key == null ? "" : group.Key.ToString());
                        if (dataInGroups != null)
                        {
                            foreach (var item in dataInGroups)
                            {
                                if (!string.IsNullOrEmpty(item.GroupName))
                                {
                                    markProcess.AddVariable(item.GroupName, group.Key == null ? "" : item.DictValueGroupName[group.Key] == null ? "" : item.DictValueGroupName[group.Key]);
                                }
                            }
                        }

                        markProcess.AddVariable(maBieuMau, listMember);
                        markProcess.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
                    }
                    else
                    {
                        if (dataInGroups != null)
                        {
                            foreach (var item in dataInGroups)
                            {
                                if (!string.IsNullOrEmpty(item.GroupName))
                                {
                                    markProcess.AddVariable(item.GroupName, group.Key == null ? "" : item.DictValueGroupName[group.Key] == null ? "" : item.DictValueGroupName[group.Key]);
                                }
                            }
                        }

                        markProcess.AddVariable(groupName, string.Empty);
                        markProcess.ApplyMarkers(UnknownVariableAction.Skip);
                    }

                    // Insert template rows
                    if (i > 0)
                    {
                        workSheet.InsertRow(range.Row, rowCount, ExcelInsertOptions.FormatAsAfter);
                        tmpRange.CopyTo(workSheet.Range[groupBox], ExcelCopyRangeOptions.All);
                    }
                }
            }
            else
            {
                // Delete
                IRange[] rowSet2 = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);
                if (rowSet2 != null)
                {
                    range = rowSet2[0];
                    workSheet.DeleteRow(range.Row);
                    workSheet.DeleteRow(range.Row - 1);
                    workSheet.DeleteRow(range.Row - 2);
                    isDeleteRow = true;
                }
            }
            if (groupData != null && groupData.Count > 0)
            {
                IRange[] rowSet = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);
                // Check if it has empty rows
                if (rowSet != null)
                {
                    // Delete row
                    for (int i = rowSet.Count() - 1; i >= 0; i--)
                    {
                        range = rowSet[i];

                        // Delete
                        if (range != null)
                        {
                            workSheet.DeleteRow(isDeleteRow ? range.Row - 2 : range.Row);
                        }
                    }
                }
            }

            string BRK_ROW = "[BRK]";

            // Remove temporary sheet
            workSheet.Replace(BRK_ROW, string.Empty);
            workBook.Worksheets.Remove(tmpSheet);
            workSheet.Replace("NULL", string.Empty);

            return workBook;
        }

        public static IWorkbook CreateWorkbookSimplePosition(IWorkbook workBook, int positionSheet, List<object> dataSource, string maBieuMau, Dictionary<string, string> replaceValues, bool addCommonReplace = false, List<string> arrCotCanXoa = null)
        {
            // Create excel engine
            IWorksheet workSheet = workBook.Worksheets[positionSheet];
            ITemplateMarkersProcessor markProcessor = workSheet.CreateTemplateMarkersProcessor();

            //AddImageQRCode(maBieuMau, workSheet);

            // xóa cột không cần thiết
            if (arrCotCanXoa != null && arrCotCanXoa.Count() > 0)
            {
                foreach (string columValue in arrCotCanXoa)
                {
                    IRange col = workSheet.FindStringEndsWith(columValue, ExcelFindType.Text);

                    // Delete
                    if (col != null)
                    {
                        workSheet.DeleteColumn(col.Column);
                    }
                }
                arrCotCanXoa.Clear();
            }
            // Add common replace
            if (addCommonReplace)
            {
                if (replaceValues == null)
                {
                    replaceValues = new Dictionary<string, string>();
                }
                AddCommonReplaceValue(replaceValues);
            }

            // Replace value
            if (replaceValues != null && replaceValues.Count > 0)
            {
                // Find and replace values
                foreach (KeyValuePair<string, string> replacer in replaceValues)
                {
                    Replace(workSheet, replacer.Key, replacer.Value);
                }
            }
            bool isDeleteRow = false;

            if (dataSource != null && dataSource.Count > 0)
            {
                // Fill variables
                markProcessor.AddVariable(maBieuMau, dataSource);
                // End template
                markProcessor.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
            }
            else
            {
                IRange range2 = workSheet.FindFirst(TMP_ROW, ExcelFindType.Text);
                // Delete
                if (range2 != null)
                {
                    workSheet.DeleteRow(range2.Row - 1);
                    isDeleteRow = true;
                }
            }
            IRange range = workSheet.FindFirst(TMP_ROW, ExcelFindType.Text);
            // Delete
            if (range != null)
            {
                workSheet.DeleteRow(isDeleteRow ? range.Row - 1 : range.Row);
            }

            #region Constants

            // Ẩn đi những cột không cần thiết
            if (_lstColumnsHide != null && _lstColumnsHide.Count > 0)
            {
                foreach (int i in _lstColumnsHide)
                {
                    workSheet.Columns[i].ColumnWidth = 0;
                }
                _lstColumnsHide.Clear();
            }

            // Auto size
            if (_lstColumnsOutToSize != null && _lstColumnsOutToSize.Count > 0)
            {
                foreach (int i in _lstColumnsOutToSize)
                {
                    workSheet.Columns[i].AutofitColumns();
                }
                _lstColumnsOutToSize.Clear();
            }

            #endregion Constants

            workSheet.Replace("NULL", string.Empty);
            PageSetup(ref workSheet);

            return workBook;
        }

        /// <summary>
        /// Hàm dùng chung dành cho các báo cáo 2 cấp theo vị trí sheet
        /// </summary>
        public static IWorkbook ExportGroupTwoLevelExcelDataPosition(IWorkbook workBook, int positionSheet, List<Dictionary<string, List<Dictionary<string, List<object>>>>> pDataSource, Dictionary<string, string> pReplaceValues, List<string> pColumnsDelete,
                                                 string pGroupBox1, string pGroupBox2, string pGroupName1, string pGroupName2, string maBieuMau, bool pIsFindAll = false, List<RequestExcelDataInGroup> dataInGroups = null)
        {
            // Create excel engine
            IWorksheet workSheet = workBook.Worksheets[positionSheet];
            ITemplateMarkersProcessor markProcessor = workSheet.CreateTemplateMarkersProcessor();
            IWorksheet tmpSheet = workBook.Worksheets.Create(TMP_SHEET);

            //Sao chép group vào mẫu tạm
            IRange range1 = workSheet.Range[pGroupBox1];
            IRange range2 = workSheet.Range[pGroupBox2];
            int rowCount1 = range1.Rows.Count();
            int rowCount2 = range2.Rows.Count();

            IRange tmpRange1 = tmpSheet.Range[pGroupBox1];
            IRange tmpRange2 = tmpSheet.Range[pGroupBox2];
            range1.CopyTo(tmpRange1, ExcelCopyRangeOptions.All);

            //Thay thế giá trị trên sheet
            FindAndReplace(workSheet, pReplaceValues);

            #region Delete column

            if (pColumnsDelete != null && pColumnsDelete.Count > 0)
            {
                if (!pIsFindAll)
                {
                    foreach (string columValue in pColumnsDelete)
                    {
                        IRange localtion = workSheet.FindFirst(columValue, ExcelFindType.Text);

                        if (localtion != null)
                        {
                            workSheet.DeleteColumn(localtion.Column, pColumnsDelete.Count);
                            break;
                        }
                    }
                }
                else
                {
                    foreach (string columValue in pColumnsDelete)
                    {
                        IRange[] localtions = workSheet.FindAll(columValue, ExcelFindType.Text);

                        if (localtions != null)
                        {
                            for (int i = localtions.Count() - 1; i >= 0; i--)
                            {
                                if (localtions[i] != null)
                                {
                                    workSheet.DeleteColumn(localtions[i].Column, 1);
                                }
                            }
                        }
                    }
                }
            }

            #endregion Delete column

            #region Replacer Values

            foreach (var dicTieuChiCap1 in pDataSource)
            {
                var listDicTieuChiCap1 = dicTieuChiCap1.SelectMany(o => o.Value).ToList();
                //Có thể sử dụng
                //var listDicTieuChiCap1 = dicTieuChiCap1.Values.SelectMany(o => o).ToList();
                //Không thể sử dụng
                //var listDicTieuChiCap1 = dicTieuChiCap1.Values.ToList();

                Replace(workSheet, pGroupName1, dicTieuChiCap1.Keys.FirstOrDefault());

                foreach (var dicTieuChiCap2 in listDicTieuChiCap1)
                {
                    var lstDicTieuChiCap2 = dicTieuChiCap2.SelectMany(o => o.Value).ToList();

                    // Create template maker
                    ITemplateMarkersProcessor markProcess = workSheet.CreateTemplateMarkersProcessor();

                    //Fill data into templates
                    if (lstDicTieuChiCap2.Count > 0)
                    {
                        Replace(workSheet, pGroupName2, dicTieuChiCap2.Keys.FirstOrDefault());
                        markProcess.AddVariable(maBieuMau, lstDicTieuChiCap2);
                    }
                    else
                    {
                        Replace(workSheet, pGroupName1, string.Empty);
                    }
                    string key = string.Empty;
                    if (dataInGroups != null)
                    {
                        foreach (var item in dataInGroups)
                        {
                            if (!string.IsNullOrEmpty(item.GroupName))
                            {
                                key = dicTieuChiCap1.Keys?.FirstOrDefault() + "_" + dicTieuChiCap2.Keys?.FirstOrDefault();
                                markProcess.AddVariable(item.GroupName, string.IsNullOrEmpty(key) ? "" : item.DictValueGroupName[key] != null ? item.DictValueGroupName[key] : "");
                            }
                        }
                    }

                    markProcess.ApplyMarkers(UnknownVariableAction.ReplaceBlank);

                    // Insert template rows - Chèn dòng tạm nếu không phải là danh sách cuối cùng
                    if (listDicTieuChiCap1.IndexOf(dicTieuChiCap2) != listDicTieuChiCap1.Count - 1)
                    {
                        workSheet.InsertRow(range2.Row, rowCount2, ExcelInsertOptions.FormatAsAfter);
                        tmpRange2.CopyTo(workSheet.Range[pGroupBox2], ExcelCopyRangeOptions.All);
                    }
                }

                //Chèn dòng tạm nếu không phải là danh sách cuối cùng
                if (pDataSource.IndexOf(dicTieuChiCap1) != pDataSource.Count - 1)
                {
                    workSheet.InsertRow(range1.Row, rowCount1, ExcelInsertOptions.FormatAsAfter);
                    tmpRange1.CopyTo(workSheet.Range[pGroupBox1], ExcelCopyRangeOptions.All);
                }
            }

            #endregion Replacer Values

            #region Find [TMP] & Delete Row

            // Delete 1 Row [TMP]
            if (pDataSource != null && pDataSource.Count > 0)
            {
                // Find row
                IRange[] rowSet = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);

                // Check if it has empty rows
                if (rowSet != null)
                {
                    // Delete row
                    for (int i = rowSet.Count() - 1; i >= 0; i--)
                    {
                        range1 = rowSet[i];

                        // Delete
                        if (range1 != null)
                        {
                            workSheet.DeleteRow(range1.Row);
                        }
                    }
                }
            }
            // Delete 4 Row [TMP]
            else
            {
                // Delete
                IRange[] rowSet2 = workSheet.FindAll(TMP_ROW, ExcelFindType.Text);
                if (rowSet2 != null)
                {
                    range1 = rowSet2[0];
                    workSheet.DeleteRow(range1.Row);
                    workSheet.DeleteRow(range1.Row - 1);
                    workSheet.DeleteRow(range1.Row - 2);
                    workSheet.DeleteRow(range1.Row - 3);
                    workSheet.DeleteRow(range1.Row - 4);
                }
            }

            IRange[] range_Temp = workSheet.FindAll(TMP_ROW2, ExcelFindType.Text);
            if (range_Temp != null)
            {
                // Delete row
                for (int k = range_Temp.Count() - 1; k >= 0; k--)
                {
                    range1 = range_Temp[k];

                    // Delete
                    if (range1 != null)
                    {
                        workSheet.DeleteRow(range1.Row);
                    }
                }
            }

            #endregion Find [TMP] & Delete Row

            string BRK_ROW = "[BRK]";

            // Remove temporary sheet
            workSheet.Replace(BRK_ROW, string.Empty);
            workBook.Worksheets.Remove(tmpSheet);

            return workBook;
        }

        private static IWorkbook CreateWorkbook<T>(List<T> dataSource, string maBieuMau, Dictionary<string, string> replaceValues, Stream templateFile, bool addCommonReplace = false, List<string> arrCotCanXoa = null)
        {
            // Create excel engine
            IWorkbook workBook = ExcelEngine.Excel.Workbooks.Open(templateFile);
            IWorksheet workSheet = workBook.Worksheets[0];
            ITemplateMarkersProcessor markProcessor = workSheet.CreateTemplateMarkersProcessor();

            //AddImageQRCode(maBieuMau, workSheet);

            // xóa cột không cần thiết
            if (arrCotCanXoa != null && arrCotCanXoa.Count() > 0)
            {
                foreach (string columValue in arrCotCanXoa)
                {
                    IRange col = workSheet.FindStringEndsWith(columValue, ExcelFindType.Text);

                    // Delete
                    if (col != null)
                    {
                        workSheet.DeleteColumn(col.Column);
                    }
                }
                arrCotCanXoa.Clear();
            }
            // Add common replace
            if (addCommonReplace)
            {
                if (replaceValues == null)
                {
                    replaceValues = new Dictionary<string, string>();
                }
                AddCommonReplaceValue(replaceValues);
            }

            // Replace value
            if (replaceValues != null && replaceValues.Count > 0)
            {
                // Find and replace values
                foreach (KeyValuePair<string, string> replacer in replaceValues)
                {
                    Replace(workSheet, replacer.Key, replacer.Value);
                }
            }
            bool isDeleteRow = false;

            if (dataSource != null && dataSource.Count > 0)
            {
                // Fill variables
                markProcessor.AddVariable(maBieuMau, dataSource);
                // End template
                markProcessor.ApplyMarkers(UnknownVariableAction.ReplaceBlank);
            }
            else
            {
                IRange range2 = workSheet.FindFirst(TMP_ROW, ExcelFindType.Text);
                // Delete
                if (range2 != null)
                {
                    workSheet.DeleteRow(range2.Row - 1);
                    isDeleteRow = true;
                }
            }
            IRange range = workSheet.FindFirst(TMP_ROW, ExcelFindType.Text);
            // Delete
            if (range != null)
            {
                workSheet.DeleteRow(isDeleteRow ? range.Row - 1 : range.Row);
            }

            #region Constants

            // Ẩn đi những cột không cần thiết
            if (_lstColumnsHide != null && _lstColumnsHide.Count > 0)
            {
                foreach (int i in _lstColumnsHide)
                {
                    workSheet.Columns[i].ColumnWidth = 0;
                }
                _lstColumnsHide.Clear();
            }

            // Auto size
            if (_lstColumnsOutToSize != null && _lstColumnsOutToSize.Count > 0)
            {
                foreach (int i in _lstColumnsOutToSize)
                {
                    workSheet.Columns[i].AutofitColumns();
                }
                _lstColumnsOutToSize.Clear();
            }

            #endregion Constants

            workSheet.Replace("NULL", string.Empty);
            PageSetup(ref workSheet);

            return workBook;
        }

        public static ExcelEngine ExcelEngine
        {
            get
            {
                if (_engine == null)
                    _engine = new ExcelEngine();

                return _engine;
            }
        }

        private static void AddCommonReplaceValue(Dictionary<string, string> replacer)
        {
            var vn = new System.Globalization.CultureInfo("vi-VN");
            var now = DateTime.Now;
            replacer.Add(R_THU, vn.DateTimeFormat.GetDayName(now.DayOfWeek));
            replacer.Add(R_DAY, now.Day.ToString().PadLeft(2, '0'));
            replacer.Add(R_MONTH, now.Month.ToString().PadLeft(2, '0'));
            replacer.Add(R_YEAR, now.Year.ToString());
            //replacer.Add(R_USER, CommonBase.CurrentUserInfo.FullName);
            //replacer.Add(R_PLACE, VL_PLACE);
        }

        public static string GetEnumDesciption(System.Enum val)
        {
            string name = System.Enum.GetName(val.GetType(), val);
            System.Reflection.FieldInfo obj = val.GetType().GetField(name);
            if (obj != null)
            {
                object[] attributes = obj.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                return attributes.Length > 0 ? ((System.ComponentModel.DescriptionAttribute)attributes[0]).Description : null;
            }
            return null;
        }

        private static void PageSetup(ref IWorksheet workSheet, bool insertFooter = false)
        {
            if (insertFooter)
            {
                workSheet.PageSetup.RightFooter = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
                workSheet.PageSetup.RightFooter = "&P" + "&\"Arial ,Bold Italic\"";
            }

            if (_leftMargin.HasValue)
            {
                workSheet.PageSetup.LeftMargin = _leftMargin.Value;
            }
        }

        private static void AddImageQRCode(string value, IWorksheet workSheet)
        {
            try
            {
                IRange rangeQRCode = workSheet.FindFirst("%QRCode", ExcelFindType.Text);
                if (rangeQRCode != null)
                {
                    int columnQR = rangeQRCode.Column;
                    int rowQR = rangeQRCode.Row;

                    MemoryStream imageStream = ConvertToImageQRCode(value);
                    IPictureShape shape = workSheet.Pictures.AddPicture(rowQR, columnQR, imageStream);
                    //Re-sizing a Picture
                    shape.Height = 420;
                    shape.Width = 120;
                }
            }
            catch (Exception ex)
            {
            }
        }

        public static MemoryStream ConvertToImageQRCode(string qrCode)
        {
            //Initialize a new PdfQRBarcode instance
            PdfQRBarcode barcode = new PdfQRBarcode();
            //Set the XDimension and text for barcode
            barcode.XDimension = 3;
            barcode.Text = qrCode;

            ////Convert the barcode to image
            //Image barcodeImage = barcode.ToImage(new SizeF(70, 70));

            ////Save image
            var imageStream = new MemoryStream();
            //barcodeImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);

            return imageStream;
        }


        private static void WriteColumHeader(IWorksheet xlsSheet, int startRow, int startCol, List<string> arrColName, List<int> arrColWidth, int rowHeight)
        {
            for (int i = 0; i < arrColName.Count; i++)
            {
                xlsSheet.Range[startRow, startCol + i].Text = arrColName[i];
                //xlsSheet.Range[startRow, startCol + i].ColumnWidth = 20;
            }

            //xlsSheet.Range[startRow, 1].RowHeight = rowHeight;
            CellStyle(xlsSheet, startRow, startCol, startRow, startCol + arrColName.Count, FONT_NAME, CAPTION_FONT_SIZE, true, false);
            xlsSheet.Range[startRow, startCol, startRow, startCol + arrColName.Count].HorizontalAlignment = ExcelHAlign.HAlignCenter;
            xlsSheet.Range[startRow, startCol, startRow, startCol + arrColName.Count].VerticalAlignment = ExcelVAlign.VAlignCenter;
        }

        private static void WriteColumHeader(IWorksheet xlsSheet, int startRow, int startCol, List<RawDataInfo> headerDatas)
        {
            for (int i = 0; i < headerDatas.Count; i++)
            {
                xlsSheet.Range[startRow, startCol + i].Text = headerDatas[i].TieuDeColumn;
                xlsSheet.Range[startRow, startCol + i].ColumnWidth = (double)headerDatas[i].DoRong;
                xlsSheet.Range[startRow, startCol + i].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                xlsSheet.Range[startRow, startCol + i].VerticalAlignment = ExcelVAlign.VAlignCenter;
            }

            CellStyle(xlsSheet, startRow, startCol, startRow, startCol + headerDatas.Count, FONT_NAME, CAPTION_FONT_SIZE, true, false);
        }

        /// <summary>
        /// Draws the table border.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="lineStyle">The line style.</param>
        private static void DrawTableBorder(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, ExcelLineStyle lineStyle)
        {
            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders.LineStyle = lineStyle;
            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
            xlsSheet[startRow, startCol, endRow, endCol].CellStyle.Borders.ColorRGB = Syncfusion.Drawing.Color.Black;

            xlsSheet.Range[startRow, startCol, endRow, endCol].WrapText = true;
        }

        /// <summary>
        /// Colses the alighment.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="cols">The cols.</param>
        /// <param name="HAlight">The H alight.</param>
        private static void ColsAlighment(IWorksheet xlsSheet, int[] cols, ExcelHAlign HAlight)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                ColAlighment(xlsSheet, cols[i], HAlight);
            }
        }

        /// <summary>
        /// Cols the alighment.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="col">The start col.</param>
        /// <param name="HAlight">The H alight.</param>
        private static void ColAlighment(IWorksheet xlsSheet, int col, ExcelHAlign HAlight)
        {
            xlsSheet.Range[1, col, ROW_MAXIMUM, col].CellStyle.HorizontalAlignment = HAlight;
            xlsSheet.Range[1, col, ROW_MAXIMUM, col].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
        }

        /// <summary>
        /// Cells the alighment.
        /// </summary>
        /// <param name="xlsSheet">The XLS sheet.</param>
        /// <param name="startRow">The start row.</param>
        /// <param name="startCol">The start col.</param>
        /// <param name="endRow">The end row.</param>
        /// <param name="endCol">The end col.</param>
        /// <param name="HAlight">The H alight.</param>
        /// <param name="VAlight">The V alight.</param>
        private static void CellAlighment(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, ExcelHAlign HAlight, ExcelVAlign VAlight)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.HorizontalAlignment = HAlight;
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.VerticalAlignment = VAlight;
        }

        private static void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Bold = isBold;
        }

        private static void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold, bool isItalic)
        {
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold);
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Italic = isItalic;
        }

        private static void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, ExcelKnownColors color)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Color = color;
        }

        private static void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold, ExcelKnownColors color)
        {
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold);
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, color);
        }

        private static void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, bool isBold, bool isItalic, ExcelKnownColors color)
        {
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold, isItalic);
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, color);
        }

        private static void CellStyle(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol, string fontName, int fontSize, bool isBold, bool isItalic)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.FontName = fontName;
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.Font.Size = fontSize;
            CellStyle(xlsSheet, startRow, startCol, endRow, endCol, isBold, isItalic);
        }

        private static void CellStyleBackGround(IWorksheet xlsSheet, int startRow, int startCol, int endRow, int endCol)
        {
            xlsSheet.Range[startRow, startCol, endRow, endCol].CellStyle.ColorIndex = ExcelKnownColors.Grey_25_percent;
        }

        #endregion Support function
    }
}