using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricTransportBlog.Infrastructure.Migrations
{
    public partial class ContactKeyGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PublicationComments");

            migrationBuilder.CreateTable(
                name: "LineTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypeOfVehicle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClassColor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Counter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportNetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportNetworks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoFile = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PublishedById = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportNetworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportNetworks_AspNetUsers_PublishedById",
                        column: x => x.PublishedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransportNetworks_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransportNetworkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LineTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lines_LineTypes_LineTypeId",
                        column: x => x.LineTypeId,
                        principalTable: "LineTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lines_TransportNetworks_TransportNetworkId",
                        column: x => x.TransportNetworkId,
                        principalTable: "TransportNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TransportNetworkId",
                table: "Contacts",
                column: "TransportNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_LineTypeId",
                table: "Lines",
                column: "LineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lines_TransportNetworkId",
                table: "Lines",
                column: "TransportNetworkId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportNetworks_ContactId",
                table: "TransportNetworks",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportNetworks_PublishedById",
                table: "TransportNetworks",
                column: "PublishedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_TransportNetworks_TransportNetworkId",
                table: "Contacts",
                column: "TransportNetworkId",
                principalTable: "TransportNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_TransportNetworks_TransportNetworkId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Lines");

            migrationBuilder.DropTable(
                name: "LineTypes");

            migrationBuilder.DropTable(
                name: "TransportNetworks");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PublicationComments",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
