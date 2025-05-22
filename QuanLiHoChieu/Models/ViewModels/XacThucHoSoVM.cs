using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models.ViewModels
{
    public class XacThucHoSoVM
    {
        [Required]
        public string FormID { get; set; } = null!;
        public string? GhiChu { get; set; }
        public string? TrangThai { get; set; }
    }
}
