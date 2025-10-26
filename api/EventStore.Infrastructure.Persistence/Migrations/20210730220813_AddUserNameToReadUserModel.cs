 using Microsoft.EntityFrameworkCore.Migrations;

namespace EventStore.Infrastructure.Persistence.Migrations
{
    public partial class AddUserNameToReadUserModel : Migration
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
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "read",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
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

            migrationBuilder.DropColumn(
                name: "UserName",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "read",
                table: "User",
                column: "AggregateRootId");
        }
    }
}
