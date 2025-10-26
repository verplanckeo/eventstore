using Microsoft.EntityFrameworkCore.Migrations;

namespace EventStore.Infrastructure.Persistence.Migrations
{
    public partial class AddUserNameIndexForReadUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "read",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "AggregateRootId",
                schema: "read",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "read",
                table: "User",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "read",
                table: "User",
                column: "AggregateRootId");

            migrationBuilder.CreateIndex(
                name: "IX_Username",
                schema: "read",
                table: "User",
                column: "UserName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "read",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Username",
                schema: "read",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                schema: "read",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AggregateRootId",
                schema: "read",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "read",
                table: "User",
                column: "UserName");
        }
    }
}
