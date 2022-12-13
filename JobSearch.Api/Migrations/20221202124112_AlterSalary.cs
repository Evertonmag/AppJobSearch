using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobSearch.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterSalary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Jobs",
                newName: "InitialSalary");

            migrationBuilder.AddColumn<double>(
                name: "FinalSalary",
                table: "Jobs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinalSalary",
                table: "Jobs");

            migrationBuilder.RenameColumn(
                name: "InitialSalary",
                table: "Jobs",
                newName: "Salary");
        }
    }
}
