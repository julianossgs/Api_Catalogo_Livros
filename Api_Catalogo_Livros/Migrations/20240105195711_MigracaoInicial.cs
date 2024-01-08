using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Catalogo_Livros.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    AutorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.AutorId);
                });

            migrationBuilder.CreateTable(
                name: "Editoras",
                columns: table => new
                {
                    EditoraId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoras", x => x.EditoraId);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    LivroId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Ano_Publicacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    AutoresAutorId = table.Column<int>(type: "int", nullable: true),
                    EditoraId = table.Column<int>(type: "int", nullable: false),
                    EditorasEditoraId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.LivroId);
                    table.ForeignKey(
                        name: "FK_Livros_Autores_AutoresAutorId",
                        column: x => x.AutoresAutorId,
                        principalTable: "Autores",
                        principalColumn: "AutorId");
                    table.ForeignKey(
                        name: "FK_Livros_Editoras_EditorasEditoraId",
                        column: x => x.EditorasEditoraId,
                        principalTable: "Editoras",
                        principalColumn: "EditoraId");
                });

            migrationBuilder.CreateIndex(
                name: "Ix_Nome",
                table: "Autores",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "Ix_Nome",
                table: "Editoras",
                column: "Nome");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AutoresAutorId",
                table: "Livros",
                column: "AutoresAutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_EditorasEditoraId",
                table: "Livros",
                column: "EditorasEditoraId");

            migrationBuilder.CreateIndex(
                name: "Ix_Nome",
                table: "Livros",
                column: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Editoras");
        }
    }
}
