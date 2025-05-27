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
                        CONVERT(VARCHAR(128), DECRYPTBYKEY(SĐT)) AS SDT,
                        CONVERT(VARCHAR(256), DECRYPTBYKEY(Email)) AS Email,
                        ChucVu,
                        Username
                    FROM [User];

                    EXEC sp_CloseSymmetricKey;
                END;
                ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE sp_UpdateUser
                    @UserID VARCHAR(20),
                    @HoTen NVARCHAR(256),
                    @NgaySinh DATE,
                    @QueQuan NVARCHAR(100),
                    @SDT VARCHAR(128),
                    @Email VARCHAR(256),
                    @ChucVu NVARCHAR(50)
                AS
                BEGIN
                    BEGIN TRY
                        EXEC sp_OpenSymmetricKey;

                        UPDATE [User]
                        SET 
                            HoTen = ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @HoTen),
                            NgaySinh = @NgaySinh,
                            QueQuan = @QueQuan,
                            SĐT = ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @SDT),
                            Email = ENCRYPTBYKEY(KEY_GUID('SymmetricKey_AES'), @Email),
                            ChucVu = @ChucVu
                        WHERE UserID = @UserID;

                        EXEC sp_CloseSymmetricKey;
                    END TRY
                    BEGIN CATCH
                        EXEC sp_CloseSymmetricKey;
                        THROW;
                    END CATCH
                END;
           ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_InsertUser;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_SelectUser;");
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS sp_UpdateUser;");
        }
    }
}
