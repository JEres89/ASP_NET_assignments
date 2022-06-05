using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class AddedCityModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 312203518);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 399444226);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 523178533);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1591867130);

            migrationBuilder.DropColumn(
                name: "City",
                table: "PeopleDataTable");

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "PeopleDataTable",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.UniqueConstraint("AK_Cities_Name", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 2140680517, "Göteborg" },
                    { 318113213, "Staden" },
                    { 1377319211, "Skogen" },
                    { 46947043, "Luftslottet" }
                });

            migrationBuilder.InsertData(
                table: "PeopleDataTable",
                columns: new[] { "Id", "CityName", "Name", "Phonenumber" },
                values: new object[,]
                {
                    { 426844326, "Göteborg", "Jens Eresund", "+46706845909" },
                    { 800789915, "Staden", "Abel Abrahamsson", "+00123456789" },
                    { 1552201324, "Skogen", "Bror Björn", "+5555555555" },
                    { 1012430503, "Luftslottet", "Örjan Örn", "1111111111" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeopleDataTable_CityName",
                table: "PeopleDataTable",
                column: "CityName");

            migrationBuilder.AddForeignKey(
                name: "FK_PeopleDataTable_Cities_CityName",
                table: "PeopleDataTable",
                column: "CityName",
                principalTable: "Cities",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeopleDataTable_Cities_CityName",
                table: "PeopleDataTable");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_PeopleDataTable_CityName",
                table: "PeopleDataTable");

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 426844326);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 800789915);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1012430503);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1552201324);

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "PeopleDataTable");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PeopleDataTable",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "PeopleDataTable",
                columns: new[] { "Id", "City", "Name", "Phonenumber" },
                values: new object[,]
                {
                    { 399444226, "Göteborg", "Jens Eresund", "+46706845909" },
                    { 523178533, "Staden", "Abel Abrahamsson", "+00123456789" },
                    { 1591867130, "Skogen", "Bror Björn", "+5555555555" },
                    { 312203518, "Luftslottet", "Örjan Örn", "1111111111" }
                });
        }
    }
}
