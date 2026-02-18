using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AniBento.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUserMediaCollections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMedias_AspNetUsers_ApplicationUserId",
                table: "UserMedias");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMedias_Medias_MediaId",
                table: "UserMedias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMedias",
                table: "UserMedias");

            migrationBuilder.DropIndex(
                name: "IX_UserMedias_ApplicationUserId",
                table: "UserMedias");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserMedias");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserMedias",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMedias",
                table: "UserMedias",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Collections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsPrivate = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collections_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CollectionItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CollectionId = table.Column<int>(type: "integer", nullable: false),
                    UserMediaId = table.Column<int>(type: "integer", nullable: false),
                    AddedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollectionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CollectionItems_Collections_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollectionItems_UserMedias_UserMediaId",
                        column: x => x.UserMediaId,
                        principalTable: "UserMedias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMedias_UserId_MediaId",
                table: "UserMedias",
                columns: new[] { "UserId", "MediaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_CollectionId",
                table: "CollectionItems",
                column: "CollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_CollectionId_UserMediaId",
                table: "CollectionItems",
                columns: new[] { "CollectionId", "UserMediaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CollectionItems_UserMediaId",
                table: "CollectionItems",
                column: "UserMediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_UserId",
                table: "Collections",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMedias_Medias_MediaId",
                table: "UserMedias",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMedias_Medias_MediaId",
                table: "UserMedias");

            migrationBuilder.DropTable(
                name: "CollectionItems");

            migrationBuilder.DropTable(
                name: "Collections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserMedias",
                table: "UserMedias");

            migrationBuilder.DropIndex(
                name: "IX_UserMedias_UserId_MediaId",
                table: "UserMedias");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserMedias");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "UserMedias",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserMedias",
                table: "UserMedias",
                columns: new[] { "UserId", "MediaId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserMedias_ApplicationUserId",
                table: "UserMedias",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMedias_AspNetUsers_ApplicationUserId",
                table: "UserMedias",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMedias_Medias_MediaId",
                table: "UserMedias",
                column: "MediaId",
                principalTable: "Medias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
