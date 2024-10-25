using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalogo.Migrations
{
    /// <inheritdoc />
    public partial class up : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Categorias(Nome,ImagemUrl) Values ('Bebidas','Bebidas.jpg')");
            migrationBuilder.Sql("Insert into Categorias(Nome,ImagemUrl) Values ('Domésticos','Utensilios.jpg')");
            migrationBuilder.Sql("Insert into Categorias(Nome,ImagemUrl) Values ('Sobremesas','Sobremesas.jpg')");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Categorias");
        }
    }
}
