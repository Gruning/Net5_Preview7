using Microsoft.EntityFrameworkCore.Migrations;

namespace Net5_DataAccess.Migrations
{
    public partial class renameGenreToGenreNameInGenreTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "Name",
            //    table: "Genres",
            //    newName: "GenreName");
            migrationBuilder.AddColumn<string>(
                name: "GenreName",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: (true)
                );
            migrationBuilder.Sql("UPDATE dbo.genres SET GenreName=Name");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Genres"
                );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.RenameColumn(
            //    name: "GenreName",
            //    table: "Genres",
            //    newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Genres",
                type: "nvarchar(max)",
                nullable: (true)
                );
            migrationBuilder.Sql("UPDATE dbo.genres SET Name=GenreName");

            migrationBuilder.DropColumn(
                name: "GenreName",
                table: "Genres"
                );


        }
    }
}
