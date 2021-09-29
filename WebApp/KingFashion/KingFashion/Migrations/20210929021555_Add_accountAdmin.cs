using Microsoft.EntityFrameworkCore.Migrations;

namespace KingFashion.Migrations
{
    public partial class Add_accountAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "23c39696-d9f7-4425-bf22-003472af407b", "kingfashionc0321g1@gmail.com", "kingfashionc0321g1@gmail.com", "kingfashionc0321g1@gmail.com", "AQAAAAEAACcQAAAAEDWcwayb2p98Qn/550/+qDD9GXMNBaAWEUXz3fvjfk3QiWlIhgfIMHSXBzVG70xFyg==", "d32a8095-85f1-4078-8421-4e7c785a48fd", "kingfashionc0321g1@gmail.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "2c0fca4e-9376-4a70-bcc6-35bebe497866",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "d28d2847-9645-4b51-b255-1c33f3e863b9", "buu.nguyen@gmail.com", "buu.nguyen@gmail.com", "buu.nguyen@gmail.com", "AQAAAAEAACcQAAAAENgzUP5b2jEBXYI6EaziBQ2E8cGgOvK+BR6XUfFLBf63u+0pxXqKcR8vWEBASzmzSw==", "9b182938-f40d-40cd-b5f1-0aed3635bab9", "buu.nguyen@gmail.com" });
        }
    }
}
