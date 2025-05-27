using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
    /// <inheritdoc />
    public partial class Keys : Migration
    {
        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create Master Key (if it doesn't exist)
            migrationBuilder.Sql(@"
                CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'Pass@2025!';
                CREATE CERTIFICATE Cert_AES
                WITH SUBJECT = 'PASSPORT_SYSTEM_CERT_2025';
                CREATE SYMMETRIC KEY SymmetricKey_AES
                WITH ALGORITHM = AES_256
                ENCRYPTION BY CERTIFICATE Cert_AES;
            ");

            migrationBuilder.Sql(@"
                -- Mở khóa đối xứng
                CREATE PROCEDURE sp_OpenSymmetricKey
                AS
                BEGIN
                    OPEN SYMMETRIC KEY SymmetricKey_AES DECRYPTION BY CERTIFICATE Cert_AES;
                END
            ");

            migrationBuilder.Sql(@"
                -- Đóng khóa đối xứng
                CREATE PROCEDURE sp_CloseSymmetricKey
                AS
                BEGIN
                    CLOSE SYMMETRIC KEY SymmetricKey_AES;
                END
            ");
            // (Optional) Create the [User] table or update schema if needed
            // migrationBuilder.CreateTable(...);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID('sp_OpenSymmetricKey', 'P') IS NOT NULL
                    DROP PROCEDURE sp_OpenSymmetricKey;
            ");

            migrationBuilder.Sql(@"
                IF OBJECT_ID('sp_CloseSymmetricKey', 'P') IS NOT NULL
                    DROP PROCEDURE sp_CloseSymmetricKey;
            ");

            migrationBuilder.Sql(@"
                DROP SYMMETRIC KEY SymmetricKey_AES;
                DROP CERTIFICATE Cert_AES;
                DROP MASTER KEY;
            ");
        }
    }
}