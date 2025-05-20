using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using QuanLiHoChieu.Models;
using System;
using QuanLiHoChieu.Helpers;

namespace QuanLiHoChieu.Data
{
    public static class SeedData
    {
        public static void SeedDatabase(PassportDbContext context)
        {
            context.Database.Migrate();

            if (!context.ResidentDatas.Any()) 
            {
                try
                {
                    byte[] Encrypt(string? value) => value == null ? null! : AesEcbEncryption.EncryptAesEcb(value);

                    var resident = new ResidentData
                    {
                        CCCD = Encrypt("005432109876"),
                        HoTen = Encrypt("Hoàng Thị D"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1995, 7, 10),
                        NoiSinh = Encrypt("Cần Thơ"),
                        NgayCap = new DateTime(2013, 2, 1),
                        NoiCap = Encrypt("Phòng Công An Cần Thơ"),
                        DanToc = "Kinh",
                        TonGiao = null,
                        SĐT = Encrypt("0932109876"),
                        ttTinhThanh = Encrypt("Cần Thơ"),
                        ttQuanHuyen = Encrypt("Ninh Kiều"),
                        ttPhuongXa = Encrypt("An Phú"),
                        ttSoNhaDuong = Encrypt("77 Lê Lợi"),
                        thtTinhThanh = Encrypt("Cần Thơ"),
                        thtQuanHuyen = Encrypt("Cái Răng"),
                        thtPhuongXa = Encrypt("Hưng Phú"),
                        thtSoNhaDuong = Encrypt("99 Võ Văn Kiệt"),
                        HoTenCha = Encrypt("Hoàng Văn E"),
                        NgaySinhCha = new DateTime(1970, 1, 15),
                        HoTenMe = Encrypt("Trần Thị F"),
                        NgaySinhMe = new DateTime(1972, 3, 20)
                    };

                    context.ResidentDatas.Add(resident);
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi seed dữ liệu bằng stored procedure: {ex.Message}");
                }
            }

            if (!context.TaiKhoans.Any() && !context.Users.Any())
            {
                try
                {
                    var users = new[]
                    {
                    new
                    {
                        HoTen = "Nguyễn Văn Giám",
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1980, 1, 15),
                        QueQuan = "Hà Nội",
                        SDT = "0912345678",
                        Email = "giam@example.com",
                        ChucVu = "GiamSat",
                        Username = "giam01"
                    },
                    new
                    {
                        HoTen = "Trần Thị Lưu",
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1985, 5, 20),
                        QueQuan = "Đà Nẵng",
                        SDT = "0923456789",
                        Email = "luu@example.com",
                        ChucVu = "LuuTru",
                        Username = "luu01"
                    },
                    new
                    {
                        HoTen = "Phạm Văn Duyệt",
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1990, 3, 25),
                        QueQuan = "Hải Phòng",
                        SDT = "0934567890",
                        Email = "duyet@example.com",
                        ChucVu = "XetDuyet",
                        Username = "duyet01"
                    },
                    new
                    {
                        HoTen = "Lê Thị Thực",
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1992, 7, 10),
                        QueQuan = "Cần Thơ",
                        SDT = "0945678901",
                        Email = "thuc@example.com",
                        ChucVu = "XacThuc",
                        Username = "thuc01"
                    }
                };

                    string defaultPassword = "123456";
                    var hashedPassword = TypeConvertHelper.ComputeSHA256(defaultPassword); // hoặc lấy sẵn byte[] hashed

                    foreach (var user in users)
                    {
                        var taiKhoan = new TaiKhoan
                        {
                            Username = user.Username,
                            MatKhau = hashedPassword
                        };

                        context.TaiKhoans.Add(taiKhoan);
                        context.SaveChanges();

                        string userId = Guid.NewGuid().ToString("N")[..20];

                        context.Database.ExecuteSqlRaw(
                            "EXEC sp_InsertUser @UserID, @HoTen, @GioiTinh, @NgaySinh, @QueQuan, @SDT, @Email, @ChucVu, @Username",
                            new SqlParameter("@UserID", userId),
                            new SqlParameter("@HoTen", user.HoTen),
                            new SqlParameter("@GioiTinh", user.GioiTinh),
                            new SqlParameter("@NgaySinh", user.NgaySinh),
                            new SqlParameter("@QueQuan", user.QueQuan),
                            new SqlParameter("@SDT", user.SDT),
                            new SqlParameter("@Email", user.Email),
                            new SqlParameter("@ChucVu", user.ChucVu),
                            new SqlParameter("@Username", user.Username)
                        );

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi seed dữ liệu bằng stored procedure: {ex.Message}");
                }
            }
        }
    }
}