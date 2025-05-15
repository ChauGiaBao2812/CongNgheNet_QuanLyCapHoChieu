using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models
{
    [Index(nameof(FormID), IsUnique = true)]
    public class LuuTru
    {
        [Key]
        [StringLength(20)]
        public string PassportID { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string UserID { get; set; } = null!;

        [Required]
        [StringLength(20)]
        public string FormID { get; set; } = null!;

        [Required]
        public DateTime NgayNop { get; set; }

        [Required]
        public DateTime NgayCap { get; set; }

        [Required]
        public DateTime CoGiaTriDen { get; set; }

        [ForeignKey(nameof(FormID))]
        public PassportData PassportData { get; set; } = null!;

        [ForeignKey(nameof(UserID))]
        public User User { get; set; } = null!;
    }
}
