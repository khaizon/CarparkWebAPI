using Microsoft.EntityFrameworkCore.Migrations;

namespace CarparkAPIApp.Migrations
{
    public partial class addedcontactnumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContactNumber",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactNumber",
                table: "AspNetUsers");
        }
    }
}
