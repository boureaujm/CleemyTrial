using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CleemyInfrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cleemy");

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "Cleemy",
                columns: table => new
                {
                    Code = table.Column<string>(nullable: false),
                    Label = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Cleemy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    AuthorizedCurrencyCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Currencies_AuthorizedCurrencyCode",
                        column: x => x.AuthorizedCurrencyCode,
                        principalSchema: "Cleemy",
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "Cleemy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    PaymentNature = table.Column<int>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    CurrencyCode = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Currencies_CurrencyCode",
                        column: x => x.CurrencyCode,
                        principalSchema: "Cleemy",
                        principalTable: "Currencies",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payments_Users_UserId",
                        column: x => x.UserId,
                        principalSchema: "Cleemy",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Cleemy",
                table: "Currencies",
                columns: new[] { "Code", "Label" },
                values: new object[] { "USD", "US Dollar" });

            migrationBuilder.InsertData(
                schema: "Cleemy",
                table: "Currencies",
                columns: new[] { "Code", "Label" },
                values: new object[] { "RUB", "Russian Ruble" });

            migrationBuilder.InsertData(
                schema: "Cleemy",
                table: "Users",
                columns: new[] { "Id", "AuthorizedCurrencyCode", "FirstName", "LastName" },
                values: new object[] { 1, "USD", "Stark", "Anthony" });

            migrationBuilder.InsertData(
                schema: "Cleemy",
                table: "Users",
                columns: new[] { "Id", "AuthorizedCurrencyCode", "FirstName", "LastName" },
                values: new object[] { 2, "RUB", "Romanova", "Natasha" });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CurrencyCode",
                schema: "Cleemy",
                table: "Payments",
                column: "CurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_UserId",
                schema: "Cleemy",
                table: "Payments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthorizedCurrencyCode",
                schema: "Cleemy",
                table: "Users",
                column: "AuthorizedCurrencyCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments",
                schema: "Cleemy");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "Cleemy");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "Cleemy");
        }
    }
}