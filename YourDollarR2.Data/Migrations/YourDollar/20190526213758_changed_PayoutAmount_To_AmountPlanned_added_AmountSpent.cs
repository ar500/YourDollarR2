using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YourDollarR2.DataAccess.Migrations.YourDollar
{
    public partial class changed_PayoutAmount_To_AmountPlanned_added_AmountSpent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "PayoutAmount",
                table: "Expenses",
                newName: "AmountPlanned");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetId",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<decimal>(
                name: "AmountSpent",
                table: "Expenses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "AmountPlanned",
                table: "Expenses");

            migrationBuilder.RenameColumn(
                name: "AmountSpent",
                table: "Expenses",
                newName: "PayoutAmount");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetId",
                table: "Expenses",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
