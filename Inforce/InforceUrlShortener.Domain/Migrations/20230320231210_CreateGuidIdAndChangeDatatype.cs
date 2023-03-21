using Microsoft.EntityFrameworkCore.Migrations;

namespace InforceUrlShortener.Domain.Migrations
{
    public partial class CreateGuidIdAndChangeDatatype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "ShortUrls",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedByUserId",
                table: "ShortUrls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "GuidId",
                table: "ShortUrls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShortUrls_OriginalUrl",
                table: "ShortUrls",
                column: "OriginalUrl",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShortUrls_OriginalUrl",
                table: "ShortUrls");

            migrationBuilder.DropColumn(
                name: "GuidId",
                table: "ShortUrls");

            migrationBuilder.AlterColumn<string>(
                name: "OriginalUrl",
                table: "ShortUrls",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedByUserId",
                table: "ShortUrls",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
