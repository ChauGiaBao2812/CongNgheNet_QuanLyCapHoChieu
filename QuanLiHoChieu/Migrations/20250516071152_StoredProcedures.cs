using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
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
                        OPEN SYMMETRIC KEY UserDataKey DECRYPTION BY PASSWORD = 'UserDataPassword';

                        INSERT INTO [User] (
                            UserID, HoTen, GioiTinh, NgaySinh, QueQuan,
                            SĐT, Email, ChucVu, Username
                        )
                        VALUES (
                            @UserID,
                            ENCRYPTBYKEY(KEY_GUID('UserDataKey'), @HoTen),
                            @GioiTinh,
                            @NgaySinh,
                            @QueQuan,
                            ENCRYPTBYKEY(KEY_GUID('UserDataKey'), @SDT),
                            ENCRYPTBYKEY(KEY_GUID('UserDataKey'), @Email),
                            @ChucVu,
                            @Username
                        );

                        CLOSE SYMMETRIC KEY UserDataKey;
                    END TRY
                    BEGIN CATCH
                        IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = 'UserDataKey')
                            CLOSE SYMMETRIC KEY UserDataKey;

                        THROW;
                    END CATCH
                END;
                ");

            // Create sp_SelectUser (decrypt fields)
            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_SelectUser
                AS
                BEGIN
                    OPEN SYMMETRIC KEY UserDataKey DECRYPTION BY PASSWORD = 'UserDataPassword';

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

                    CLOSE SYMMETRIC KEY UserDataKey;
                END;
                ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertUser;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_SelectUser;");
        }

    }
}
