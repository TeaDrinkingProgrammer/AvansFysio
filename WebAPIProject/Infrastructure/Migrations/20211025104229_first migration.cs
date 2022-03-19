using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations {
    public partial class firstmigration : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Diagnoses",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Lichaamslocalisatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pathologie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Proceedings",
                columns: table => new {
                    Id = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Lichaamslocalisatie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pathologie = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Proceedings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Proceedings");
        }
    }
}
