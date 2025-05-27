using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models
{
    public class PassportData
    {
        [Key]
        [StringLength(20)]
        public string FormID { get; set; } = null!;

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
        public byte[] CCCD { get; set; } = null!;  // AES encrypted; FK to ResidentData.CCCD

        [Required]
        public DateTime NgayCap { get; set; }

        [Required]
        public byte[] NoiCap { get; set; } = null!;  // AES encrypted

        [Required]
        [StringLength(50)]
        public string DanToc { get; set; } = null!;

        [StringLength(50)]
        public string? TonGiao { get; set; }  // nullable

        [Required]
        public byte[] SĐT { get; set; } = null!;  // AES encrypted

        [Required]
        public byte[] Email {  get; set; } = null!;

        public string? Hinh { get; set; }

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
        public byte[]? thtTinhThanh { get; set; } = null!;

        public byte[]? thtQuanHuyen { get; set; } = null!;

        public byte[]? thtPhuongXa { get; set; } = null!;

        public byte[]? thtSoNhaDuong { get; set; } = null!;

        // Optional fields
        public byte[]? NgheNghiep { get; set; }
        public byte[]? CoQuan { get; set; }
        public byte[]? DiaChiCoQuan { get; set; }
        public byte[]? HoTenCha { get; set; }
        public DateTime? NgaySinhCha { get; set; }
        public byte[]? HoTenMe { get; set; }
        public DateTime? NgaySinhMe { get; set; }

        [Required]
        public string NoiDungDeNghi { get; set; } = null!;  // NVARCHAR(MAX)

        [Required]
        [StringLength(100)]
        public string NoiTiepNhanHS { get; set; } = null!;

        [Required]
        public DateTime NgayNop { get; set; }

        // Navigation property to ResidentData (optional, if you have ResidentData entity)
        [ForeignKey(nameof(CCCD))]
        public ResidentData? ResidentData { get; set; }
    }

}
