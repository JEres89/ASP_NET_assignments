using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class Role_and_User_seeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.AddColumn<int>(
                name: "Birthdate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d13ef063-96a1-48b3-949f-f628ba8a0b7a", "ee15d964-2a0e-4ab5-ba4f-011d78e5055d", "Admin", "ADMIN" },
                    { "43738b5d-1d9a-4337-a794-cc7d6221a3b1", "3c7495c7-3472-408b-abb5-2ffbbf727864", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Birthdate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4", 0, 0, "ec2c3248-72cf-4f65-8b85-6a2a23a79663", null, false, "Jens", "Eresund", false, null, null, "MASTERADMIN", "AQAAAAEAACcQAAAAEDIE7LW+Q3Z4HvoJ8lLdwUCPJJbLAi1pOItRs0aN2qcTsQRT+FbWkBWm4Af/o05RXg==", null, false, "ab0c9a99-f462-4439-99b6-19bd828db0b6", false, "MasterAdmin" });

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageId" },
                values: new object[] { 3, 3 });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4", "d13ef063-96a1-48b3-949f-f628ba8a0b7a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43738b5d-1d9a-4337-a794-cc7d6221a3b1");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4", "d13ef063-96a1-48b3-949f-f628ba8a0b7a" });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d13ef063-96a1-48b3-949f-f628ba8a0b7a");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4");

            migrationBuilder.DropColumn(
                name: "Birthdate",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageId" },
                values: new object[] { 3, 1 });
        }
    }
}
