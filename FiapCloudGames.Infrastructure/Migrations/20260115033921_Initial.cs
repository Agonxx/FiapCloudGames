using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FiapCloudGames.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CadastradoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nivel = table.Column<int>(type: "int", nullable: false),
                    CadastradoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioJogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    JogoId = table.Column<int>(type: "int", nullable: false),
                    AdiquiridoEm = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioJogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioJogos_Jogos_JogoId",
                        column: x => x.JogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioJogos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jogos",
                columns: new[] { "Id", "CadastradoEm", "Descricao", "Preco", "Titulo" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 15, 0, 39, 20, 590, DateTimeKind.Local).AddTicks(1280), "RPG", 249.90m, "Elden Ring" },
                    { 2, new DateTime(2026, 1, 15, 0, 39, 20, 591, DateTimeKind.Local).AddTicks(1252), "Esporte", 249.90m, "EA FC 26" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "CadastradoEm", "Email", "Nivel", "Nome", "Senha" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 1, 15, 3, 39, 20, 591, DateTimeKind.Utc).AddTicks(5431), "rafhita1@gmail.com", 2, "Rafael Santos", "jx2A6WDVUKiccfcAYTCJJg==" },
                    { 2, new DateTime(2026, 1, 15, 3, 39, 20, 591, DateTimeKind.Utc).AddTicks(5874), "admin@fcg.com", 2, "Admin FCG", "IuL0vUhf2mtqMeh2otN5GQ==" },
                    { 3, new DateTime(2026, 1, 15, 3, 39, 20, 591, DateTimeKind.Utc).AddTicks(5875), "user@fcg.com", 1, "User FCG", "BxZkP2nHgsa2DbpyDZLDRQ==" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioJogos_JogoId",
                table: "UsuarioJogos",
                column: "JogoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioJogos_UsuarioId",
                table: "UsuarioJogos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioJogos");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
