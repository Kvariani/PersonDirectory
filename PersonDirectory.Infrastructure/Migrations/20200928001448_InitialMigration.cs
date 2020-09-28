using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PersonDirectory.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    IDNumber = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RelatedPersonToPerson",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    RelatedPersonId = table.Column<int>(nullable: false),
                    RelationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RelatedPersonToPerson", x => new { x.PersonId, x.RelatedPersonId });
                    table.ForeignKey(
                        name: "FK_RelatedPersonToPerson_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RelatedPersonToPerson_Persons_RelatedPersonId",
                        column: x => x.RelatedPersonId,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TelNumber",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: true),
                    TelNumberType = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelNumber", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TelNumber_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_Firstname_Lastname_IDNumber",
                table: "Persons",
                columns: new[] { "Firstname", "Lastname", "IDNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_RelatedPersonToPerson_RelatedPersonId",
                table: "RelatedPersonToPerson",
                column: "RelatedPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_TelNumber_PersonId",
                table: "TelNumber",
                column: "PersonId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RelatedPersonToPerson");

            migrationBuilder.DropTable(
                name: "TelNumber");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
