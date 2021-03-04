using Microsoft.EntityFrameworkCore.Migrations;

namespace Bookish.Migrations
{
    public partial class foreignkeycopy2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Copies_Book_Member_BookId",
                table: "Copies_Book_Member",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Copies_Book_Member_MemberId",
                table: "Copies_Book_Member",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Book_Member_Books_BookId",
                table: "Copies_Book_Member",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Copies_Book_Member_Members_MemberId",
                table: "Copies_Book_Member",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "MemberId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Book_Member_Books_BookId",
                table: "Copies_Book_Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Copies_Book_Member_Members_MemberId",
                table: "Copies_Book_Member");

            migrationBuilder.DropIndex(
                name: "IX_Copies_Book_Member_BookId",
                table: "Copies_Book_Member");

            migrationBuilder.DropIndex(
                name: "IX_Copies_Book_Member_MemberId",
                table: "Copies_Book_Member");
        }
    }
}
