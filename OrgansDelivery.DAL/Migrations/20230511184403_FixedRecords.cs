using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixedRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Conditions_ConditionsId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Containers_ContainerId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Organs_Containers_ContainerId",
                table: "Organs");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Containers_ContainerId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Devices_DeviceId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ContainerId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_DeviceId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Organs_ContainerId",
                table: "Organs");

            migrationBuilder.DropIndex(
                name: "IX_Devices_ContainerId",
                table: "Devices");

            migrationBuilder.DropIndex(
                name: "IX_Containers_ConditionsId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "ConditionsId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "OrganId",
                table: "Containers");

            migrationBuilder.AddColumn<Guid>(
                name: "ConditionsId",
                table: "Records",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganId",
                table: "Records",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ContainerId",
                table: "Organs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConditionsId",
                table: "Organs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsArchival",
                table: "Conditions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "1df54a52-5730-4079-9f24-9e54bd4671aa", "AQAAAAIAAYagAAAAECMWnanlFz8BC2H/HPdtqS+4iNrOdT7jarfO84lIY7Q6D6eb1g3cutrWOL8yr/vRFA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cda32f72-6d32-44d8-817f-c88b8d8ed661", "AQAAAAIAAYagAAAAEHE03i6WSFJeKSy2O99pL4YkEpQ7E5EmWvTvkC5WJK3ZgbKZ575mDfdg6w/zpNm4sw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3b5e7274-1d3d-4280-8fa0-3d87559af703", "AQAAAAIAAYagAAAAEKcBDpsmQx/2edtCKRzL0UUhCIUCwvz+tMtdyPKF/sd9jzSyiTXbXzA/npdqI+5yFw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5b4427ab-55fb-452f-946a-293ca232f956", "AQAAAAIAAYagAAAAECy/p7SyMHrInc1TKW9WkWA3u2QITj30O9zu1v4YiXkW++AblSBunpWp/5NN1908Ug==" });

            migrationBuilder.CreateIndex(
                name: "IX_Records_ConditionsId",
                table: "Records",
                column: "ConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_OrganId",
                table: "Records",
                column: "OrganId");

            migrationBuilder.CreateIndex(
                name: "IX_Organs_ConditionsId",
                table: "Organs",
                column: "ConditionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Organs_ContainerId",
                table: "Organs",
                column: "ContainerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Containers_DeviceId",
                table: "Containers",
                column: "DeviceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Devices_DeviceId",
                table: "Containers",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Organs_Conditions_ConditionsId",
                table: "Organs",
                column: "ConditionsId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Organs_Containers_ContainerId",
                table: "Organs",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Conditions_ConditionsId",
                table: "Records",
                column: "ConditionsId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Organs_OrganId",
                table: "Records",
                column: "OrganId",
                principalTable: "Organs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Containers_Devices_DeviceId",
                table: "Containers");

            migrationBuilder.DropForeignKey(
                name: "FK_Organs_Conditions_ConditionsId",
                table: "Organs");

            migrationBuilder.DropForeignKey(
                name: "FK_Organs_Containers_ContainerId",
                table: "Organs");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Conditions_ConditionsId",
                table: "Records");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Organs_OrganId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_ConditionsId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_OrganId",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Organs_ConditionsId",
                table: "Organs");

            migrationBuilder.DropIndex(
                name: "IX_Organs_ContainerId",
                table: "Organs");

            migrationBuilder.DropIndex(
                name: "IX_Containers_DeviceId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "ConditionsId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "OrganId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "ConditionsId",
                table: "Organs");

            migrationBuilder.DropColumn(
                name: "IsArchival",
                table: "Conditions");

            migrationBuilder.AddColumn<Guid>(
                name: "ContainerId",
                table: "Records",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "Records",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContainerId",
                table: "Organs",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "ContainerId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ConditionsId",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrganId",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b3ccf289-a8d3-4100-97cf-520cb2d73c4f", "AQAAAAIAAYagAAAAEK81p3EY33L0b4BHgLy4YusF/jkSx/k/3m3jxgY7BwnIXfJfJ9M6uwP3Ei044yBqeA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e3a8efb1-73b7-408e-8274-3976722b38b0", "AQAAAAIAAYagAAAAELQMcwUX3gPd23CpgzhjMulsn1kfdm+GNsLYQnG8Ua2kvr54CXxRNLfhzgJfJtG5Jg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a0078e3e-8a5f-4ad7-aed2-1df9099c75f1", "AQAAAAIAAYagAAAAEKoI8g1/0Ab68tAK8dPyyJHVZmocn9Gou9bA6dJKDqfz56NmaAdUTM5d23Z1svEjpw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3efc8601-4a55-44f3-a0d0-aa2c32685407", "AQAAAAIAAYagAAAAEKVB3u1dkBXo93mWbBt+NSgAnZ4dXS0TJTBjvvujhZ1MQdJ47vd4ZR4B5nNlQX1zdA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Records_ContainerId",
                table: "Records",
                column: "ContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Records_DeviceId",
                table: "Records",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Organs_ContainerId",
                table: "Organs",
                column: "ContainerId",
                unique: true,
                filter: "[ContainerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ContainerId",
                table: "Devices",
                column: "ContainerId",
                unique: true,
                filter: "[ContainerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Containers_ConditionsId",
                table: "Containers",
                column: "ConditionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Containers_Conditions_ConditionsId",
                table: "Containers",
                column: "ConditionsId",
                principalTable: "Conditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Containers_ContainerId",
                table: "Devices",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Organs_Containers_ContainerId",
                table: "Organs",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Containers_ContainerId",
                table: "Records",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Devices_DeviceId",
                table: "Records",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
