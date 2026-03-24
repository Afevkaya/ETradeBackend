using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETradeBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_5_ProductsAndProductImageFilesManyToManyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "files",
                type: "numeric(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "files",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "files",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "productproductimagefiles",
                columns: table => new
                {
                    productimagefilesid = table.Column<Guid>(type: "uuid", nullable: false),
                    productsid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productproductimagefiles", x => new { x.productimagefilesid, x.productsid });
                    table.ForeignKey(
                        name: "FK_productproductimagefiles_files_productimagefilesid",
                        column: x => x.productimagefilesid,
                        principalTable: "files",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productproductimagefiles_products_productsid",
                        column: x => x.productsid,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_productproductimagefiles_productsid",
                table: "productproductimagefiles",
                column: "productsid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productproductimagefiles");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "files",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "files",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "files",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);
        }
    }
}
