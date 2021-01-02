using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventStore.Infrastructure.Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "write");

            migrationBuilder.EnsureSchema(
                name: "read");

            migrationBuilder.CreateTable(
                name: "EventStore",
                schema: "write",
                columns: table => new
                {
                    Version = table.Column<int>(type: "int", nullable: false),
                    AggregateRootId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Sequence = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DomainEventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AggregateName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventStore", x => new { x.AggregateRootId, x.Version });
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "read",
                columns: table => new
                {
                    AggregateRootId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.AggregateRootId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventStore",
                schema: "write");

            migrationBuilder.DropTable(
                name: "User",
                schema: "read");
        }
    }
}
