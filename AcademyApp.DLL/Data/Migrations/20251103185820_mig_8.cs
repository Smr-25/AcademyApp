using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AcademyApp.DLL.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Email", "FirstName", "GroupId", "LastName" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "aysel.mammadova@code.edu.az", "Aysel", 1, "Mammadova" },
                    { 2, new DateTime(2001, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "elvin.aliyev@code.edu.az", "Elvin", 1, "Aliyev" },
                    { 3, new DateTime(1999, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "leyla.hasanova@code.edu.az", "Leyla", 2, "Hasanova" },
                    { 4, new DateTime(2002, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "reshad.quliyev@code.edu.az", "Rəşad", 2, "Quliyev" },
                    { 5, new DateTime(2000, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "nermin.ahmadova@code.edu.az", "Nərmin", 3, "Əhmədova" },
                    { 6, new DateTime(2001, 7, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "tural.ibrahimov@code.edu.az", "Tural", 1, "İbrahimov" },
                    { 7, new DateTime(1998, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "gunel.mammadli@code.edu.az", "Günel", 1, "Məmmədli" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
