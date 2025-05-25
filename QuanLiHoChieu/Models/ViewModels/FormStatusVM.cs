using Microsoft.AspNetCore.Mvc;

namespace QuanLiHoChieu.Models.ViewModels
{
    public class FormStatusVM
    {
        public string FormID { get; set; } = string.Empty;
        public bool? XacThucStatus { get; set; }
        public bool? XetDuyetStatus { get; set; }
        public bool? LuuTruStatus { get; set; }
        public DateTime NgayNop { get; set; }
    }
}
