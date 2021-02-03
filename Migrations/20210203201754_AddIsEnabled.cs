using Microsoft.EntityFrameworkCore.Migrations;

namespace cognito_dotnet_angular.Migrations
{
    public partial class AddIsEnabled : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Users");
        }
    }
}
