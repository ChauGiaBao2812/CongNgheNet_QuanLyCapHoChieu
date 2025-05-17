using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
    /// <inheritdoc />
    /// <inheritdoc />
    public partial class StoredProcedures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Create sp_InsertUser
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_InsertUser
                    @UserID VARCHAR(20),
                    @HoTen NVARCHAR(256),
                    @GioiTinh NVARCHAR(10),
                    @NgaySinh DATE,
                    @QueQuan NVARCHAR(100),
                    @SDT VARCHAR(128),
                    @Email VARCHAR(256),
                    @ChucVu NVARCHAR(50),
                    @Username VARCHAR(20)
                AS
                BEGIN
                    BEGIN TRY
                        EXEC sp_OpenSymmetricKey;

                        INSERT INTO [User] (
                            UserID, HoTen, GioiTinh, NgaySinh, QueQuan,
                            SĐT, Email, ChucVu, Username
                        )
                        VALUES (
                            @UserID,
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @HoTen),
                            @GioiTinh,
                            @NgaySinh,
                            @QueQuan,
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @SDT),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @Email),
                            @ChucVu,
                            @Username
                        );

                        EXEC sp_CloseSymmetricKey;
                    END TRY
                    BEGIN CATCH
                            EXEC sp_CloseSymmetricKey;
                        THROW;
                    END CATCH
                END;
                ");

            // Create sp_SelectUser (decrypt fields)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_SelectUser
                AS
                BEGIN
                    EXEC sp_OpenSymmetricKey;

                    SELECT
                        UserID,
                        CONVERT(NVARCHAR(256), DECRYPTBYKEY(HoTen)) AS HoTen,
                        GioiTinh,
                        NgaySinh,
                        QueQuan,
                        CONVERT(VARCHAR(128), DECRYPTBYKEY(SĐT)) AS SĐT,
                        CONVERT(NVARCHAR(256), DECRYPTBYKEY(Email)) AS Email,
                        ChucVu,
                        Username
                    FROM [User];

                    EXEC sp_CloseSymmetricKey;
                END;
                ");

            // Create sp_InsertResidentData (Encryption)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_InsertResidentData
                    @CCCD VARCHAR(20),
                    @HoTen NVARCHAR(100),
                    @GioiTinh NVARCHAR(10),
                    @NgaySinh DATE,
                    @NoiSinh NVARCHAR(100),
                    @NgayCap DATE,
                    @NoiCap NVARCHAR(100),
                    @DanToc NVARCHAR(50),
                    @TonGiao NVARCHAR(50) = NULL,
                    @SDT NVARCHAR(15),
                    @ttTinhThanh NVARCHAR(100),
                    @ttQuanHuyen NVARCHAR(100),
                    @ttPhuongXa NVARCHAR(100),
                    @ttSoNhaDuong NVARCHAR(100),
                    @thtTinhThanh NVARCHAR(100),
                    @thtQuanHuyen NVARCHAR(100),
                    @thtPhuongXa NVARCHAR(100),
                    @thtSoNhaDuong NVARCHAR(100),
                    @HoTenCha NVARCHAR(100) = NULL,
                    @NgaySinhCha DATE = NULL,
                    @HoTenMe NVARCHAR(100) = NULL,
                    @NgaySinhMe DATE = NULL
                AS
                BEGIN
                    BEGIN TRY
                        EXEC sp_OpenSymmetricKey;

                        INSERT INTO ResidentData (
                            CCCD, HoTen, GioiTinh, NgaySinh, NoiSinh, NgayCap, NoiCap, DanToc, TonGiao, SĐT,
                            ttTinhThanh, ttQuanHuyen, ttPhuongXa, ttSoNhaDuong,
                            thtTinhThanh, thtQuanHuyen, thtPhuongXa, thtSoNhaDuong,
                            HoTenCha, NgaySinhCha, HoTenMe, NgaySinhMe
                        )
                        VALUES (
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), CONVERT(VARCHAR(20), @CCCD)),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @HoTen),
                            @GioiTinh,
                            @NgaySinh,
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @NoiSinh),
                            @NgayCap,
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @NoiCap),
                            @DanToc,
                            @TonGiao,
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @SDT),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @ttTinhThanh),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @ttQuanHuyen),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @ttPhuongXa),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @ttSoNhaDuong),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @thtTinhThanh),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @thtQuanHuyen),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @thtPhuongXa),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @thtSoNhaDuong),
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @HoTenCha),
                            @NgaySinhCha,
                            ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @HoTenMe),
                            @NgaySinhMe
                        );
                        EXEC sp_CloseSymmetricKey;

                    END TRY
                    BEGIN CATCH
                        IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = 'SymmetricKey_AES')
                            EXEC sp_CloseSymmetricKey;

                        THROW;
                    END CATCH
                END;
                ");

            //sp_SelectResidentData (Decryption)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_SelectResidentData
                AS
                BEGIN
                    EXEC sp_OpenSymmetricKey;

                    SELECT
                        CONVERT(VARCHAR(20), DECRYPTBYKEY(CCCD)) AS CCCD,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(HoTen)) AS HoTen,
                        GioiTinh,
                        NgaySinh,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(NoiSinh)) AS NoiSinh,
                        NgayCap,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(NoiCap)) AS NoiCap,
                        DanToc,
                        TonGiao,
                        CONVERT(NVARCHAR(15), DECRYPTBYKEY(SĐT)) AS SĐT,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(ttTinhThanh)) AS ttTinhThanh,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(ttQuanHuyen)) AS ttQuanHuyen,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(ttPhuongXa)) AS ttPhuongXa,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(ttSoNhaDuong)) AS ttSoNhaDuong,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(thtTinhThanh)) AS thtTinhThanh,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(thtQuanHuyen)) AS thtQuanHuyen,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(thtPhuongXa)) AS thtPhuongXa,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(thtSoNhaDuong)) AS thtSoNhaDuong,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(HoTenCha)) AS HoTenCha,
                        NgaySinhCha,
                        CONVERT(NVARCHAR(100), DECRYPTBYKEY(HoTenMe)) AS HoTenMe,
                        NgaySinhMe
                    FROM ResidentData;

                    EXEC sp_CloseSymmetricKey;
                END;
                ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertUser;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_SelectUser;");

            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertResidentData;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_SelectResidentData;");
        }

    }
}
