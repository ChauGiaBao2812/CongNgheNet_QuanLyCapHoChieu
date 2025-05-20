using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
    /// <inheritdoc />
    public partial class Keys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Create Master Key (if it doesn't exist)
            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = '##MS_DatabaseMasterKey##')
                BEGIN
                    CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'DotnetBMCSDL';
                END
            ");

            // 2. Create Symmetric Key

            migrationBuilder.Sql(@"
                -- Create Certificate if it doesn't exist
                IF NOT EXISTS (SELECT * FROM sys.certificates WHERE name = 'Cert_AES')
                BEGIN
                    CREATE CERTIFICATE Cert_AES
                    WITH SUBJECT = 'Certificate for AES symmetric key encryption';
                END
            ");

            migrationBuilder.Sql(@"
                -- Create Symmetric Key encrypted by certificate if not exists
                IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = 'SymmetricKey_AES')
                BEGIN
                    CREATE SYMMETRIC KEY SymmetricKey_AES
                    WITH ALGORITHM = AES_256
                    ENCRYPTION BY CERTIFICATE Cert_AES;
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_OpenSymmetricKey')
                BEGIN
                    EXEC('
                        CREATE PROCEDURE sp_OpenSymmetricKey
                        AS
                        BEGIN
                            OPEN SYMMETRIC KEY SymmetricKey_AES DECRYPTION BY CERTIFICATE Cert_AES;
                        END
                    ')
                END
            ");

            migrationBuilder.Sql(@"
                IF NOT EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_CloseSymmetricKey')
                    BEGIN
                        EXEC('
                            CREATE PROCEDURE sp_CloseSymmetricKey
                            AS
                            BEGIN
                                CLOSE SYMMETRIC KEY SymmetricKey_AES;
                            END
                        ')
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
                -- Drop symmetric key if exists
                IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = 'SymmetricKey_AES')
                BEGIN
                    DROP SYMMETRIC KEY SymmetricKey_AES;
                END
            ");

            migrationBuilder.Sql(@"
                -- Drop certificate if exists
                IF EXISTS (SELECT * FROM sys.certificates WHERE name = 'Cert_AES')
                BEGIN
                    DROP CERTIFICATE Cert_AES;
                END
            ");

            migrationBuilder.Sql(@"
                -- Drop master key if exists
                IF EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = '##MS_DatabaseMasterKey##')
                BEGIN
                    DROP MASTER KEY;
                END
            ");
        }
    }
}
