using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShoppingStore.Data.Migrations
{
    public partial class AssignAdminToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into [Security].[UserRoles] (UserId,RoleId) select '8c2996df-d0ad-44fb-9e7d-f319a1a3c872',Id from [Security].[Roles]");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Security].[UserRoles] where UserId='8c2996df-d0ad-44fb-9e7d-f319a1a3c872'");

        }
    }
}
