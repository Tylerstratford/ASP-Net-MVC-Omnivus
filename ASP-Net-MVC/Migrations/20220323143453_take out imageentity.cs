using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP_Net_MVC.Migrations
{
    public partial class takeoutimageentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfileImages");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Profiles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Profiles");

            migrationBuilder.CreateTable(
                name: "ProfileImages",
                columns: table => new
                {
                    FileName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfileImages", x => x.FileName);
                });
        }
    }
}
