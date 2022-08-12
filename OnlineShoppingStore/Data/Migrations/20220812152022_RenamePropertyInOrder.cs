using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShoppingStore.Data.Migrations
{
    public partial class RenamePropertyInOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PaymentType",
                table: "Orders",
                newName: "CreditcardNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreditcardNumber",
                table: "Orders",
                newName: "PaymentType");
        }
    }
}
