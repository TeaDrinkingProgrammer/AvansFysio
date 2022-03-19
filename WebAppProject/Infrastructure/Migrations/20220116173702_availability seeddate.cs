using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class availabilityseeddate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.UpdateData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 7,
                column: "EmployeeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "EndHour", "StartHour" },
                values: new object[] { 17, 9 });

            migrationBuilder.UpdateData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EndHour", "StartHour" },
                values: new object[] { 0, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 7,
                column: "EmployeeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "EndHour", "StartHour" },
                values: new object[] { 0, 0 });

            migrationBuilder.UpdateData(
                table: "Availability",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "EndHour", "StartHour" },
                values: new object[] { 17, 9 });

            migrationBuilder.InsertData(
                table: "Availability",
                columns: new[] { "Id", "EmployeeId", "EndHour", "StartHour" },
                values: new object[,]
                {
                    { 16, 1, 0, 0 },
                    { 15, 2, 0, 0 }
                });
        }
    }
}
