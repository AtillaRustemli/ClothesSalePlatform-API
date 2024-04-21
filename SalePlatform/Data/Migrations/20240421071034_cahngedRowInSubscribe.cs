using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesSalePlatform.Data.Migrations
{
    public partial class cahngedRowInSubscribe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "290d12d1-6975-43c1-bbd7-366e6ef8a2e1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3d46120-cd74-4997-848f-805ebd78ece2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea111dd4-3829-4ca9-8912-deebab62ae55");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Subscribers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "20f229dc-f418-4361-a6cc-e7036c3e1092", "3510f4e1-394d-4103-bb6d-078d29383df7", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "68e2c2c5-3473-4a4f-b282-6991adddbdfa", "bfe10442-365e-44e2-98ce-f241f1e07924", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c5c302d6-6b9c-4c14-9e1c-190b3c0b7163", "8098937b-6c27-456a-979b-e9adcf4014d7", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "20f229dc-f418-4361-a6cc-e7036c3e1092");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "68e2c2c5-3473-4a4f-b282-6991adddbdfa");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c5c302d6-6b9c-4c14-9e1c-190b3c0b7163");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Subscribers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "290d12d1-6975-43c1-bbd7-366e6ef8a2e1", "cfb35265-4eca-4295-b304-6eafde72a3d2", "SuperAdmin", "SUPERADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3d46120-cd74-4997-848f-805ebd78ece2", "c0c6e21c-614d-4239-b635-cc64835d06a2", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ea111dd4-3829-4ca9-8912-deebab62ae55", "5f5658d0-dc11-4843-ae8f-6d574d711998", "Admin", "ADMIN" });
        }
    }
}
