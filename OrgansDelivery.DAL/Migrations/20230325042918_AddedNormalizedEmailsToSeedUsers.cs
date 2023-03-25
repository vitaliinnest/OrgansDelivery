using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedNormalizedEmailsToSeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "78807729-6828-426c-a7bf-54eb79255599", "JAVONSPENCER@DATA.COM", "AQAAAAIAAYagAAAAEMtGDqZO4LjpaJ+6FcaqbZ0si3uPe+E1/6qNWcoc5tsdLglPwHuW+jBJ11tAy5lfDA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "e46a54d9-a18e-4cbd-b933-8a5d243c4439", "CHARLESHUBER@MEDTRONIC.COM", "AQAAAAIAAYagAAAAEMpc7Evm7PE+c3i5M4ab1DG0PDPc0g5vtt6YjNgDHIuJ7AdwoH6U0IM2JlDmo1gQYg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "3ceb9d7f-b63f-470b-a1ab-9280e67eee62", "ANDRESBLAKE@BOSTONCORP.COM", "AQAAAAIAAYagAAAAEJLJQqEn60urRI+3vaHx393ElGQwn6moccwZWJrQzuRFu1vkOnSDwHq1RoD0jGP0HA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "f1e752b7-e221-4ad2-933c-f7e217df07a7", "REGANLEWIS@MEDTRONIC.COM", "AQAAAAIAAYagAAAAEL+50VO7kLltVYCL6zRUS+J1bxLpc7wqN8mBcf/so+uhR+5bh/R/IVqKIMm2Cvgx3w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "76e162e0-633a-4320-98b4-da89cd405476", null, "AQAAAAIAAYagAAAAEOYZfOIsYVqi1ClWoVAzGkeC/I0ZemJBydziT5K1piXKHRr+9TDui8iBcH8KSplVxA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "4aa3d220-9812-4dab-8c79-6867a72011d0", null, "AQAAAAIAAYagAAAAEAjt3zEGu45Uc0QdysJ9NgOOAiRbi6ZEPui1LGDzTb+oQ4G6lmNq4YmDnMfYv2a25w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "3a553c79-6173-4fc9-b2f3-5533b8b7ab60", null, "AQAAAAIAAYagAAAAEBHGli/BzXhKWwiVAB88BXCNVgOM2KxbXBGUipcLxmq87UYJsyWrwVaEGduz2uA1tg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash" },
                values: new object[] { "a2ecee72-9b22-493f-9fb3-89e1147bcdf0", null, "AQAAAAIAAYagAAAAEGnk4d2Nx8li3U5ZIBqitObufvqVCXNa4+gOXzu/u+8GwQkg40zBuQkVfv6K3VpzZw==" });
        }
    }
}
