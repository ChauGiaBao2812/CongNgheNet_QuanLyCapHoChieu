using QuanLiHoChieu.Data;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Services.Interface;
using Microsoft.EntityFrameworkCore;
using QuanLiHoChieu.Models.ViewModels;
using Microsoft.SqlServer.Server;
using QuanLiHoChieu.Models;

namespace QuanLiHoChieu.Services
{
    public class GetDataByFormIdService : IGetDataByFormIdService
    {
        private readonly PassportDbContext _context;

        public GetDataByFormIdService(PassportDbContext context)
        {
            _context = context;
        }

        public async Task<PassportResidentVM?> GetPassportResidentVMByFormIdAsync(string formId)
        {
            var data = await _context.PassportDatas
                .Include(p => p.ResidentData)
                .FirstOrDefaultAsync(p => p.FormID == formId);

            if (data == null || data.ResidentData == null)
                return null;

            var rd = data.ResidentData;

            var xuLy = await _context.XuLys
                .Where(x => x.FormID == formId && x.LoaiXuLy == "XacThuc")
                .OrderByDescending(x => x.NgayXuLy)
                .FirstOrDefaultAsync();

            return new PassportResidentVM
            {
                FormID = data.FormID,
                CCCD = AesEcbEncryption.DecryptAesEcb(data.CCCD) ?? "",

                // PassportData
                HoTenPD = AesEcbEncryption.DecryptAesEcb(data.HoTen) ?? "",
                GioiTinhPD = data.GioiTinh,
                NgaySinhPD = data.NgaySinh,
                NoiSinhPD = AesEcbEncryption.DecryptAesEcb(data.NoiSinh) ?? "",
                NgayCapPD = data.NgayCap,
                NoiCapPD = AesEcbEncryption.DecryptAesEcb(data.NoiCap) ?? "",
                DanTocPD = data.DanToc,
                TonGiaoPD = data.TonGiao,
                SDTPD = AesEcbEncryption.DecryptAesEcb(data.SĐT) ?? "",
                ttDiaChiPD = string.Join(", ",
                    AesEcbEncryption.DecryptAesEcb(data.ttSoNhaDuong),
                    AesEcbEncryption.DecryptAesEcb(data.ttPhuongXa),
                    AesEcbEncryption.DecryptAesEcb(data.ttQuanHuyen),
                    AesEcbEncryption.DecryptAesEcb(data.ttTinhThanh)),
                thtDiaChiPD = string.Join(", ",
                    new[]
                    {
                        AesEcbEncryption.DecryptAesEcb(data.thtSoNhaDuong),
                        AesEcbEncryption.DecryptAesEcb(data.thtPhuongXa),
                        AesEcbEncryption.DecryptAesEcb(data.thtQuanHuyen),
                        AesEcbEncryption.DecryptAesEcb(data.thtTinhThanh)
                    }.Where(x => !string.IsNullOrWhiteSpace(x))
                ),
                HoTenChaPD = data.HoTenCha != null ? AesEcbEncryption.DecryptAesEcb(data.HoTenCha) : null,
                NgaySinhChaPD = data.NgaySinhCha,
                HoTenMePD = data.HoTenMe != null ? AesEcbEncryption.DecryptAesEcb(data.HoTenMe) : null,
                NgaySinhMePD = data.NgaySinhMe,

                // ResidentData
                HoTenRD = AesEcbEncryption.DecryptAesEcb(rd.HoTen) ?? "",
                GioiTinhRD = rd.GioiTinh,
                NgaySinhRD = rd.NgaySinh,
                NoiSinhRD = AesEcbEncryption.DecryptAesEcb(rd.NoiSinh) ?? "",
                NgayCapRD = rd.NgayCap,
                NoiCapRD = AesEcbEncryption.DecryptAesEcb(rd.NoiCap) ?? "",
                DanTocRD = rd.DanToc,
                TonGiaoRD = rd.TonGiao,
                SDTRD = AesEcbEncryption.DecryptAesEcb(rd.SĐT) ?? "",
                ttDiaChiRD = string.Join(", ",
                    AesEcbEncryption.DecryptAesEcb(rd.ttSoNhaDuong),
                    AesEcbEncryption.DecryptAesEcb(rd.ttPhuongXa),
                    AesEcbEncryption.DecryptAesEcb(rd.ttQuanHuyen),
                    AesEcbEncryption.DecryptAesEcb(rd.ttTinhThanh)),
                thtDiaChiRD = string.Join(", ",
                    new[]
                    {
                        AesEcbEncryption.DecryptAesEcb(rd.thtSoNhaDuong),
                        AesEcbEncryption.DecryptAesEcb(rd.thtPhuongXa),
                        AesEcbEncryption.DecryptAesEcb(rd.thtQuanHuyen),
                        AesEcbEncryption.DecryptAesEcb(rd.thtTinhThanh)
                    }.Where(x => !string.IsNullOrWhiteSpace(x))
                ),
                HoTenChaRD = rd.HoTenCha != null ? AesEcbEncryption.DecryptAesEcb(rd.HoTenCha) : null,
                NgaySinhChaRD = rd.NgaySinhCha,
                HoTenMeRD = rd.HoTenMe != null ? AesEcbEncryption.DecryptAesEcb(rd.HoTenMe) : null,
                NgaySinhMeRD = rd.NgaySinhMe,

                isValidated = xuLy != null && (xuLy.TrangThai == "Verified" || xuLy.TrangThai == "Rejected")
            };
        }

        public async Task<PassportFormReviewVM?> GetPassportVMByFormIdAsync(string formId)
        {
            var form = await _context.PassportDatas.FirstOrDefaultAsync(p => p.FormID == formId);

            var xuLy = await _context.XuLys
                .Where(x => x.FormID == formId && x.LoaiXuLy == "XetDuyet")
                .OrderByDescending(x => x.NgayXuLy)
                .FirstOrDefaultAsync();

            return new PassportFormReviewVM
            {
                FormID = form?.FormID,
                CCCD = AesEcbEncryption.DecryptAesEcb(form!.CCCD) ?? "",
                HoTen = AesEcbEncryption.DecryptAesEcb(form.HoTen) ?? "",
                GioiTinh = form.GioiTinh,
                NgaySinh = form.NgaySinh,
                NoiSinh = AesEcbEncryption.DecryptAesEcb(form.NoiSinh) ?? "",
                NgayCap = form.NgayCap,
                NoiCap = AesEcbEncryption.DecryptAesEcb(form.NoiCap) ?? "",
                DanToc = form.DanToc,
                TonGiao = form.TonGiao,
                SDT = AesEcbEncryption.DecryptAesEcb(form.SĐT) ?? "",
                Email = AesEcbEncryption.DecryptAesEcb(form.Email) ?? "",
                Hinh = form.Hinh,

                PermanentAddress = string.Join(", ",
                    AesEcbEncryption.DecryptAesEcb(form.ttSoNhaDuong),
                    AesEcbEncryption.DecryptAesEcb(form.ttPhuongXa),
                    AesEcbEncryption.DecryptAesEcb(form.ttQuanHuyen),
                    AesEcbEncryption.DecryptAesEcb(form.ttTinhThanh)),

                TemporaryAddress = string.Join(", ",
                    new[]
                    {
                        AesEcbEncryption.DecryptAesEcb(form.thtSoNhaDuong),
                        AesEcbEncryption.DecryptAesEcb(form.thtPhuongXa),
                        AesEcbEncryption.DecryptAesEcb(form.thtQuanHuyen),
                        AesEcbEncryption.DecryptAesEcb(form.thtTinhThanh)
                    }.Where(x => !string.IsNullOrWhiteSpace(x))
                ),

                HoTenCha = form.HoTenCha != null ? AesEcbEncryption.DecryptAesEcb(form.HoTenCha) : null,
                NgaySinhCha = form.NgaySinhCha,
                HoTenMe = form.HoTenMe != null ? AesEcbEncryption.DecryptAesEcb(form.HoTenMe) : null,
                NgaySinhMe = form.NgaySinhMe,

                NoiDungDeNghi = form.NoiDungDeNghi,
                NoiTiepNhanHS = form.NoiTiepNhanHS,
                NgayNop = form.NgayNop,

                isValidated = xuLy != null && (xuLy.TrangThai == "Verified" || xuLy.TrangThai == "Rejected")
            };
        }
    }
}
