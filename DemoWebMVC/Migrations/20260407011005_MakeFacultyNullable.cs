using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoWebMVC.Migrations
{
    /// <inheritdoc />
    public partial class MakeFacultyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculty_FacultyID",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyID",
                table: "Students",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculty_FacultyID",
                table: "Students",
                column: "FacultyID",
                principalTable: "Faculty",
                principalColumn: "FacultyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculty_FacultyID",
                table: "Students");

            migrationBuilder.AlterColumn<int>(
                name: "FacultyID",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculty_FacultyID",
                table: "Students",
                column: "FacultyID",
                principalTable: "Faculty",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
