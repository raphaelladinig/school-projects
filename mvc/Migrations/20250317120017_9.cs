using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvc.Migrations
{
    /// <inheritdoc />
    public partial class _9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Orders_OrderId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_OrderId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Quantities",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Articles");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ArticleId",
                table: "Orders",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Articles_ArticleId",
                table: "Orders",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "ArticleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Articles_ArticleId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ArticleId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "Quantities",
                table: "Orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_OrderId",
                table: "Articles",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Orders_OrderId",
                table: "Articles",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}
