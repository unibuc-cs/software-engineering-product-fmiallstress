using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test_binance_api.Migrations
{
    /// <inheritdoc />
    public partial class walletImplemented : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinDTO_Wallet_WalletId",
                table: "CoinDTO");

            migrationBuilder.DropForeignKey(
                name: "FK_Coins_Histories_IdHistory",
                table: "Coins");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallet_AspNetUsers_IdUser",
                table: "Wallet");

            migrationBuilder.DropTable(
                name: "Histories");

            migrationBuilder.DropIndex(
                name: "IX_Coins_IdHistory",
                table: "Coins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinDTO",
                table: "CoinDTO");

            migrationBuilder.DropIndex(
                name: "IX_CoinDTO_WalletId",
                table: "CoinDTO");

            migrationBuilder.DropColumn(
                name: "IdHistory",
                table: "Coins");

            migrationBuilder.DropColumn(
                name: "IdHistory",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "CoinDTO");

            migrationBuilder.RenameTable(
                name: "Wallet",
                newName: "Wallets");

            migrationBuilder.RenameTable(
                name: "CoinDTO",
                newName: "CoinDTOs");

            migrationBuilder.RenameIndex(
                name: "IX_Wallet_IdUser",
                table: "Wallets",
                newName: "IX_Wallets_IdUser");

            migrationBuilder.AddColumn<long>(
                name: "Balance",
                table: "Wallets",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinDTOs",
                table: "CoinDTOs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WalletId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CoinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_Coins_CoinId",
                        column: x => x.CoinId,
                        principalTable: "Coins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Wallets_WalletId",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CoinId",
                table: "Transactions",
                column: "CoinId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WalletId",
                table: "Transactions",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinDTOs_Wallets_Id",
                table: "CoinDTOs",
                column: "Id",
                principalTable: "Wallets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_AspNetUsers_IdUser",
                table: "Wallets",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoinDTOs_Wallets_Id",
                table: "CoinDTOs");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_AspNetUsers_IdUser",
                table: "Wallets");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Wallets",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoinDTOs",
                table: "CoinDTOs");

            migrationBuilder.DropColumn(
                name: "Balance",
                table: "Wallets");

            migrationBuilder.RenameTable(
                name: "Wallets",
                newName: "Wallet");

            migrationBuilder.RenameTable(
                name: "CoinDTOs",
                newName: "CoinDTO");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_IdUser",
                table: "Wallet",
                newName: "IX_Wallet_IdUser");

            migrationBuilder.AddColumn<Guid>(
                name: "IdHistory",
                table: "Coins",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "IdHistory",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "CoinDTO",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Wallet",
                table: "Wallet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoinDTO",
                table: "CoinDTO",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstCreated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Histories_AspNetUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Coins_IdHistory",
                table: "Coins",
                column: "IdHistory");

            migrationBuilder.CreateIndex(
                name: "IX_CoinDTO_WalletId",
                table: "CoinDTO",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Histories_IdUser",
                table: "Histories",
                column: "IdUser",
                unique: true,
                filter: "[IdUser] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CoinDTO_Wallet_WalletId",
                table: "CoinDTO",
                column: "WalletId",
                principalTable: "Wallet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Coins_Histories_IdHistory",
                table: "Coins",
                column: "IdHistory",
                principalTable: "Histories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_AspNetUsers_IdUser",
                table: "Wallet",
                column: "IdUser",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
