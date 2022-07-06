using BaseCommon.Common.EnCrypt;


namespace API.APPLICATION.ViewModels.Media
{
    public class MediaResponse
    {
        public MediaResponse(string name, string path, long size)
        {
            Name = name;
            Path = path;
            Size = size;
            GuidId = CommonBase.EncryptStr(path);
        }
        public int FileId { get; set; }
        public string Name { get; }
        public string Path { get; }
        public long Size { get; }
        public string GuidId { get; set; }
    }
}
