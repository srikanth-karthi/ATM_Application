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
                    Balance = table.Column<int>(type: "int", nullable: false)
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
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false)
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
                values: new object[] { 123456, 1000 });

            migrationBuilder.InsertData(
                table: "DebitCardDetails",
                columns: new[] { "CardId", "AcctId", "HashSalt", "PinHashed" },
                values: new object[] { 654321, 123456, new byte[] { 156, 230, 82, 72, 34, 139, 65, 237, 23, 91, 0, 136, 31, 44, 37, 167, 80, 154, 107, 235, 83, 183, 214, 172, 111, 153, 89, 27, 168, 187, 21, 28, 48, 176, 46, 134, 229, 149, 120, 167, 85, 181, 122, 76, 121, 97, 140, 135, 90, 79, 170, 16, 28, 188, 90, 126, 19, 154, 180, 127, 0, 98, 202, 176, 100, 146, 28, 198, 192, 79, 1, 11, 243, 86, 227, 9, 186, 19, 56, 222, 80, 200, 136, 35, 204, 206, 114, 53, 185, 2, 183, 73, 72, 221, 31, 91, 118, 62, 18, 223, 141, 207, 209, 7, 251, 103, 194, 49, 137, 114, 47, 141, 201, 44, 89, 194, 90, 143, 197, 73, 9, 185, 58, 8, 36, 86, 119, 49 }, new byte[] { 20, 239, 47, 192, 24, 39, 29, 214, 158, 146, 198, 251, 63, 84, 11, 114, 102, 8, 175, 79, 36, 88, 107, 221, 236, 136, 204, 25, 155, 0, 171, 239, 55, 42, 116, 122, 245, 210, 119, 240, 192, 210, 100, 249, 106, 36, 114, 129, 171, 247, 180, 155, 33, 123, 110, 104, 135, 117, 35, 21, 67, 242, 254, 177 } });

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
