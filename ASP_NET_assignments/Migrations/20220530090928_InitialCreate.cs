using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PeopleDataTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Phonenumber = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleDataTable", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeopleDataTable");
        }
    }
}
