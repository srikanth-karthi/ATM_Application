using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ATMMiniproject.Migrations
{
    public partial class added_dummy_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AcctId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AcctId);
                });

            migrationBuilder.CreateTable(
                name: "ATMTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
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
                        name: "FK_ATMTransactions_Accounts_AcctId",
                        column: x => x.AcctId,
                        principalTable: "Accounts",
                        principalColumn: "AcctId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DebitCardDetails",
                columns: table => new
                {
                    CardId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcctId = table.Column<int>(type: "int", nullable: false),
                    PinHashed = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    HashSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebitCardDetails", x => x.CardId);
                    table.ForeignKey(
                        name: "FK_DebitCardDetails_Accounts_AcctId",
                        column: x => x.AcctId,
                        principalTable: "Accounts",
                        principalColumn: "AcctId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "AcctId", "Balance" },
                values: new object[] { 123456, 1000f });

            migrationBuilder.InsertData(
                table: "DebitCardDetails",
                columns: new[] { "CardId", "AcctId", "HashSalt", "PinHashed" },
                values: new object[] { 1, 123456, new byte[] { 74, 18, 131, 255, 225, 55, 111, 1, 54, 166, 50, 109, 241, 35, 245, 251, 45, 148, 198, 168, 22, 142, 97, 104, 185, 245, 2, 112, 172, 47, 54, 7, 108, 86, 247, 106, 213, 53, 201, 113, 211, 200, 76, 86, 19, 172, 231, 155, 200, 108, 135, 95, 149, 53, 181, 203, 25, 240, 65, 151, 209, 77, 8, 112, 16, 99, 148, 134, 234, 181, 173, 235, 91, 121, 157, 173, 196, 70, 157, 144, 174, 42, 240, 226, 125, 236, 174, 92, 91, 55, 29, 59, 75, 100, 64, 78, 68, 177, 194, 199, 35, 176, 126, 96, 195, 54, 123, 171, 189, 163, 187, 65, 45, 221, 20, 49, 153, 101, 171, 75, 191, 164, 69, 26, 182, 45, 19, 108 }, new byte[] { 249, 240, 122, 163, 205, 192, 103, 46, 171, 80, 211, 248, 179, 101, 194, 155, 23, 4, 59, 3, 134, 47, 81, 158, 20, 86, 18, 104, 244, 193, 235, 33, 254, 108, 109, 20, 48, 138, 221, 161, 112, 117, 250, 153, 161, 222, 71, 25, 173, 124, 98, 27, 52, 183, 199, 44, 55, 198, 87, 35, 206, 212, 1, 6 } });

            migrationBuilder.CreateIndex(
                name: "IX_ATMTransactions_AcctId",
                table: "ATMTransactions",
                column: "AcctId");

            migrationBuilder.CreateIndex(
                name: "IX_DebitCardDetails_AcctId",
                table: "DebitCardDetails",
                column: "AcctId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ATMTransactions");

            migrationBuilder.DropTable(
                name: "DebitCardDetails");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
