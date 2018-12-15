using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class @double : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "DeductionMedicare",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "DeductionDental",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "Deduction401",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.AlterColumn<double>(
                name: "BaseSalary",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(float));

            migrationBuilder.CreateTable(
                name: "PayChecks",
                columns: table => new
                {
                    TxnId = table.Column<Guid>(nullable: false),
                    BaseSalary = table.Column<float>(nullable: false),
                    TxnDate = table.Column<DateTimeOffset>(nullable: false),
                    FederalTax = table.Column<float>(nullable: false),
                    StateTax = table.Column<float>(nullable: false),
                    Deduction401 = table.Column<float>(nullable: false),
                    DeductionMedicare = table.Column<float>(nullable: false),
                    DeductionDental = table.Column<float>(nullable: false),
                    TakeHomSalary = table.Column<float>(nullable: false),
                    EmpId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayChecks", x => x.TxnId);
                    table.ForeignKey(
                        name: "FK_PayChecks_Employees_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employees",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PayChecks_EmpId",
                table: "PayChecks",
                column: "EmpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PayChecks");

            migrationBuilder.AlterColumn<float>(
                name: "DeductionMedicare",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "DeductionDental",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "Deduction401",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(double));

            migrationBuilder.AlterColumn<float>(
                name: "BaseSalary",
                table: "EmployeeSalaries",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}
