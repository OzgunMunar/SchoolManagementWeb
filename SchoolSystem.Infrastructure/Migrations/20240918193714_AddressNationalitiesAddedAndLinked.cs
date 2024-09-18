using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddressNationalitiesAddedAndLinked : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adresses",
                columns: table => new
                {
                    AddressGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedByUserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adresses", x => x.AddressGuid);
                });

            migrationBuilder.CreateTable(
                name: "Nationalities",
                columns: table => new
                {
                    NationalityGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedByUserGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nationalities", x => x.NationalityGuid);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressGuid",
                table: "AspNetUsers",
                column: "AddressGuid");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_NationalityGuid",
                table: "AspNetUsers",
                column: "NationalityGuid",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Adresses_AddressGuid",
                table: "AspNetUsers",
                column: "AddressGuid",
                principalTable: "Adresses",
                principalColumn: "AddressGuid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Nationalities_NationalityGuid",
                table: "AspNetUsers",
                column: "NationalityGuid",
                principalTable: "Nationalities",
                principalColumn: "NationalityGuid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Adresses_AddressGuid",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Nationalities_NationalityGuid",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Adresses");

            migrationBuilder.DropTable(
                name: "Nationalities");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressGuid",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_NationalityGuid",
                table: "AspNetUsers");
        }
    }
}
