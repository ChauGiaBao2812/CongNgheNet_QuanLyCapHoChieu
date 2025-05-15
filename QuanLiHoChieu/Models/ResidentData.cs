using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models
{
    public class ResidentData
    {
        [Key]
        [Required]
        public byte[] CCCD { get; set; } = null!;  // AES encrypted primary key

        [Required]
        public byte[] HoTen { get; set; } = null!;  // AES encrypted

        [Required]
        [StringLength(10)]
        public string GioiTinh { get; set; } = null!;

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        public byte[] NoiSinh { get; set; } = null!;  // AES encrypted

        [Required]
        public DateTime NgayCap { get; set; }

        [Required]
        public byte[] NoiCap { get; set; } = null!;  // AES encrypted

        [Required]
        [StringLength(50)]
        public string DanToc { get; set; } = null!;

        [StringLength(50)]
        public string? TonGiao { get; set; }

        [Required]
        public byte[] SĐT { get; set; } = null!;  // AES encrypted

        // Địa chỉ thường trú (tt)
        [Required]
        public byte[] ttTinhThanh { get; set; } = null!;

        [Required]
        public byte[] ttQuanHuyen { get; set; } = null!;

        [Required]
        public byte[] ttPhuongXa { get; set; } = null!;

        [Required]
        public byte[] ttSoNhaDuong { get; set; } = null!;

        // Địa chỉ tạm trú (tht)
        [Required]
        public byte[] thtTinhThanh { get; set; } = null!;

        [Required]
        public byte[] thtQuanHuyen { get; set; } = null!;

        [Required]
        public byte[] thtPhuongXa { get; set; } = null!;

        [Required]
        public byte[] thtSoNhaDuong { get; set; } = null!;

        // Optional fields for parents info
        public byte[]? HoTenCha { get; set; }
        public DateTime? NgaySinhCha { get; set; }
        public byte[]? HoTenMe { get; set; }
        public DateTime? NgaySinhMe { get; set; }
    }
}
