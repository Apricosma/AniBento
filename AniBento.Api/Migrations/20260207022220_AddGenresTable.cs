using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AniBento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddGenresTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "MovieDetails");

            migrationBuilder.DropColumn(
                name: "Genres",
                table: "MangaDetails");

            migrationBuilder.DropColumn(
                name: "Genres",
                table: "AnimeDetails");

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NameNormalized = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MediaGenre",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "integer", nullable: false),
                    GenreId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaGenre", x => new { x.MediaId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_MediaGenre_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MediaGenre_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_NameNormalized",
                table: "Genres",
                column: "NameNormalized",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MediaGenre_GenreId",
                table: "MediaGenre",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MediaGenre_MediaId",
                table: "MediaGenre",
                column: "MediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MediaGenre");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.AddColumn<string[]>(
                name: "Genres",
                table: "MovieDetails",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "Genres",
                table: "MangaDetails",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "Genres",
                table: "AnimeDetails",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }
    }
}
