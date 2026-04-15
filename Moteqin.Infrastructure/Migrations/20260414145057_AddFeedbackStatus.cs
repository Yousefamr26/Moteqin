using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Moteqin.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFeedbackStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "status",
                table: "Feedbacks");
        }
    }
}
