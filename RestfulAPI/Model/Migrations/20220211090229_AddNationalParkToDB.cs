using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestfulAPI.Migrations
{
    public partial class AddNationalParkToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "nationalParks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Established = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nationalParks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nationalParks");
        }
    }
}
