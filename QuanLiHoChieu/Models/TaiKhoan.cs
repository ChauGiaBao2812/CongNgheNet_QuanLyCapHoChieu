using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models
{
    public class TaiKhoan
    {
        [Key]
        [StringLength(20)]
        public string Username { get; set; } = null!;

        // Hashed password stored as VARBINARY(64)
        [Required]
        public byte[] MatKhau { get; set; } = null!;

        // Navigation property for related User (1:1 or 1:0..1)
        public User? User { get; set; }
    }
}
