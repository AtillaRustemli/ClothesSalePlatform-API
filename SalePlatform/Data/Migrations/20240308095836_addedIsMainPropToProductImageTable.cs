using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesSalePlatform.Data.Migrations
{
    public partial class addedIsMainPropToProductImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "120e97c3-b789-496f-84de-da4821560a47", "ca8af5c8-8b11-4ba5-95e1-6b4186052538", "Member", "MEMBER" },
                    { "f25c2da9-85c2-43d6-8428-f9d112a804c0", "2c8f2ecd-facc-4b58-9060-c051078d79e9", "Admin", "ADMIN" },
                    { "f6b8d32b-c289-44ed-990e-95aeeb0176f4", "2008c958-906b-4b4c-9fda-644193c68a56", "SuperAdmin", "SUPERADMIN" }
                });

            migrationBuilder.UpdateData(
                table: "ProductImages",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsMain",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "120e97c3-b789-496f-84de-da4821560a47");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f25c2da9-85c2-43d6-8428-f9d112a804c0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f6b8d32b-c289-44ed-990e-95aeeb0176f4");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductImages");

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
    }
}
