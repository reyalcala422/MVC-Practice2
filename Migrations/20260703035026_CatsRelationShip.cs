using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPractice2.Migrations
{
    /// <inheritdoc />
    public partial class CatsRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CatUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Cats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cats_UserId",
                table: "Cats",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cats_Users_UserId",
                table: "Cats",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cats_Users_UserId",
                table: "Cats");

            migrationBuilder.DropIndex(
                name: "IX_Cats_UserId",
                table: "Cats");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Cats");

            migrationBuilder.CreateTable(
                name: "CatUser",
                columns: table => new
                {
                    CatsId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatUser", x => new { x.CatsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_CatUser_Cats_CatsId",
                        column: x => x.CatsId,
                        principalTable: "Cats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CatUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CatUser_UsersId",
                table: "CatUser",
                column: "UsersId");
        }
    }
}
