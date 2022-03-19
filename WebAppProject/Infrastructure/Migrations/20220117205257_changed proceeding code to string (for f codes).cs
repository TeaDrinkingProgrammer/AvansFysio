using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changedproceedingcodetostringforfcodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProceedingCode",
                table: "TreatmentPlans",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "TreatmentPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProceedingCode",
                value: "1430");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ProceedingCode",
                table: "TreatmentPlans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "TreatmentPlans",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProceedingCode",
                value: 1430);
        }
    }
}
