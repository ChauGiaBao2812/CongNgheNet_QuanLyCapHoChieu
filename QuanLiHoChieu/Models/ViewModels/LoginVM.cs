using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Password { get; set; } = null!;  // plain text password from view
    }
}
