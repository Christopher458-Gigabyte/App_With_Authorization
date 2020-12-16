using Microsoft.EntityFrameworkCore.Migrations;

namespace App_With_Authorization.Data.Migrations
{
    public partial class etwas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerID",
                table: "Module",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Module");
        }
    }
}
