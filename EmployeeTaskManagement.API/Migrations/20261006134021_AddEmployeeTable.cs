using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeTaskManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(
                        type: "int",
                        nullable: false)
                        .Annotation(
                            "SqlServer:Identity",
                            "1, 1"),

                    EmployeeName = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    Email = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    Department = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    Designation = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false),

                    Salary = table.Column<decimal>(
                        type: "decimal(18,2)",
                        precision: 18,
                        scale: 2,
                        nullable: false),

                    IsActive = table.Column<bool>(
                        type: "bit",
                        nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_Employees",
                        x => x.EmployeeId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}