using BaseCommon.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.HRM.DOMAIN.DomainObjects.Media
{
    public class AttachmentFile
    {
        #region Fields

        private int _id;
        private string _name;
        private EAttachmentFileType _type;
        private double? _size;
        private string _path;
        private bool? _forWeb;
        private string _checkSum;
        private DateTime? _createDate;
        private int? _createBy;
        private DateTime? _updateDate;
        private int? _updateBy;

        #endregion Fields

        #region Constructors

        protected AttachmentFile()
        {
        }

        public AttachmentFile(string name, EAttachmentFileType type, string path, long size)
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

        public EAttachmentFileType Type { get => _type; }
        public double? Size { get => _size; }

        //[Required(ErrorMessage = nameof(EAttachmentFileErrorCode.AFC03))]
        public string Path { get => _path; }

        public bool? ForWeb { get => _forWeb; }
        public string CheckSum { get => _checkSum; }

   

        #endregion Behaviours
    }
}
