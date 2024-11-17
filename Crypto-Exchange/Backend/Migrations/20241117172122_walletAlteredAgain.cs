using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test_binance_api.Migrations
{
    /// <inheritdoc />
    public partial class walletAlteredAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Transactions",
                newName: "IdWallet");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                newName: "IX_Transactions_IdWallet");

            migrationBuilder.AddColumn<Guid>(
                name: "IdWallet",
                table: "Wallets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_IdWallet",
                table: "Transactions",
                column: "IdWallet",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Wallets_IdWallet",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "IdWallet",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "IdWallet",
                table: "Transactions",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_IdWallet",
                table: "Transactions",
                newName: "IX_Transactions_WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Wallets_WalletId",
                table: "Transactions",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
