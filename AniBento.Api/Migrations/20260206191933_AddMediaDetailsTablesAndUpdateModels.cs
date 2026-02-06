using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AniBento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddMediaDetailsTablesAndUpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EpisodeOrChapterCount",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Medias");

            migrationBuilder.DropColumn(
                name: "Studio",
                table: "Medias");

            migrationBuilder.RenameColumn(
                name: "enteredAt",
                table: "Medias",
                newName: "EnteredAt");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EnteredAt",
                table: "Medias",
                type: "timestamptz",
                nullable: false,
                defaultValueSql: "now()",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "ReleaseDate",
                table: "Medias",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "MediaType",
                table: "Medias",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "AnimeDetails",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "integer", nullable: false),
                    Studio = table.Column<string>(type: "text", nullable: true),
                    EpisodeCount = table.Column<int>(type: "integer", nullable: false),
                    Genres = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeDetails", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_AnimeDetails_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaDetails",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "integer", nullable: false),
                    Publisher = table.Column<string>(type: "text", nullable: true),
                    ChapterCount = table.Column<int>(type: "integer", nullable: false),
                    VolumeCount = table.Column<int>(type: "integer", nullable: false),
                    Genres = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaDetails", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_MangaDetails_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieDetails",
                columns: table => new
                {
                    MediaId = table.Column<int>(type: "integer", nullable: false),
                    Studio = table.Column<string>(type: "text", nullable: true),
                    Directors = table.Column<string[]>(type: "text[]", nullable: true),
                    Genres = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDetails", x => x.MediaId);
                    table.ForeignKey(
                        name: "FK_MovieDetails_Medias_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Medias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeDetails");

            migrationBuilder.DropTable(
                name: "MangaDetails");

            migrationBuilder.DropTable(
                name: "MovieDetails");

            migrationBuilder.RenameColumn(
                name: "EnteredAt",
                table: "Medias",
                newName: "enteredAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReleaseDate",
                table: "Medias",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MediaType",
                table: "Medias",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "enteredAt",
                table: "Medias",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamptz",
                oldDefaultValueSql: "now()");

            migrationBuilder.AddColumn<int>(
                name: "EpisodeOrChapterCount",
                table: "Medias",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Medias",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Studio",
                table: "Medias",
                type: "text",
                nullable: true);
        }
    }
}
