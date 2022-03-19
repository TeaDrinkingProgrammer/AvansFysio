using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changedvariablenamestoEnglish : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pathologie",
                table: "Proceedings",
                newName: "ExplanationRequired");

            migrationBuilder.RenameColumn(
                name: "Lichaamslocalisatie",
                table: "Proceedings",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Pathologie",
                table: "Diagnoses",
                newName: "Pathology");

            migrationBuilder.RenameColumn(
                name: "Lichaamslocalisatie",
                table: "Diagnoses",
                newName: "BodyLocalisation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExplanationRequired",
                table: "Proceedings",
                newName: "Pathologie");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Proceedings",
                newName: "Lichaamslocalisatie");

            migrationBuilder.RenameColumn(
                name: "Pathology",
                table: "Diagnoses",
                newName: "Pathologie");

            migrationBuilder.RenameColumn(
                name: "BodyLocalisation",
                table: "Diagnoses",
                newName: "Lichaamslocalisatie");
        }
    }
}
