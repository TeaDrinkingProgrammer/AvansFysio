using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class newversionofavailability : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PatientFiles_PatientFileId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Availability_Employees_EmployeeId",
                table: "Availability");

            migrationBuilder.DropIndex(
                name: "IX_Availability_EmployeeId",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "DateRange_Duration",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "DateRange_End",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "DateRange_Start",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "DateRange_Duration",
                table: "Appointments");

            migrationBuilder.AddColumn<int>(
                name: "EndHour",
                table: "Availability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StartHour",
                table: "Availability",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PatientFileId",
                table: "Appointments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "Appointments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PatientFiles_PatientFileId",
                table: "Appointments",
                column: "PatientFileId",
                principalTable: "PatientFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatientId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_PatientFiles_PatientFileId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "EndHour",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "StartHour",
                table: "Availability");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointments");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DateRange_Duration",
                table: "Availability",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRange_End",
                table: "Availability",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateRange_Start",
                table: "Availability",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Availability",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PatientFileId",
                table: "Appointments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DateRange_Duration",
                table: "Appointments",
                type: "time",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availability_EmployeeId",
                table: "Availability",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_PatientFiles_PatientFileId",
                table: "Appointments",
                column: "PatientFileId",
                principalTable: "PatientFiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Availability_Employees_EmployeeId",
                table: "Availability",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
