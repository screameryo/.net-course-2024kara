using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDataBases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    f_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    l_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    m_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    bdate = table.Column<DateOnly>(type: "date", maxLength: 50, nullable: false),
                    passport_series = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    passport_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    telephone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bonuses = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "currency",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_currency", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_position", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name_cur = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    account_number = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    client_id = table.Column<Guid>(type: "uuid", nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_account_client_client_id",
                        column: x => x.client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    position_id = table.Column<Guid>(type: "uuid", maxLength: 50, nullable: true),
                    salary = table.Column<decimal>(type: "numeric", nullable: false),
                    department_id = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: true),
                    contract = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    f_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    l_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    m_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    b_date = table.Column<DateOnly>(type: "date", maxLength: 50, nullable: false),
                    passport_series = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    passport_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    telephone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    create_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    bonuses = table.Column<List<string>>(type: "text[]", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.id);
                    table.ForeignKey(
                        name: "FK_employee_department_department_id",
                        column: x => x.department_id,
                        principalTable: "department",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_employee_position_position_id",
                        column: x => x.position_id,
                        principalTable: "position",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_account_client_id",
                table: "account",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_client_passport_series_passport_number",
                table: "client",
                columns: new[] { "passport_series", "passport_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_department_id",
                table: "employee",
                column: "department_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_passport_series_passport_number",
                table: "employee",
                columns: new[] { "passport_series", "passport_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_position_id",
                table: "employee",
                column: "position_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");

            migrationBuilder.DropTable(
                name: "currency");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "client");

            migrationBuilder.DropTable(
                name: "department");

            migrationBuilder.DropTable(
                name: "position");
        }
    }
}
