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
                    // Insert resident 1
                    context.Database.ExecuteSqlRaw(
                        "EXEC sp_InsertResidentData " +
                        "@CCCD = {0}, @HoTen = {1}, @GioiTinh = {2}, @NgaySinh = {3}, @NoiSinh = {4}, @NgayCap = {5}, @NoiCap = {6}, " +
                        "@DanToc = {7}, @TonGiao = {8}, @SDT = {9}, " +
                        "@ttTinhThanh = {10}, @ttQuanHuyen = {11}, @ttPhuongXa = {12}, @ttSoNhaDuong = {13}, " +
                        "@thtTinhThanh = {14}, @thtQuanHuyen = {15}, @thtPhuongXa = {16}, @thtSoNhaDuong = {17}, " +
                        "@HoTenCha = {18}, @NgaySinhCha = {19}, @HoTenMe = {20}, @NgaySinhMe = {21}",
                        "001234567890",
                        "Nguyễn Văn A",
                        "Nam",
                        new DateTime(1990, 1, 15),
                        "Hà Nội",
                        new DateTime(2010, 6, 1),
                        "Cục QLHC về TTXH",
                        "Kinh",
                        "Không",
                        "0901234567",
                        "Hà Nội",
                        "Đống Đa",
                        "Phương Mai",
                        "123 Lê Duẩn",
                        "TP HCM",
                        "Quận 1",
                        "Bến Nghé",
                        "456 Nguyễn Huệ",
                        "Nguyễn Văn B",
                        new DateTime(1960, 4, 10),
                        "Trần Thị C",
                        new DateTime(1965, 8, 20)
                    );

                    // Insert resident 2
                    context.Database.ExecuteSqlRaw(
                        "EXEC sp_InsertResidentData " +
                        "@CCCD = {0}, @HoTen = {1}, @GioiTinh = {2}, @NgaySinh = {3}, @NoiSinh = {4}, @NgayCap = {5}, @NoiCap = {6}, " +
                        "@DanToc = {7}, @TonGiao = {8}, @SDT = {9}, " +
                        "@ttTinhThanh = {10}, @ttQuanHuyen = {11}, @ttPhuongXa = {12}, @ttSoNhaDuong = {13}, " +
                        "@thtTinhThanh = {14}, @thtQuanHuyen = {15}, @thtPhuongXa = {16}, @thtSoNhaDuong = {17}, " +
                        "@HoTenCha = {18}, @NgaySinhCha = {19}, @HoTenMe = {20}, @NgaySinhMe = {21}",
                        "009876543210",
                        "Lê Thị B",
                        "Nữ",
                        new DateTime(1992, 5, 20),
                        "Đà Nẵng",
                        new DateTime(2011, 4, 20),
                        "Cục QLHC về TTXH",
                        "Kinh",
                        "Phật giáo",
                        "0912345678",
                        "Đà Nẵng",
                        "Hải Châu",
                        "Thạch Thang",
                        "789 Trần Phú",
                        "Hà Nội",
                        "Ba Đình",
                        "Ngọc Hà",
                        "321 Kim Mã",
                        "Lê Văn D",
                        new DateTime(1962, 3, 5),
                        "Ngô Thị E",
                        new DateTime(1967, 10, 12)
                    );

                    // Bản ghi 3 (có nhiều trường null)
                    var parameters3 = new[]
                    {
                        new SqlParameter("@CCCD", "007654321098"),
                        new SqlParameter("@HoTen", "Phan Văn C"),
                        new SqlParameter("@GioiTinh", "Nam"),
                        new SqlParameter("@NgaySinh", new DateTime(1985, 12, 30)),
                        new SqlParameter("@NoiSinh", "Hải Phòng"),
                        new SqlParameter("@NgayCap", new DateTime(2005, 8, 10)),
                        new SqlParameter("@NoiCap", "Phòng Công An Hải Phòng"),
                        new SqlParameter("@DanToc", "Kinh"),
                        new SqlParameter("@TonGiao", DBNull.Value), // null
                        new SqlParameter("@SDT", "0987654321"),
                        new SqlParameter("@ttTinhThanh", "Hải Phòng"),
                        new SqlParameter("@ttQuanHuyen", "Ngô Quyền"),
                        new SqlParameter("@ttPhuongXa", "Cầu Đất"),
                        new SqlParameter("@ttSoNhaDuong", "45 Trần Phú"),
                        new SqlParameter("@thtTinhThanh", "Hải Phòng"),
                        new SqlParameter("@thtQuanHuyen", "Lê Chân"),
                        new SqlParameter("@thtPhuongXa", "Nghĩa Xá"),
                        new SqlParameter("@thtSoNhaDuong", "22 Lạch Tray"),
                        new SqlParameter("@HoTenCha", DBNull.Value),  // null
                        new SqlParameter("@NgaySinhCha", DBNull.Value),  // null
                        new SqlParameter("@HoTenMe", DBNull.Value),  // null
                        new SqlParameter("@NgaySinhMe", DBNull.Value)  // null
                    };
                    context.Database.ExecuteSqlRaw(
                        "EXEC sp_InsertResidentData " +
                        "@CCCD, @HoTen, @GioiTinh, @NgaySinh, @NoiSinh, @NgayCap, @NoiCap, @DanToc, @TonGiao, @SDT, " +
                        "@ttTinhThanh, @ttQuanHuyen, @ttPhuongXa, @ttSoNhaDuong, @thtTinhThanh, @thtQuanHuyen, @thtPhuongXa, @thtSoNhaDuong, " +
                        "@HoTenCha, @NgaySinhCha, @HoTenMe, @NgaySinhMe",
                        parameters3);

                    // Bản ghi 4 (có TonGiao null, có thông tin cha mẹ)
                    var parameters4 = new[]
                    {
                        new SqlParameter("@CCCD", "005432109876"),
                        new SqlParameter("@HoTen", "Hoàng Thị D"),
                        new SqlParameter("@GioiTinh", "Nữ"),
                        new SqlParameter("@NgaySinh", new DateTime(1995, 7, 10)),
                        new SqlParameter("@NoiSinh", "Cần Thơ"),
                        new SqlParameter("@NgayCap", new DateTime(2013, 2, 1)),
                        new SqlParameter("@NoiCap", "Phòng Công An Cần Thơ"),
                        new SqlParameter("@DanToc", "Kinh"),
                        new SqlParameter("@TonGiao", DBNull.Value), // null
                        new SqlParameter("@SDT", "0932109876"),
                        new SqlParameter("@ttTinhThanh", "Cần Thơ"),
                        new SqlParameter("@ttQuanHuyen", "Ninh Kiều"),
                        new SqlParameter("@ttPhuongXa", "An Phú"),
                        new SqlParameter("@ttSoNhaDuong", "77 Lê Lợi"),
                        new SqlParameter("@thtTinhThanh", "Cần Thơ"),
                        new SqlParameter("@thtQuanHuyen", "Cái Răng"),
                        new SqlParameter("@thtPhuongXa", "Hưng Phú"),
                        new SqlParameter("@thtSoNhaDuong", "99 Võ Văn Kiệt"),
                        new SqlParameter("@HoTenCha", "Hoàng Văn E"),
                        new SqlParameter("@NgaySinhCha", new DateTime(1970, 1, 15)),
                        new SqlParameter("@HoTenMe", "Trần Thị F"),
                        new SqlParameter("@NgaySinhMe", new DateTime(1972, 3, 20))
                    };

                    context.Database.ExecuteSqlRaw(
                        "EXEC sp_InsertResidentData " +
                        "@CCCD, @HoTen, @GioiTinh, @NgaySinh, @NoiSinh, @NgayCap, @NoiCap, @DanToc, @TonGiao, @SDT, " +
                        "@ttTinhThanh, @ttQuanHuyen, @ttPhuongXa, @ttSoNhaDuong, @thtTinhThanh, @thtQuanHuyen, @thtPhuongXa, @thtSoNhaDuong, " +
                        "@HoTenCha, @NgaySinhCha, @HoTenMe, @NgaySinhMe",
                        parameters4);


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