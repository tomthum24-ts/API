using Aspose.Words;
using Aspose.Words.Loading;
using Aspose.Words.MailMerging;
using Aspose.Words.Replacing;
using Aspose.Words.Saving;
using BaseCommon.Common.Report.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BaseCommon.Common.Report.Infrastructures.AsposeWordServives
{
    public class AsposeWordService : IAsposeWordService
    {
        private const string _markerDeThi = "{{DeThi}}";
        private const string _markerSoTrang = "{{TotalPage}}";

        public AsposeWordService()
        {
            new Aspose.Words.License().SetLicense(BaseCommon.LStream);
        }

        #region Public

        public Document CreateDocument()
        {
            return new Document();
        }

        public Document CreateDocument(Stream stream)
        {
            return new Document(stream);
        }

        public Document CreateDocument(byte[] dataByte)
        {
            MemoryStream mSteam = new(dataByte);
            var docTemp = new Document(mSteam);
            try { mSteam.Close(); } catch { }

            return docTemp;
        }

        public Document CreateDocument(Stream stream, LoadOptions loadOptions)
        {
            return new Document(stream, loadOptions);
        }

        public Document CreateDocument(string fileName)
        {
            return new Document(fileName);
        }

        public Document CreateDocument(string fileName, LoadOptions loadOptions)
        {
            return new Document(fileName, loadOptions);
        }

        public Document AlignVerticalImage(Document document)
        {
            var vLineHeight = 5;
            Aspose.Words.NodeCollection shapes = document.GetChildNodes(Aspose.Words.NodeType.Shape, true);
            foreach (Aspose.Words.Drawing.Shape shape in shapes)
            {
                if (shape != null)
                    shape.Font.Position = -(shape.Height / 2 - vLineHeight);
            }
            return document;
        }

        public string SaveFile(Document document, string path, SaveFormat type)
        {
            try
            {
                document.Save(path, type);
            }
            catch (Exception)
            {
            }
            return path;
        }

        public string SaveFile(Document doc, SaveFormat type)
        {
            try
            {
                var text = "";
                using (MemoryStream stream = new MemoryStream())
                {
                    doc.Save(stream, type);
                    text = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    stream.Close();
                    stream.Dispose();
                }
                return text;
            }
            catch
            {
            }
            return "";
        }

        public MemoryStream SaveFileToMemoryStream(Document doc, SaveFormat type)
        {
            try
            {
                MemoryStream stream = new MemoryStream();
                doc.Save(stream, type);
                return stream;
            }
            catch
            {
            }
            return null;
        }

        public byte[] SaveFileToByte(Document doc, SaveFormat type)
        {
            try
            {
                byte[] fileContent;
                using (MemoryStream stream = new MemoryStream())
                {
                    doc.Save(stream, type);
                    fileContent = stream.ToArray();
                    stream.Close();
                    stream.Dispose();
                }
                return fileContent;
            }
            catch
            {
            }
            return null;
        }

        public void RemoveBlankLine(Document doc)
        {
            RemoveSectionBreaks(doc);
            //Remove empty paragraphs
            foreach (Paragraph paragraph in doc.GetChildNodes(NodeType.Paragraph, true))
            {
                if (paragraph.ToString(SaveFormat.Text).Trim().Length == 0 && paragraph.GetChildNodes(NodeType.Shape, true).Count == 0 && !paragraph.IsInCell)
                {
                    paragraph.Remove();
                }
                else if (paragraph.ToString(SaveFormat.Text).Trim().Contains("[<GCCT>]"))
                {
                    paragraph.ParagraphFormat.Alignment = ParagraphAlignment.Center;
                }
            }
            // ký tự thêm dòng mới
            doc.Range.Replace("[<&>]", "", new FindReplaceOptions());
            // Ký tự ghi chú cuối trang
            doc.Range.Replace("[<GCCT>]", "", new FindReplaceOptions());
            doc.UpdatePageLayout();
        }

        public void RemoveFirstBlankLine(Document doc)
        {
            Paragraph paragraph = (Paragraph)doc.GetChildNodes(NodeType.Paragraph, true).FirstOrDefault();
            if (paragraph != null && paragraph.ToString(SaveFormat.Text).Trim().Length == 0 && paragraph.GetChildNodes(NodeType.Shape, true).Count == 0 && !paragraph.IsInCell)
            {
                paragraph.Remove();
            }
        }

        public Document MergeTeamplate(Document mainDoc, Document subDoc)
        {
            FindReplaceOptions options = new FindReplaceOptions
            {
                ReplacingCallback = new InsertDocumentAtReplaceHandler(subDoc)
            };
            mainDoc.Range.Replace(_markerDeThi, "", options);
            return mainDoc;
        }

        public void MailMerge(Document document, Dictionary<string, string> dicMailMerge)
        {
            if (dicMailMerge != null && dicMailMerge.Count > 0)
            {
                dicMailMerge.Add("TotalPage", document.PageCount.ToString());
                var fieldName = dicMailMerge.Select(m => m.Key).ToArray();
                var fieldValue = dicMailMerge.Select(m => m.Value).ToArray();
                document.MailMerge.Execute(fieldName, fieldValue);
            }
        }

        public void MailMergeWithHTML(Document document, Dictionary<string, string> dicMailMerge)
        {
            if (dicMailMerge != null && dicMailMerge.Count > 0)
            {
                dicMailMerge.Add("TotalPage", document.PageCount.ToString());
                var fieldName = dicMailMerge.Select(m => m.Key).ToArray();
                var fieldValue = dicMailMerge.Select(m => m.Value).ToArray();
                document.MailMerge.FieldMergingCallback = new HandleMergeFieldInsertHtml();
                document.MailMerge.Execute(fieldName, fieldValue);
            }
        }

        public void RemoveLastBlankLine(Document doc)
        {
            var lstPara = doc.GetChildNodes(NodeType.Paragraph, true);
            for (int i = lstPara.Count - 1; i >= 0; i--)
            {
                var paragraph = (Paragraph)lstPara[i];
                if (paragraph.ToString(SaveFormat.Text).Trim().Length == 0 && paragraph.GetChildNodes(NodeType.Shape, true).Count == 0 && !paragraph.IsInCell)
                {
                    paragraph.Remove();
                }
                else break;
            }
        }

        public byte[] ConvertRightNowAspose(Document document, SaveFormat saveFormat)
        {
            Document document1 = document;
            document1.PageColor = Color.White;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                if (saveFormat == SaveFormat.Html)
                {
                    document1.Save(memoryStream, new HtmlSaveOptions(SaveFormat.Html)
                    {
                        ExportXhtmlTransitional = true,
                        ImageSavingCallback = new HandleImageSaving(),
                        ExportImagesAsBase64 = true
                    });
                }
                else
                {
                    document1.Save(memoryStream, saveFormat);
                }

                return memoryStream.GetBuffer();
            }
        }

        public string ConvertDocumentToHtml(Document document)
        {
            string html = "";
            document.PageColor = Color.White;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                document.Save(memoryStream, new HtmlSaveOptions(SaveFormat.Html)
                {
                    ExportXhtmlTransitional = true,
                    ImageSavingCallback = new HandleImageSaving(),
                    ExportImagesAsBase64 = true
                });

                html = Encoding.UTF8.GetString(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
            }

            while (html[0] != '<')
            {
                html = html.Substring(1);
            }

            // Chỉ lấy nội dung thẻ p bắt đầu => p cuối
            var match1 = Regex.Matches(html, @"<p.*<\/p>");
            if (match1.Count > 0)
            {
                html = match1[0].Value;
            }

            // Thay thẻ p đầu thành span => khi in ko bị xuống dòng
            var match2 = Regex.Matches(html, @"<p.*?<\/p>");
            if (match2.Count > 0)
            {
                var htmlReplace = match2[0].Value.Replace("<p", "<span").Replace("</p>", "</span>");
                html = html.Replace(match2[0].Value, htmlReplace);
            }

            return html;
        }

        #endregion Public

        #region Private

        private void RemoveSectionBreaks(Document doc)
        {
            // Loop through all sections starting from the section that precedes the last one
            // and moving to the first section.
            for (int i = doc.Sections.Count - 2; i >= 0; i--)
            {
                // Copy the content of the current section to the beginning of the last section.
                doc.LastSection.PrependContent(doc.Sections[i]);
                // Remove the copied section.
                doc.Sections[i].Remove();
            }
        }

        private static void InsertDocument(Node insertionDestination, Document docToInsert)
        {
            if (insertionDestination.NodeType == NodeType.Paragraph || insertionDestination.NodeType == NodeType.Table)
            {
                CompositeNode dstStory = insertionDestination.ParentNode;

                NodeImporter importer = new(docToInsert, insertionDestination.Document, ImportFormatMode.KeepSourceFormatting);

                foreach (Section srcSection in docToInsert.Sections.OfType<Section>())
                {
                    foreach (Node srcNode in srcSection.Body)
                    {
                        // Skip the node if it is the last empty paragraph in a section.
                        if (srcNode.NodeType == NodeType.Paragraph)
                        {
                            Paragraph para = (Paragraph)srcNode;
                            if (para.IsEndOfSection && !para.HasChildNodes)
                                continue;
                        }

                        Node newNode = importer.ImportNode(srcNode, true);

                        dstStory.InsertAfter(newNode, insertionDestination);
                        insertionDestination = newNode;
                    }
                }
            }
            else
            {
                throw new ArgumentException("The destination node must be either a paragraph or table.");
            }
        }

        public Document CreateDocument(Stream stream, System.Xml.Linq.LoadOptions loadOptions)
        {
            throw new NotImplementedException();
        }

        public Document CreateDocument(string fileName, System.Xml.Linq.LoadOptions loadOptions)
        {
            throw new NotImplementedException();
        }

        #endregion Private

        #region InternalClass

        private class InsertDocumentAtReplaceHandler : IReplacingCallback
        {
            private Document _subDoc;

            public InsertDocumentAtReplaceHandler(Document subDoc)
            {
                _subDoc = subDoc;
            }

            ReplaceAction IReplacingCallback.Replacing(ReplacingArgs args)
            {
                // Insert a document after the paragraph, containing the match text.
                Paragraph para = (Paragraph)args.MatchNode.ParentNode;
                InsertDocument(para, _subDoc);

                // Remove the paragraph with the match text.
                para.Remove();
                return ReplaceAction.Skip;
            }
        }

        private class HandleMergeFieldInsertHtml : IFieldMergingCallback
        {
            /// <summary>
            /// Called when a mail merge merges data into a MERGEFIELD.
            /// </summary>
            void IFieldMergingCallback.FieldMerging(FieldMergingArgs args)
            {
                if (args.DocumentFieldName.StartsWith("HTML_"))
                {
                    // Add parsed HTML data to the document's body.
                    DocumentBuilder builder = new DocumentBuilder(args.Document);
                    builder.MoveToMergeField(args.DocumentFieldName);
                    builder.InsertHtml((string)args.FieldValue);

                    // Since we have already inserted the merged content manually,
                    // we will not need to respond to this event by returning content via the "Text" property.
                    args.Text = string.Empty;
                }
            }

            void IFieldMergingCallback.ImageFieldMerging(ImageFieldMergingArgs args)
            {
                // Do nothing.
            }
        }

        #endregion InternalClass
    }

    #region External class

    public class HandleImageSaving : IImageSavingCallback
    {
        void IImageSavingCallback.ImageSaving(ImageSavingArgs e)
        {
            e.ImageStream = (Stream)new MemoryStream();
            e.KeepImageStreamOpen = false;
        }
    }

    #endregion External class
}