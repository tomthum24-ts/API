using BaseCommon.Common.Report.Interfaces;
using BaseCommon.Common.Report.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using Syncfusion.XlsIO;
using Syncfusion.XlsIORenderer;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace BaseCommon.Common.Report.Infrastructures
{
    public class ExportService : IExportService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ExportService(IMemoryCache memoryCache, IHttpContextAccessor httpContextAccessor)
        {
            _memoryCache = memoryCache;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Common

        private readonly Dictionary<string, string> MappingExtension = new Dictionary<string, string>()
        {
            { ReportConstant.ContentTypeForExcel, ".xls" },
            { ReportConstant.ContentTypeForPDF, ".pdf" },
            { ReportConstant.ContentTypeForWord, ".doc" },
            { ReportConstant.ContentTypeForExcelXLSX, ".xlsx" },
            { ReportConstant.ContentTypeForWordDocX, ".docx" },
        };

        public string GetExtensionFile(string contentType)
        {
            return MappingExtension[contentType];
        }

        public string GetContentType(string extension)
        {
            return MappingExtension.FirstOrDefault(x => x.Value == extension).Key;
        }

        private void BuildReplaceInfo(ref Dictionary<string, string> replaceSameValues, string pMaBieuMau, bool pIsWord = false)
        {
            if (replaceSameValues == null)
            {
                replaceSameValues = new Dictionary<string, string>();
            }
            try
            {
                if (pIsWord)
                {
                    replaceSameValues.Add("FooterSystem", $"In từ phần mềm: , lúc {DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy")}");
                }
                else
                {
                    var vn = new System.Globalization.CultureInfo("vi-VN");
                    replaceSameValues.Add("%Thu", vn.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));
                    replaceSameValues.Add("%Ngay", DateTime.Now.Day.ToString().PadLeft(2, '0'));
                    replaceSameValues.Add("%Thang", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                    replaceSameValues.Add("%Nam", DateTime.Now.Year.ToString());
                    replaceSameValues.Add("%Gio", DateTime.Now.Hour.ToString());
                    replaceSameValues.Add("%Phut", DateTime.Now.Minute.ToString());
                    replaceSameValues.Add("%FooterSystem", $"In từ phần mềm: , lúc {DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy")}");
                    replaceSameValues.Add("%SYS_Thu", vn.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));
                    replaceSameValues.Add("%SYS_Ngay", DateTime.Now.Day.ToString().PadLeft(2, '0'));
                    replaceSameValues.Add("%SYS_Thang", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                    replaceSameValues.Add("%SYS_Nam", DateTime.Now.Year.ToString());
                    replaceSameValues.Add("%SYS_Gio", DateTime.Now.Hour.ToString());
                    replaceSameValues.Add("%SYS_Phut", DateTime.Now.Minute.ToString());
                    replaceSameValues.Add("%SYS_FooterSystem", $"In từ phần mềm: , lúc {DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy")}");

                    // Lấy thêm mã biểu mẫu khi cần
                    replaceSameValues.Add("%SYS_MaBieuMau", pMaBieuMau ?? "");
                    // Lấy tất cả các biến và giá trị có thể hỗ trợ
                    replaceSameValues.Add("%SYS_AllKey", string.Join("|", replaceSameValues.Select(m => m.Key.Replace("%", ""))));
                    replaceSameValues.Add("%SYS_AllValue", string.Join("|", replaceSameValues.Select(m => m.Value ?? "")));
                }
            }
            catch
            {
                // Tình huống xấu nhất có lỗi thì trả về như cũ hahaha!!!!!!!

                if (pIsWord)
                {
                    replaceSameValues.Add("FooterSystem", $"In từ phần mềm: , lúc {DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy")}");
                }
                else
                {
                    var vn = new System.Globalization.CultureInfo("vi-VN");
                    replaceSameValues.Add("%Thu", vn.DateTimeFormat.GetDayName(DateTime.Today.DayOfWeek));
                    replaceSameValues.Add("%Ngay", DateTime.Now.Day.ToString().PadLeft(2, '0'));
                    replaceSameValues.Add("%Thang", DateTime.Now.Month.ToString().PadLeft(2, '0'));
                    replaceSameValues.Add("%Nam", DateTime.Now.Year.ToString());
                    replaceSameValues.Add("%Gio", DateTime.Now.Hour.ToString());
                    replaceSameValues.Add("%Phut", DateTime.Now.Minute.ToString());
                    replaceSameValues.Add("%FooterSystem", $"In từ phần mềm: , lúc {DateTime.Now.ToString("HH:mm:ss - dd/MM/yyyy")}");
                }
            }
        }

        #endregion Common

        #region Excel

        public MemoryStream ExportExcelData<T>(IEnumerable<T> dataSource, string maBieuMau, byte[] noiDungBieuMau, string tenBieuMau, Dictionary<string, string> replaceSameValues, List<string> columnDelete = null)
        {
            BuildReplaceInfo(ref replaceSameValues, maBieuMau);

            IWorkbook workbook = ExcelReportHelper.OutSimpleReport(dataSource.ToList(), noiDungBieuMau, maBieuMau, replaceSameValues, false, columnDelete);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            workbook.SaveAs(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportGroupExcelData<K, T>(IEnumerable<IGrouping<K, T>> groupData, string maBieuMau,
            byte[] noiDungBieuMau, string groupBox, string groupName, Dictionary<string, string> replaceSameValues)
        {
            BuildReplaceInfo(ref replaceSameValues, maBieuMau);
            IWorkbook workbook = ExcelReportHelper.OutGroupReport(groupData.ToList(), noiDungBieuMau, replaceSameValues, groupBox, maBieuMau, groupName, null);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            workbook.SaveAs(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportMultiSheet<T>(List<List<T>> dataSource, List<Dictionary<string, string>> replaceValues, int countSheet
           , byte[] noidungBieuMau, string maBieuMau, Dictionary<string, string> sameReplace = null, List<string> sheetName = null, List<string> columnDelete = null)
        {
            BuildReplaceInfo(ref sameReplace, maBieuMau);
            IWorkbook workbook = ExcelReportHelper.OutSimpleReportMultiSheet(dataSource.ToList(), replaceValues, countSheet, noidungBieuMau, maBieuMau, null, sheetName, null);
            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            workbook.SaveAs(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportGroupTwoLevelExcelData<T>(List<Dictionary<string, List<Dictionary<string, List<T>>>>> pDataSource, Dictionary<string, string> replaceSameValues, List<string> pColumnsDelete,
                                                byte[] noidungBieuMau, string pGroupBox1, string pGroupBox2, string pGroupName1, string pGroupName2, string maBieuMau, bool pIsFindAll = false)
        {
            BuildReplaceInfo(ref replaceSameValues, maBieuMau);
            IWorkbook workbook = ExcelReportHelper.ExportGroupTwoLevelExcelData(pDataSource, replaceSameValues, pColumnsDelete, noidungBieuMau, pGroupBox1, pGroupBox2, pGroupName1, pGroupName2, maBieuMau, pIsFindAll);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            workbook.SaveAs(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public DataTable ReadDataInStream(Stream stream)
        {
            using ExcelEngine excelEngine = new ExcelEngine();
            //Initialize application
            IApplication app = excelEngine.Excel;
            //Open existing Excel workbook from the stream
            IWorkbook workBook = app.Workbooks.Open(stream);
            IWorksheet worksheet = workBook.Worksheets.First();
            //   IComboBoxShape comboBox = worksheet.ComboBoxes.AddComboBox(2, 3, 20, 100);
            //Export worksheet data into Collection Objects
            return worksheet.ExportDataTable(1, 1, worksheet.Rows.Count(), worksheet.Columns.Count(), ExcelExportDataTableOptions.ColumnNames);
        }

        public MemoryStream ExportExcelCustomMutipleSheet(byte[] noiDungBieuMau, List<RequestExcelSimpleReport> requestSimpleDatas = null, List<RequestExcelGroupDataReport> requestGroupDatas = null, List<RequestExcelGroupTwoLevelDataReport> requestGroupTwoLevels = null)
        {
            IWorkbook workbook = ExcelReportHelper.OutputCustomMutipleSheet(noiDungBieuMau, requestSimpleDatas, requestGroupDatas, requestGroupTwoLevels);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            workbook.SaveAs(outputStream);
            outputStream.Position = 0;
            workbook.Close();
            return outputStream;
        }

        public MemoryStream ExportExcelSystemControl(byte[] noiDungBieuMau, string title, List<Dictionary<object, object>> contentData, Dictionary<string, RawDataInfo> headerData)
        {
            IWorkbook workbook = ExcelReportHelper.AutoExportToExcel(noiDungBieuMau, title, contentData, headerData);
            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            workbook.SaveAs(outputStream);
            outputStream.Position = 0;
            workbook.Close();
            return outputStream;
        }

        #endregion Excel

        #region Word

        public MemoryStream ExportWordData(byte[] noiDungBieuMau, Dictionary<string, string> pField, List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null)
        {
            BuildReplaceInfo(ref pField, "", true);
            WordDocument document = WordReportHelper.Export(noiDungBieuMau, pField, wordTemplateTables, byteImages, qrCode, requestGroup);
            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream, FormatType.Docx);
            document.Close();
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportWordDataForKHCN(byte[] noiDungBieuMau, Dictionary<string, string> pField, List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null)
        {
            BuildReplaceInfo(ref pField, "", true);
            WordDocument document = WordReportHelper.ExportForKHCN(noiDungBieuMau, pField, wordTemplateTables, byteImages, qrCode, requestGroup);
            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream, FormatType.Docx);
            document.Close();
            outputStream.Position = 0;
            return outputStream;
        }

        #endregion Word

        #region PDF

        public MemoryStream ExportPdfFromExcel<T>(IEnumerable<T> dataSource, string maBieuMau, byte[] noiDungBieuMau, string tenBieuMau, Dictionary<string, string> replaceSameValues, List<string> columnDelete = null)
        {
            BuildReplaceInfo(ref replaceSameValues, maBieuMau);
            IWorkbook workbook = ExcelReportHelper.OutSimpleReport(dataSource.ToList(), noiDungBieuMau, maBieuMau, replaceSameValues, false, columnDelete);

            //Initialize XlsIORendererSettings
            XlsIORendererSettings settings = new XlsIORendererSettings();

            //Enable AutoDetectComplexScript property
            settings.AutoDetectComplexScript = true;

            //Initialize XlsIORenderer
            XlsIORenderer renderer = new XlsIORenderer();

            //Convert the Excel document to PDF with renderer settings
            PdfDocument document = renderer.ConvertToPDF(workbook, settings);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportPdfFromGroupExcelData<K, T>(IEnumerable<IGrouping<K, T>> groupData, string maBieuMau,
            byte[] noiDungBieuMau, string groupBox, string groupName, Dictionary<string, string> replaceSameValues)
        {
            BuildReplaceInfo(ref replaceSameValues, maBieuMau);
            IWorkbook workbook = ExcelReportHelper.OutGroupReport(groupData.ToList(), noiDungBieuMau, replaceSameValues, groupBox, maBieuMau, groupName, null);

            //Initialize XlsIORendererSettings
            XlsIORendererSettings settings = new XlsIORendererSettings();

            //Enable AutoDetectComplexScript property
            settings.AutoDetectComplexScript = true;

            //Initialize XlsIORenderer
            XlsIORenderer renderer = new XlsIORenderer();

            //Convert the Excel document to PDF with renderer settings
            PdfDocument document = renderer.ConvertToPDF(workbook, settings);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportPdfFormGroupTwoLevelExcelData<T>(List<Dictionary<string, List<Dictionary<string, List<T>>>>> pDataSource, Dictionary<string, string> replaceSameValues, List<string> pColumnsDelete,
                                                byte[] noidungBieuMau, string pGroupBox1, string pGroupBox2, string pGroupName1, string pGroupName2, string maBieuMau, bool pIsFindAll = false)
        {
            BuildReplaceInfo(ref replaceSameValues, maBieuMau);
            IWorkbook workbook = ExcelReportHelper.ExportGroupTwoLevelExcelData(pDataSource, replaceSameValues, pColumnsDelete, noidungBieuMau, pGroupBox1, pGroupBox2, pGroupName1, pGroupName2, maBieuMau, pIsFindAll);

            //Initialize XlsIORendererSettings
            XlsIORendererSettings settings = new XlsIORendererSettings();

            //Enable AutoDetectComplexScript property
            settings.AutoDetectComplexScript = true;

            //Initialize XlsIORenderer
            XlsIORenderer renderer = new XlsIORenderer();

            //Convert the Excel document to PDF with renderer settings
            PdfDocument document = renderer.ConvertToPDF(workbook, settings);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportPdfFromExcelCustomMutipleSheet(byte[] noiDungBieuMau, List<RequestExcelSimpleReport> requestSimpleDatas = null, List<RequestExcelGroupDataReport> requestGroupDatas = null, List<RequestExcelGroupTwoLevelDataReport> requestGroupTwoLevels = null)
        {
            IWorkbook workbook = ExcelReportHelper.OutputCustomMutipleSheet(noiDungBieuMau, requestSimpleDatas, requestGroupDatas, requestGroupTwoLevels);

            //Initialize XlsIORendererSettings
            XlsIORendererSettings settings = new XlsIORendererSettings();

            //Enable AutoDetectComplexScript property
            settings.AutoDetectComplexScript = true;

            //Initialize XlsIORenderer
            XlsIORenderer renderer = new XlsIORenderer();

            //Convert the Excel document to PDF with renderer settings
            PdfDocument document = renderer.ConvertToPDF(workbook, settings);

            //Save the workbook to stream
            MemoryStream outputStream = new MemoryStream();
            document.Save(outputStream);
            outputStream.Position = 0;
            return outputStream;
        }

        public MemoryStream ExportPdfFromWord(byte[] noiDungBieuMau, Dictionary<string, string> pField, List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null)
        {
            BuildReplaceInfo(ref pField, "", true);
            WordDocument document = WordReportHelper.Export(noiDungBieuMau, pField, wordTemplateTables, byteImages, qrCode, requestGroup);
            MemoryStream outputStreamWord = new MemoryStream();
            document.Save(outputStreamWord, FormatType.Docx);
            //Instantiation of DocIORenderer for Word to PDF conversion
            DocIORenderer render = new DocIORenderer();
            render.Settings.EmbedFonts = true;
            //render.Settings.EmbedCompleteFonts = true;
            //render.Settings.UpdateDocumentFields = true;
            //render.Settings.PdfConformanceLevel = PdfConformanceLevel.Pdf_A1B;
            //Converts Word document into PDF document
            PdfDocument pdfDocument = render.ConvertToPDF(outputStreamWord);
            //Releases all resources used by the Word document and DocIO Renderer objects

            MemoryStream outputStream = new MemoryStream();
            pdfDocument.Save(outputStream);
            document.Close();
            outputStream.Position = 0;
            return outputStream;
        }

        #endregion PDF

        public ThongTinPreviewFile ExportToJpg(byte[] noiDungBieuMau, string contentType)
        {
            //var licenseFilePath = Path.GetFullPath(Path.Combine(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath, "../Doconut.lic"));

            //var docViewer = new DocViewer(_memoryCache, _httpContextAccessor, licenseFilePath);

            //var documentOptions = new DocOptions
            //{
            //    Password = "",
            //    ImageResolution = 200,
            //    Watermark = "ASCVN",
            //    TimeOut = 3600
            //};

            //var pdfConfig = new PdfConfig { ExtractHyperlinks = true };
            //BaseConfig wordConfig = new WordConfig { ConvertPdf = true, PdfConfig = pdfConfig };

            //if (contentType == ReportConstant.ContentTypeForWord || contentType == ReportConstant.ContentTypeForWordDocX)
            //    docViewer.OpenDocument(noiDungBieuMau, ".doc", wordConfig, documentOptions);
            //else if (contentType == ReportConstant.ContentTypeForPDF)
            //    docViewer.OpenDocument(noiDungBieuMau, ".pdf", pdfConfig, documentOptions);

            var thongTinPreviewFile = new ThongTinPreviewFile();//{ TotalPage = docViewer.TotalPages };

            //// Generate Thumbnails
            //MemoryStream stream = new MemoryStream();

            //for (int iThumb = 1; iThumb <= thongTinPreviewFile.TotalPage; iThumb++)
            //{
            //    var pageThumbnail = docViewer.GetThumbnail(iThumb, 10000, 100, false);
            //    pageThumbnail.Save(stream, ImageFormat.Jpeg);
            //    string imageBase64Data = Convert.ToBase64String(stream.ToArray());
            //    thongTinPreviewFile.Images.Add(imageBase64Data);
            //    stream = new MemoryStream();
            //}
            //// End Thumbnails
            return thongTinPreviewFile;
        }
    }
}