using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmaApp.Migrations
{
    public partial class frist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c_name",
                table: "Sales");

            migrationBuilder.AddColumn<int>(
                name: "c_id",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    c_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    c_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    c_age = table.Column<int>(type: "int", nullable: false),
                    c_sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    c_phno = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.c_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_c_id",
                table: "Sales",
                column: "c_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customer_c_id",
                table: "Sales",
                column: "c_id",
                principalTable: "Customer",
                principalColumn: "c_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customer_c_id",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropIndex(
                name: "IX_Sales_c_id",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "c_id",
                table: "Sales");

            migrationBuilder.AddColumn<string>(
                name: "c_name",
                table: "Sales",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
