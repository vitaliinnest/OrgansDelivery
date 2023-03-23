using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ApplySeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"), null, "manager", null },
                    { new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"), null, "employee", null },
                    { new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"), null, "admin", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "Language", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Surname", "TenantId", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"), 0, "e45d052b-fd6c-4965-80ad-b575ecffcdca", "JavonSpencer@data.com", true, 0, false, null, "Javon", null, null, "AQAAAAIAAYagAAAAEKtpauWh+ctn9hTvggDLngtxZCIUTbsNHVIeBMP7x5eCxCVjhjS7F3rK0/jXQv+u/w==", null, false, null, "Spencer", new Guid("00000000-0000-0000-0000-000000000000"), false, null },
                    { new Guid("da5bec95-daf8-41d6-b980-69091c89532a"), 0, "79448760-bf6a-45e6-9c1c-3ee1597519d6", "CharlesHuber@medtronic.com", true, 0, false, null, "Charles", null, null, "AQAAAAIAAYagAAAAEDtvy3SY1dKME2WfvgHjb4UHjXJuHif2eXk5jbLi30HOMbff47MW3UGYq9U1pbpRtw==", null, false, null, "Huber", new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"), false, null },
                    { new Guid("f7355727-a103-41fe-a1a8-a543b9682605"), 0, "10067289-a393-4262-834a-8e53edec2c90", "AndresBlake@bostoncorp.com", false, 0, false, null, "Andres", null, null, "AQAAAAIAAYagAAAAEHdSyHhvugYoZFkcrejPTn1cTjsCOASPMuQVcmOdWw4NY8U6hbed4CtPMnu1OiLSug==", null, false, null, "Blake", new Guid("f4c76e71-6a6c-476a-9d15-328a09447646"), false, null },
                    { new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"), 0, "c870f62d-7ee0-4a21-8d5e-de19562915ea", "ReganLewis@medtronic.com", true, 0, false, null, "Regan", null, null, "AQAAAAIAAYagAAAAEGKY6NGJW41VTp3MHOVflW7L4C57V+7/R0O5XSOW5vd5HcQvmNZkPQ3LSJ1rZOQUFQ==", null, false, null, "Lewis", new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"), false, null }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "Id", "Description", "Name", "Url" },
                values: new object[,]
                {
                    { new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"), "Better health access for our world by combining the powerof healthcare technology with strong partnerships,we are helping put equity within reach", "Medtronic", "medtronic" },
                    { new Guid("f4c76e71-6a6c-476a-9d15-328a09447646"), "A Boston Scientific device brought Caroline relief – and the inspiration to pursue medicine", "Boston Scientific Corporation", "bostoncorp" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"), new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a") },
                    { new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"), new Guid("da5bec95-daf8-41d6-b980-69091c89532a") },
                    { new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"), new Guid("f7355727-a103-41fe-a1a8-a543b9682605") },
                    { new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"), new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"), new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"), new Guid("da5bec95-daf8-41d6-b980-69091c89532a") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"), new Guid("f7355727-a103-41fe-a1a8-a543b9682605") });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"), new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320") });

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("a906297e-d95d-4116-b479-e5e3f323b26d"));

            migrationBuilder.DeleteData(
                table: "Tenants",
                keyColumn: "Id",
                keyValue: new Guid("f4c76e71-6a6c-476a-9d15-328a09447646"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"));
        }
    }
}
