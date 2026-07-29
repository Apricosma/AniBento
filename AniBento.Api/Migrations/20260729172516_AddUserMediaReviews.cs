using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniBento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserMediaReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Review",
                table: "UserMedias",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "UserMedias");
        }
    }
}
