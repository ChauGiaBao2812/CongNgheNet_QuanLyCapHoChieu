namespace QuanLiHoChieu.Models.ViewModels
{
    public class PassportResidentVM
    {
        public string FormID { get; set; } = null!;
        public string CCCD { get; set; } = null!;

        // PassportData
        public string HoTenPD { get; set; } = null!;
        public string GioiTinhPD { get; set; } = null!;
        public DateTime NgaySinhPD { get; set; }
        public string NoiSinhPD { get; set; } = null!;
        public DateTime NgayCapPD { get; set; }
        public string NoiCapPD { get; set; } = null!;
        public string DanTocPD { get; set; } = null!;
        public string? TonGiaoPD { get; set; }
        public string SDTPD { get; set; } = null!;
        public string ttDiaChiPD { get; set; } = null!;
        public string thtDiaChiPD { get; set; } = null!;
        public string? HoTenChaPD { get; set; }
        public DateTime? NgaySinhChaPD { get; set; }
        public string? HoTenMePD { get; set; }
        public DateTime? NgaySinhMePD { get; set; }

        // ResidentData
        public string HoTenRD { get; set; } = null!;
        public string GioiTinhRD { get; set; } = null!;
        public DateTime NgaySinhRD { get; set; }
        public string NoiSinhRD { get; set; } = null!;
        public DateTime NgayCapRD { get; set; }
        public string NoiCapRD { get; set; } = null!;
        public string DanTocRD { get; set; } = null!;
        public string? TonGiaoRD { get; set; }
        public string SDTRD { get; set; } = null!;
        public string ttDiaChiRD { get; set; } = null!;
        public string thtDiaChiRD { get; set; } = null!;
        public string? HoTenChaRD { get; set; }
        public DateTime? NgaySinhChaRD { get; set; }
        public string? HoTenMeRD { get; set; }
        public DateTime? NgaySinhMeRD { get; set; }

        // Validated?
        public bool isValidated {  get; set; }
    }
}
