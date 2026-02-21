using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniBento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddPinStatusToCollection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPinned",
                table: "Collections",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPinned",
                table: "Collections");
        }
    }
}
