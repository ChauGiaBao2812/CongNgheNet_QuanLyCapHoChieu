using System.ComponentModel.DataAnnotations;

namespace QuanLiHoChieu.Models.ViewModels
{
    public class PassportFormReviewVM
    {
        public string? FormID { get; set; }

        public string CCCD { get; set; } = null!;

        public string HoTen { get; set; } = null!;

        public string GioiTinh { get; set; } = null!;

        public DateTime NgaySinh { get; set; }

        public string NoiSinh { get; set; } = null!;

        public DateTime NgayCap { get; set; }

        public string NoiCap { get; set; } = null!;

        public string DanToc { get; set; } = null!;

        public string? TonGiao { get; set; }

        public string SDT { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Hinh { get; set; }

        // Combined Permanent Address
        public string PermanentAddress { get; set; } = null!;

        // Combined Temporary Address
        public string TemporaryAddress { get; set; } = null!;

        // Optional fields
        public string? HoTenCha { get; set; }
        public DateTime? NgaySinhCha { get; set; }
        public string? HoTenMe { get; set; }
        public DateTime? NgaySinhMe { get; set; }

        // Acquirement
        public string? NoiDungDeNghi { get; set; }
        public string? NoiTiepNhanHS { get; set; }
        public DateTime? NgayNop { get; set; }

        public bool isValidated { get; set; }
    }

}
