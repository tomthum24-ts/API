namespace BaseCommon.Common.AttachFile
{
    public class MediaOptions
    {
        public string MediaUploadUrl { get; set; }
        public string MediaUrl { get; set; }
        public string FolderForWeb { get; set; }
        public string[] PermittedExtensions { get; set; }
        public long SizeLimit { get; set; }
    }
}