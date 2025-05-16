using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace QuanLiHoChieu.Models.ViewModels
{
    public class TaiKhoanUserViewModel
    {
        // TaiKhoan fields
        [Required]
        [StringLength(20)]
        public string Username { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [StringLength(255)]
        public string Password { get; set; } = null!;  // plain text password from view

        // User fields
        [StringLength(20)]
        public string? UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string GioiTinh { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [Required]
        [StringLength(100)]
        public string QueQuan { get; set; } = null!;

        [Required]
        [StringLength(15)]
        [Phone]
        public string SDT { get; set; } = null!;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string ChucVu { get; set; } = null!;
    }
}
