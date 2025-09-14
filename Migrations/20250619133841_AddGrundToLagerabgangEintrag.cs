using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagerverwaltungApp.Migrations
{
    /// <inheritdoc />
    public partial class AddGrundToLagerabgangEintrag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Grund",
                table: "Lagerabgaenge",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grund",
                table: "Lagerabgaenge");
        }
    }
}
