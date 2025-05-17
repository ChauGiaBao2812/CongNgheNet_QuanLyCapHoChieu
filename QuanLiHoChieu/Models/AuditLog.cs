using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models
{
    public class AuditLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LogID { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Username { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Action { get; set; }

        [Required]
        [MaxLength(100)]
        public string? TableName { get; set; }

        public DateTime TimeStamp { get; set; } = DateTime.Now;
    }
}
