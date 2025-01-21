using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SeedProductsAndBranches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("61e628b6-64a0-419e-8b33-0e7c76f0a312"), "Branch 1" },
                    { new Guid("b9770758-9dae-422f-9d3e-c018c0339ea8"), "Branch 2" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BasePrice", "Name" },
                values: new object[,]
                {
                    { new Guid("ad081d1f-46eb-43ae-80ca-37a9b6f4540e"), 20.00m, "Product C" },
                    { new Guid("ca0165b8-c74f-4e8b-8d96-48a7b938c5c9"), 50.00m, "Product A" },
                    { new Guid("fe06f942-f3bc-4efd-b8c2-986474d6f967"), 30.00m, "Product B" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("61e628b6-64a0-419e-8b33-0e7c76f0a312"));

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: new Guid("b9770758-9dae-422f-9d3e-c018c0339ea8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ad081d1f-46eb-43ae-80ca-37a9b6f4540e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ca0165b8-c74f-4e8b-8d96-48a7b938c5c9"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe06f942-f3bc-4efd-b8c2-986474d6f967"));
        }
    }
}
