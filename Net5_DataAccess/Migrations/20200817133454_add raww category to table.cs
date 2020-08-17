using Microsoft.EntityFrameworkCore.Migrations;

namespace Net5_DataAccess.Migrations
{
    public partial class addrawwcategorytotable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into tbl_Category value('Cat 1')");
            migrationBuilder.Sql("insert into tbl_Category value('Cat 1')");
            migrationBuilder.Sql("insert into tbl_Category value('Cat 1')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
