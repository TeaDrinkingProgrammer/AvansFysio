using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Changedseeddata2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Availability",
                table: "Availability");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availability",
                table: "Availability",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Availability",
                columns: new[] { "Id", "EmployeeId", "EndHour", "StartHour" },
                values: new object[,]
                {
                    { 16, 1, 0, 0 },
                    { 1, 1, 0, 0 },
                    { 2, 1, 17, 9 },
                    { 3, 1, 17, 9 },
                    { 4, 1, 17, 9 },
                    { 5, 1, 17, 9 },
                    { 6, 1, 15, 9 },
                    { 7, 2, 0, 0 },
                    { 8, 2, 0, 0 },
                    { 9, 2, 0, 0 },
                    { 10, 2, 17, 9 },
                    { 11, 2, 17, 9 },
                    { 12, 2, 17, 9 },
                    { 13, 2, 17, 9 },
                    { 14, 2, 0, 0 },
                    { 15, 2, 0, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availability_EmployeeId",
                table: "Availability",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Availability",
                table: "Availability");

            migrationBuilder.DropIndex(
                name: "IX_Availability_EmployeeId",
                table: "Availability");

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Availability",
                table: "Availability",
                columns: new[] { "EmployeeId", "Id" });
        }
    }
}
