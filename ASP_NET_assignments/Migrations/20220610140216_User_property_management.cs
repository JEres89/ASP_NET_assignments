using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class User_property_management : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.AddColumn<int>(
                name: "UserDetailsColumns",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43738b5d-1d9a-4337-a794-cc7d6221a3b1",
                column: "ConcurrencyStamp",
                value: "ba52ae1c-c5a1-44b9-af21-cdde6758a42f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d13ef063-96a1-48b3-949f-f628ba8a0b7a",
                column: "ConcurrencyStamp",
                value: "0f1044dd-bbf7-4063-9e8a-2d5ebea3ed27");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ab1a459-726c-4a66-8d04-20644b3a4b89", "AQAAAAEAACcQAAAAEIPp96KUMR5P7PolFiVQwB9nynqshiUNuG1Cp7LmYlUg4KaTDW9hnFjly2wY1rz2Xg==", "9f748168-d9e0-4ed4-b090-db6cecc4227c" });

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageId" },
                values: new object[] { 3, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DropColumn(
                name: "UserDetailsColumns",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43738b5d-1d9a-4337-a794-cc7d6221a3b1",
                column: "ConcurrencyStamp",
                value: "3c7495c7-3472-408b-abb5-2ffbbf727864");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d13ef063-96a1-48b3-949f-f628ba8a0b7a",
                column: "ConcurrencyStamp",
                value: "ee15d964-2a0e-4ab5-ba4f-011d78e5055d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ec2c3248-72cf-4f65-8b85-6a2a23a79663", "AQAAAAEAACcQAAAAEDIE7LW+Q3Z4HvoJ8lLdwUCPJJbLAi1pOItRs0aN2qcTsQRT+FbWkBWm4Af/o05RXg==", "ab0c9a99-f462-4439-99b6-19bd828db0b6" });

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageId" },
                values: new object[] { 3, 3 });
        }
    }
}
