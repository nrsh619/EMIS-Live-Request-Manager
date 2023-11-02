using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiveQueryManager.DataAccess.Context.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AttachmentType",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentType_AttachmentId", x => x.AttachmentId);
                });

            migrationBuilder.CreateTable(
                name: "LiveDataRequests",
                columns: table => new
                {
                    RequestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedBy = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    AssignedTo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CreationDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    StatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LiveDataRequest_RequestId", x => x.RequestId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status_StatusId", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    AttachmentId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Path = table.Column<string>(type: "text", nullable: false),
                    AttachmentType = table.Column<int>(type: "integer", nullable: false),
                    RequestId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.AttachmentId);
                    table.ForeignKey(
                        name: "FK_Attachment_LiveDataRequest_RequestId",
                        column: x => x.RequestId,
                        principalTable: "LiveDataRequests",
                        principalColumn: "RequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AttachmentType",
                columns: new[] { "AttachmentId", "Description" },
                values: new object[,]
                {
                    { 1, "Request" },
                    { 2, "Response" },
                    { 3, "Error" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "Description" },
                values: new object[,]
                {
                    { 1, "Created" },
                    { 2, "InProgress" },
                    { 3, "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_RequestId",
                table: "Attachment",
                column: "RequestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "AttachmentType");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "LiveDataRequests");
        }
    }
}
