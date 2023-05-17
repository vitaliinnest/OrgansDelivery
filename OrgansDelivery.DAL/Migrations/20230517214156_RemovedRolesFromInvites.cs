using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRolesFromInvites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"), new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"));

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Invites");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Devices");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"), new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320") });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "16ac21ac-28fe-4c9a-8f3a-a8dd109c39d8", "AQAAAAIAAYagAAAAEC29Yi1HSnK8NDh0ThFvRpL2m++WladDl5AATBb1+7ecY+sS7qRDfKEM1+mQfxj4Uw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0671396f-5deb-4283-8535-710fb398b159", "AQAAAAIAAYagAAAAEPd8EAcyFKwCPB/Favb+/lnphwCBypFugHvzz+eBUArGu3XFmrQPAQ0SKMABQsJVeQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a8f959b5-2cd8-47f3-9d06-4097e9ab2216", "AQAAAAIAAYagAAAAENk0QouD4v+Joj+5WCMiCz4GeZDtH40Kq5WkGqkOHwOQnXrrEUYvGKB5M5T+yo6OVg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "2811c71d-e9e5-4a0b-b073-ffeec2360436", "AQAAAAIAAYagAAAAEBJOlFHl8dQxc6CACEx48/M8vBIa9xBz7YQm4T2zUd1pp4aPfePuz829J7smBGu02w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"), new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320") });

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Invites",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "RoleId",
                table: "Invites",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Devices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"),
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"), null, "worker", "WORKER" });

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

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"), new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320") });
        }
    }
}
