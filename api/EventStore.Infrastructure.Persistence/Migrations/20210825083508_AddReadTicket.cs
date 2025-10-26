using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventStore.Infrastructure.Persistence.Migrations
{
    public partial class AddReadTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ticket",
                schema: "read",
                columns: table => new
                {
                    AggregateRootId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TicketState = table.Column<int>(type: "int", nullable: false),
                    TicketType = table.Column<int>(type: "int", nullable: false),
                    TicketPriority = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ticket", x => x.AggregateRootId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketUserName",
                schema: "read",
                table: "Ticket",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL")
                .Annotation("SqlServer:Include", new[] { "UserId", "Title", "TicketPriority", "TicketState", "TicketType" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ticket",
                schema: "read");
        }
    }
}
