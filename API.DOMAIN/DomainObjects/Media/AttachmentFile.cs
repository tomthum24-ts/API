using BaseCommon.Common.Enum;

namespace API.DOMAIN
{
    public class AttachmentFile : APIEntity
    {
        #region Fields

        private int _id;
        private string _name;
        private string _type;
        private double? _size;
        private string _path;
        private bool? _forWeb;
        private string _checkSum;

        #endregion Fields

        #region Constructors

        protected AttachmentFile()
        {
        }

        public AttachmentFile(string name, string type, string path, long size)
        {
            _name = name;
            _type = type;
            _path = path;
            _size = size;
        }

        #endregion Constructors

        #region Behaviours

        public int Id { get => _id; }

        //[Required(ErrorMessage = nameof(EAttachmentFileErrorCode.AFC01))]
        //[MaxLength(200, ErrorMessage = nameof(EAttachmentFileErrorCode.AFC02))]
        public string Name { get => _name; }

        public string Type { get => _type; }
        public double? Size { get => _size; }

        //[Required(ErrorMessage = nameof(EAttachmentFileErrorCode.AFC03))]
        public string Path { get => _path; }

        public bool? ForWeb { get => _forWeb; }
        public string CheckSum { get => _checkSum; }

        #endregion Behaviours
    }
}