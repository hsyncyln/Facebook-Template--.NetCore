using Microsoft.EntityFrameworkCore.Migrations;

namespace Facebook.Migrations
{
    public partial class Facebookvs9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FriendId",
                table: "Friends");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FriendId",
                table: "Friends",
                nullable: false,
                defaultValue: 0);
        }
    }
}
