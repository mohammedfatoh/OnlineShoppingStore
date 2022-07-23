using Microsoft.EntityFrameworkCore.Migrations;
using OnlineShoppingStore.Models;

#nullable disable

namespace OnlineShoppingStore.Data.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[] { Guid.NewGuid().ToString(), RolesNames.RoleUser, RolesNames.RoleUser.ToUpper(), Guid.NewGuid().ToString() },
                schema: "Security"
                );

            migrationBuilder.InsertData(
               table: "Roles",
               columns: new[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), RolesNames.RoleAdmin, RolesNames.RoleAdmin.ToUpper(), Guid.NewGuid().ToString() },
               schema: "Security"
               );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[Roles]");
        }
    }
}
