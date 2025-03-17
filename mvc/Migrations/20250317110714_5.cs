using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvc.Migrations
{
    /// <inheritdoc />
    public partial class _5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Carts_CartId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Articles_CartId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Carts",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Carts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_ArticleId",
                table: "Carts",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Articles_ArticleId",
                table: "Carts",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Articles_ArticleId",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_ArticleId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Carts");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Carts",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_CartId",
                table: "Articles",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Carts_CartId",
                table: "Articles",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                table: "Carts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
