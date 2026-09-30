using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lhhLap06.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lhhCategory",
                columns: table => new
                {
                    categoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    categoryName = table.Column<string>(type: "nvarchar(10)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lhhCategory", x => x.categoryId);
                });

            migrationBuilder.CreateTable(
                name: "lhhProduct",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Descriptions = table.Column<string>(type: "ntext", maxLength: 1000, nullable: false),
                    CategryId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lhhProduct", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_lhhProduct_lhhCategory_categoryId",
                        column: x => x.categoryId,
                        principalTable: "lhhCategory",
                        principalColumn: "categoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lhhProduct_categoryId",
                table: "lhhProduct",
                column: "categoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lhhProduct");

            migrationBuilder.DropTable(
                name: "lhhCategory");
        }
    }
}
