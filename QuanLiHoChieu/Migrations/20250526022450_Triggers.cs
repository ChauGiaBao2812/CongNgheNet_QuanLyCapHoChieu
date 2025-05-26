using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuanLiHoChieu.Migrations
{
    public partial class Triggers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TRIGGER trg_Audit_User_Update
                ON [User]
                AFTER UPDATE
                AS
                BEGIN
                    INSERT INTO AuditLog(Username, Action, TableName, TimeStamp)
                    VALUES (SYSTEM_USER, 'UPDATE', 'User', GETDATE());
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                IF OBJECT_ID('trg_Audit_User_Update', 'TR') IS NOT NULL
                    DROP TRIGGER trg_Audit_User_Update;
            ");
        }
    }
}
