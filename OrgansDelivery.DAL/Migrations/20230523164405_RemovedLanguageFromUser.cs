using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedLanguageFromUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9fd17e95-a2f6-4a5a-bbca-3e32f9ef0050", "AQAAAAIAAYagAAAAEFPJ9WRefKJNsea89GczwG6JbH42itT2c4FhyVsSlOSP5TfolgW/u8xnWRbKkq9tGA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5be56fcf-639c-4918-b510-e74c3e8ed604", "AQAAAAIAAYagAAAAEDU7ZMGlSRg9xDN9OS7gY1ZFFLJc5ELzvXIsEZCWyhUsGj4S/dj71k85asLZAxw5Gg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "38389204-d46e-43fc-abd2-c36ffaaacc56", "AQAAAAIAAYagAAAAEGk9i1kuUEcIL/Bsf/u1ZHh81xQhqag/RTN+9d7k+WHjUgw1QGC2HuHrC0hXndOBYQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "69baa759-1b7f-4f4b-8898-b4e8f8dfab53", "AQAAAAIAAYagAAAAEDrHgjgmPRwMXN1xb2WDbjmkVvZEfp+J5wxSWg/3a5o3Cxj0JcwY7wUKbOv0g09igg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "Language", "PasswordHash" },
                values: new object[] { "16ac21ac-28fe-4c9a-8f3a-a8dd109c39d8", 0, "AQAAAAIAAYagAAAAEC29Yi1HSnK8NDh0ThFvRpL2m++WladDl5AATBb1+7ecY+sS7qRDfKEM1+mQfxj4Uw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "Language", "PasswordHash" },
                values: new object[] { "0671396f-5deb-4283-8535-710fb398b159", 0, "AQAAAAIAAYagAAAAEPd8EAcyFKwCPB/Favb+/lnphwCBypFugHvzz+eBUArGu3XFmrQPAQ0SKMABQsJVeQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "Language", "PasswordHash" },
                values: new object[] { "a8f959b5-2cd8-47f3-9d06-4097e9ab2216", 0, "AQAAAAIAAYagAAAAENk0QouD4v+Joj+5WCMiCz4GeZDtH40Kq5WkGqkOHwOQnXrrEUYvGKB5M5T+yo6OVg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "Language", "PasswordHash" },
                values: new object[] { "2811c71d-e9e5-4a0b-b073-ffeec2360436", 0, "AQAAAAIAAYagAAAAEBJOlFHl8dQxc6CACEx48/M8vBIa9xBz7YQm4T2zUd1pp4aPfePuz829J7smBGu02w==" });
        }
    }
}
