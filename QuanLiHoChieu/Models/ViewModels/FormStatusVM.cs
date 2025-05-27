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

        public string? UserXacThucID {  get; set; }
        public string? UserXetDuyetID {  get; set; }
        public string? UserLuuTruID {  get; set; }
        public string? UserXacThucName {  get; set; }
        public string? UserXetDuyetName {  get; set; }
        public string? UserLuuTruName { get; set; }
        public string? NoteXacThuc {  get; set; }
        public string? NoteXetDuyet {  get; set; }
        

    }
}
