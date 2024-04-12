using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatefiles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProfileImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicUrl",
                table: "ProfileImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MessageFiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicUrl",
                table: "MessageFiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FeedImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublicUrl",
                table: "FeedImages",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProfileImages");

            migrationBuilder.DropColumn(
                name: "PublicUrl",
                table: "ProfileImages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MessageFiles");

            migrationBuilder.DropColumn(
                name: "PublicUrl",
                table: "MessageFiles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FeedImages");

            migrationBuilder.DropColumn(
                name: "PublicUrl",
                table: "FeedImages");
        }
    }
}
