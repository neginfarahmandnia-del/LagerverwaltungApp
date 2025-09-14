using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LagerverwaltungApp.Migrations
{
    /// <inheritdoc />
    public partial class AddKategorieIdToProdukt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KategorieId",
                table: "Artikel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Artikel_KategorieId",
                table: "Artikel",
                column: "KategorieId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artikel_Kategorien_KategorieId",
                table: "Artikel",
                column: "KategorieId",
                principalTable: "Kategorien",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artikel_Kategorien_KategorieId",
                table: "Artikel");

            migrationBuilder.DropIndex(
                name: "IX_Artikel_KategorieId",
                table: "Artikel");

            migrationBuilder.DropColumn(
                name: "KategorieId",
                table: "Artikel");
        }
    }
}
