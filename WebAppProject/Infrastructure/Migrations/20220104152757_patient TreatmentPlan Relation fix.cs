using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations {
    public partial class patientTreatmentPlanRelationfix : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    BigNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new {
                    PatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    EmailAdress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientFileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.UniqueConstraint("AK_Patients_EmailAdress", x => x.EmailAdress);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlans",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientFileId = table.Column<int>(type: "int", nullable: false),
                    SessionDuration = table.Column<TimeSpan>(type: "time", nullable: false),
                    SessionCount = table.Column<int>(type: "int", nullable: false),
                    ProceedingCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_TreatmentPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Availability",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateRange_Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateRange_End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateRange_Duration = table.Column<TimeSpan>(type: "time", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Availability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Availability_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PatientFiles",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KindOfPatient = table.Column<int>(type: "int", nullable: false),
                    IntakeById = table.Column<int>(type: "int", nullable: false),
                    IntakeUnderSupervisionOfId = table.Column<int>(type: "int", nullable: true),
                    DateOfAdmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOfDischarge = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MainTherapistId = table.Column<int>(type: "int", nullable: false),
                    TreatmentPlanId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_PatientFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Employees_IntakeById",
                        column: x => x.IntakeById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Employees_IntakeUnderSupervisionOfId",
                        column: x => x.IntakeUnderSupervisionOfId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Employees_MainTherapistId",
                        column: x => x.MainTherapistId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_PatientFiles_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientFiles_TreatmentPlans_TreatmentPlanId",
                        column: x => x.TreatmentPlanId,
                        principalTable: "TreatmentPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    PatientFileId = table.Column<int>(type: "int", nullable: false),
                    DateRange_Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateRange_End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateRange_Duration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Appointments_PatientFiles_PatientFileId",
                        column: x => x.PatientFileId,
                        principalTable: "PatientFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Remark",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientFileId = table.Column<int>(type: "int", nullable: false),
                    RemarkText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RemarkById = table.Column<int>(type: "int", nullable: true),
                    VisibleForPatient = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Remark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Remark_Employees_RemarkById",
                        column: x => x.RemarkById,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Remark_PatientFiles_PatientFileId",
                        column: x => x.PatientFileId,
                        principalTable: "PatientFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BigNumber", "EmailAdress", "Name", "PhoneNumber", "RegistrationNumber" },
                values: new object[,]
                {
                    { 1, 123456, "henkpanken@gmail.com", "Henk Panken", "0615468795", 3329102 },
                    { 2, null, "larsdejong@gmail.com", "Lars de Jong", "0684950698", 38564976 }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "PatientId", "Dob", "EmailAdress", "Gender", "Name", "PatientFileId", "PhoneNumber", "RegistrationNumber" },
                values: new object[] { 1, new DateTime(1990, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "janjanssen@gmail.com", 0, "Jan Janssen", 0, "0612345678", 123434556 });

            migrationBuilder.InsertData(
                table: "TreatmentPlans",
                columns: new[] { "Id", "PatientFileId", "ProceedingCode", "SessionCount", "SessionDuration" },
                values: new object[] { 1, 0, 1430, 2, new TimeSpan(0, 1, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "PatientFiles",
                columns: new[] { "Id", "Condition", "DateOfAdmission", "DateOfDischarge", "DiagnosisDescription", "DiagnosisId", "IntakeById", "IntakeUnderSupervisionOfId", "KindOfPatient", "MainTherapistId", "PatientId", "TreatmentPlanId" },
                values: new object[] { 1, "ietsitis", new DateTime(2021, 10, 28, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "lorem ipsum", 100, 1, null, 1, 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_EmployeeId",
                table: "Appointments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientFileId",
                table: "Appointments",
                column: "PatientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Availability_EmployeeId",
                table: "Availability",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_IntakeById",
                table: "PatientFiles",
                column: "IntakeById");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_IntakeUnderSupervisionOfId",
                table: "PatientFiles",
                column: "IntakeUnderSupervisionOfId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_MainTherapistId",
                table: "PatientFiles",
                column: "MainTherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_PatientId",
                table: "PatientFiles",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientFiles_TreatmentPlanId",
                table: "PatientFiles",
                column: "TreatmentPlanId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Remark_PatientFileId",
                table: "Remark",
                column: "PatientFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Remark_RemarkById",
                table: "Remark",
                column: "RemarkById");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Availability");

            migrationBuilder.DropTable(
                name: "Remark");

            migrationBuilder.DropTable(
                name: "PatientFiles");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "TreatmentPlans");
        }
    }
}
