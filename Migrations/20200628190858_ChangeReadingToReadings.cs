using Microsoft.EntityFrameworkCore.Migrations;

namespace BookClubApi.Migrations
{
    public partial class ChangeReadingToReadings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("Reading", null, "Readings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable("Readings", null, "Reading");
        }
    }
}
