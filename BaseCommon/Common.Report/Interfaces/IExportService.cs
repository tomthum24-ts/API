using BaseCommon.Common.Report.Infrastructures;
using BaseCommon.Common.Report.Models;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;


namespace BaseCommon.Common.Report.Interfaces
{
    public interface IExportService
    {
        MemoryStream ExportExcelData<T>(IEnumerable<T> dataSource, string maBieuMau, byte[] noiDungBieuMau, string tenBieuMau, Dictionary<string, string> replaceSameValues, List<string> columnDelete = null);

        MemoryStream ExportGroupExcelData<K, T>(IEnumerable<IGrouping<K, T>> groupData, string maBieuMau,
            byte[] noiDungBieuMau, string groupBox, string groupName, Dictionary<string, string> replaceSameValues);

        MemoryStream ExportWordData(byte[] noiDungBieuMau, Dictionary<string, string> pField, List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null);

        MemoryStream ExportGroupTwoLevelExcelData<T>(List<Dictionary<string, List<Dictionary<string, List<T>>>>> pDataSource, Dictionary<string, string> replaceSameValues, List<string> pColumnsDelete,
                                                byte[] noidungBieuMau, string pGroupBox1, string pGroupBox2, string pGroupName1, string pGroupName2, string maBieuMau, bool pIsFindAll = false);

        MemoryStream ExportPdfFromExcel<T>(IEnumerable<T> dataSource, string maBieuMau, byte[] noiDungBieuMau, string tenBieuMau, Dictionary<string, string> replaceSameValues, List<string> columnDelete = null);

        MemoryStream ExportPdfFromWord(byte[] noiDungBieuMau, Dictionary<string, string> pField, List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null);

        MemoryStream ExportPdfFromGroupExcelData<K, T>(IEnumerable<IGrouping<K, T>> groupData, string maBieuMau,
            byte[] noiDungBieuMau, string groupBox, string groupName, Dictionary<string, string> replaceSameValues);

        MemoryStream ExportPdfFormGroupTwoLevelExcelData<T>(List<Dictionary<string, List<Dictionary<string, List<T>>>>> pDataSource, Dictionary<string, string> replaceSameValues, List<string> pColumnsDelete,
                                                byte[] noidungBieuMau, string pGroupBox1, string pGroupBox2, string pGroupName1, string pGroupName2, string maBieuMau, bool pIsFindAll = false);

        string GetExtensionFile(string contentType);

        string GetContentType(string extension);

        MemoryStream ExportMultiSheet<T>(List<List<T>> dataSource, List<Dictionary<string, string>> replaceValues, int countSheet
          , byte[] noidungBieuMau, string maBieuMau, Dictionary<string, string> sameReplace = null, List<string> sheetName = null, List<string> columnDelete = null);

        DataTable ReadDataInStream(Stream stream);

        MemoryStream ExportExcelCustomMutipleSheet(byte[] noiDungBieuMau, List<RequestExcelSimpleReport> requestSimpleDatas = null, List<RequestExcelGroupDataReport> requestGroupDatas = null, List<RequestExcelGroupTwoLevelDataReport> requestGroupTwoLevels = null);

        MemoryStream ExportPdfFromExcelCustomMutipleSheet(byte[] noiDungBieuMau, List<RequestExcelSimpleReport> requestSimpleDatas = null, List<RequestExcelGroupDataReport> requestGroupDatas = null, List<RequestExcelGroupTwoLevelDataReport> requestGroupTwoLevels = null);

        ThongTinPreviewFile ExportToJpg(byte[] noiDungBieuMau, string contentType);

        MemoryStream ExportExcelSystemControl(byte[] noiDungBieuMau, string title, List<Dictionary<object, object>> contentData, Dictionary<string, RawDataInfo> headerData);
        MemoryStream ExportWordDataForKHCN(byte[] noiDungBieuMau, Dictionary<string, string> pField, List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null);
    }
}