using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestfulAPI.Migrations
{
    public partial class AddTrailsTodb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "trails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Diffculty = table.Column<int>(nullable: false),
                    NationalParkId = table.Column<int>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trails_nationalParks_NationalParkId",
                        column: x => x.NationalParkId,
                        principalTable: "nationalParks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_trails_NationalParkId",
                table: "trails",
                column: "NationalParkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trails");
        }
    }
}
