using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMMiniproject.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DebitCardDetails",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", maxLength: 16, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcctId = table.Column<int>(type: "int", nullable: false),
                    PinHashed = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HashSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitCardDetails", x => x.CardId);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AcctId = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AcctId);
                    table.ForeignKey(
                        name: "FK_Accounts_DebitCardDetails_AcctId",
                        column: x => x.AcctId,
                        principalTable: "DebitCardDetails",
                        principalColumn: "CardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ATMTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false),
                    AcctId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ATMTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_ATMTransactions_Accounts_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Accounts",
                        principalColumn: "AcctId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATMTransactions");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "DebitCardDetails");
        }
    }
}
