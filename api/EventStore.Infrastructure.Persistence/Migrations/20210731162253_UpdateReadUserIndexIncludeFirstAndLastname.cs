using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventStore.Infrastructure.Persistence.Migrations
{
    public partial class UpdateReadUserIndexIncludeFirstAndLastname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Username",
                schema: "read",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                schema: "read",
                table: "User",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "FirstName", "LastName" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Username",
                schema: "read",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                schema: "read",
                table: "User",
                column: "UserName");
        }
    }
}
