using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bernardi.Luca._5H.Ecommerce.Migrations
{
    /// <inheritdoc />
    public partial class v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carrello",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Prodotto = table.Column<int>(type: "INTEGER", nullable: false),
                    Utente = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantita = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrello", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carrello");
        }
    }
}
