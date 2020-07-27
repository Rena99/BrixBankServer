using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Account.Data.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailVerifications",
                columns: table => new
                {
                    EmailVerificationId = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    VerificationCode = table.Column<int>(nullable: false),
                    ExpirationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailVerifications", x => x.EmailVerificationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailVerifications");
        }
    }
}
