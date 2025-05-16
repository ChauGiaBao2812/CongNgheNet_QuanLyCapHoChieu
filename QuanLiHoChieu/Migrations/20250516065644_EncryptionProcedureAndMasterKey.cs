using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
    /// <inheritdoc />
    public partial class EncryptionProcedureAndMasterKey : Migration
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
                IF NOT EXISTS (SELECT * FROM sys.symmetric_keys WHERE name = 'UserDataKey')
                BEGIN
                    CREATE SYMMETRIC KEY UserDataKey
                    WITH ALGORITHM = AES_256
                    ENCRYPTION BY PASSWORD = 'UserDataPassword';
                END
            ");

            // (Optional) Create the [User] table or update schema if needed
            // migrationBuilder.CreateTable(...);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP SYMMETRIC KEY IF EXISTS UserDataKey;");
            migrationBuilder.Sql("DROP MASTER KEY;");
        }
    }
}
