using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniBento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizeTitleAndDescriptionColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescriptionNormalized",
                table: "Medias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleNormalized",
                table: "Medias",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionNormalized",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "TitleNormalized",
                table: "Medias");
        }
    }
}
