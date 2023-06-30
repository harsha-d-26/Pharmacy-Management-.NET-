using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmaApp.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    med_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    med_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<double>(type: "float", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    rack = table.Column<int>(type: "int", nullable: false),
                    mfg_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    exp_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.med_id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    sup_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sup_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_no = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.sup_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    purchase_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchase_qty = table.Column<int>(type: "int", nullable: false),
                    purchase_amt = table.Column<double>(type: "float", nullable: false),
                    purchase_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    med_id = table.Column<int>(type: "int", nullable: false),
                    sup_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purchase", x => x.purchase_id);
                    table.ForeignKey(
                        name: "FK_Purchase_Medicines_med_id",
                        column: x => x.med_id,
                        principalTable: "Medicines",
                        principalColumn: "med_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Purchase_Suppliers_sup_id",
                        column: x => x.sup_id,
                        principalTable: "Suppliers",
                        principalColumn: "sup_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    sale_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    total_amt = table.Column<double>(type: "float", nullable: false),
                    c_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    med_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.sale_id);
                    table.ForeignKey(
                        name: "FK_Sales_Medicines_med_id",
                        column: x => x.med_id,
                        principalTable: "Medicines",
                        principalColumn: "med_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_med_id",
                table: "Purchase",
                column: "med_id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchase_sup_id",
                table: "Purchase",
                column: "sup_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_med_id",
                table: "Sales",
                column: "med_id");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_user_id",
                table: "Sales",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Purchase");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
