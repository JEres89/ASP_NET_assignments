using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class Somethingmightbemissing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "43738b5d-1d9a-4337-a794-cc7d6221a3b1",
                column: "ConcurrencyStamp",
                value: "40945b65-0898-4fe4-b3e7-497509107b88");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d13ef063-96a1-48b3-949f-f628ba8a0b7a",
                column: "ConcurrencyStamp",
                value: "2b4257c3-7385-4086-8af7-e815b1405334");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "72f3b5bb-60e5-4c3a-9857-5b8669db0bf4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de038db2-ba8c-4c02-a472-e3ce63bb4e7a", "AQAAAAEAACcQAAAAEARMOmnkFdPOoZqFPQVrNmwHAqyIMtVmnHMiiQortOEc6kmFbNMLc9ZtJMRqxLHWVA==", "87feb9a1-52b1-48f1-b5eb-292c6e41f1db" });

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageId" },
                values: new object[] { 3, 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 3 });

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
    }
}
