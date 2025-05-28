using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
    /// <inheritdoc />
    public partial class AddDatabaseViews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE VIEW View_XT_Resident AS
                SELECT
                    CONVERT(varchar, CCCD) AS CCCD,
                    CONVERT(varchar, HoTen) AS HoTen,
                    GioiTinh,
                    NgaySinh,
                    CONVERT(varchar, NoiSinh) AS NoiSinh,
                    NgayCap,
                    CONVERT(varchar, NoiCap) AS NoiCap,
                    DanToc,
                    TonGiao,
                    CONVERT(varchar, SĐT) AS SĐT,
                    CONVERT(varchar, ttTinhThanh) AS TinhThanhThuongTru,
                    CONVERT(varchar, ttQuanHuyen) AS QuanHuyenThuongTru,
                    CONVERT(varchar, ttPhuongXa) AS PhuongXaThuongTru,
                    CONVERT(varchar, ttSoNhaDuong) AS SoNhaThuongTru,
                    CONVERT(varchar, thtTinhThanh) AS TinhThanhTamTru,
                    CONVERT(varchar, thtQuanHuyen) AS QuanHuyenTamTru,
                    CONVERT(varchar, thtPhuongXa) AS PhuongXaTamTru,
                    CONVERT(varchar, thtSoNhaDuong) AS SoNhaTamTru,
                    CONVERT(varchar, HoTenCha) AS HoTenCha,
                    NgaySinhCha,
                    CONVERT(varchar, HoTenMe) AS HoTenMe,
                    NgaySinhMe
                FROM ResidentData;
            ");

                    migrationBuilder.Sql(@"
                CREATE VIEW View_XD_PassportForm AS
                SELECT
                    pd.FormID,
                    CONVERT(varchar, pd.HoTen) AS HoTen,
                    pd.GioiTinh,
                    pd.NgaySinh,
                    CONVERT(varchar, pd.NoiSinh) AS NoiSinh,
                    CONVERT(varchar, pd.CCCD) AS CCCD,
                    pd.NgayCap,
                    CONVERT(varchar, pd.NoiCap) AS NoiCap,
                    pd.DanToc,
                    pd.TonGiao,
                    CONVERT(varchar, pd.SĐT) AS SĐT,
                    CONVERT(varchar, pd.Email) AS Email,
                    pd.Hinh,
                    CONVERT(varchar, pd.ttTinhThanh) AS TinhThanhThuongTru,
                    CONVERT(varchar, pd.ttQuanHuyen) AS QuanHuyenThuongTru,
                    CONVERT(varchar, pd.ttPhuongXa) AS PhuongXaThuongTru,
                    CONVERT(varchar, pd.ttSoNhaDuong) AS SoNhaThuongTru,
                    CONVERT(varchar, pd.thtTinhThanh) AS TinhThanhTamTru,
                    CONVERT(varchar, pd.thtQuanHuyen) AS QuanHuyenTamTru,
                    CONVERT(varchar, pd.thtPhuongXa) AS PhuongXaTamTru,
                    CONVERT(varchar, pd.thtSoNhaDuong) AS SoNhaTamTru,
                    CONVERT(varchar, pd.HoTenCha) AS HoTenCha,
                    pd.NgaySinhCha,
                    CONVERT(varchar, pd.HoTenMe) AS HoTenMe,
                    pd.NgaySinhMe,
                    pd.NoiDungDeNghi,
                    pd.NoiTiepNhanHS,
                    pd.NgayNop
                FROM PassportData pd;
            ");

                    migrationBuilder.Sql(@"
                CREATE VIEW View_LT_Status AS
                SELECT
                    lt.PassportID,
                    lt.FormID,
                    lt.UserID,
                    lt.NgayNop,
                    lt.NgayCap,
                    lt.CoGiaTriDen
                FROM LuuTru lt;
            ");

                    migrationBuilder.Sql(@"
                CREATE VIEW View_GS_AuditLog AS
                SELECT
                    LogID,
                    Username,
                    Action,
                    TableName,
                    TimeStamp
                FROM AuditLog;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS View_XT_Resident;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS View_XD_PassportForm;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS View_LT_Status;");
            migrationBuilder.Sql("DROP VIEW IF EXISTS View_GS_AuditLog;");
        }
    }
}
