using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETradeBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_2_RenamePropertiesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderproduct_orders_ordersıd",
                table: "orderproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_orderproduct_products_productsıd",
                table: "orderproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_customerıd",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "ısdeleted",
                table: "products",
                newName: "isdeleted");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "products",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "ısdeleted",
                table: "orders",
                newName: "isdeleted");

            migrationBuilder.RenameColumn(
                name: "customerıd",
                table: "orders",
                newName: "customerid");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_orders_customerıd",
                table: "orders",
                newName: "IX_orders_customerid");

            migrationBuilder.RenameColumn(
                name: "productsıd",
                table: "orderproduct",
                newName: "productsid");

            migrationBuilder.RenameColumn(
                name: "ordersıd",
                table: "orderproduct",
                newName: "ordersid");

            migrationBuilder.RenameIndex(
                name: "IX_orderproduct_productsıd",
                table: "orderproduct",
                newName: "IX_orderproduct_productsid");

            migrationBuilder.RenameColumn(
                name: "ısdeleted",
                table: "customers",
                newName: "isdeleted");

            migrationBuilder.RenameColumn(
                name: "ıd",
                table: "customers",
                newName: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_orderproduct_orders_ordersid",
                table: "orderproduct",
                column: "ordersid",
                principalTable: "orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderproduct_products_productsid",
                table: "orderproduct",
                column: "productsid",
                principalTable: "products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_customerid",
                table: "orders",
                column: "customerid",
                principalTable: "customers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderproduct_orders_ordersid",
                table: "orderproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_orderproduct_products_productsid",
                table: "orderproduct");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_customers_customerid",
                table: "orders");

            migrationBuilder.RenameColumn(
                name: "isdeleted",
                table: "products",
                newName: "ısdeleted");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "products",
                newName: "ıd");

            migrationBuilder.RenameColumn(
                name: "isdeleted",
                table: "orders",
                newName: "ısdeleted");

            migrationBuilder.RenameColumn(
                name: "customerid",
                table: "orders",
                newName: "customerıd");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "orders",
                newName: "ıd");

            migrationBuilder.RenameIndex(
                name: "IX_orders_customerid",
                table: "orders",
                newName: "IX_orders_customerıd");

            migrationBuilder.RenameColumn(
                name: "productsid",
                table: "orderproduct",
                newName: "productsıd");

            migrationBuilder.RenameColumn(
                name: "ordersid",
                table: "orderproduct",
                newName: "ordersıd");

            migrationBuilder.RenameIndex(
                name: "IX_orderproduct_productsid",
                table: "orderproduct",
                newName: "IX_orderproduct_productsıd");

            migrationBuilder.RenameColumn(
                name: "isdeleted",
                table: "customers",
                newName: "ısdeleted");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "customers",
                newName: "ıd");

            migrationBuilder.AddForeignKey(
                name: "FK_orderproduct_orders_ordersıd",
                table: "orderproduct",
                column: "ordersıd",
                principalTable: "orders",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderproduct_products_productsıd",
                table: "orderproduct",
                column: "productsıd",
                principalTable: "products",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_customers_customerıd",
                table: "orders",
                column: "customerıd",
                principalTable: "customers",
                principalColumn: "ıd",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
