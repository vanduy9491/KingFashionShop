using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KingFashion.Migrations
{
    public partial class AddUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c0c6661b-0964-4e62-8083-3cac6a6741ec", "1", "SystemAdmin", "SystemAdmin" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "32ffd287-205f-43a2-9f0d-80sc5309fb47", "2", "Customer", "Customer" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "Admin", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Intro", "LastLogin", "LaststName", "LockoutEnabled", "LockoutEnd", "MiddleName", "Mobile", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "Profile", "RegisteredAt", "SecurityStamp", "TwoFactorEnabled", "UserName", "Vendor" },
                values: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", 0, false, "d28d2847-9645-4b51-b255-1c33f3e863b9", "buu.nguyen@gmail.com", false, null, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, null, null, null, "buu.nguyen@gmail.com", "buu.nguyen@gmail.com", "AQAAAAEAACcQAAAAENgzUP5b2jEBXYI6EaziBQ2E8cGgOvK+BR6XUfFLBf63u+0pxXqKcR8vWEBASzmzSw==", null, false, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9b182938-f40d-40cd-b5f1-0aed3635bab9", false, "buu.nguyen@gmail.com", false });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", "c0c6661b-0964-4e62-8083-3cac6a6741ec" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "32ffd287-205f-43a2-9f0d-80sc5309fb47");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2c0fca4e-9376-4a70-bcc6-35bebe497866", "c0c6661b-0964-4e62-8083-3cac6a6741ec" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c0c6661b-0964-4e62-8083-3cac6a6741ec");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2c0fca4e-9376-4a70-bcc6-35bebe497866");
        }
    }
}
