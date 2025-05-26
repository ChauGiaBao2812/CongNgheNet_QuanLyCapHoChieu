using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using QuanLiHoChieu.Models;
using System;
using QuanLiHoChieu.Helpers;
using System.Reflection.Emit;

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

                    var resident1 = new ResidentData
                    {
                        CCCD = Encrypt("005432109876"),
                        HoTen = Encrypt("Hoàng Thị D"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1995, 7, 10),
                        NoiSinh = Encrypt("Cần Thơ"),
                        NgayCap = new DateTime(2013, 2, 1),
                        NoiCap = Encrypt("Phòng Công An Cần Thơ"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
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

                    var resident2 = new ResidentData
                    {
                        CCCD = Encrypt("012345678901"),
                        HoTen = Encrypt("Nguyễn Văn A"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1988, 3, 15),
                        NoiSinh = Encrypt("Hà Nội"),
                        NgayCap = new DateTime(2010, 6, 20),
                        NoiCap = Encrypt("Phòng Công An Hà Nội"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0987654321"),
                        ttTinhThanh = Encrypt("Hà Nội"),
                        ttQuanHuyen = Encrypt("Ba Đình"),
                        ttPhuongXa = Encrypt("Phúc Xá"),
                        ttSoNhaDuong = Encrypt("12 Hoàng Diệu"),
                        thtTinhThanh = Encrypt("Hà Nội"),
                        thtQuanHuyen = Encrypt("Cầu Giấy"),
                        thtPhuongXa = Encrypt("Dịch Vọng"),
                        thtSoNhaDuong = Encrypt("45 Xuân Thủy"),
                        HoTenCha = Encrypt("Nguyễn Văn B"),
                        NgaySinhCha = new DateTime(1960, 5, 10),
                        HoTenMe = Encrypt("Trần Thị C"),
                        NgaySinhMe = new DateTime(1963, 9, 30)
                    };

                    var resident3 = new ResidentData
                    {
                        CCCD = Encrypt("098765432109"),
                        HoTen = Encrypt("Trần Thị B"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1992, 11, 5),
                        NoiSinh = Encrypt("Đà Nẵng"),
                        NgayCap = new DateTime(2014, 4, 15),
                        NoiCap = Encrypt("Phòng Công An Đà Nẵng"),
                        DanToc = "Kinh",
                        TonGiao = "Phật Giáo",
                        SĐT = Encrypt("0912345678"),
                        ttTinhThanh = Encrypt("Đà Nẵng"),
                        ttQuanHuyen = Encrypt("Hải Châu"),
                        ttPhuongXa = Encrypt("Thạch Thang"),
                        ttSoNhaDuong = Encrypt("78 Lê Lợi"),
                        thtTinhThanh = Encrypt("Đà Nẵng"),
                        thtQuanHuyen = Encrypt("Thanh Khê"),
                        thtPhuongXa = Encrypt("Hòa Khê"),
                        thtSoNhaDuong = Encrypt("99 Nguyễn Lương Bằng"),
                        HoTenCha = Encrypt("Trần Văn C"),
                        NgaySinhCha = new DateTime(1965, 7, 20),
                        HoTenMe = Encrypt("Lê Thị D"),
                        NgaySinhMe = new DateTime(1967, 12, 10)
                    };

                    var resident4 = new ResidentData
                    {
                        CCCD = Encrypt("014785236901"),
                        HoTen = Encrypt("Lê Văn C"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1985, 9, 25),
                        NoiSinh = Encrypt("Hồ Chí Minh"),
                        NgayCap = new DateTime(2012, 3, 30),
                        NoiCap = Encrypt("Phòng Công An TP. Hồ Chí Minh"),
                        DanToc = "Kinh",
                        TonGiao = "Thiên Chúa Giáo",
                        SĐT = Encrypt("0909876543"),
                        ttTinhThanh = Encrypt("Hồ Chí Minh"),
                        ttQuanHuyen = Encrypt("Quận 1"),
                        ttPhuongXa = Encrypt("Bến Nghé"),
                        ttSoNhaDuong = Encrypt("123 Lê Thánh Tôn"),
                        thtTinhThanh = Encrypt("Hồ Chí Minh"),
                        thtQuanHuyen = Encrypt("Quận 3"),
                        thtPhuongXa = Encrypt("Phường 6"),
                        thtSoNhaDuong = Encrypt("456 Nguyễn Đình Chiểu"),
                        HoTenCha = Encrypt("Lê Văn D"),
                        NgaySinhCha = new DateTime(1958, 10, 5),
                        HoTenMe = Encrypt("Phạm Thị E"),
                        NgaySinhMe = new DateTime(1960, 2, 18)
                    };


                    context.ResidentDatas.Add(resident1);
                    context.ResidentDatas.Add(resident2);
                    context.ResidentDatas.Add(resident3);
                    context.ResidentDatas.Add(resident4);

                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi seed dữ liệu bằng stored procedure: {ex.Message}");
                }
            }

            if (!context.PassportDatas.Any())
            {
                try
                {
                    byte[] Encrypt(string? value) => value == null ? null! : AesEcbEncryption.EncryptAesEcb(value);

                    string FormId = Guid.NewGuid().ToString("N").Substring(0, 20);

                    var passport1 = new PassportData
                    {
                        FormID = FormId,
                        HoTen = Encrypt("Hoàng Thị D"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1995, 7, 10),
                        NoiSinh = Encrypt("Cần Thơ"),
                        CCCD = Encrypt("005432109876"), // FK to ResidentData
                        NgayCap = new DateTime(2024, 1, 10),
                        NoiCap = Encrypt("Cục Quản Lý Xuất Nhập Cảnh"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0932109876"),
                        Email = Encrypt("hoangthid@example.com"),
                        Hinh = "seed-data.jpg",
                        ttTinhThanh = Encrypt("Cần Thơ"),
                        ttQuanHuyen = Encrypt("Ninh Kiều"),
                        ttPhuongXa = Encrypt("An Phú"),
                        ttSoNhaDuong = Encrypt("77 Lê Lợi"),
                        thtTinhThanh = Encrypt("Cần Thơ"),
                        thtQuanHuyen = Encrypt("Cái Răng"),
                        thtPhuongXa = Encrypt("Hưng Phú"),
                        thtSoNhaDuong = Encrypt("99 Võ Văn Kiệt"),
                        NgheNghiep = Encrypt("Nhân viên văn phòng"),
                        CoQuan = Encrypt("Công ty TNHH ABC"),
                        DiaChiCoQuan = Encrypt("123 Trần Hưng Đạo, Cần Thơ"),
                        HoTenCha = Encrypt("Hoàng Văn E"),
                        NgaySinhCha = new DateTime(1970, 1, 15),
                        HoTenMe = Encrypt("Trần Thị F"),
                        NgaySinhMe = new DateTime(1972, 3, 20),
                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông cho mục đích du lịch.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an TP. Cần Thơ",
                        NgayNop = DateTime.Now
                    };

                    var passport2 = new PassportData
                    {
                        FormID = Guid.NewGuid().ToString("N").Substring(0, 20),
                        HoTen = Encrypt("Nguyễn Văn A"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1988, 3, 15),
                        NoiSinh = Encrypt("Hà Nội"),
                        CCCD = Encrypt("012345678901"),
                        NgayCap = new DateTime(2019, 6, 20),
                        NoiCap = Encrypt("Cục Quản Lý Xuất Nhập Cảnh"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0932109877"),
                        Email = Encrypt("nguyenvana@quaiquoai.com"),
                        Hinh = "seed_data.jpg",
                        ttTinhThanh = Encrypt("Hà Nội"),
                        ttQuanHuyen = Encrypt("Ba Đình"),
                        ttPhuongXa = Encrypt("Phúc Xá"),
                        ttSoNhaDuong = Encrypt("12 Hoàng Diệu"),
                        thtTinhThanh = Encrypt("Hà Nội"),
                        thtQuanHuyen = Encrypt("Cầu Giấy"),
                        thtPhuongXa = Encrypt("Dịch Vọng"),
                        thtSoNhaDuong = Encrypt("45 Xuân Thủy"),
                        NgheNghiep = Encrypt("Kỹ sư phần mềm"),
                        CoQuan = Encrypt("Công ty Công nghệ XYZ"),
                        DiaChiCoQuan = Encrypt("456 Nguyễn Trãi, Hà Nội"),
                        HoTenCha = Encrypt("Nguyễn Văn B"),
                        NgaySinhCha = new DateTime(1960, 5, 10),
                        HoTenMe = Encrypt("Trần Thị C"),
                        NgaySinhMe = new DateTime(1963, 9, 30),
                        NoiDungDeNghi = "Cấp hộ chiếu đi công tác nước ngoài.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Hà Nội",
                        NgayNop = DateTime.Now
                    };

                    var passport3 = new PassportData
                    {
                        FormID = Guid.NewGuid().ToString("N").Substring(0, 20),
                        HoTen = Encrypt("Trần Thị Bất Ổn"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(2045, 10, 31),
                        NoiSinh = Encrypt("Thành phố Ảo"),
                        CCCD = Encrypt("098765432109"),
                        NgayCap = new DateTime(2040, 1, 1), 
                        NoiCap = Encrypt("Cục Xuất Nhập Cảnh Ảo"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0000000000"),
                        Email = Encrypt("baton.tran@khongtontai.com"),
                        Hinh = "seed-data.jpg",
                        ttTinhThanh = Encrypt("Đà Nẵng"),
                        ttQuanHuyen = Encrypt("Hải Châu"),
                        ttPhuongXa = Encrypt("Thạch Thang"),
                        ttSoNhaDuong = Encrypt("999 Hư Cấu"),
                        thtTinhThanh = Encrypt("Đà Nẵng"),
                        thtQuanHuyen = Encrypt("Thanh Khê"),
                        thtPhuongXa = Encrypt("Hòa Khê"),
                        thtSoNhaDuong = Encrypt("888 Ảo Mộng"),
                        NgheNghiep = Encrypt("Thợ săn UFO"),
                        CoQuan = Encrypt("Hiệp hội Ngoài Hành Tinh"),
                        DiaChiCoQuan = Encrypt("Vũ trụ bao la"),
                        HoTenCha = Encrypt("Trần Văn Bị Lạc"),
                        NgaySinhCha = new DateTime(1950, 1, 1),
                        HoTenMe = Encrypt("Lê Thị Mất Tích"),
                        NgaySinhMe = new DateTime(1955, 2, 2),
                        NoiDungDeNghi = "Cấp hộ chiếu đi tìm người ngoài hành tinh.",
                        NoiTiepNhanHS = "Phòng Quản lý Xuất nhập cảnh Vũ trụ",
                        NgayNop = DateTime.Now
                    };

                    var passport4 = new PassportData
                    {
                        FormID = Guid.NewGuid().ToString("N").Substring(0, 20),
                        HoTen = Encrypt("Lê Văn C"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1940, 8, 9), // Older than parents, just for fun
                        NoiSinh = Encrypt("Hồ Chí Minh"),
                        CCCD = Encrypt("014785236901"),
                        NgayCap = new DateTime(2023, 4, 30),
                        NoiCap = Encrypt("Phòng Công An TP. Hồ Chí Minh"),
                        DanToc = "Kinh",
                        TonGiao = "Thiên Chúa Giáo",
                        SĐT = Encrypt("0909876543"),
                        Email = Encrypt("levanc@oldschool.vn"),
                        Hinh = "seed-data.jpg",
                        ttTinhThanh = Encrypt("Hồ Chí Minh"),
                        ttQuanHuyen = Encrypt("Quận 1"),
                        ttPhuongXa = Encrypt("Bến Nghé"),
                        ttSoNhaDuong = Encrypt("123 Lê Thánh Tôn"),
                        thtTinhThanh = Encrypt("Hồ Chí Minh"),
                        thtQuanHuyen = Encrypt("Quận 3"),
                        thtPhuongXa = Encrypt("Phường 6"),
                        thtSoNhaDuong = Encrypt("456 Nguyễn Đình Chiểu"),
                        NgheNghiep = Encrypt("Cựu chiến binh"),
                        CoQuan = Encrypt("Hội Cựu Chiến Binh"),
                        DiaChiCoQuan = Encrypt("123 Đường Ký Ức, Quận 1"),
                        HoTenCha = Encrypt("Lê Văn D"),
                        NgaySinhCha = new DateTime(1920, 5, 5),
                        HoTenMe = Encrypt("Phạm Thị E"),
                        NgaySinhMe = new DateTime(1925, 6, 6),
                        NoiDungDeNghi = "Cấp hộ chiếu đặc biệt cho người cao tuổi.",
                        NoiTiepNhanHS = "Phòng Quản lý Xuất nhập cảnh TP. HCM",
                        NgayNop = DateTime.Now
                    };



                    var passportList = new List<PassportData> { passport1, passport2, passport3, passport4 };

                    foreach (var passport in passportList)
                    {
                        try
                        {
                            context.PassportDatas.Add(passport);
                            context.SaveChanges();
                            Console.WriteLine($"Saved passport: {AesEcbEncryption.DecryptAesEcb(passport.CCCD)}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Failed to save passport: {AesEcbEncryption.DecryptAesEcb(passport.CCCD)} - Reason: {ex.Message}");
                            context.ChangeTracker.Clear(); // Clear the failed tracked state
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi seed PassportData: {ex.Message}");
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