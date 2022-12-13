using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobSearch.Api.Migrations
{
    /// <inheritdoc />
    public partial class AlterNamecaompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Jobs",
                newName: "Company");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Company",
                table: "Jobs",
                newName: "CompanyName");
        }
    }
}
