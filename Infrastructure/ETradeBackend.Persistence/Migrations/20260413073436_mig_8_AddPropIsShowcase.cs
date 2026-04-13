using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETradeBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_8_AddPropIsShowcase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_productproductimagefiles",
                table: "productproductimagefiles");

            migrationBuilder.DropIndex(
                name: "IX_productproductimagefiles_productsid",
                table: "productproductimagefiles");

            migrationBuilder.AddColumn<bool>(
                name: "isshowcase",
                table: "productproductimagefiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_productproductimagefiles",
                table: "productproductimagefiles",
                columns: new[] { "productsid", "productimagefilesid" });

            migrationBuilder.CreateIndex(
                name: "IX_productproductimagefiles_productimagefilesid",
                table: "productproductimagefiles",
                column: "productimagefilesid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_productproductimagefiles",
                table: "productproductimagefiles");

            migrationBuilder.DropIndex(
                name: "IX_productproductimagefiles_productimagefilesid",
                table: "productproductimagefiles");

            migrationBuilder.DropColumn(
                name: "isshowcase",
                table: "productproductimagefiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_productproductimagefiles",
                table: "productproductimagefiles",
                columns: new[] { "productimagefilesid", "productsid" });

            migrationBuilder.CreateIndex(
                name: "IX_productproductimagefiles_productsid",
                table: "productproductimagefiles",
                column: "productsid");
        }
    }
}
