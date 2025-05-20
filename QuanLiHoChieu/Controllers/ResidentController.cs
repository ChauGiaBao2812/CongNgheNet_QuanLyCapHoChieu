using Microsoft.AspNetCore.Mvc;
using System.Text;

using QuanLiHoChieu.Data;   // Your DbContext namespace
using QuanLiHoChieu.Models; // Your ResidentData model namespace

[ApiController]
[Route("api/[controller]")]
public class ResidentController : ControllerBase
{
    private readonly PassportDbContext _context;
    private readonly string _aesKey = "your-32-char-key-1234567890abcde";

    public ResidentController(PassportDbContext context)
    {
        _context = context;
    }

    // AES decrypt method
    private string DecryptFromBytes(byte[] ciphertext)
    {
        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_aesKey);
            aes.Mode = System.Security.Cryptography.CipherMode.ECB;
            aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            var decryptor = aes.CreateDecryptor();
            var decryptedBytes = decryptor.TransformFinalBlock(ciphertext, 0, ciphertext.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }

    // GET api/resident/{cccd}
    [HttpGet("{cccd}")]
    public IActionResult GetResident(string cccd)
    {
        // Encrypt the incoming cccd to match the stored encrypted key
        byte[] encryptedCCCD;
        try
        {
            encryptedCCCD = EncryptToBytes(cccd);
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
            HoTen = DecryptFromBytes(resident.HoTen),
            GioiTinh = resident.GioiTinh,
            NgaySinh = resident.NgaySinh,
            NoiSinh = DecryptFromBytes(resident.NoiSinh),
            NgayCap = resident.NgayCap,
            NoiCap = DecryptFromBytes(resident.NoiCap),
            DanToc = resident.DanToc,
            TonGiao = resident.TonGiao,
            SDT = DecryptFromBytes(resident.SĐT),

            // Address thường trú
            ttTinhThanh = DecryptFromBytes(resident.ttTinhThanh),
            ttQuanHuyen = DecryptFromBytes(resident.ttQuanHuyen),
            ttPhuongXa = DecryptFromBytes(resident.ttPhuongXa),
            ttSoNhaDuong = DecryptFromBytes(resident.ttSoNhaDuong),

            // Address tạm trú
            thtTinhThanh = DecryptFromBytes(resident.thtTinhThanh),
            thtQuanHuyen = DecryptFromBytes(resident.thtQuanHuyen),
            thtPhuongXa = DecryptFromBytes(resident.thtPhuongXa),
            thtSoNhaDuong = DecryptFromBytes(resident.thtSoNhaDuong),

            // Parents info (nullable)
            HoTenCha = resident.HoTenCha != null ? DecryptFromBytes(resident.HoTenCha) : null,
            NgaySinhCha = resident.NgaySinhCha,
            HoTenMe = resident.HoTenMe != null ? DecryptFromBytes(resident.HoTenMe) : null,
            NgaySinhMe = resident.NgaySinhMe
        };

        return Ok(result);
    }

    // Helper: Encrypt string to bytes (same as used when inserting)
    private byte[] EncryptToBytes(string plaintext)
    {
        using (var aes = System.Security.Cryptography.Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_aesKey);
            aes.Mode = System.Security.Cryptography.CipherMode.ECB;
            aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            var encryptor = aes.CreateEncryptor();
            byte[] inputBytes = Encoding.UTF8.GetBytes(plaintext);
            return encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        }
    }
}