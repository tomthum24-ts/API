using BaseCommon.Utilities;
using System.IO;

namespace API.APPLICATION.ViewModels.BieuMau
{
    public class BieuMauInfoResponseViewModel
    {
        public MemoryStream OutputStream { get; set; }
        private string _tenBieuMau;

        public string TenBieuMau
        {
            get => _tenBieuMau;
            set
            {
                _tenBieuMau = StringHelpers.RemoveSign4VietnameseString(value);
            }
        }

        public string ContentType { get; set; }
    }
}