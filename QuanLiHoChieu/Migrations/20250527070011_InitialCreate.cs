using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditLog",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Action = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLog", x => x.LogID);
                });

            migrationBuilder.CreateTable(
                name: "ResidentData",
                columns: table => new
                {
                    CCCD = table.Column<byte[]>(type: "varbinary(900)", nullable: false),
                    HoTen = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiSinh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    NgayCap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiCap = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DanToc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TonGiao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SĐT = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ttTinhThanh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ttQuanHuyen = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ttPhuongXa = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ttSoNhaDuong = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    thtTinhThanh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    thtQuanHuyen = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    thtPhuongXa = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    thtSoNhaDuong = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HoTenCha = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NgaySinhCha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoTenMe = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NgaySinhMe = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentData", x => x.CCCD);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    MatKhau = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Activated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "PassportData",
                columns: table => new
                {
                    FormID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiSinh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CCCD = table.Column<byte[]>(type: "varbinary(900)", nullable: false),
                    NgayCap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NoiCap = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    DanToc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TonGiao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SĐT = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Email = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Hinh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ttTinhThanh = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ttQuanHuyen = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ttPhuongXa = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ttSoNhaDuong = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    thtTinhThanh = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    thtQuanHuyen = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    thtPhuongXa = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    thtSoNhaDuong = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NgheNghiep = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    CoQuan = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    DiaChiCoQuan = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    HoTenCha = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NgaySinhCha = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoTenMe = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    NgaySinhMe = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NoiDungDeNghi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiTiepNhanHS = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NgayNop = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassportData", x => x.FormID);
                    table.ForeignKey(
                        name: "FK_PassportData_ResidentData_CCCD",
                        column: x => x.CCCD,
                        principalTable: "ResidentData",
                        principalColumn: "CCCD",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HoTen = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QueQuan = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SĐT = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Email = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ChucVu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_User_TaiKhoan_Username",
                        column: x => x.Username,
                        principalTable: "TaiKhoan",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LuuTru",
                columns: table => new
                {
                    PassportID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FormID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayNop = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayCap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CoGiaTriDen = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LuuTru", x => x.PassportID);
                    table.ForeignKey(
                        name: "FK_LuuTru_PassportData_FormID",
                        column: x => x.FormID,
                        principalTable: "PassportData",
                        principalColumn: "FormID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LuuTru_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "XuLy",
                columns: table => new
                {
                    XuLyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    NgayXuLy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LoaiXuLy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuLy", x => x.XuLyID);
                    table.ForeignKey(
                        name: "FK_XuLy_PassportData_FormID",
                        column: x => x.FormID,
                        principalTable: "PassportData",
                        principalColumn: "FormID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_XuLy_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LuuTru_FormID",
                table: "LuuTru",
                column: "FormID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LuuTru_UserID",
                table: "LuuTru",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_PassportData_CCCD",
                table: "PassportData",
                column: "CCCD");

            migrationBuilder.CreateIndex(
                name: "IX_User_Username",
                table: "User",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_XuLy_FormID",
                table: "XuLy",
                column: "FormID");

            migrationBuilder.CreateIndex(
                name: "IX_XuLy_UserID",
                table: "XuLy",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditLog");

            migrationBuilder.DropTable(
                name: "LuuTru");

            migrationBuilder.DropTable(
                name: "XuLy");

            migrationBuilder.DropTable(
                name: "PassportData");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "ResidentData");

            migrationBuilder.DropTable(
                name: "TaiKhoan");
        }
    }
}
