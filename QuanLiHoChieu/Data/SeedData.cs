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
                        CCCD = Encrypt("036123456789"), // 036: mã tỉnh Cần Thơ theo CCCD thật
                        HoTen = Encrypt("Hoàng Thị Duyên"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1995, 7, 10),
                        NoiSinh = Encrypt("Phường An Phú, Quận Ninh Kiều, Cần Thơ"),
                        NgayCap = new DateTime(2013, 2, 1),
                        NoiCap = Encrypt("Công an thành phố Cần Thơ"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0932109876"),

                        // Thường trú
                        ttTinhThanh = Encrypt("Cần Thơ"),
                        ttQuanHuyen = Encrypt("Ninh Kiều"),
                        ttPhuongXa = Encrypt("An Phú"),
                        ttSoNhaDuong = Encrypt("77 Lê Lợi, KDC Hưng Phú"),

                        // Tạm trú
                        thtTinhThanh = Encrypt("Cần Thơ"),
                        thtQuanHuyen = Encrypt("Cái Răng"),
                        thtPhuongXa = Encrypt("Hưng Phú"),
                        thtSoNhaDuong = Encrypt("99 Võ Văn Kiệt, Khu dân cư Nam Long"),

                        // Cha mẹ
                        HoTenCha = Encrypt("Hoàng Văn Dũng"),
                        NgaySinhCha = new DateTime(1970, 1, 15),
                        HoTenMe = Encrypt("Trần Thị Hoa"),
                        NgaySinhMe = new DateTime(1973, 3, 20)
                    };

                    var resident2 = new ResidentData
                    {
                        CCCD = Encrypt("001198803015"), // 001: mã CCCD Hà Nội
                        HoTen = Encrypt("Nguyễn Văn Anh"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1988, 3, 15),
                        NoiSinh = Encrypt("Phường Phúc Xá, Quận Ba Đình, Hà Nội"),
                        NgayCap = new DateTime(2010, 6, 20),
                        NoiCap = Encrypt("Công an thành phố Hà Nội"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0987654321"),
                        ttTinhThanh = Encrypt("Hà Nội"),
                        ttQuanHuyen = Encrypt("Ba Đình"),
                        ttPhuongXa = Encrypt("Phúc Xá"),
                        ttSoNhaDuong = Encrypt("12 Hoàng Diệu, Khu A"),
                        thtTinhThanh = Encrypt("Hà Nội"),
                        thtQuanHuyen = Encrypt("Cầu Giấy"),
                        thtPhuongXa = Encrypt("Dịch Vọng"),
                        thtSoNhaDuong = Encrypt("45 Xuân Thủy, Tòa nhà CT2"),
                        HoTenCha = Encrypt("Nguyễn Văn Bằng"),
                        NgaySinhCha = new DateTime(1960, 5, 10),
                        HoTenMe = Encrypt("Trần Thị Cúc"),
                        NgaySinhMe = new DateTime(1963, 9, 30)
                    };


                    var resident3 = new ResidentData
                    {
                        CCCD = Encrypt("102199211005"), // 102: mã CCCD Đà Nẵng
                        HoTen = Encrypt("Trần Thị Bích"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1992, 11, 5),
                        NoiSinh = Encrypt("Phường Thạch Thang, Quận Hải Châu, Đà Nẵng"),
                        NgayCap = new DateTime(2014, 4, 15),
                        NoiCap = Encrypt("Công an thành phố Đà Nẵng"),
                        DanToc = "Kinh",
                        TonGiao = "Phật Giáo",
                        SĐT = Encrypt("0912345678"),
                        ttTinhThanh = Encrypt("Đà Nẵng"),
                        ttQuanHuyen = Encrypt("Hải Châu"),
                        ttPhuongXa = Encrypt("Thạch Thang"),
                        ttSoNhaDuong = Encrypt("78 Lê Lợi, gần chợ Đống Đa"),
                        thtTinhThanh = Encrypt("Đà Nẵng"),
                        thtQuanHuyen = Encrypt("Thanh Khê"),
                        thtPhuongXa = Encrypt("Hòa Khê"),
                        thtSoNhaDuong = Encrypt("99 Nguyễn Lương Bằng, KDC Phước Lý"),
                        HoTenCha = Encrypt("Trần Văn Cường"),
                        NgaySinhCha = new DateTime(1965, 7, 20),
                        HoTenMe = Encrypt("Lê Thị Diễm"),
                        NgaySinhMe = new DateTime(1967, 12, 10)
                    };


                    var resident4 = new ResidentData
                    {
                        CCCD = Encrypt("079198509025"), // 079: mã CCCD TP.HCM
                        HoTen = Encrypt("Lê Văn Chương"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1985, 9, 25),
                        NoiSinh = Encrypt("Phường Bến Nghé, Quận 1, TP.HCM"),
                        NgayCap = new DateTime(2012, 3, 30),
                        NoiCap = Encrypt("Công an TP. Hồ Chí Minh"),
                        DanToc = "Kinh",
                        TonGiao = "Thiên Chúa Giáo",
                        SĐT = Encrypt("0909876543"),
                        ttTinhThanh = Encrypt("Hồ Chí Minh"),
                        ttQuanHuyen = Encrypt("Quận 1"),
                        ttPhuongXa = Encrypt("Bến Nghé"),
                        ttSoNhaDuong = Encrypt("123 Lê Thánh Tôn, gần Vincom Center"),
                        thtTinhThanh = Encrypt("Hồ Chí Minh"),
                        thtQuanHuyen = Encrypt("Quận 3"),
                        thtPhuongXa = Encrypt("Phường 6"),
                        thtSoNhaDuong = Encrypt("456 Nguyễn Đình Chiểu, chung cư Hòa Bình"),
                        HoTenCha = Encrypt("Lê Văn Dũng"),
                        NgaySinhCha = new DateTime(1958, 10, 5),
                        HoTenMe = Encrypt("Phạm Thị Em"),
                        NgaySinhMe = new DateTime(1960, 2, 18)
                    };

                    var resident5 = new ResidentData
                    {
                        CCCD = Encrypt("031199504322"),
                        HoTen = Encrypt("Phạm Thị Hằng"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1995, 4, 22),
                        NoiSinh = Encrypt("Quận Lê Chân, Hải Phòng"),
                        NgayCap = new DateTime(2013, 5, 1),
                        NoiCap = Encrypt("Công an thành phố Hải Phòng"),
                        DanToc = "Hoa",
                        TonGiao = "Không",
                        SĐT = Encrypt("0911223344"),
                        ttTinhThanh = Encrypt("Hải Phòng"),
                        ttQuanHuyen = Encrypt("Lê Chân"),
                        ttPhuongXa = Encrypt("An Dương"),
                        ttSoNhaDuong = Encrypt("23 Trần Nguyên Hãn"),
                        thtTinhThanh = Encrypt("Hải Phòng"),
                        thtQuanHuyen = Encrypt("Ngô Quyền"),
                        thtPhuongXa = Encrypt("Máy Tơ"),
                        thtSoNhaDuong = Encrypt("56 Lạch Tray"),
                        HoTenCha = Encrypt("Phạm Văn Tân"),
                        NgaySinhCha = new DateTime(1969, 3, 12),
                        HoTenMe = Encrypt("Nguyễn Thị Hoa"),
                        NgaySinhMe = new DateTime(1972, 7, 9)
                    };

                    var resident6 = new ResidentData
                    {
                        CCCD = Encrypt("089199901115"),
                        HoTen = Encrypt("Ngô Văn Bình"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1999, 1, 11),
                        NoiSinh = Encrypt("Huyện Châu Phú, An Giang"),
                        NgayCap = new DateTime(2017, 9, 30),
                        NoiCap = Encrypt("Công an tỉnh An Giang"),
                        DanToc = "Khơ Me",
                        TonGiao = "Hòa Hảo",
                        SĐT = Encrypt("0978123456"),
                        ttTinhThanh = Encrypt("An Giang"),
                        ttQuanHuyen = Encrypt("Châu Phú"),
                        ttPhuongXa = Encrypt("Khánh Hòa"),
                        ttSoNhaDuong = Encrypt("15 Nguyễn Văn Cừ"),
                        thtTinhThanh = Encrypt("An Giang"),
                        thtQuanHuyen = Encrypt("Long Xuyên"),
                        thtPhuongXa = Encrypt("Mỹ Bình"),
                        thtSoNhaDuong = Encrypt("88 Trần Hưng Đạo"),
                        HoTenCha = Encrypt("Ngô Văn Hiếu"),
                        NgaySinhCha = new DateTime(1972, 5, 18),
                        HoTenMe = Encrypt("Lê Thị Tuyết"),
                        NgaySinhMe = new DateTime(1974, 8, 30)
                    };

                    var resident7 = new ResidentData
                    {
                        CCCD = Encrypt("040199406050"),
                        HoTen = Encrypt("Trần Văn Huy"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1994, 6, 5),
                        NoiSinh = Encrypt("Huyện Nghi Lộc, Nghệ An"),
                        NgayCap = new DateTime(2013, 11, 10),
                        NoiCap = Encrypt("Công an tỉnh Nghệ An"),
                        DanToc = "Thái",
                        TonGiao = "Không",
                        SĐT = Encrypt("0966123456"),
                        ttTinhThanh = Encrypt("Nghệ An"),
                        ttQuanHuyen = Encrypt("Nghi Lộc"),
                        ttPhuongXa = Encrypt("Nghi Thái"),
                        ttSoNhaDuong = Encrypt("21 Đặng Thai Mai"),
                        thtTinhThanh = Encrypt("Nghệ An"),
                        thtQuanHuyen = Encrypt("Vinh"),
                        thtPhuongXa = Encrypt("Lê Lợi"),
                        thtSoNhaDuong = Encrypt("120 Nguyễn Văn Cừ"),
                        HoTenCha = Encrypt("Trần Văn Lưu"),
                        NgaySinhCha = new DateTime(1965, 10, 9),
                        HoTenMe = Encrypt("Nguyễn Thị Tâm"),
                        NgaySinhMe = new DateTime(1967, 2, 20)
                    };

                    var resident8 = new ResidentData
                    {
                        CCCD = Encrypt("056199308170"),
                        HoTen = Encrypt("Đinh Thị Hòa"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1993, 8, 17),
                        NoiSinh = Encrypt("TP. Nha Trang, Khánh Hòa"),
                        NgayCap = new DateTime(2011, 7, 2),
                        NoiCap = Encrypt("Công an tỉnh Khánh Hòa"),
                        DanToc = "Chăm",
                        TonGiao = "Không",
                        SĐT = Encrypt("0901345789"),
                        ttTinhThanh = Encrypt("Khánh Hòa"),
                        ttQuanHuyen = Encrypt("Nha Trang"),
                        ttPhuongXa = Encrypt("Vĩnh Hải"),
                        ttSoNhaDuong = Encrypt("42 Hùng Vương"),
                        thtTinhThanh = Encrypt("Khánh Hòa"),
                        thtQuanHuyen = Encrypt("Cam Ranh"),
                        thtPhuongXa = Encrypt("Cam Lộc"),
                        thtSoNhaDuong = Encrypt("33 Nguyễn Trãi"),
                        HoTenCha = Encrypt("Đinh Văn Lợi"),
                        NgaySinhCha = new DateTime(1962, 6, 11),
                        HoTenMe = Encrypt("Lê Thị Lan"),
                        NgaySinhMe = new DateTime(1964, 1, 27)
                    };

                    var resident9 = new ResidentData
                    {
                        CCCD = Encrypt("074199810302"),
                        HoTen = Encrypt("Võ Hoàng Phúc"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1998, 10, 30),
                        NoiSinh = Encrypt("Thị xã Bến Cát, Bình Dương"),
                        NgayCap = new DateTime(2016, 8, 18),
                        NoiCap = Encrypt("Công an tỉnh Bình Dương"),
                        DanToc = "Mường",
                        TonGiao = "Không",
                        SĐT = Encrypt("0988654321"),
                        ttTinhThanh = Encrypt("Bình Dương"),
                        ttQuanHuyen = Encrypt("Bến Cát"),
                        ttPhuongXa = Encrypt("Phú An"),
                        ttSoNhaDuong = Encrypt("81 Lê Văn Tám"),
                        thtTinhThanh = Encrypt("Bình Dương"),
                        thtQuanHuyen = Encrypt("Thuận An"),
                        thtPhuongXa = Encrypt("Bình Chuẩn"),
                        thtSoNhaDuong = Encrypt("12 Nguyễn Thị Minh Khai"),
                        HoTenCha = Encrypt("Võ Văn Thắng"),
                        NgaySinhCha = new DateTime(1970, 4, 4),
                        HoTenMe = Encrypt("Nguyễn Thị Mỹ"),
                        NgaySinhMe = new DateTime(1972, 9, 12)
                    };

                    var resident10 = new ResidentData
                    {
                        CCCD = Encrypt("068199306301"),
                        HoTen = Encrypt("Bùi Thị Mai"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1993, 6, 30),
                        NoiSinh = Encrypt("TP. Đà Lạt, Lâm Đồng"),
                        NgayCap = new DateTime(2012, 1, 15),
                        NoiCap = Encrypt("Công an tỉnh Lâm Đồng"),
                        DanToc = "Tày",
                        TonGiao = "Phật Giáo",
                        SĐT = Encrypt("0977123456"),
                        ttTinhThanh = Encrypt("Lâm Đồng"),
                        ttQuanHuyen = Encrypt("Đà Lạt"),
                        ttPhuongXa = Encrypt("Phường 1"),
                        ttSoNhaDuong = Encrypt("11 Trần Phú"),
                        thtTinhThanh = Encrypt("Lâm Đồng"),
                        thtQuanHuyen = Encrypt("Bảo Lộc"),
                        thtPhuongXa = Encrypt("Lộc Tiến"),
                        thtSoNhaDuong = Encrypt("89 Phan Bội Châu"),
                        HoTenCha = Encrypt("Bùi Văn Tâm"),
                        NgaySinhCha = new DateTime(1967, 11, 5),
                        HoTenMe = Encrypt("Phạm Thị Hoa"),
                        NgaySinhMe = new DateTime(1970, 4, 8)
                    };

                    var residentList = new List<ResidentData> { resident1, resident2, resident3, resident4 , resident5,
                        resident6, resident7, resident8, resident9, resident10 };


                    foreach (var resident in residentList)
                    {
                        try
                        {
                            context.ResidentDatas.Add(resident);
                            context.SaveChanges();
                            Console.WriteLine($"Saved resident: {AesEcbEncryption.DecryptAesEcb(resident.CCCD)}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"❌ Failed to save passport: {AesEcbEncryption.DecryptAesEcb(resident.CCCD)} - Reason: {ex.Message}");
                            context.ChangeTracker.Clear();
                        }
                    }

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
                        HoTen = Encrypt("Hoàng Thị Duyên"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1995, 7, 10),
                        NoiSinh = Encrypt("Phường An Phú, Quận Ninh Kiều, Cần Thơ"),
                        CCCD = Encrypt("036123456789"),
                        NgayCap = new DateTime(2013, 2, 1),
                        NoiCap = Encrypt("Công an thành phố Cần Thơ"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0932109876"),
                        Email = Encrypt("hoangthiduyen@example.com"),
                        Hinh = "seed-data.jpg",

                        ttTinhThanh = Encrypt("Cần Thơ"),
                        ttQuanHuyen = Encrypt("Ninh Kiều"),
                        ttPhuongXa = Encrypt("An Phú"),
                        ttSoNhaDuong = Encrypt("77 Lê Lợi, KDC Hưng Phú"),

                        thtTinhThanh = Encrypt("Cần Thơ"),
                        thtQuanHuyen = Encrypt("Cái Răng"),
                        thtPhuongXa = Encrypt("Hưng Phú"),
                        thtSoNhaDuong = Encrypt("99 Võ Văn Kiệt, Khu dân cư Nam Long"),

                        NgheNghiep = Encrypt("Nhân viên văn phòng"),
                        CoQuan = Encrypt("Công ty TNHH ABC"),
                        DiaChiCoQuan = Encrypt("123 Trần Hưng Đạo, Cần Thơ"),

                        HoTenCha = Encrypt("Hoàng Văn Dũng"),
                        NgaySinhCha = new DateTime(1970, 1, 15),
                        HoTenMe = Encrypt("Trần Thị Hoa"),
                        NgaySinhMe = new DateTime(1973, 3, 20),

                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông cho mục đích du lịch.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an TP. Cần Thơ",
                        NgayNop = DateTime.Now
                    };

                    var FormId2 = Guid.NewGuid().ToString("N").Substring(0, 20);
                    var passport2 = new PassportData
                    {
                        FormID = FormId2,
                        HoTen = Encrypt("Nguyễn Văn Anh"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1988, 3, 15),
                        NoiSinh = Encrypt("Phường Phúc Xá, Quận Ba Đình, Hà Nội"),
                        CCCD = Encrypt("001198803015"),
                        NgayCap = new DateTime(2010, 6, 20),
                        NoiCap = Encrypt("Công an thành phố Hà Nội"),
                        DanToc = "Kinh",
                        TonGiao = "Không",
                        SĐT = Encrypt("0987654321"),
                        Email = Encrypt("nguyenvananh@example.com"),
                        Hinh = "seed-data.jpg",

                        ttTinhThanh = Encrypt("Hà Nội"),
                        ttQuanHuyen = Encrypt("Ba Đình"),
                        ttPhuongXa = Encrypt("Phúc Xá"),
                        ttSoNhaDuong = Encrypt("12 Hoàng Diệu, Khu A"),

                        thtTinhThanh = Encrypt("Hà Nội"),
                        thtQuanHuyen = Encrypt("Cầu Giấy"),
                        thtPhuongXa = Encrypt("Dịch Vọng"),
                        thtSoNhaDuong = Encrypt("45 Xuân Thủy, Tòa nhà CT2"),

                        NgheNghiep = Encrypt("Kỹ sư phần mềm"),
                        CoQuan = Encrypt("Công ty TNHH XYZ"),
                        DiaChiCoQuan = Encrypt("456 Lê Duẩn, Hà Nội"),

                        HoTenCha = Encrypt("Nguyễn Văn Bằng"),
                        NgaySinhCha = new DateTime(1960, 5, 10),
                        HoTenMe = Encrypt("Trần Thị Cúc"),
                        NgaySinhMe = new DateTime(1963, 9, 30),

                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông để đi công tác nước ngoài.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an TP. Hà Nội",
                        NgayNop = DateTime.Now
                    };

                    var FormId3 = Guid.NewGuid().ToString("N").Substring(0, 20);
                    var passport3 = new PassportData
                    {
                        FormID = FormId3,
                        HoTen = Encrypt("Trần Thị Bích"),
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1992, 11, 5),
                        NoiSinh = Encrypt("Phường Thạch Thang, Quận Hải Châu, Đà Nẵng"),
                        CCCD = Encrypt("102199211005"),
                        NgayCap = new DateTime(2014, 4, 15),
                        NoiCap = Encrypt("Công an thành phố Đà Nẵng"),
                        DanToc = "Kinh",
                        TonGiao = "Phật Giáo",
                        SĐT = Encrypt("0912345678"),
                        Email = Encrypt("tranthibich@example.com"),
                        Hinh = "seed-data.jpg",

                        ttTinhThanh = Encrypt("Đà Nẵng"),
                        ttQuanHuyen = Encrypt("Hải Châu"),
                        ttPhuongXa = Encrypt("Thạch Thang"),
                        ttSoNhaDuong = Encrypt("78 Lê Lợi, gần chợ Đống Đa"),

                        thtTinhThanh = Encrypt("Đà Nẵng"),
                        thtQuanHuyen = Encrypt("Thanh Khê"),
                        thtPhuongXa = Encrypt("Hòa Khê"),
                        thtSoNhaDuong = Encrypt("99 Nguyễn Lương Bằng, KDC Phước Lý"),

                        NgheNghiep = Encrypt("Giáo viên"),
                        CoQuan = Encrypt("Trường THPT Đà Nẵng"),
                        DiaChiCoQuan = Encrypt("123 Nguyễn Văn Linh, Đà Nẵng"),

                        HoTenCha = Encrypt("Trần Văn Cường"),
                        NgaySinhCha = new DateTime(1965, 7, 20),
                        HoTenMe = Encrypt("Lê Thị Diễm"),
                        NgaySinhMe = new DateTime(1967, 12, 10),

                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông để du học.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an TP. Đà Nẵng",
                        NgayNop = DateTime.Now
                    };

                    var FormId4 = Guid.NewGuid().ToString("N").Substring(0, 20);
                    var passport4 = new PassportData
                    {
                        FormID = FormId4,
                        HoTen = Encrypt("Lê Văn Chương"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1985, 9, 25),
                        NoiSinh = Encrypt("Phường Bến Nghé, Quận 1, TP.HCM"),
                        CCCD = Encrypt("079198509025"),
                        NgayCap = new DateTime(2012, 3, 30),
                        NoiCap = Encrypt("Công an TP. Hồ Chí Minh"),
                        DanToc = "Kinh",
                        TonGiao = "Thiên Chúa Giáo",
                        SĐT = Encrypt("0909876543"),
                        Email = Encrypt("levanchuong@example.com"),
                        Hinh = "seed-data.jpg",

                        ttTinhThanh = Encrypt("Hồ Chí Minh"),
                        ttQuanHuyen = Encrypt("Quận 1"),
                        ttPhuongXa = Encrypt("Bến Nghé"),
                        ttSoNhaDuong = Encrypt("123 Lê Thánh Tôn, gần Vincom Center"),

                        thtTinhThanh = Encrypt("Hồ Chí Minh"),
                        thtQuanHuyen = Encrypt("Quận 3"),
                        thtPhuongXa = Encrypt("Phường 6"),
                        thtSoNhaDuong = Encrypt("456 Nguyễn Đình Chiểu, chung cư Hòa Bình"),

                        NgheNghiep = Encrypt("Nhân viên kinh doanh"),
                        CoQuan = Encrypt("Công ty TNHH Kinh doanh ABC"),
                        DiaChiCoQuan = Encrypt("789 Trần Hưng Đạo, TP. Hồ Chí Minh"),

                        HoTenCha = Encrypt("Lê Văn Dũng"),
                        NgaySinhCha = new DateTime(1958, 10, 5),
                        HoTenMe = Encrypt("Phạm Thị Em"),
                        NgaySinhMe = new DateTime(1960, 2, 18),

                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông cho mục đích công tác.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an TP. Hồ Chí Minh",
                        NgayNop = DateTime.Now
                    };

                    var FormId5 = Guid.NewGuid().ToString("N").Substring(0, 20);
                    var passport5 = new PassportData
                    {
                        FormID = FormId5,
                        HoTen = Encrypt("Phạm Thị Hằng"),                 // Đúng tên
                        GioiTinh = "Nữ",
                        NgaySinh = new DateTime(1995, 4, 22),              // Đúng ngày sinh
                        NoiSinh = Encrypt("Quận Lê Chân, Hải Phòng"),      // Đúng nơi sinh

                        CCCD = Encrypt("031199504322"),                     // Đúng CCCD
                        NgayCap = new DateTime(2014, 5, 10),                // **Ngày cấp khác (so với resident là 2013/5/1)**
                        NoiCap = Encrypt("Công an tỉnh Hải Phòng"),          // **Nơi cấp khác (so với resident là Công an thành phố Hải Phòng)**

                        DanToc = "Hoa",
                        TonGiao = "Không",
                        SĐT = Encrypt("0911223344"),                       // Đúng số điện thoại
                        Email = Encrypt("phamthi.hang@example.com"),
                        Hinh = "seed-data.jpg",

                        ttTinhThanh = Encrypt("Hải Phòng"),
                        ttQuanHuyen = Encrypt("Lê Chân"),
                        ttPhuongXa = Encrypt("An Dương"),
                        ttSoNhaDuong = Encrypt("23 Trần Nguyên Hãn"),

                        thtTinhThanh = Encrypt("Hải Phòng"),
                        thtQuanHuyen = Encrypt("Ngô Quyền"),
                        thtPhuongXa = Encrypt("Máy Tơ"),
                        thtSoNhaDuong = Encrypt("58 Lạch Tray"), // **Số nhà tạm trú khác so với resident là 56 Lạch Tray**

                        NgheNghiep = Encrypt("Nhân viên văn phòng"),
                        CoQuan = Encrypt("Công ty TNHH XYZ"),
                        DiaChiCoQuan = Encrypt("789 Đường Cầu Đất, Hải Phòng"),

                        HoTenCha = Encrypt("Phạm Văn Tân"),
                        NgaySinhCha = new DateTime(1969, 3, 12),
                        HoTenMe = Encrypt("Nguyễn Thị Hoa"),
                        NgaySinhMe = new DateTime(1972, 7, 9),

                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông cho mục đích công tác.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an TP. Hải Phòng",
                        NgayNop = DateTime.Now
                    };

                    var FormId6 = Guid.NewGuid().ToString("N").Substring(0, 20);
                    var passport6 = new PassportData
                    {
                        FormID = FormId6,
                        HoTen = Encrypt("Ngô Văn Bình"),                    // Đúng tên
                        GioiTinh = "Nam",

                        // Khai sai ngày sinh (muộn hơn so với resident)
                        NgaySinh = new DateTime(2001, 2, 15),               // Sai ngày sinh

                        NoiSinh = Encrypt("Huyện Châu Phú, An Giang"),     // Đúng nơi sinh

                        CCCD = Encrypt("089199901115"),                     // Đúng CCCD

                        NgayCap = new DateTime(2017, 9, 30),                // Ngày cấp đúng

                        NoiCap = Encrypt("Công an tỉnh An Giang"),          // Nơi cấp đúng

                        // Sai dân tộc
                        DanToc = "Kinh",                                     // Khai sai dân tộc, thực tế là Khơ Me

                        TonGiao = "Hòa Hảo",

                        SĐT = Encrypt("0978123456"),                         // Đúng số điện thoại
                        Email = Encrypt("ngovanbinh@example.com"),
                        Hinh = "seed-data.jpg",

                        ttTinhThanh = Encrypt("An Giang"),
                        ttQuanHuyen = Encrypt("Châu Phú"),
                        ttPhuongXa = Encrypt("Khánh Hòa"),
                        ttSoNhaDuong = Encrypt("15 Nguyễn Văn Cừ"),

                        thtTinhThanh = Encrypt("An Giang"),
                        thtQuanHuyen = Encrypt("Long Xuyên"),
                        thtPhuongXa = Encrypt("Mỹ Bình"),
                        thtSoNhaDuong = Encrypt("88 Trần Hưng Đạo"),

                        NgheNghiep = Encrypt("Kỹ sư xây dựng"),
                        CoQuan = Encrypt("Công ty Xây dựng ABC"),
                        DiaChiCoQuan = Encrypt("123 Đường Lê Lợi, An Giang"),

                        HoTenCha = Encrypt("Ngô Văn Hiếu"),
                        NgaySinhCha = new DateTime(1972, 5, 18),

                        HoTenMe = Encrypt("Lê Thị Tuyết"),
                        NgaySinhMe = new DateTime(1974, 8, 30),

                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông cho mục đích học tập.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an tỉnh An Giang",
                        NgayNop = DateTime.Now
                    };

                    var FormId7 = Guid.NewGuid().ToString("N").Substring(0, 20);
                    var passport7 = new PassportData
                    {
                        FormID = FormId7,
                        HoTen = Encrypt("Ngô Văn Bình"),
                        GioiTinh = "Nam",
                        NgaySinh = new DateTime(1999, 1, 11),               // Khớp đúng với resident6
                        NoiSinh = Encrypt("Huyện Châu Phú, An Giang"),
                        CCCD = Encrypt("089199901115"),
                        NgayCap = new DateTime(2017, 9, 30),
                        NoiCap = Encrypt("Công an tỉnh An Giang"),
                        DanToc = "Khơ Me",                                  // Khớp đúng dân tộc
                        TonGiao = "Hòa Hảo",
                        SĐT = Encrypt("0978123456"),
                        Email = Encrypt("ngovanbinh@example.com"),
                        Hinh = "seed-data.jpg",
                        ttTinhThanh = Encrypt("An Giang"),
                        ttQuanHuyen = Encrypt("Châu Phú"),
                        ttPhuongXa = Encrypt("Khánh Hòa"),
                        ttSoNhaDuong = Encrypt("15 Nguyễn Văn Cừ"),
                        thtTinhThanh = Encrypt("An Giang"),
                        thtQuanHuyen = Encrypt("Long Xuyên"),
                        thtPhuongXa = Encrypt("Mỹ Bình"),
                        thtSoNhaDuong = Encrypt("88 Trần Hưng Đạo"),
                        NgheNghiep = Encrypt("Kỹ sư xây dựng"),
                        CoQuan = Encrypt("Công ty Xây dựng ABC"),
                        DiaChiCoQuan = Encrypt("123 Đường Lê Lợi, An Giang"),
                        HoTenCha = Encrypt("Ngô Văn Hiếu"),
                        NgaySinhCha = new DateTime(1972, 5, 18),
                        HoTenMe = Encrypt("Lê Thị Tuyết"),
                        NgaySinhMe = new DateTime(1974, 8, 30),
                        NoiDungDeNghi = "Cấp hộ chiếu phổ thông cho mục đích học tập.",
                        NoiTiepNhanHS = "Phòng Quản lý xuất nhập cảnh Công an tỉnh An Giang",
                        NgayNop = DateTime.Now
                    };



                    var passportList = new List<PassportData> { passport1, passport2, passport3, passport4,
                        passport5, passport6, passport7 };

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