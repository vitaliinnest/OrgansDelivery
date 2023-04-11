using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedDevice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConditionsIntervalCheckInSecs",
                table: "Containers");

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConditionsIntervalCheckInMs = table.Column<int>(type: "int", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6f70863f-728b-4aff-810d-f1ff9d640768", "AQAAAAIAAYagAAAAEPoe3epSBAC7z6imG+2Ql7OGkdLIaK/kfSEiexuau6mz/fyB5rlNtV/CWrVMnZ7qdA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "ce7dd51a-a366-47f5-b042-59c7021e8a7a", "AQAAAAIAAYagAAAAEPpAfOO9ARPpbI7OR5WbaBTDpo+NpeJKXwdF/uqllkwmugo3zccRgnh8Zdm1lRWK2A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "74786e40-5cc7-4644-9ab0-219a77ee443d", "AQAAAAIAAYagAAAAEP+tXjDjEjxWzD9XObSkzkGlDrc4DL1TipEfTLlxUe17VP+4pcaUE0eysZcHn2aK2g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "68a6f0f5-dc07-4a36-a53a-46422a33e9b2", "AQAAAAIAAYagAAAAECYdiYk2eXGTf9gyfVyGxgn5zb7mlueh81tUh9EIBVC0pC0sIegRtJPLI4ApW+b46g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.AddColumn<int>(
                name: "ConditionsIntervalCheckInSecs",
                table: "Containers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "78807729-6828-426c-a7bf-54eb79255599", "AQAAAAIAAYagAAAAEMtGDqZO4LjpaJ+6FcaqbZ0si3uPe+E1/6qNWcoc5tsdLglPwHuW+jBJ11tAy5lfDA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e46a54d9-a18e-4cbd-b933-8a5d243c4439", "AQAAAAIAAYagAAAAEMpc7Evm7PE+c3i5M4ab1DG0PDPc0g5vtt6YjNgDHIuJ7AdwoH6U0IM2JlDmo1gQYg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3ceb9d7f-b63f-470b-a1ab-9280e67eee62", "AQAAAAIAAYagAAAAEJLJQqEn60urRI+3vaHx393ElGQwn6moccwZWJrQzuRFu1vkOnSDwHq1RoD0jGP0HA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f1e752b7-e221-4ad2-933c-f7e217df07a7", "AQAAAAIAAYagAAAAEL+50VO7kLltVYCL6zRUS+J1bxLpc7wqN8mBcf/so+uhR+5bh/R/IVqKIMm2Cvgx3w==" });
        }
    }
}
