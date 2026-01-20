using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.APPLICATION.ViewModels.Jobs
{
    public class JobsResponseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // Nội dung
        public int? Category { get; set; } // Mã danh mục loại việc
        public string CategoryName { get; set; } //Tên danh mục loại việc
        public DateTime? TimeStart { get; set; } // Ngày bắt đầu làm
        public int? TimeZone { get; set; } // Cả ngày = 1 / Chiều =2 / Sáng = 3
        public int? JobNumber { get; set; } // Số người làm
        public int? JobNumberRemain { get; set; } // Số người còn lại
        public bool? IsHome { get; set; } //Có chỗ ở lại
        public bool? IsEating { get; set; } // Bao ăn
        public int? Province { get; set; } // Tỉnh
        public int? District { get; set; } // Xã
        public string Address { get; set; }// Địa chỉ
        public int? TypeJob { get; set; } // Hình thức tính công
        public decimal? PriceFrom { get; set; } //Mức công từ
        public decimal? PriceTo { get; set; } //Mức công đến
        public int? TypePayment { get; set; } // Kiểu thanh toán 
        public int? TimePayment { get; set; } // Thời điểm trả
        public string Detail { get; set; } // Mô tả công việc chi tiết 
        public string Require { get; set; } // Yêu cầu kinh nghiệm/sức khỏe
        public int? Cancel { get; set; } //Quy định hủy kèo
        public int? Tools { get; set; } //Dụng cụ/đồ bảo hộ : 1. Chủ vườn chuẩn bị 2. Người làm chuẩn bị 3. Khác
    }
}
