using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "currency_id",
                table: "account",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_account_currency_id",
                table: "account",
                column: "currency_id");

            migrationBuilder.AddForeignKey(
                name: "FK_account_currency_currency_id",
                table: "account",
                column: "currency_id",
                principalTable: "currency",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_account_currency_currency_id",
                table: "account");

            migrationBuilder.DropIndex(
                name: "IX_account_currency_id",
                table: "account");

            migrationBuilder.DropColumn(
                name: "currency_id",
                table: "account");
        }
    }
}
