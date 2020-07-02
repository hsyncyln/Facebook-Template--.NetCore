using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Migrations
{
    public partial class Facebookvs11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_OtherUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_OtherUserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "OtherUserId",
                table: "Messages",
                newName: "FriendId");

            migrationBuilder.AddColumn<int>(
                name: "FriendUserUserId",
                table: "Messages",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_FriendUserUserId",
                table: "Messages",
                column: "FriendUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_FriendUserUserId",
                table: "Messages",
                column: "FriendUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_FriendUserUserId",
                table: "Messages");

            migrationBuilder.DropIndex(
                name: "IX_Messages_FriendUserUserId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "FriendUserUserId",
                table: "Messages");

            migrationBuilder.RenameColumn(
                name: "FriendId",
                table: "Messages",
                newName: "OtherUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_OtherUserId",
                table: "Messages",
                column: "OtherUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_OtherUserId",
                table: "Messages",
                column: "OtherUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
