using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RenamedEmployeeToWorker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "worker", "WORKER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "db1be569-483c-4c2f-ab49-6b98689bb938", "AQAAAAIAAYagAAAAEMK5fHEEPEIpjClhrED7/HNiYKwVY9GigCpFPYoHLS0ILrgMzakwWgPhZzQl4uGVmg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a93a2f64-1653-4e6c-ad29-c6fcf5b0ae50", "AQAAAAIAAYagAAAAEEnBAuSA+H5Li6TfioRW6V/Bt6n/fwSh24N8rYYNt5Tm2y4WLBc6wp/mI0PID+uc8A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7cbfd788-5e6a-438c-adbf-a34d538ff193", "AQAAAAIAAYagAAAAEB4gWkepG0tAWjmGhzRxyjST7zaRw7KzqHciyDbHxHZdEkhck3WeA6X5mzN3G6WmXw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6ab540c5-5643-4d8a-afde-86d889cb1465", "AQAAAAIAAYagAAAAECI7pH1IrwWOAZQS8waGJL49EP62JnWd68TzvciMhBB8CY4UrmlHqFho/zooaFKBhg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "employee", "EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99194a24-55c8-412a-95dc-351bc001dc20", "AQAAAAIAAYagAAAAEFEls8hQbTGOdeADrZ8qVmzKCpEQCv9CXkoezrfo3+hLXfVQywLobjn6Nf0EoCdRYw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "47ce4b0d-b5c0-4918-bd69-25e6bc6f8f8f", "AQAAAAIAAYagAAAAEMMfAFpzxef8LIuZEPbOi+Dk8L2TUPw3ZBW8cg46egSnYp2emikaQDpVapZUxH7Iyg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "08384400-b04a-4be6-95fc-7196290327d4", "AQAAAAIAAYagAAAAEHO5octuYLaGUxRVvHWnNww9Yu1qNwgyg6k4K+WIM6gI0IMLLg62Ll9by4foBJjsAQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f85e37b4-18e9-4153-ba4c-5ce2704935d5", "AQAAAAIAAYagAAAAEBAYuWyzbD/ceXKXgYccme7gFYvhXwPhvfTUqpHffr7cuzxRktRoJvJ5gii3r+y17Q==" });
        }
    }
}
