using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventStore.Infrastructure.Persistence.Migrations
{
    public partial class AddVersionToReadTicketEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketUserName",
                schema: "read",
                table: "Ticket");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                schema: "read",
                table: "Ticket",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TicketUserName",
                schema: "read",
                table: "Ticket",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "UserId", "Title", "TicketPriority", "TicketState", "TicketType", "Version" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TicketUserName",
                schema: "read",
                table: "Ticket");

            migrationBuilder.DropColumn(
                name: "Version",
                schema: "read",
                table: "Ticket");

            migrationBuilder.CreateIndex(
                name: "IX_TicketUserName",
                schema: "read",
                table: "Ticket",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "UserId", "Title", "TicketPriority", "TicketState", "TicketType" });
        }
    }
}
