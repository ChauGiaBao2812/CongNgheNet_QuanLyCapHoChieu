using Microsoft.AspNetCore.Mvc;
using System.Text;
using QuanLiHoChieu.Helpers;
using QuanLiHoChieu.Data;   // Your DbContext namespace
using QuanLiHoChieu.Models; // Your ResidentData model namespace

[ApiController]
[Route("api/[controller]")]
public class ResidentController : ControllerBase
{
    private readonly PassportDbContext _context;

    public ResidentController(PassportDbContext context)
    {
        _context = context;
    }

    // GET api/resident/{cccd}
    [HttpGet("{cccd}")]
    public IActionResult GetResident(string cccd)
    {
        // Encrypt the incoming cccd to match the stored encrypted key
        byte[] encryptedCCCD;
        try
        {
            encryptedCCCD = AesEcbEncryption.EncryptAesEcb(cccd);
        }
        catch
        {
            return BadRequest("Invalid CCCD format.");
        }

        var resident = _context.ResidentDatas.Find(encryptedCCCD);
        if (resident == null)
        {
            return NotFound("Resident not found.");
        }

        // Decrypt fields before returning
        var result = new
        {
            CCCD = cccd,
            HoTen = AesEcbEncryption.DecryptAesEcb(resident.HoTen),
            GioiTinh = resident.GioiTinh,
            NgaySinh = resident.NgaySinh,
            NoiSinh = AesEcbEncryption.DecryptAesEcb(resident.NoiSinh),
            NgayCap = resident.NgayCap,
            NoiCap = AesEcbEncryption.DecryptAesEcb(resident.NoiCap),
            DanToc = resident.DanToc,
            TonGiao = resident.TonGiao,
            SDT = AesEcbEncryption.DecryptAesEcb(resident.SĐT),

            // Address thường trú
            ttTinhThanh = AesEcbEncryption.DecryptAesEcb(resident.ttTinhThanh),
            ttQuanHuyen = AesEcbEncryption.DecryptAesEcb(resident.ttQuanHuyen),
            ttPhuongXa = AesEcbEncryption.DecryptAesEcb(resident.ttPhuongXa),
            ttSoNhaDuong = AesEcbEncryption.DecryptAesEcb(resident.ttSoNhaDuong),

            // Address tạm trú
            thtTinhThanh = AesEcbEncryption.DecryptAesEcb(resident.thtTinhThanh),
            thtQuanHuyen = AesEcbEncryption.DecryptAesEcb(resident.thtQuanHuyen),
            thtPhuongXa = AesEcbEncryption.DecryptAesEcb(resident.thtPhuongXa),
            thtSoNhaDuong = AesEcbEncryption.DecryptAesEcb(resident.thtSoNhaDuong),

            // Parents info (nullable)
            HoTenCha = resident.HoTenCha != null ? AesEcbEncryption.DecryptAesEcb(resident.HoTenCha) : null,
            NgaySinhCha = resident.NgaySinhCha,
            HoTenMe = resident.HoTenMe != null ? AesEcbEncryption.DecryptAesEcb(resident.HoTenMe) : null,
            NgaySinhMe = resident.NgaySinhMe
        };

        return Ok(result);
    }
}