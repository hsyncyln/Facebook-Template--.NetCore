using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Migrations
{
    public partial class Facebookvs10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_FriendUserUserId",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "FriendUserUserId",
                table: "Friends",
                newName: "FriendUser2UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_FriendUserUserId",
                table: "Friends",
                newName: "IX_Friends_FriendUser2UserId");

            migrationBuilder.AddColumn<int>(
                name: "FriendUser1UserId",
                table: "Friends",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendUser1UserId",
                table: "Friends",
                column: "FriendUser1UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_FriendUser1UserId",
                table: "Friends",
                column: "FriendUser1UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_FriendUser2UserId",
                table: "Friends",
                column: "FriendUser2UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_FriendUser1UserId",
                table: "Friends");

            migrationBuilder.DropForeignKey(
                name: "FK_Friends_Users_FriendUser2UserId",
                table: "Friends");

            migrationBuilder.DropIndex(
                name: "IX_Friends_FriendUser1UserId",
                table: "Friends");

            migrationBuilder.DropColumn(
                name: "FriendUser1UserId",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "FriendUser2UserId",
                table: "Friends",
                newName: "FriendUserUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Friends_FriendUser2UserId",
                table: "Friends",
                newName: "IX_Friends_FriendUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friends_Users_FriendUserUserId",
                table: "Friends",
                column: "FriendUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
