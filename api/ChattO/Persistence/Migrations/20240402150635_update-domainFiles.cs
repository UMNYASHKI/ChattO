using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updatedomainFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ProfileImages");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "ProfileImages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MessageFiles");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "MessageFiles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FeedImages");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "FeedImages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ProfileImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "ProfileImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MessageFiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "MessageFiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FeedImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "FeedImages",
                type: "text",
                nullable: true);
        }
    }
}
