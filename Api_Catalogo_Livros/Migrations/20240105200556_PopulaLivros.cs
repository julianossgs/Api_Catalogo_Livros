using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Catalogo_Livros.Migrations
{
    /// <inheritdoc />
    public partial class PopulaLivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Livros(Nome,Preco,Ano_Publicacao,AutorId,EditoraId) " +
                "Values('Hábitos Saudáveis','100.23','2019','1','1')");

            mb.Sql("Insert into Livros(Nome,Preco,Ano_Publicacao,AutorId,EditoraId) " +
                "Values('Receitas Caseiras','45.77','2020','2','2')");

            mb.Sql("Insert into Livros(Nome,Preco,Ano_Publicacao,AutorId,EditoraId) " +
                "Values('A Casa Marrom','68.99','2021','3','3')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Livros");
        }
    }
}
