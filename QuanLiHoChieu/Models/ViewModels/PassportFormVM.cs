using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models.ViewModels
{
    public class PassportFormVM
    {
        public string? FormID { get; set; }

        [Required]
        [RegularExpression(@"^0\d{11}$", ErrorMessage = "CCCD không hợp lệ.")]
        public string CCCD { get; set; } = null!;

        [Required]
        public string HoTen { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string GioiTinh { get; set; } = null!;

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public string NoiSinh { get; set; } = null!;

        [Required]
        public DateTime NgayCap { get; set; }

        [Required]
        public string NoiCap { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string DanToc { get; set; } = null!;

        [StringLength(50)]
        public string? TonGiao { get; set; }

        [Required]
        public string SDT { get; set; } = null!;

        [Required]
        public string Email {  get; set; } = null!;

        public string? Hinh { get; set; }

        // Địa chỉ thường trú
        [Required]
        public string ttTinhThanh { get; set; } = null!;

        [Required]
        public string ttQuanHuyen { get; set; } = null!;

        [Required]
        public string ttPhuongXa { get; set; } = null!;

        [Required]
        public string ttSoNhaDuong { get; set; } = null!;

        // Địa chỉ tạm trú
        public string? thtTinhThanh { get; set; } = null!;

        public string? thtQuanHuyen { get; set; } = null!;

        public string? thtPhuongXa { get; set; } = null!;

        public string? thtSoNhaDuong { get; set; } = null!;

        // Optional fields
        public string? HoTenCha { get; set; }
        public DateTime? NgaySinhCha { get; set; }
        public string? HoTenMe { get; set; }
        public DateTime? NgaySinhMe { get; set; }
    }

}
