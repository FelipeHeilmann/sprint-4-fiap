using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WiseBuddy.Api.Migrations
{
    /// <inheritdoc />
    public partial class SuitabilityResposta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatSessions");

            migrationBuilder.CreateTable(
                name: "SuitabilityRespostas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SuitabilityId = table.Column<int>(type: "integer", nullable: false),
                    PerguntaId = table.Column<int>(type: "integer", nullable: false),
                    TextoPergunta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    RespostaSelecionada = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PontuacaoObtida = table.Column<int>(type: "integer", nullable: false),
                    DataResposta = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuitabilityRespostas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuitabilityRespostas_Suitabilities_SuitabilityId",
                        column: x => x.SuitabilityId,
                        principalTable: "Suitabilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suitabilities_DataTeste",
                table: "Suitabilities",
                column: "DataTeste");

            migrationBuilder.CreateIndex(
                name: "IX_Suitabilities_UsuarioId_DataTeste",
                table: "Suitabilities",
                columns: new[] { "UsuarioId", "DataTeste" });

            migrationBuilder.CreateIndex(
                name: "IX_SuitabilityRespostas_PerguntaId",
                table: "SuitabilityRespostas",
                column: "PerguntaId");

            migrationBuilder.CreateIndex(
                name: "IX_SuitabilityRespostas_SuitabilityId",
                table: "SuitabilityRespostas",
                column: "SuitabilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuitabilityRespostas");

            migrationBuilder.DropIndex(
                name: "IX_Suitabilities_DataTeste",
                table: "Suitabilities");

            migrationBuilder.DropIndex(
                name: "IX_Suitabilities_UsuarioId_DataTeste",
                table: "Suitabilities");

            migrationBuilder.CreateTable(
                name: "ChatSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    DataFim = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DataInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    MensagensJson = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSessions_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatSessions_UsuarioId",
                table: "ChatSessions",
                column: "UsuarioId");
        }
    }
}
