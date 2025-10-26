using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventStore.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Project",
                schema: "read",
                columns: table => new
                {
                    AggregateRootId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Billable = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.AggregateRootId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCode",
                schema: "read",
                table: "Project",
                column: "Code",
                unique: true)
                .Annotation("SqlServer:Include", new[] { "Name", "Billable", "IsRemoved" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Project",
                schema: "read");
        }
    }
}
