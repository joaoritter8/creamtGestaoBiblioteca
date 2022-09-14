using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoBiblioteca.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CatNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CatID);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    PessoaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PessoaNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PessoaCPF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoaRG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoaEndereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoaTelefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoaEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoaTipo = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    PessoaStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.PessoaID);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    LivroID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivroISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivroTitulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PessoaID = table.Column<int>(type: "int", nullable: false),
                    LivroEditora = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivroEdicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LivroAno = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CatID = table.Column<int>(type: "int", nullable: false),
                    LivroStatus = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.LivroID);
                    table.ForeignKey(
                        name: "FK_Livro_Categoria_CatID",
                        column: x => x.CatID,
                        principalTable: "Categoria",
                        principalColumn: "CatID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Pessoa_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Movimentacao",
                columns: table => new
                {
                    MovimentacaoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LivroID = table.Column<int>(type: "int", nullable: false),
                    PessoaID = table.Column<int>(type: "int", nullable: false),
                    ProfID = table.Column<int>(type: "int", nullable: false),
                    DataEmprestimo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataMaxima = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataDevolucao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MovimentacaoStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentacao", x => x.MovimentacaoID);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Livro_LivroID",
                        column: x => x.LivroID,
                        principalTable: "Livro",
                        principalColumn: "LivroID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Pessoa_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Movimentacao_Pessoa_ProfID",
                        column: x => x.ProfID,
                        principalTable: "Pessoa",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_CatID",
                table: "Livro",
                column: "CatID");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_PessoaID",
                table: "Livro",
                column: "PessoaID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_LivroID",
                table: "Movimentacao",
                column: "LivroID");

            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_PessoaID",
                table: "Movimentacao",
                column: "PessoaID");
            migrationBuilder.CreateIndex(
                name: "IX_Movimentacao_ProfID",
                table: "Movimentacao",
                column: "ProfID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentacao");

            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Pessoa");

        }
    }
}
