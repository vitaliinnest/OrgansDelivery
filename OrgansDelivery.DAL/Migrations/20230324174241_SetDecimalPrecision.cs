using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class SetDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "76e162e0-633a-4320-98b4-da89cd405476", "AQAAAAIAAYagAAAAEOYZfOIsYVqi1ClWoVAzGkeC/I0ZemJBydziT5K1piXKHRr+9TDui8iBcH8KSplVxA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4aa3d220-9812-4dab-8c79-6867a72011d0", "AQAAAAIAAYagAAAAEAjt3zEGu45Uc0QdysJ9NgOOAiRbi6ZEPui1LGDzTb+oQ4G6lmNq4YmDnMfYv2a25w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3a553c79-6173-4fc9-b2f3-5533b8b7ab60", "AQAAAAIAAYagAAAAEBHGli/BzXhKWwiVAB88BXCNVgOM2KxbXBGUipcLxmq87UYJsyWrwVaEGduz2uA1tg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a2ecee72-9b22-493f-9fb3-89e1147bcdf0", "AQAAAAIAAYagAAAAEGnk4d2Nx8li3U5ZIBqitObufvqVCXNa4+gOXzu/u+8GwQkg40zBuQkVfv6K3VpzZw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "39fc7548-afc1-4381-a212-89f2771da78c", "AQAAAAIAAYagAAAAEDT6h3q59THUYxLULmHkuKXiLWSCz8/abKCoIjuvNz1DijUVB34PI1H+lwe1g7QfzA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98df55a9-42d1-4788-bcf2-c89f589641e1", "AQAAAAIAAYagAAAAEAh8s6nCQqII+zJa5rBMiX5Khd5CpLXF+z7rK9W/NivWwXETU1HyMh8uZ9GlHoxeNw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3e1121c9-e86c-4cc1-9633-75635faab4c4", "AQAAAAIAAYagAAAAEBuvSXFfLnBDDEmUaSX3VRLLof80Af/iSbCAWsDqh1WNRoFpjbaQ/qJLAIss5QVowQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d034a64c-ede2-4fe9-828a-c8aa5ab1b394", "AQAAAAIAAYagAAAAEE3TREUYWl1y26NBzXOhMwOltxMK7i/Q4n/owg0KfYpvk6Z6EXOHa/cqybFOkKRt7A==" });
        }
    }
}
