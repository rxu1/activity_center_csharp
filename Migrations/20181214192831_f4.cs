using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetBelt.Migrations
{
    public partial class f4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RemoveDate",
                table: "Activities",
                newName: "Time");

            migrationBuilder.AddColumn<int>(
                name: "DurationType",
                table: "Activities",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationType",
                table: "Activities");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Activities",
                newName: "RemoveDate");
        }
    }
}
