using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASP_NET_assignments.Migrations
{
    public partial class Identity_implemented : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 425975129, 1115537332 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 425975129, 1815848149 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1153083005, 1115537332 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1153083005, 1815848149 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1180735110, 1686208453 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1180735110, 1815848149 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1822467822, 1115537332 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1822467822, 1815848149 });

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1115537332);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1686208453);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1815848149);

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

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Sverige" },
                    { 2, "Norge" },
                    { 3, "Ingenstans" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Svenska" },
                    { 2, "Engelska" },
                    { 3, "Spanska" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "CountryName", "Name" },
                values: new object[,]
                {
                    { 1, "Sverige", "Göteborg" },
                    { 2, "Sverige", "Staden" },
                    { 3, "Norge", "Skogen" },
                    { 4, "Ingenstans", "Luftslottet" }
                });

            migrationBuilder.InsertData(
                table: "PeopleDataTable",
                columns: new[] { "Id", "CityName", "Name", "Phonenumber" },
                values: new object[,]
                {
                    { 1, "Göteborg", "Jens Eresund", "+46706845909" },
                    { 2, "Staden", "Abel Abrahamsson", "+00123456789" },
                    { 3, "Skogen", "Bror Björn", "+5555555555" },
                    { 4, "Luftslottet", "Örjan Örn", "1111111111" }
                });

            migrationBuilder.InsertData(
                table: "PersonLanguages",
                columns: new[] { "PersonId", "LanguageId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 1, 1 },
                    { 2, 2 },
                    { 2, 1 },
                    { 3, 2 },
                    { 3, 1 },
                    { 4, 2 },
                    { 4, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "PersonLanguages",
                keyColumns: new[] { "PersonId", "LanguageId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Languages",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PeopleDataTable",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);

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
        }
    }
}
