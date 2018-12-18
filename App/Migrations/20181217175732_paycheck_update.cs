using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class paycheck_update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "TakeHomSalary",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "StateTax",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "FederalTax",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "DeductionMedicare",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "DeductionDental",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Deduction401",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "BaseSalary",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(float));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "TakeHomSalary",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "StateTax",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "FederalTax",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "DeductionMedicare",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "DeductionDental",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Deduction401",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "BaseSalary",
                table: "PayChecks",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
