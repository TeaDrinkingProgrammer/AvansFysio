using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class correctedincorrectdiagnosisidinseeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PatientFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiagnosisId",
                value: 1168);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PatientFiles",
                keyColumn: "Id",
                keyValue: 1,
                column: "DiagnosisId",
                value: 100);
        }
    }
}
