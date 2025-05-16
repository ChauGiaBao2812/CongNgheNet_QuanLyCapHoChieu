using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models
{
    public class User
    {
        [Required]
        [Key]
        [StringLength(20)]
        public string UserID { get; set; } = null!;

        // AES encrypted fields stored as VARBINARY
        [Required]
        public byte[] HoTen { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string GioiTinh { get; set; } = null!;

        [Required]
        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(100)]
        public string QueQuan { get; set; } = null!;

        [Required]
        public byte[] SĐT { get; set; } = null!;

        [Required]
        public byte[] Email { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string ChucVu { get; set; } = null!;

        // Foreign key to TaiKhoan.Username
        [Required]
        [StringLength(20)]
        public string Username { get; set; } = null!;

        [ForeignKey(nameof(Username))]
        public TaiKhoan TaiKhoan { get; set; } = null!;
    }
}
