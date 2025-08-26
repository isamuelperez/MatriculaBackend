using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Matricula.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Materias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Credits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Programa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Identification = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MateriaTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriaTeachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriaTeachers_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriaTeachers_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentMaterias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    MateriaId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentMaterias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentMaterias_Materias_MateriaId",
                        column: x => x.MateriaId,
                        principalTable: "Materias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentMaterias_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentMaterias_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MateriaTeachers_MateriaId",
                table: "MateriaTeachers",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_MateriaTeachers_TeacherId",
                table: "MateriaTeachers",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMaterias_MateriaId",
                table: "StudentMaterias",
                column: "MateriaId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMaterias_StudentId",
                table: "StudentMaterias",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentMaterias_TeacherId",
                table: "StudentMaterias",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_Identification",
                table: "Students",
                column: "Identification",
                unique: true,
                filter: "[Identification] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_Identification",
                table: "Teachers",
                column: "Identification",
                unique: true,
                filter: "[Identification] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MateriaTeachers");

            migrationBuilder.DropTable(
                name: "StudentMaterias");

            migrationBuilder.DropTable(
                name: "Materias");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
