using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedContainerRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conditions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Humidity_ExpectedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Humidity_AllowedDeviation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Light_ExpectedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Light_AllowedDeviation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Temperature_ExpectedValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Temperature_AllowedDeviation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Orientation_ExpectedValue_X = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Orientation_ExpectedValue_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Orientation_AllowedDeviation_X = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Orientation_AllowedDeviation_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conditions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ConditionsIntervalCheckInSecs = table.Column<int>(type: "int", nullable: false),
                    OrganId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ConditionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Containers_Conditions_ConditionsId",
                        column: x => x.ConditionsId,
                        principalTable: "Conditions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConditionsHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Temperature = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Humidity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Light = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Orientation_X = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Orientation_Y = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConditionsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConditionsHistory_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrganCreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organs_Containers_ContainerId",
                        column: x => x.ContainerId,
                        principalTable: "Containers",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8323df92-2f54-4de1-a13d-808472a034ad", "AQAAAAIAAYagAAAAEASu5KpaoTJvZ6YYIajtAvuUiCt0hb7cttCBRS3gdDu8wycaDucSwMgdTioMMm23+g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cfc0dff5-919f-4571-8e11-024d0a1bb984", "AQAAAAIAAYagAAAAEB63YDno51O7yeTc+JStfy2FwPXwEx4xTW7ZBPwL0/2M3Z40lH67KFJX5o67TQFpzw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7e192a9b-ca4e-4019-9323-9d594d279933", "AQAAAAIAAYagAAAAEGFTGIIxgTihHkv1fzjhXx3vOnScYTRSS33bxv7vYg901jgGloyiiSvvQCFGoP8Olw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5c84ea14-27fd-47e9-b186-de7843d334c3", "AQAAAAIAAYagAAAAEEYE4u2rTziUSFALmiFouSzIvKgp5LbEqMxTyxbKfHrWJCC5aQNyGrO7Jw27QZT7kQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_ConditionsHistory_ContainerId",
                table: "ConditionsHistory",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ConditionsId",
                table: "Containers",
                column: "ConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Organs_ContainerId",
                table: "Organs",
                column: "ContainerId",
                unique: true,
                filter: "[ContainerId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConditionsHistory");

            migrationBuilder.DropTable(
                name: "Organs");

            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Conditions");

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
    }
}
