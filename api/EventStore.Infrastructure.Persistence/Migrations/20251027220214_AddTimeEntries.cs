using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeEntry",
                schema: "read",
                columns: table => new
                {
                    AggregateRootId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ActivityType = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntry", x => x.AggregateRootId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_From",
                schema: "read",
                table: "TimeEntry",
                column: "From");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_IsRemoved_From",
                schema: "read",
                table: "TimeEntry",
                columns: new[] { "IsRemoved", "From" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_ProjectId",
                schema: "read",
                table: "TimeEntry",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntry_UserId",
                schema: "read",
                table: "TimeEntry",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntry",
                schema: "read");
        }
    }
}
