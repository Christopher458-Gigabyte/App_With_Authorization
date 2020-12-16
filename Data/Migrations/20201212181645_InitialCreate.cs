using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App_With_Authorization.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Module",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Modulnummer = table.Column<int>(nullable: false),
                    Modulname = table.Column<string>(nullable: true),
                    Semester = table.Column<int>(nullable: false),
                    Workload = table.Column<int>(nullable: false),
                    Semesterwochenstunden = table.Column<int>(nullable: false),
                    Leistungspunkte = table.Column<int>(nullable: false),
                    Turnus = table.Column<string>(nullable: true),
                    Curriculare_Zuordnung = table.Column<string>(nullable: true),
                    Lernziele = table.Column<string>(nullable: true),
                    Schlüsselqualifikationen = table.Column<string>(nullable: true),
                    Inhaltliche_Beschreibung = table.Column<string>(nullable: true),
                    Unterrichtsform = table.Column<string>(nullable: true),
                    Prüfungsart = table.Column<string>(nullable: true),
                    Prüfungsform = table.Column<string>(nullable: true),
                    Verbindlichkeit = table.Column<string>(nullable: true),
                    Literaturangaben = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Module", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Module");
        }
    }
}
