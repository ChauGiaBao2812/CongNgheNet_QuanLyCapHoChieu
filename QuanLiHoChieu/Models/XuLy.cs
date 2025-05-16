using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models
{
    public class XuLy
    {
        [Key]
        public int XuLyID { get; set; }
       
        [Required]
        [StringLength(20)]
        public string FormID { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string UserID { get; set; } = null!;

        [Required]
        public DateTime NgayXuLy { get; set; }

        [Required]
        [StringLength(50)]
        public string TrangThai { get; set; } = null!;

        [StringLength(255)]
        public string? GhiChu { get; set; }

        [Required]
        [StringLength(50)]
        public string LoaiXuLy { get; set; } = null!;

        // Navigation properties

        [ForeignKey(nameof(FormID))]
        public PassportData PassportData { get; set; } = null!;

        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;
    }
}
