using Microsoft.EntityFrameworkCore.Migrations;

namespace CarDealership.Data.Migrations
{
    public partial class UserFullNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dealers_AspNetUsers_UserId1",
                table: "Dealers");

            migrationBuilder.DropIndex(
                name: "IX_Dealers_UserId1",
                table: "Dealers");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Dealers");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "Dealers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dealers_UserId1",
                table: "Dealers",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Dealers_AspNetUsers_UserId1",
                table: "Dealers",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
