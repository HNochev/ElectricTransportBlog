using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricTransportBlog.Infrastructure.Migrations
{
    public partial class AddedLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_LineTypes_LineTypeId",
                table: "Lines");

            migrationBuilder.RenameColumn(
                name: "WebPage",
                table: "Lines",
                newName: "WorkDayInterval");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Lines",
                newName: "WeekendInterval");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Lines",
                newName: "ShortLineDescription");

            migrationBuilder.AlterColumn<Guid>(
                name: "LineTypeId",
                table: "Lines",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LineDescription",
                table: "Lines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Lines",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_LineTypes_LineTypeId",
                table: "Lines",
                column: "LineTypeId",
                principalTable: "LineTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lines_LineTypes_LineTypeId",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "LineDescription",
                table: "Lines");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Lines");

            migrationBuilder.RenameColumn(
                name: "WorkDayInterval",
                table: "Lines",
                newName: "WebPage");

            migrationBuilder.RenameColumn(
                name: "WeekendInterval",
                table: "Lines",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "ShortLineDescription",
                table: "Lines",
                newName: "Email");

            migrationBuilder.AlterColumn<Guid>(
                name: "LineTypeId",
                table: "Lines",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Lines_LineTypes_LineTypeId",
                table: "Lines",
                column: "LineTypeId",
                principalTable: "LineTypes",
                principalColumn: "Id");
        }
    }
}
