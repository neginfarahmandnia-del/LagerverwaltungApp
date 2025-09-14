using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagerverwaltungApp.Migrations
{
    /// <inheritdoc />
    public partial class AddFelderToLagerabgangEintrag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BenutzerId",
                table: "Lagerabgaenge",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NachherMenge",
                table: "Lagerabgaenge",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VorherMenge",
                table: "Lagerabgaenge",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Lagerabgaenge_BenutzerId",
                table: "Lagerabgaenge",
                column: "BenutzerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lagerabgaenge_AspNetUsers_BenutzerId",
                table: "Lagerabgaenge",
                column: "BenutzerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lagerabgaenge_AspNetUsers_BenutzerId",
                table: "Lagerabgaenge");

            migrationBuilder.DropIndex(
                name: "IX_Lagerabgaenge_BenutzerId",
                table: "Lagerabgaenge");

            migrationBuilder.DropColumn(
                name: "BenutzerId",
                table: "Lagerabgaenge");

            migrationBuilder.DropColumn(
                name: "NachherMenge",
                table: "Lagerabgaenge");

            migrationBuilder.DropColumn(
                name: "VorherMenge",
                table: "Lagerabgaenge");
        }
    }
}
