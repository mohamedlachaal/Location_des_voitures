using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace tpvoiture.Migrations
{
    public partial class creation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    prenom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    tel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "marques",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libelle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_marques", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "voitures",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    matricule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nbr_portes = table.Column<int>(type: "int", nullable: false),
                    nbr_places = table.Column<int>(type: "int", nullable: false),
                    photo_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    couleur = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarqueId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_voitures", x => x.id);
                    table.ForeignKey(
                        name: "FK_voitures_marques_MarqueId",
                        column: x => x.MarqueId,
                        principalTable: "marques",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "assurances",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    agence = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prix = table.Column<int>(type: "int", nullable: false),
                    VoitureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assurances", x => x.id);
                    table.ForeignKey(
                        name: "FK_assurances_voitures_VoitureId",
                        column: x => x.VoitureId,
                        principalTable: "voitures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_debut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_fin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    retour = table.Column<bool>(type: "bit", nullable: false),
                    prixjour = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    VoitureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_locations", x => x.id);
                    table.ForeignKey(
                        name: "FK_locations_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_locations_voitures_VoitureId",
                        column: x => x.VoitureId,
                        principalTable: "voitures",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_assurances_VoitureId",
                table: "assurances",
                column: "VoitureId");

            migrationBuilder.CreateIndex(
                name: "IX_locations_ClientId",
                table: "locations",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_locations_VoitureId",
                table: "locations",
                column: "VoitureId");

            migrationBuilder.CreateIndex(
                name: "IX_voitures_MarqueId",
                table: "voitures",
                column: "MarqueId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assurances");

            migrationBuilder.DropTable(
                name: "locations");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "voitures");

            migrationBuilder.DropTable(
                name: "marques");
        }
    }
}
