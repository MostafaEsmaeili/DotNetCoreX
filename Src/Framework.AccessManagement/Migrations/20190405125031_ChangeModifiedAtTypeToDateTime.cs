using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Framework.AccessManagement.Migrations
{
    public partial class ChangeModifiedAtTypeToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                schema: "sec",
                table: "Resource",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedAt",
                schema: "sec",
                table: "AccessControl",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ModifiedAt",
                schema: "sec",
                table: "Resource",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedAt",
                schema: "sec",
                table: "AccessControl",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
