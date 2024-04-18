using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KoncarTaskTriangles.Migrations
{
    /// <inheritdoc />
    public partial class updateTriangleModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Triangles_Users_UserModelId",
                table: "Triangles");

            migrationBuilder.DropIndex(
                name: "IX_Triangles_UserModelId",
                table: "Triangles");

            migrationBuilder.DropColumn(
                name: "UserModelId",
                table: "Triangles");

            migrationBuilder.CreateIndex(
                name: "IX_Triangles_UserId",
                table: "Triangles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Triangles_Users_UserId",
                table: "Triangles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Triangles_Users_UserId",
                table: "Triangles");

            migrationBuilder.DropIndex(
                name: "IX_Triangles_UserId",
                table: "Triangles");

            migrationBuilder.AddColumn<int>(
                name: "UserModelId",
                table: "Triangles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Triangles_UserModelId",
                table: "Triangles",
                column: "UserModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Triangles_Users_UserModelId",
                table: "Triangles",
                column: "UserModelId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
