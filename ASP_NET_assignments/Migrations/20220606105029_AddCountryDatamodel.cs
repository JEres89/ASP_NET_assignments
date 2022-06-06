using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class AddCountryDatamodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 46947043);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 318113213);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1377319211);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2140680517);

            migrationBuilder.AddColumn<string>(
                name: "CountryName",
                table: "Cities",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                    table.UniqueConstraint("AK_Countries_Name", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1095920195, "Sverige" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2016426638, "Norge" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[] { 895571250, "Ingenstans" });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryName", "Name" },
                values: new object[,]
                {
                    { 1381616042, "Sverige", "Göteborg" },
                    { 1397648271, "Sverige", "Staden" },
                    { 2100253267, "Norge", "Skogen" },
                    { 120459992, "Ingenstans", "Luftslottet" }
                });

            migrationBuilder.InsertData(
                table: "PeopleDataTable",
                columns: new[] { "Id", "CityName", "Name", "Phonenumber" },
                values: new object[,]
                {
                    { 1343096344, "Göteborg", "Jens Eresund", "+46706845909" },
                    { 1060981163, "Staden", "Abel Abrahamsson", "+00123456789" },
                    { 1836446275, "Skogen", "Bror Björn", "+5555555555" },
                    { 207136652, "Luftslottet", "Örjan Örn", "1111111111" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CountryName",
                table: "Cities",
                column: "CountryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Countries_CountryName",
                table: "Cities",
                column: "CountryName",
                principalTable: "Countries",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Countries_CountryName",
                table: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CountryName",
                table: "Cities");

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 207136652);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1060981163);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1343096344);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1836446275);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 120459992);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1381616042);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1397648271);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2100253267);

            migrationBuilder.DropColumn(
                name: "CountryName",
                table: "Cities");

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
        }
    }
}
