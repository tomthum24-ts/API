using BaseCommon.Enums;
using System.ComponentModel.DataAnnotations;

namespace API.DOMAIN.DomainObjects.BieuMau
{
    public class SysBieuMau : APIEntity
    {
        #region Fields

        private string _maBieuMau;
        private string _tenBieuMau;
        private string _tenFile;
        private byte[] _noiDung;
        private string _loaiFile;
        private string _ghiChu;
        private string _checkSum;
        private string _groupName;
        private bool? _isExportPDF;

        #endregion Fields

        #region Constructors

        protected SysBieuMau()
        {
        }

        #endregion Constructors

        #region Properties

        [MaxLength(50, ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC02))]
        [Required(ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC03))]
        public string MaBieuMau { get => _maBieuMau; }

        [MaxLength(200, ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC04))]
        [Required(ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC09))]
        public string TenBieuMau { get => _tenBieuMau; }

        [MaxLength(200, ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC05))]
        public string TenFile { get => _tenFile; }

        public byte[] NoiDung { get => _noiDung; }

        [MaxLength(50, ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC06))]
        public string LoaiFile { get => _loaiFile; }

        [MaxLength(4000, ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC07))]
        public string GhiChu { get => _ghiChu; }

        [MaxLength(50, ErrorMessage = nameof(ESysBieuMauErrorCode.SBMC08))]
        public string CheckSum { get => _checkSum; }

        public string GroupName { get => _groupName; }
        public bool? IsExportPDF { get => _isExportPDF; }

        #endregion Properties

        #region Behaviours

        public void SetMaBieuMau(string maBieuMau)
        {
            _maBieuMau = maBieuMau;
        }

        public void SetTenBieuMau(string tenBieuMau)
        {
            _tenBieuMau = tenBieuMau;
        }

        public void SetTenFile(string tenFile)
        {
            _tenFile = tenFile;
        }

        public void SetNoiDung(byte[] noiDung) => _noiDung = noiDung;

        public void SetLoaiFile(string loaiFile)
        {
            _loaiFile = loaiFile;
        }

        public void SetGhiChu(string ghiChu)
        {
            _ghiChu = ghiChu;
        }

        public void SetCheckSum(string checkSum)
        {
            _checkSum = checkSum;
        }

        public void SetGroupName(string groupName) => _groupName = groupName;

        public void SetIsExportPDF(bool? isExportPDF) => _isExportPDF = isExportPDF;

        #endregion Behaviours
    }
}