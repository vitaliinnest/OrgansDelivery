using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedDeviceIdToRecordAndRenamedRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConditionsHistory_Containers_ContainerId",
                table: "ConditionsHistory");

            migrationBuilder.DropForeignKey(
                name: "FK_Organs_Containers_ContainerId",
                table: "Organs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ConditionsHistory",
                table: "ConditionsHistory");

            migrationBuilder.RenameTable(
                name: "ConditionsHistory",
                newName: "Records");

            migrationBuilder.RenameIndex(
                name: "IX_ConditionsHistory_ContainerId",
                table: "Records",
                newName: "IX_Records_ContainerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContainerId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "Containers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ContainerId",
                table: "Records",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "DeviceId",
                table: "Records",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "Id");

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
                name: "IX_Devices_ContainerId",
                table: "Devices",
                column: "ContainerId",
                unique: true,
                filter: "[ContainerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Records_DeviceId",
                table: "Records",
                column: "DeviceId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "IX_Devices_ContainerId",
                table: "Devices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropIndex(
                name: "IX_Records_DeviceId",
                table: "Records");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "DeviceId",
                table: "Records");

            migrationBuilder.RenameTable(
                name: "Records",
                newName: "ConditionsHistory");

            migrationBuilder.RenameIndex(
                name: "IX_Records_ContainerId",
                table: "ConditionsHistory",
                newName: "IX_ConditionsHistory_ContainerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "ContainerId",
                table: "Devices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ContainerId",
                table: "ConditionsHistory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ConditionsHistory",
                table: "ConditionsHistory",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_ConditionsHistory_Containers_ContainerId",
                table: "ConditionsHistory",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Organs_Containers_ContainerId",
                table: "Organs",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id");
        }
    }
}
