using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagerverwaltungApp.Migrations
{
    /// <inheritdoc />
    public partial class AddGrundUndKommentarToWarenausgang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grund",
                table: "Warenausgaenge",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kommentar",
                table: "Warenausgaenge",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grund",
                table: "Warenausgaenge");

            migrationBuilder.DropColumn(
                name: "Kommentar",
                table: "Warenausgaenge");
        }
    }
}
