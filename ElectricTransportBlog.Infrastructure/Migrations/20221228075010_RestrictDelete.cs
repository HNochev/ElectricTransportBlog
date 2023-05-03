using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricTransportBlog.Infrastructure.Migrations
{
    public partial class RestrictDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_TransportNetworks_TransportNetworkId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_TransportNetworks_Contacts_ContactId",
                table: "TransportNetworks");

            migrationBuilder.DropIndex(
                name: "IX_TransportNetworks_ContactId",
                table: "TransportNetworks");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_TransportNetworkId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TransportNetworkId",
                table: "Contacts",
                column: "TransportNetworkId",
                unique: true);

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

            migrationBuilder.DropIndex(
                name: "IX_Contacts_TransportNetworkId",
                table: "Contacts");

            migrationBuilder.CreateIndex(
                name: "IX_TransportNetworks_ContactId",
                table: "TransportNetworks",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TransportNetworkId",
                table: "Contacts",
                column: "TransportNetworkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_TransportNetworks_TransportNetworkId",
                table: "Contacts",
                column: "TransportNetworkId",
                principalTable: "TransportNetworks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransportNetworks_Contacts_ContactId",
                table: "TransportNetworks",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
