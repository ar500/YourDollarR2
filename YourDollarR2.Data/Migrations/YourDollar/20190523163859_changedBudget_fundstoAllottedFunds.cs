using Microsoft.EntityFrameworkCore.Migrations;

namespace YourDollarR2.DataAccess.Migrations.YourDollar
{
    public partial class changedBudget_fundstoAllottedFunds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Funds",
                table: "Budgets",
                newName: "AllottedFunds");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AllottedFunds",
                table: "Budgets",
                newName: "Funds");
        }
    }
}
