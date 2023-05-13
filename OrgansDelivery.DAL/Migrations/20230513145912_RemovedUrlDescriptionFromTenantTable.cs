using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedUrlDescriptionFromTenantTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Tenants");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Tenants");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2d0003f1-539b-4ee4-acab-38af38c40184", "AQAAAAIAAYagAAAAEB9bvUMB1tBfbVWXQVyg1iN/wXAJI5267nMtAJDNP4M4Hi7kem5sOK9UQmyI2hPM3A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6580941d-17ee-4501-b926-347d631b56df", "AQAAAAIAAYagAAAAEPjuxZ7qrjUmz91aaX9rEyO9kveAU52vfJZA+Tu3F0Mx5/cmPu02doh9q9mxZyo7gg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "4d0c5756-fa62-40d1-9465-ac10da3af7b6", "AQAAAAIAAYagAAAAEK0t4s31WC7cxfCQW9ou0tnxNuwbTT0uPLDJercj18oRHeEkNY6IQ3rBPvt+ysi6QA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "40c5d9a7-def6-49df-a771-500f8d113294", "AQAAAAIAAYagAAAAEL1SFH9bb2t/q0md9gShnQ/eUWnFwzy5+lwa+Mrlb/tRYufSdmtzmxVJBk8BgVwwNQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Tenants",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"),
                columns: new[] { "Description", "Url" },
                values: new object[] { "Better health access for our world by combining the powerof healthcare technology with strong partnerships,we are helping put equity within reach", "medtronic" });

            migrationBuilder.UpdateData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("f4c76e71-6a6c-476a-9d15-328a09447646"),
                columns: new[] { "Description", "Url" },
                values: new object[] { "A Boston Scientific device brought Caroline relief – and the inspiration to pursue medicine", "bostoncorp" });
        }
    }
}
