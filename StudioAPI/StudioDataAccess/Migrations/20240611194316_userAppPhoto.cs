using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudioDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class userAppPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserPhoto",
                table: "AspNetUsers",
                type: "varchar(MAX)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserPhoto",
                table: "AspNetUsers");
        }
    }
}
