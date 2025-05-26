using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models.ViewModels
{
    public class DecryptedUserVM
    {
        public string? UserID { get; set; }
        public string? HoTen { get; set; }
        public string? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string? QueQuan { get; set; }

        [Phone]
        public string? SDT { get; set; }
        public string? Email { get; set; }
        public string? ChucVu { get; set; }
        public string? Username { get; set; }

        public string ChucVuDisplay => ChucVu switch
        {
            "GiamSat" => "Giám sát",
            "XetDuyet" => "Xét duyệt",
            "XacThuc" => "Xác thực",
            "LuuTru" => "Lưu trữ",
            _ => "Không rõ"
        };
    }   
}
