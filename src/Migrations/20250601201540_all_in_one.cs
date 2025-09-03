using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace edu_for_you.Migrations
{
    /// <inheritdoc />
    public partial class all_in_one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sobrenome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    dataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    genero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nivelEnsino = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    areaInteresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    foto_perfil = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PerfilProfessor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    biografia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email_contato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    areaAtuacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefone_contato = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    formacao_academica = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    experiencia_profissional = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    habilidades = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    idiomas = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    linkedin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    avaliacao_media = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerfilProfessor", x => x.id);
                    table.ForeignKey(
                        name: "FK_PerfilProfessor_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoProfessor",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_professor = table.Column<int>(type: "int", nullable: false),
                    nota = table.Column<double>(type: "float", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_avaliacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoProfessor", x => x.id);
                    table.ForeignKey(
                        name: "FK_AvaliacaoProfessor_PerfilProfessor_id_professor",
                        column: x => x.id_professor,
                        principalTable: "PerfilProfessor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacaoProfessor_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Curso",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_professor = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    categoria = table.Column<int>(type: "int", nullable: false),
                    carga_horaria = table.Column<int>(type: "int", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    capa_curso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Curso", x => x.id);
                    table.ForeignKey(
                        name: "FK_Curso_PerfilProfessor_id_professor",
                        column: x => x.id_professor,
                        principalTable: "PerfilProfessor",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AvaliacaoCurso",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_curso = table.Column<int>(type: "int", nullable: false),
                    nota = table.Column<double>(type: "float", nullable: false),
                    comentario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_avaliacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvaliacaoCurso", x => x.id);
                    table.ForeignKey(
                        name: "FK_AvaliacaoCurso_Curso_id_curso",
                        column: x => x.id_curso,
                        principalTable: "Curso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvaliacaoCurso_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Conteudo",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_curso = table.Column<int>(type: "int", nullable: false),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    licao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    video_curso = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conteudo", x => x.id);
                    table.ForeignKey(
                        name: "FK_Conteudo_Curso_id_curso",
                        column: x => x.id_curso,
                        principalTable: "Curso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Foruns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    id_curso = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foruns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Foruns_Curso_id_curso",
                        column: x => x.id_curso,
                        principalTable: "Curso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Matricula",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_curso = table.Column<int>(type: "int", nullable: false),
                    data_matricula = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matricula", x => x.id);
                    table.ForeignKey(
                        name: "FK_Matricula_Curso_id_curso",
                        column: x => x.id_curso,
                        principalTable: "Curso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matricula_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Postagem",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_curso = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    postagem_pai_id = table.Column<int>(type: "int", nullable: true),
                    titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    conteudo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Postagem", x => x.id);
                    table.ForeignKey(
                        name: "FK_Postagem_Curso_id_curso",
                        column: x => x.id_curso,
                        principalTable: "Curso",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Postagem_Postagem_postagem_pai_id",
                        column: x => x.postagem_pai_id,
                        principalTable: "Postagem",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Postagem_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Topicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Conteudo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    id_forum = table.Column<int>(type: "int", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topicos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topicos_Foruns_id_forum",
                        column: x => x.id_forum,
                        principalTable: "Foruns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoCurso_id_curso",
                table: "AvaliacaoCurso",
                column: "id_curso");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoCurso_id_usuario",
                table: "AvaliacaoCurso",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoProfessor_id_professor",
                table: "AvaliacaoProfessor",
                column: "id_professor");

            migrationBuilder.CreateIndex(
                name: "IX_AvaliacaoProfessor_id_usuario",
                table: "AvaliacaoProfessor",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Conteudo_id_curso",
                table: "Conteudo",
                column: "id_curso");

            migrationBuilder.CreateIndex(
                name: "IX_Curso_id_professor",
                table: "Curso",
                column: "id_professor");

            migrationBuilder.CreateIndex(
                name: "IX_Foruns_id_curso",
                table: "Foruns",
                column: "id_curso",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_id_curso",
                table: "Matricula",
                column: "id_curso");

            migrationBuilder.CreateIndex(
                name: "IX_Matricula_id_usuario",
                table: "Matricula",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_PerfilProfessor_id_usuario",
                table: "PerfilProfessor",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Postagem_id_curso",
                table: "Postagem",
                column: "id_curso");

            migrationBuilder.CreateIndex(
                name: "IX_Postagem_id_usuario",
                table: "Postagem",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_Postagem_postagem_pai_id",
                table: "Postagem",
                column: "postagem_pai_id");

            migrationBuilder.CreateIndex(
                name: "IX_Topicos_id_forum",
                table: "Topicos",
                column: "id_forum");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvaliacaoCurso");

            migrationBuilder.DropTable(
                name: "AvaliacaoProfessor");

            migrationBuilder.DropTable(
                name: "Conteudo");

            migrationBuilder.DropTable(
                name: "Matricula");

            migrationBuilder.DropTable(
                name: "Postagem");

            migrationBuilder.DropTable(
                name: "Topicos");

            migrationBuilder.DropTable(
                name: "Foruns");

            migrationBuilder.DropTable(
                name: "Curso");

            migrationBuilder.DropTable(
                name: "PerfilProfessor");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
