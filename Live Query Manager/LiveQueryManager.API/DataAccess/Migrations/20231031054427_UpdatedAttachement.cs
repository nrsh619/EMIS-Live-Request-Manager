using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveQueryManager.API.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAttachement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Attachments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Attachments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
