using Aspose.Words;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace BaseCommon.Common.Report.Interfaces
{
    public interface IAsposeWordService
    {
        Document CreateDocument();

        Document CreateDocument(Stream stream);

        Document CreateDocument(byte[] dataByte);

        Document CreateDocument(Stream stream, LoadOptions loadOptions);

        Document CreateDocument(string fileName);

        Document CreateDocument(string fileName, LoadOptions loadOptions);

        Document AlignVerticalImage(Document document);

        string SaveFile(Document document, string path, SaveFormat type);

        string SaveFile(Document doc, SaveFormat type);

        MemoryStream SaveFileToMemoryStream(Document doc, SaveFormat type);

        byte[] SaveFileToByte(Document doc, SaveFormat type);

        void RemoveBlankLine(Document doc);

        void RemoveFirstBlankLine(Document doc);

        Document MergeTeamplate(Document mainDoc, Document subDoc);

        void MailMerge(Document document, Dictionary<string, string> dicMailMerge);

        void RemoveLastBlankLine(Document doc);

        byte[] ConvertRightNowAspose(Document document, SaveFormat saveFormat);

        string ConvertDocumentToHtml(Document document);

        void MailMergeWithHTML(Document document, Dictionary<string, string> dicMailMerge);
    }
}