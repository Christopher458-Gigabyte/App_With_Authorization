using Microsoft.EntityFrameworkCore.Migrations;

namespace App_With_Authorization.Data.Migrations
{
    public partial class etwas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Module",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Module");
        }
    }
}
