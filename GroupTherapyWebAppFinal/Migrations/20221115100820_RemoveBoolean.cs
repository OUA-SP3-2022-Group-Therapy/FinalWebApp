using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GroupTherapyWebAppFinal.Migrations
{
    public partial class RemoveBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RememberMe",
                table: "UserModels");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RememberMe",
                table: "UserModels",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
