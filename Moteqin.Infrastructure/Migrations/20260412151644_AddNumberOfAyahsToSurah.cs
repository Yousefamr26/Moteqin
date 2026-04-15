using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moteqin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberOfAyahsToSurah : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfAyahs",
                table: "Surahs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfAyahs",
                table: "Surahs");
        }
    }
}
