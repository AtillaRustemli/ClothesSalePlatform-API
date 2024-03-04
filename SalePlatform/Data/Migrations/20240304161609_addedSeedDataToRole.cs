using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesSalePlatform.Data.Migrations
{
    public partial class addedSeedDataToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3034069b-ff9b-49ee-bcbe-19815b156b23", "20e96ac3-ebc1-4169-880b-e0dd7cbca39c", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "45934cce-48b7-4c11-9174-222d4eb0761e", "6a088057-cc4c-4500-b3c5-c2786b2c2f11", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ce301c20-9a47-41ed-9644-f9c33393e856", "a0622c28-0764-4ed4-acd0-1132b7740f61", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3034069b-ff9b-49ee-bcbe-19815b156b23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45934cce-48b7-4c11-9174-222d4eb0761e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ce301c20-9a47-41ed-9644-f9c33393e856");
        }
    }
}
