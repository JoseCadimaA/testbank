using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transaction.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitTransactionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    sourceAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    targetAccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    transferTypeId = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(15,2)", nullable: false),
                    status = table.Column<int>(type: "integer", maxLength: 10, nullable: false),
                    createAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
