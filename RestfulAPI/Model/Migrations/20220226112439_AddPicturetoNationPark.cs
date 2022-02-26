using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RestfulAPI.Migrations
{
    public partial class AddPicturetoNationPark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "nationalParks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "nationalParks");
        }
    }
}
