using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ultimate_anime_api.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "StudioId", "Address", "Country", "Name" },
                values: new object[] { new Guid("edefae6b-643d-425d-82f5-5c62578dae9c"), "Tokyo", "Japan", "Kyoto Animation" });

            migrationBuilder.InsertData(
                table: "Studios",
                columns: new[] { "StudioId", "Address", "Country", "Name" },
                values: new object[] { new Guid("70a98b6c-7f0c-448a-bee4-35dd2bed9fae"), "Tokyo", "Japan", "Madhouse" });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "AnimeId", "Episodes", "Name", "ReleaseDate", "StudioId" },
                values: new object[] { new Guid("060c1a7f-509c-473c-937e-4f2e1b06f2b9"), 13, "Violet Evergarden", new DateTime(2018, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("edefae6b-643d-425d-82f5-5c62578dae9c") });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "AnimeId", "Episodes", "Name", "ReleaseDate", "StudioId" },
                values: new object[] { new Guid("8737ffe8-acd6-4412-be6c-a19c1ac51fa2"), 22, "Hyouka", new DateTime(2012, 4, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("edefae6b-643d-425d-82f5-5c62578dae9c") });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "AnimeId", "Episodes", "Name", "ReleaseDate", "StudioId" },
                values: new object[] { new Guid("2510d138-3262-4b90-81e1-250fafcdcd13"), 22, "One Punch Man", new DateTime(2015, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("70a98b6c-7f0c-448a-bee4-35dd2bed9fae") });

            migrationBuilder.InsertData(
                table: "Animes",
                columns: new[] { "AnimeId", "Episodes", "Name", "ReleaseDate", "StudioId" },
                values: new object[] { new Guid("e6f66a13-940a-4ede-82a9-bda5ac1e6e5e"), 148, "Hunter x Hunter (2011)", new DateTime(2011, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("70a98b6c-7f0c-448a-bee4-35dd2bed9fae") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "AnimeId",
                keyValue: new Guid("060c1a7f-509c-473c-937e-4f2e1b06f2b9"));

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "AnimeId",
                keyValue: new Guid("2510d138-3262-4b90-81e1-250fafcdcd13"));

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "AnimeId",
                keyValue: new Guid("8737ffe8-acd6-4412-be6c-a19c1ac51fa2"));

            migrationBuilder.DeleteData(
                table: "Animes",
                keyColumn: "AnimeId",
                keyValue: new Guid("e6f66a13-940a-4ede-82a9-bda5ac1e6e5e"));

            migrationBuilder.DeleteData(
                table: "Studios",
                keyColumn: "StudioId",
                keyValue: new Guid("70a98b6c-7f0c-448a-bee4-35dd2bed9fae"));

            migrationBuilder.DeleteData(
                table: "Studios",
                keyColumn: "StudioId",
                keyValue: new Guid("edefae6b-643d-425d-82f5-5c62578dae9c"));
        }
    }
}
