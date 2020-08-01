using Microsoft.EntityFrameworkCore.Migrations;

namespace Net5_DataAccess.Migrations
{
    public partial class updatetableauthorprimarykey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Authors",
                newName: "Author_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Author_Id",
                table: "Authors",
                newName: "AuthorId");
        }
    }
}
