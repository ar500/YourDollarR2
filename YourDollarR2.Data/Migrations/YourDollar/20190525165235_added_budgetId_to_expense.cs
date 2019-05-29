using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace YourDollarR2.DataAccess.Migrations.YourDollar
{
    public partial class added_budgetId_to_expense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetId",
                table: "Expenses",
                nullable: true,
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses");

            migrationBuilder.AlterColumn<Guid>(
                name: "BudgetId",
                table: "Expenses",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Budgets_BudgetId",
                table: "Expenses",
                column: "BudgetId",
                principalTable: "Budgets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
