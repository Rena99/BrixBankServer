using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Account.Data.Migrations
{
    public partial class FourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OperationsHistory",
                columns: table => new
                {
                    OperationHistoryId = table.Column<Guid>(nullable: false),
                    AccountId = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<Guid>(nullable: false),
                    Debit = table.Column<bool>(nullable: false),
                    TransactionAmount = table.Column<int>(nullable: false),
                    Balance = table.Column<int>(nullable: false),
                    OperationTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationsHistory", x => x.OperationHistoryId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OperationsHistory");
        }
    }
}
