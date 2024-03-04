using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEnd.Infra.Data.EF.Migrations
{
    /// <inheritdoc />
    public partial class StartProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Plano = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    PrazoEmDias = table.Column<int>(type: "integer", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "date", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "date", nullable: false),
                    DataTermino = table.Column<DateTime>(type: "date", nullable: false),
                    DataPrevistaTermino = table.Column<DateTime>(type: "date", nullable: false),
                    ValorDiaria = table.Column<decimal>(type: "numeric", nullable: false),
                    ValorAdicional = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorMulta = table.Column<decimal>(type: "numeric", nullable: true),
                    ValorTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    MotoId = table.Column<Guid>(type: "uuid", nullable: true),
                    EntregadorId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notificacoes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EntregadorId = table.Column<Guid>(type: "uuid", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: false),
                    DataNoticacao = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notificacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Ano = table.Column<int>(type: "integer", nullable: false),
                    Modelo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Placa = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    LocacaoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Motos_Locacoes_LocacaoId",
                        column: x => x.LocacaoId,
                        principalTable: "Locacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ValorDaCorrida = table.Column<decimal>(type: "numeric", nullable: false),
                    EntregadorId = table.Column<Guid>(type: "uuid", nullable: true),
                    NotificacaoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pedidos_Notificacoes_NotificacaoId",
                        column: x => x.NotificacaoId,
                        principalTable: "Notificacoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Entregadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    CNH = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    CategoriaCNH = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    CNPJ = table.Column<string>(type: "text", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    NumeroCNH = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false),
                    LocacaoId = table.Column<Guid>(type: "uuid", nullable: true),
                    NotificacaoId = table.Column<Guid>(type: "uuid", nullable: true),
                    PedidoId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entregadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entregadores_Locacoes_LocacaoId",
                        column: x => x.LocacaoId,
                        principalTable: "Locacoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entregadores_Notificacoes_NotificacaoId",
                        column: x => x.NotificacaoId,
                        principalTable: "Notificacoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Entregadores_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Entregadores",
                columns: new[] { "Id", "Ativo", "CNH", "CNPJ", "CategoriaCNH", "DataNascimento", "LocacaoId", "Nome", "NotificacaoId", "NumeroCNH", "PedidoId" },
                values: new object[,]
                {
                    { new Guid("9ec54dc0-d8ab-4c6b-b04e-3147a11bfafd"), true, "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp", "10.029.0190/0001-91", "AB", new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Maria José", null, "92385233", null },
                    { new Guid("c8fbc31b-2265-4b77-a4c3-73eea103bd0d"), true, "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp", "10.029.0190/0001-89", "AB", new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Pedro Antonio", null, "92385234", null },
                    { new Guid("d87c0305-70e3-4a1d-ae2f-b98878010528"), true, "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp", "10.029.0190/0001-92", "AB", new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Antonio Pedro", null, "92385232", null },
                    { new Guid("f1f6f4dd-0998-4149-8b60-0055eeec24cd"), true, "https://conteudo.imguol.com.br/c/entretenimento/ae/2022/06/03/nova-cnh-2022-1654284075548_v2_900x506.jpg.webp", "10.029.0190/0001-90", "AB", new DateTime(1978, 5, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "José Antonio", null, "92385235", null }
                });

            migrationBuilder.InsertData(
                table: "Locacoes",
                columns: new[] { "Id", "DataCriacao", "DataInicio", "DataPrevistaTermino", "DataTermino", "EntregadorId", "MotoId", "Plano", "PrazoEmDias", "Status", "ValorAdicional", "ValorDiaria", "ValorMulta", "ValorTotal" },
                values: new object[] { new Guid("3f104368-957e-40d1-801e-f1058dbf2183"), new DateTime(2024, 2, 19, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2250), new DateTime(2024, 2, 20, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2300), new DateTime(2024, 2, 26, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2300), new DateTime(2024, 2, 26, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2300), null, null, "7Dias", 7, "Ativo", null, 30m, null, 210m });

            migrationBuilder.InsertData(
                table: "Motos",
                columns: new[] { "Id", "Ano", "Ativo", "LocacaoId", "Modelo", "Placa" },
                values: new object[,]
                {
                    { new Guid("2e1a7ac9-6134-4dea-982d-c9c2051327cc"), 2008, true, null, "CG 150 START", "RVG-0J82" },
                    { new Guid("3a8707ae-617e-4c9c-acd6-8cd0b855abcb"), 2014, true, null, "XRE", "RVG-0J83" },
                    { new Guid("874f1665-a264-4fd3-9809-495ea35583eb"), 2019, true, null, "CG 150 TITAN", "RVG-0J80" },
                    { new Guid("c08e1b10-b53a-469d-8236-079f8259b10f"), 2015, true, null, "CG 160 FAN", "RVG-0J84" },
                    { new Guid("e9fc6772-ebfc-44c1-ba1a-6bc05ca1ebee"), 2021, true, null, "XRE", "RVG-0J81" }
                });

            migrationBuilder.InsertData(
                table: "Pedidos",
                columns: new[] { "Id", "DataCriacao", "EntregadorId", "NotificacaoId", "Status", "ValorDaCorrida" },
                values: new object[] { new Guid("0fa34707-bb03-448b-8753-2550f18f1204"), new DateTime(2024, 2, 19, 21, 58, 51, 833, DateTimeKind.Local).AddTicks(2660), null, null, "Disponível", 87m });

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_CNPJ",
                table: "Entregadores",
                column: "CNPJ",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_LocacaoId",
                table: "Entregadores",
                column: "LocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_NotificacaoId",
                table: "Entregadores",
                column: "NotificacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_NumeroCNH",
                table: "Entregadores",
                column: "NumeroCNH",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Entregadores_PedidoId",
                table: "Entregadores",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_LocacaoId",
                table: "Motos",
                column: "LocacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Motos_Placa",
                table: "Motos",
                column: "Placa",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_NotificacaoId",
                table: "Pedidos",
                column: "NotificacaoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Entregadores");

            migrationBuilder.DropTable(
                name: "Motos");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Locacoes");

            migrationBuilder.DropTable(
                name: "Notificacoes");
        }
    }
}