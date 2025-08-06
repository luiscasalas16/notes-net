using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Service.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderState",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentState = table.Column<string>(
                        type: "varchar(64)",
                        maxLength: 64,
                        nullable: false
                    ),
                    OrderTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    PaymentIntentId = table.Column<string>(
                        type: "varchar(64)",
                        maxLength: 64,
                        nullable: true
                    ),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    CustomerEmail = table.Column<string>(
                        type: "varchar(256)",
                        maxLength: 256,
                        nullable: true
                    ),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderState", x => x.CorrelationId);
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "OrderState");
        }
    }
}
