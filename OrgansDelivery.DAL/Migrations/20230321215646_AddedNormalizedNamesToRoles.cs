using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedNormalizedNamesToRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"),
                column: "NormalizedName",
                value: "MANAGER");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"),
                column: "NormalizedName",
                value: "EMPLOYEE");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"),
                column: "NormalizedName",
                value: "ADMIN");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afab5690-50ea-4043-bf7b-0f825f068b94"),
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("afd73abe-c965-44b6-a5ac-42a728507f68"),
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ca8531d7-5592-4678-80d2-aecdcb462208"),
                column: "NormalizedName",
                value: null);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9ca0e14e-6942-400d-a574-034cacd2f759", "AQAAAAIAAYagAAAAEJSWger0s3dwtIoVgFktNOV2v+MeALGI13g9UYOf+8trcNROZgAgcEo/QdS2N3BB9Q==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "37d66e05-34f9-45d7-8af7-6d31453d3542", "AQAAAAIAAYagAAAAEP0B413HadY0jEGiRDVZiq3ZVl3Ur4ngkuIQwuX5VkwoArp1qiKz7tl9jeauOhCiIQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "69f206cb-3960-4a16-9653-d482b37726a5", "AQAAAAIAAYagAAAAEINs06Cb634aGZEyaYlPSFN+2ZRdC1c0JPltozKKL7Ko4Ilm8LadX4VBIkiFVa0Jpg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "71be6c02-f795-4626-a8a5-8b980f6a5f01", "AQAAAAIAAYagAAAAEJgFUcth24Vcinb18M2f5g9nRqgwhBMOgOCnsAgvgiTmy2q8GREcUu5Vd79LztRpJQ==" });
        }
    }
}
