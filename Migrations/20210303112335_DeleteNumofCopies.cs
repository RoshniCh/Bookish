using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookish.Migrations
{
    public partial class DeleteNumofCopies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumCopies",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumCopies",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
