using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelLayer.Migrations
{
    public partial class ModifiedLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Logs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Logs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
