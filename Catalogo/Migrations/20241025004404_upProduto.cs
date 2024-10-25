using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace Catalogo.Migrations
{
    /// <inheritdoc />
    public partial class upProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert Into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,Cadastro,CategoriaId) Values('Coca Cola','Bebida feita de coca',5.60,'coca.jpg',20,Now(),1)");
            migrationBuilder.Sql("Insert Into Produtos(Nome,Descricao,Preco,ImagemUrl,Estoque,Cadastro,CategoriaId) Values('Pepsu Cola','Bebida da pepsi feita de coca',4.60,'pepsi.jpg',20,Now(),1)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Produtos");
        }
    }
}
