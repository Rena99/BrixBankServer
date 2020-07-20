using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Account.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Customers",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("c2d788a9-7187-4bfc-95c0-38fd7342bcb3"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenDate",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2020, 7, 20, 13, 2, 3, 884, DateTimeKind.Local).AddTicks(1619));

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Accounts",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValue: new Guid("d3bf8eb3-5c80-4569-aaee-0d0a2bb98c12"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CustomerId",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("c2d788a9-7187-4bfc-95c0-38fd7342bcb3"),
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OpenDate",
                table: "Accounts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2020, 7, 20, 13, 2, 3, 884, DateTimeKind.Local).AddTicks(1619),
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountId",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("d3bf8eb3-5c80-4569-aaee-0d0a2bb98c12"),
                oldClrType: typeof(Guid));
        }
    }
}
