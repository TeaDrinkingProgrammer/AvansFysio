using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class addedtreatmentrelationtopatientfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PatientFileId",
                table: "Treatments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_PatientFileId",
                table: "Treatments",
                column: "PatientFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Treatments_PatientFiles_PatientFileId",
                table: "Treatments",
                column: "PatientFileId",
                principalTable: "PatientFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Treatments_PatientFiles_PatientFileId",
                table: "Treatments");

            migrationBuilder.DropIndex(
                name: "IX_Treatments_PatientFileId",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "PatientFileId",
                table: "Treatments");
        }
    }
}
