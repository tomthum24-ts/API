

using BaseCommon.Common.Report.Models;
using HtmlAgilityPack;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.Pdf.Barcode;
using Syncfusion.Pdf.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;

namespace BaseCommon.Common.Report.Infrastructures
{
    public class WordTemplateTable
    {
        public HashSet<string> ColumnKeyWord { get; set; }
        public List<object> DataTable { get; set; }
        public string Prefix { get; set; }
        public bool IsDeleteRows { get; set; }
        public bool IsFormatHTML { get; set; }
    }

    public static class WordReportHelper
    {
        #region enum

        public enum WordVersion
        {
            Word2003,
            Word2007,
            Word2010,
            Unknow,
        }

        public enum SaveFileResult
        {
            Success,
            Cancel,
            Failed,
        }

        #endregion enum

        #region Variables

        private static IsolatedStorageFile _store;
        public const string _filterWord = "Word Document|*.doc";
        public const string MailMegreHTMLPrefix = "HTML_";

        #endregion Variables

        #region Public Static Methods

        /// <summary>
        /// Exports the specified p loai bieu mau.
        /// </summary>
        /// <param name="pMaBieuMau">The p loai bieu mau.</param>
        /// <param name="pField">The p field.</param>
        /// <param name="pShowSaveDialog">if set to <c>true</c> [p show save dialog].</param>
        /// <returns></returns>
        public static WordDocument Export(byte[] noiDungBieuMau, Dictionary<string, string> pField,
            List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null)
        {
            WordDocument synDoc = ExcutiveMailMerge(noiDungBieuMau, pField, requestGroup);

            if (wordTemplateTables != null && wordTemplateTables.Count > 0)
            {
                foreach (var item in wordTemplateTables)
                {
                    if (item.DataTable != null && item.DataTable.Count > 0)
                    {
                        RepeatRowInTable(ref synDoc, item);
                    }
                }
            }

            foreach (WTextBox textBox in synDoc.TextBoxes)
            {
                AddImageToWord(byteImages, textBox);
                AddQRCodeToWord(qrCode, textBox);
            }

            return synDoc;
        }

        /// <summary>
        /// Exports the specified p loai bieu mau.
        /// </summary>
        /// <param name="pMaBieuMau">The p loai bieu mau.</param>
        /// <param name="pField">The p field.</param>
        /// <param name="pShowSaveDialog">if set to <c>true</c> [p show save dialog].</param>
        /// <returns></returns>
        public static WordDocument Export(byte[] noiDungBieuMau, Dictionary<string, string> pField,
            List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null)
        {
            WordDocument synDoc = ExcutiveMailMerge(noiDungBieuMau, pField);

            if (wordTemplateTables != null && wordTemplateTables.Count > 0)
            {
                foreach (var item in wordTemplateTables)
                {
                    if (item.DataTable != null && item.DataTable.Count > 0)
                    {
                        RepeatRowInTable(ref synDoc, item);
                    }
                }
            }

            foreach (WTextBox textBox in synDoc.TextBoxes)
            {
                AddImageToWord(byteImages, textBox);
                AddQRCodeToWord(qrCode, textBox);
            }

            return synDoc;
        }

        public static WordDocument ExcutiveMailMerge(byte[] noiDungBieuMau, Dictionary<string, string> pField, List<RequestGroupTableWordReport> requestGroup = null)
        {
            MemoryStream stream = new MemoryStream();

            if (GetBieuMau(noiDungBieuMau, ref stream))
            {
                //Prepare field, value for Document
                Dictionary<string, string> dicFields = pField;

                WordDocument synDoc = new WordDocument();
                synDoc.Open(stream, FormatType.Automatic);

                stream.Close();
                if (requestGroup != null && requestGroup.Any())
                {
                    foreach (var item in requestGroup)
                    {
                        if (item.GroupData != null)
                        {
                            MailMergeDataTable dataTable = new MailMergeDataTable(item.GroupName, item.GroupData);
                            synDoc.MailMerge.RemoveEmptyParagraphs = true;
                            synDoc.MailMerge.RemoveEmptyGroup = true;
                            synDoc.MailMerge.MergeField += MailMerge_MergeField;
                            //Performs the mail merge for group
                            synDoc.MailMerge.ExecuteNestedGroup(dataTable);
                            //Removes empty page at the end of Word document
                            RemoveEmptyPage(synDoc);
                        }
                    }
                }

                //Set empty when field not found
                synDoc.MailMerge.GetMergeFieldNames().ToList<string>().ForEach(delegate (string fieldName)
                {
                    if (!dicFields.ContainsKey(fieldName))
                    {
                        dicFields[fieldName] = string.Empty;
                    }
                });

                synDoc.MailMerge.MergeField += MailMerge_MergeField;

                synDoc.MailMerge.Execute(dicFields.Keys.ToArray<string>(), dicFields.Values.ToArray<string>());

                synDoc.UpdateDocumentFields();
                return synDoc;
            }

            return null;
        }

        public static WordDocument ExcutiveMailMerge(byte[] noiDungBieuMau, Dictionary<string, string> pField)
        {
            MemoryStream stream = new MemoryStream();

            if (GetBieuMau(noiDungBieuMau, ref stream))
            {
                //Prepare field, value for Document
                Dictionary<string, string> dicFields = pField;

                WordDocument synDoc = new WordDocument();
                synDoc.Open(stream, FormatType.Automatic);

                stream.Close();

                //Set empty when field not found
                synDoc.MailMerge.GetMergeFieldNames().ToList<string>().ForEach(delegate (string fieldName)
                {
                    if (!dicFields.ContainsKey(fieldName))
                    {
                        dicFields[fieldName] = string.Empty;
                    }
                });

                synDoc.MailMerge.MergeField += MailMerge_MergeField;

                synDoc.MailMerge.Execute(dicFields.Keys.ToArray<string>(), dicFields.Values.ToArray<string>());
                synDoc.UpdateDocumentFields();
                return synDoc;
            }

            return null;
        }

        private static string ConvertHtmlToXHtml(string html)
        {
            try
            {
                html = WebUtility.HtmlDecode(html);
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.OptionOutputAsXml = true;
                htmlDocument.OptionAutoCloseOnEnd = true;
                htmlDocument.LoadHtml(html);
                string xhtml = htmlDocument.DocumentNode.OuterHtml;
                return xhtml;
            }
            catch
            {
                return html;
            }
        }

        /// <summary>
        /// Mail merge even handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void MailMerge_MergeField(object sender, MergeFieldEventArgs args)
        {
            if (args.CurrentMergeField.FieldName.StartsWith(MailMegreHTMLPrefix) && args.CurrentMergeField.OwnerParagraph != null)
            {
                try
                {
                    WParagraph paragraph = args.CurrentMergeField.OwnerParagraph;
                    int mergeFieldIndex = paragraph.ChildEntities.IndexOf(args.CurrentMergeField);
                    int mergeFieldParaIndex = paragraph.OwnerTextBody.ChildEntities.IndexOf(paragraph);
                    paragraph.ChildEntities.Remove(args.CurrentMergeField);
                    paragraph.ChildEntities.Clear();
                    //paragraph.Document.XHTMLValidateOption = XHTMLValidationType.None;
                    //paragraph.OwnerTextBody.InsertXHTML(ConvertHtmlToXHtml(args.Text), mergeFieldParaIndex, mergeFieldIndex);
                    paragraph.AppendHTML(args.Text);
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Get Dictionary for mail merge from store procedure
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionaryForMailMerge(string key, string value, char splitChar = '#')
        {
            List<string> keys = key.Split(splitChar).ToList();
            List<string> values = value.Split(splitChar).ToList();
            var dictionary = keys.Zip(values, (k, v) => new { Key = k, Value = v })
                     .ToDictionary(x => x.Key, x => x.Value);
            return dictionary;
        }

        public static bool RepeatRowInTable(ref WordDocument document, WordTemplateTable duLieuWords)
        {
            var fields = duLieuWords.ColumnKeyWord;
            //Đảo ngược listOBJ
            duLieuWords.DataTable.Reverse();
            var objectCollection = duLieuWords.DataTable;
            var prefix = duLieuWords.Prefix;
            var isDeleteRows = duLieuWords.IsDeleteRows;
            WTable tableRepeart = null;
            WTableRow cloneRow = null;
            int rowIndex = -1;

            bool nextTable = false;
            foreach (WSection section in document.Sections)
            {
                foreach (WTable table in section.Tables)
                {
                    nextTable = false;
                    foreach (WTableRow row in table.Rows)
                    {
                        foreach (WTableCell cell in row.Cells)
                        {
                            if (cell.LastParagraph != null)
                            {
                                if (fields.Count(m => (prefix + m) == cell.LastParagraph.Text) > 0)
                                {
                                    cloneRow = row.Clone();
                                    rowIndex = row.GetRowIndex();
                                    tableRepeart = table;
                                    nextTable = true;
                                    break;
                                }
                            }
                        }

                        if (nextTable)
                            break;
                    }
                }
            }

            if (tableRepeart != null)
            {
                if (prefix.Contains("^"))
                {
                    if (isDeleteRows)
                    {
                        tableRepeart.Rows.RemoveAt(0);
                        tableRepeart.Rows.RemoveAt(0);
                        tableRepeart.Rows.RemoveAt(0);
                        tableRepeart.Rows.RemoveAt(0);
                    }
                }
                else
                {
                    if (!duLieuWords.IsFormatHTML)
                    {
                        foreach (object obj in objectCollection)
                        {
                            foreach (var field in fields)
                            {
                                WTableCell cell = tableRepeart.Rows[rowIndex].Cells.OfType<WTableCell>().FirstOrDefault<WTableCell>(m => m.LastParagraph.Text == (prefix + field));
                                if (cell != null)
                                {
                                    object objValue = obj.GetType().GetProperty(field.ToString()).GetValue(obj, null);
                                    cell.LastParagraph.Text = objValue == null ? "" : objValue.ToString();
                                }
                            }
                            if (objectCollection.IndexOf(obj) != objectCollection.Count - 1)
                                tableRepeart.Rows.Insert(rowIndex, cloneRow.Clone());
                        }
                    }
                    else
                    {
                        foreach (object obj in objectCollection)
                        {
                            foreach (var field in fields)
                            {
                                WTableCell cell = tableRepeart.Rows[rowIndex].Cells.OfType<WTableCell>().FirstOrDefault<WTableCell>(m => m.LastParagraph.Text == (prefix + field));
                                if (cell != null)
                                {
                                    object objValue = obj.GetType().GetProperty(field.ToString()).GetValue(obj, null);
                                    cell.LastParagraph.Text = string.Empty;
                                    cell.LastParagraph.AppendHTML(objValue == null ? "" : objValue.ToString());
                                }
                            }
                            if (objectCollection.IndexOf(obj) != objectCollection.Count - 1)
                                tableRepeart.Rows.Insert(rowIndex, cloneRow.Clone());
                        }
                    }
                }
            }

            return true;
        }

        #endregion Public Static Methods

        #region Private Methods

        /// <summary>
        /// Removes empty paragraphs from the end of Word document.
        /// </summary>
        /// <param name="document">The Word document</param>
        private static void RemoveEmptyPage(WordDocument document)
        {
            WTextBody textBody = document.LastSection.Body;

            //A flag to determine any renderable item found in the Word document.
            bool IsRenderableItem = false;
            //Iterates text body items.
            for (int itemIndex = textBody.ChildEntities.Count - 1; itemIndex >= 0 && !IsRenderableItem; itemIndex--)
            {
                //Check item is empty paragraph and removes it.
                if (textBody.ChildEntities[itemIndex] is WParagraph)
                {
                    WParagraph paragraph = textBody.ChildEntities[itemIndex] as WParagraph;
                    //Iterates into paragraph
                    for (int pIndex = paragraph.Items.Count - 1; pIndex >= 0; pIndex--)
                    {
                        ParagraphItem paragraphItem = paragraph.Items[pIndex];

                        //If page break found in end of document, then remove it to preserve contents in same page
                        if ((paragraphItem is Break && (paragraphItem as Break).BreakType == BreakType.PageBreak))
                            paragraph.Items.RemoveAt(pIndex);

                        //Check paragraph contains any renderable items.
                        else if (!(paragraphItem is BookmarkStart || paragraphItem is BookmarkEnd))
                        {
                            IsRenderableItem = true;
                            //Found renderable item and break the iteration.
                            break;
                        }
                    }
                    //Remove empty paragraph and the paragraph with bookmarks only
                    if (paragraph.Items.Count == 0 || !IsRenderableItem)
                        textBody.ChildEntities.RemoveAt(itemIndex);
                }
            }
        }

        public static Stream ResizeImage(System.Drawing.Image imgToResize, float width, float height)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = (width / (float)sourceWidth);
            nPercentH = (height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            System.Drawing.Image imageResult = b;
            var memoryStream = new MemoryStream();
            imageResult.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            return memoryStream;
        }

        /// <summary>
        /// Resize an image keeping its aspect ratio (cropping may occur).
        /// </summary>
        /// <param name="source"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private static MemoryStream ResizeImageKeepAspectRatio(byte[] byteArray, int width, int height)
        {
            MemoryStream memoryStream = new MemoryStream();
            try
            {
                // Increase Quality image
                width *= 2;
                height *= 2;

                System.Drawing.Image source = null;
                using (var ms = new MemoryStream(byteArray))
                    source = System.Drawing.Image.FromStream(ms);
                System.Drawing.Image imageResult = null;

                if (source.Width != width || source.Height != height)
                {
                    // Resize image
                    float sourceRatio = (float)source.Width / source.Height;

                    using (var target = new Bitmap(width, height))
                    {
                        using (var g = System.Drawing.Graphics.FromImage(target))
                        {
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;

                            // Scaling
                            float scaling;
                            float scalingY = (float)source.Height / height;
                            float scalingX = (float)source.Width / width;
                            if (scalingX < scalingY) scaling = scalingX; else scaling = scalingY;

                            int newWidth = (int)(source.Width / scaling);
                            int newHeight = (int)(source.Height / scaling);

                            // Correct float to int rounding
                            if (newWidth < width) newWidth = width;
                            if (newHeight < height) newHeight = height;

                            // See if image needs to be cropped
                            int shiftX = 0;
                            int shiftY = 0;

                            if (newWidth > width)
                            {
                                shiftX = (newWidth - width) / 2;
                            }

                            if (newHeight > height)
                            {
                                shiftY = (newHeight - height) / 2;
                            }

                            // Draw image
                            g.DrawImage(source, -shiftX, -shiftY, newWidth, newHeight);
                        }

                        imageResult = new Bitmap(target);
                    }
                }
                else
                {
                    // Image size matched the given size
                    imageResult = new Bitmap(source);
                }

                imageResult.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception)
            {
                memoryStream = null;
            }

            return memoryStream;
        }

        /// <summary>
        /// Add QRCode in textbox word with param $_qrcode
        /// </summary>
        /// <param name="qrCode"></param>
        /// <param name="textBox"></param>
        private static void AddQRCodeToWord(string qrCode, WTextBox textBox)
        {
            try
            {
                if (!string.IsNullOrEmpty(qrCode) && textBox.TextBoxBody.Paragraphs.Count > 0 &&
                            textBox.TextBoxBody.Paragraphs[0].Text == "$_qrcode")
                {
                    // Clear text "$_qrcode"
                    textBox.TextBoxBody.Paragraphs.RemoveAt(0);

                    //Initialize a new PdfQRBarcode instance
                    Syncfusion.Pdf.Barcode.PdfQRBarcode barcode = new PdfQRBarcode();
                    //Set the XDimension and text for barcode
                    barcode.XDimension = 3;
                    barcode.Text = qrCode;

                    //Convert the barcode to image
                    //Image barcodeImage = barcode.ToImage(new SizeF(70, 70));

                    //Save image
                    //using MemoryStream imageStream = new MemoryStream();
                    //barcodeImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);

                    // Add barcode to file
                    //textBox.TextBoxBody.AddParagraph().AppendPicture(imageStream);

                    // Remove fontsize $_qrcode
                    textBox.TextBoxBody.Paragraphs[0].BreakCharacterFormat.FontSize = 0;

                    // Align barcode to center
                    //textBox.TextBoxBody.Paragraphs[1].ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                    //textBox.TextBoxFormat.LineColor = Syncfusion.Drawing.Color.White;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Add Image in textbox word with param $image
        /// </summary>
        /// <param name="byteImages"></param>
        /// <param name="textBox"></param>
        private static void AddImageToWord(byte[] byteImages, WTextBox textBox)
        {
            try
            {
                // Find image textbox
                if (byteImages != null && textBox.TextBoxBody.Paragraphs.Count > 0 &&
                    textBox.TextBoxBody.Paragraphs[0].Text == "$image")
                {
                    // Clear text "$image"
                    textBox.TextBoxBody.Paragraphs.RemoveAt(0);

                    IWPicture picture = textBox.TextBoxBody.AddParagraph().AppendPicture(byteImages);
                    // Remove fontsize $image
                    textBox.TextBoxBody.Paragraphs[0].BreakCharacterFormat.FontSize = 0;

                    // Align barcode to center
                    textBox.TextBoxBody.Paragraphs[1].ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                    textBox.TextBoxFormat.LineColor = Syncfusion.Drawing.Color.White;

                    //Resizes the picture to fit within text box
                    float scalePercentage;
                    if (picture.Width != textBox.TextBoxFormat.Width)
                    {
                        //Calculates value for width scale factor
                        scalePercentage = textBox.TextBoxFormat.Width / picture.Width * 100;
                        //This will resize the width
                        picture.WidthScale *= scalePercentage / 100;
                    }

                    if (picture.Height != textBox.TextBoxFormat.Height)
                    {
                        //Calculates value for height scale factor
                        scalePercentage = textBox.TextBoxFormat.Height / picture.Height * 100;
                        //This will resize the height
                        picture.HeightScale *= scalePercentage / 100;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void CreateBarCodeImage(string barCode, WTextBox textBox)
        {
            try
            {
                if (!string.IsNullOrEmpty(barCode) && textBox.TextBoxBody.Paragraphs.Count > 0 &&
                    textBox.TextBoxBody.Paragraphs[0].Text == "$_barcode")
                {
                    PdfCode39Barcode barcode = new PdfCode39Barcode();
                    barcode.Text = barCode;
                    barcode.BarHeight = 12;

                    //Disable the barcode text
                    barcode.TextDisplayLocation = TextLocation.None;

                    ////Convert the barcode to image
                    //Image barcodeImage = barcode.ToImage(new SizeF(50, 12));

                    ////Save image
                    //using MemoryStream imageStream = new MemoryStream();
                    //barcodeImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);

                    // Clear text "$barcode"
                    textBox.TextBoxBody.Paragraphs.RemoveAt(0);

                    // Add barcode to file
                    //textBox.TextBoxBody.AddParagraph().AppendPicture(imageStream);

                    // Remove fontsize $_barcode
                    textBox.TextBoxBody.Paragraphs[0].BreakCharacterFormat.FontSize = 0;

                    // Align barcode to center
                    textBox.TextBoxBody.Paragraphs[1].ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                    //textBox.TextBoxFormat.LineColor = Syncfusion.Drawing.Color.White;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private static void RepeatRow(ref IWTable table, int rowIndex, HashSet<System.Enum> fields, List<object> Value, WTableRow cloneRow = null, string prefix = "@")
        {
            if (cloneRow == null)
                cloneRow = table.Rows[rowIndex].Clone();

            foreach (object obj in Value)
            {
                foreach (System.Enum field in fields)
                {
                    WTableCell cell = table.Rows[rowIndex].Cells.OfType<WTableCell>().FirstOrDefault<WTableCell>(m => m.LastParagraph.Text == (prefix + field));
                    if (cell != null)
                    {
                        object objValue = obj.GetType().GetProperty(field.ToString()).GetValue(obj, null);

                        cell.LastParagraph.Text = objValue == null ? "" : objValue.ToString();
                    }
                }
                if (Value.IndexOf(obj) != Value.Count - 1)
                    table.Rows.Insert(rowIndex, cloneRow.Clone());
            }
        }

        private static bool GetBieuMau(byte[] noiDungBieuMau, ref MemoryStream pStream)
        {
            if (noiDungBieuMau != null && noiDungBieuMau.Length > 0)
            {
                pStream = new MemoryStream(noiDungBieuMau);
                return true;
            }
            else
            {
                return false;
            }
        }

        private static List<string> GetFileSetting()
        {
            if (!Store.FileExists(FileSettingPath))
            {
                CreateFileSetting(null);
            }

            StreamReader streamR = new StreamReader(new IsolatedStorageFileStream(FileSettingPath, FileMode.Open, FileAccess.Read, Store));
            List<string> lstr = new List<string>();
            while (!streamR.EndOfStream)
            {
                lstr.Add(streamR.ReadLine());
            }

            streamR.Close();

            if (lstr.Count != 2)
            {
                lstr.Clear();
                CreateFileSetting(null);
                lstr = GetFileSetting();
            }

            return lstr;
        }

        private static void CreateFileSetting(params string[] pListPath)
        {
            IsolatedStorageFileStream file = Store.CreateFile(FileSettingPath);
            StreamWriter stream = new StreamWriter(file);
            if (pListPath == null || pListPath.Count() == 0)
            {
                pListPath = new string[2];
                pListPath[0] = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                pListPath[1] = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            foreach (string str in pListPath)
            {
                stream.WriteLine(str);
            }
            stream.Close();
        }

        [DllImport("user32.dll")]
        private static extern bool AllowSetForegroundWindow(int dwProcessId);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// Inserts the rows in table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document">The document.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="ObjectCollection">The object collection.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        /// <date format = "mm/dd/yyyy">7/31/2013</date>
        public static bool InsertRowsInTable<T>(ref WordDocument document, HashSet<System.Enum> fields, List<T> ObjectCollection, string prefix = "@")
        {
            WTable tableRepeart = null;
            WTableRow cloneRow = null;
            int rowIndex = -1;

            bool nextTable = false;
            foreach (WSection section in document.Sections)
            {
                foreach (WTable table in section.Tables)
                {
                    nextTable = false;
                    foreach (WTableRow row in table.Rows)
                    {
                        foreach (WTableCell cell in row.Cells)
                        {
                            if (fields.Count(m => (prefix + m) == cell.LastParagraph.Text) > 0)
                            {
                                cloneRow = row.Clone();
                                rowIndex = row.GetRowIndex();
                                tableRepeart = table;
                                nextTable = true;
                                break;
                            }
                        }

                        if (nextTable)
                            break;
                    }
                }
            }

            if (tableRepeart != null)
            {
                if (ObjectCollection.Count > 0)
                {
                    foreach (T obj in ObjectCollection)
                    {
                        foreach (System.Enum field in fields)
                        {
                            WTableCell cell =
                                tableRepeart.Rows[rowIndex].Cells.OfType<WTableCell>().FirstOrDefault<WTableCell>(
                                    m => m.LastParagraph.Text == (prefix + field));
                            if (cell != null)
                            {
                                object objValue = obj.GetType().GetProperty(field.ToString()).GetValue(obj, null);
                                //cell.LastParagraph.Text = objValue == null ? "" : objValue.ToString();
                                cell.Paragraphs.RemoveAt(0);
                                Type type = obj.GetType().GetProperty(field.ToString()).PropertyType;
                                IWTextRange range;
                                if (type == typeof(decimal?))
                                {
                                    range = cell.AddParagraph().AppendText(objValue == null ? "" : ((Decimal)objValue).ToString("C0", System.Globalization.CultureInfo.CreateSpecificCulture("vi-VN")));
                                    cell.LastParagraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Right;
                                }
                                else
                                {
                                    range = cell.AddParagraph().AppendText(objValue == null ? "" : objValue.ToString());
                                    if (field.ToString() == "STT")
                                    {
                                        cell.LastParagraph.ParagraphFormat.HorizontalAlignment = Syncfusion.DocIO.DLS.HorizontalAlignment.Center;
                                    }
                                }
                                // IWTextRange range = cell.AddParagraph().AppendText(objValue == null ? "" : objValue.ToString());
                                range.CharacterFormat.FontName = "Times New Roman";
                                //range.CharacterFormat.TextColor = Color.Blue;
                            }
                        }
                        if (ObjectCollection.IndexOf(obj) != ObjectCollection.Count - 1)
                            tableRepeart.Rows.Insert(rowIndex, cloneRow.Clone());
                    }
                }
                else
                {
                    foreach (System.Enum field in fields)
                    {
                        WTableCell cell =
                            tableRepeart.Rows[rowIndex].Cells.OfType<WTableCell>().FirstOrDefault<WTableCell>(
                                m => m.LastParagraph.Text == (prefix + field));
                        if (cell != null)
                        {
                            cell.LastParagraph.Text = "";
                        }
                    }
                }
            }

            return true;
        }

        #endregion Private Methods

        #region Properties

        public static string GetLastSavePath
        {
            get
            {
                return GetFileSetting()[0];
            }
        }

        public static string GetLastOpenpath
        {
            get
            {
                return GetFileSetting()[1];
            }
        }

        public static String FileSettingPath
        {
            get
            {
                if (!Store.DirectoryExists("ExportWord"))
                {
                    Store.CreateDirectory("ExportWord");
                }

                return "ExportWord\\setting.ini";
            }
        }

        public static IsolatedStorageFile Store
        {
            get
            {
                if (_store == null)
                {
                    _store = IsolatedStorageFile.GetStore(IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
                }
                return _store;
            }
        }

        #endregion Properties

        #region KHCN
        /// <summary>
        /// Exports the specified p loai bieu mau.
        /// </summary>
        /// <param name="pMaBieuMau">The p loai bieu mau.</param>
        /// <param name="pField">The p field.</param>
        /// <param name="pShowSaveDialog">if set to <c>true</c> [p show save dialog].</param>
        /// <returns></returns>
        public static WordDocument ExportForKHCN(byte[] noiDungBieuMau, Dictionary<string, string> pField,
            List<WordTemplateTable> wordTemplateTables = null, byte[] byteImages = null, string qrCode = null, List<RequestGroupTableWordReport> requestGroup = null)
        {
            WordDocument synDoc = ExcutiveMailMergeForKHCN(noiDungBieuMau, pField);

            if (wordTemplateTables != null && wordTemplateTables.Count > 0)
            {
                foreach (var item in wordTemplateTables)
                {
                    if (item.DataTable != null && item.DataTable.Count > 0)
                    {
                        RepeatRowInTable(ref synDoc, item);
                    }
                }
            }

            foreach (WTextBox textBox in synDoc.TextBoxes)
            {
                AddImageToWord(byteImages, textBox);
                AddQRCodeToWord(qrCode, textBox);
            }

            return synDoc;
        }
        public static WordDocument ExcutiveMailMergeForKHCN(byte[] noiDungBieuMau, Dictionary<string, string> pField)
        {
            MemoryStream stream = new MemoryStream();

            if (GetBieuMau(noiDungBieuMau, ref stream))
            {
                //Prepare field, value for Document
                Dictionary<string, string> dicFields = pField;

                WordDocument synDoc = new WordDocument();
                synDoc.Open(stream, FormatType.Automatic);

                stream.Close();

                //Set empty when field not found
                synDoc.MailMerge.GetMergeFieldNames().ToList<string>().ForEach(delegate (string fieldName)
                {
                    if (!dicFields.ContainsKey(fieldName))
                    {
                        dicFields[fieldName] = string.Empty;
                    }
                });

                synDoc.MailMerge.MergeField += MailMerge_MergeFieldForKHCN;

                synDoc.MailMerge.Execute(dicFields.Keys.ToArray<string>(), dicFields.Values.ToArray<string>());
                synDoc.UpdateDocumentFields();
                return synDoc;
            }

            return null;
        }

        /// <summary>
        /// Mail merge even handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public static void MailMerge_MergeFieldForKHCN(object sender, MergeFieldEventArgs args)
        {
            if (args.CurrentMergeField.FieldName.StartsWith(MailMegreHTMLPrefix) && args.CurrentMergeField.OwnerParagraph != null)
            {
                try
                {
                    WParagraph paragraph = args.CurrentMergeField.OwnerParagraph;
                    int mergeFieldIndex = paragraph.ChildEntities.IndexOf(args.CurrentMergeField);
                    int mergeFieldParaIndex = paragraph.OwnerTextBody.ChildEntities.IndexOf(paragraph);
                    paragraph.ChildEntities.Remove(args.CurrentMergeField);
                    paragraph.ChildEntities.Clear();
                    paragraph.Document.XHTMLValidateOption = XHTMLValidationType.None;
                    paragraph.OwnerTextBody.InsertXHTML(ConvertHtmlToXHtmlForKHCN(args.Text), mergeFieldParaIndex, mergeFieldIndex);
                    //paragraph.AppendHTML(args.Text);
                }
                catch
                {
                }
            }
        }
        private static string ConvertHtmlToXHtmlForKHCN(string html)
        {
            try
            {
                html = WebUtility.HtmlDecode(html);
                HtmlDocument htmlDocument = new()
                {
                    OptionOutputAsXml = true,
                    OptionAutoCloseOnEnd = true,
                    //OptionFixNestedTags = true,
                    //OptionEmptyCollection = true
                };
                htmlDocument.LoadHtml(html);
                string xhtml = htmlDocument.DocumentNode.OuterHtml;
                return xhtml;
            }
            catch
            {
                return html;
            }
        }
        #endregion
    }
}
