using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Net_MVC.Migrations
{
    public partial class countryadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Profiles",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Profiles");
        }
    }
}
