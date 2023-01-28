using Microsoft.EntityFrameworkCore.Migrations;

namespace Ecomm_BookProject_2_DataAccess.Migrations
{
    public partial class updateNameCoverTypeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Namee",
                table: "CoverTypes",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "CoverTypes",
                newName: "Namee");
        }
    }
}
