using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class Added_PersonLanguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 895571250);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1095920195);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2016426638);

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonLanguages",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonLanguages", x => new { x.PersonId, x.LanguageId });
                    table.ForeignKey(
                        name: "FK_PersonLanguages_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonLanguages_PeopleDataTable_PersonId",
                        column: x => x.PersonId,
                        principalTable: "PeopleDataTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1375161546, "Sverige" },
                    { 1987401714, "Norge" },
                    { 780588204, "Ingenstans" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1686208453, "Svenska" },
                    { 1815848149, "Engelska" },
                    { 1115537332, "Spanska" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryName", "Name" },
                values: new object[,]
                {
                    { 1492625250, "Sverige", "Göteborg" },
                    { 926749618, "Sverige", "Staden" },
                    { 741622396, "Norge", "Skogen" },
                    { 1609786201, "Ingenstans", "Luftslottet" }
                });

            migrationBuilder.InsertData(
                table: "PeopleDataTable",
                columns: new[] { "Id", "CityName", "Name", "Phonenumber" },
                values: new object[,]
                {
                    { 1822467822, "Göteborg", "Jens Eresund", "+46706845909" },
                    { 1180735110, "Staden", "Abel Abrahamsson", "+00123456789" },
                    { 1153083005, "Skogen", "Bror Björn", "+5555555555" },
                    { 425975129, "Luftslottet", "Örjan Örn", "1111111111" }
                });

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageId" },
                values: new object[,]
                {
                    { 1822467822, 1815848149 },
                    { 1822467822, 1115537332 },
                    { 1180735110, 1815848149 },
                    { 1180735110, 1686208453 },
                    { 1153083005, 1815848149 },
                    { 1153083005, 1115537332 },
                    { 425975129, 1815848149 },
                    { 425975129, 1115537332 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersonLanguages_LanguageId",
                table: "PersonLanguages",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonLanguages");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 425975129);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1153083005);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1180735110);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1822467822);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 741622396);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 926749618);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1492625250);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1609786201);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 780588204);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1375161546);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1987401714);

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
        }
    }
}
