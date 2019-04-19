using Microsoft.EntityFrameworkCore.Migrations;

namespace DotNetBelt.Migrations
{
    public partial class f5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DurationType",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "DurationType",
                table: "Activities",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
