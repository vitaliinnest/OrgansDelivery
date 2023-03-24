using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedNameDescriptionToContainer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Containers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "39fc7548-afc1-4381-a212-89f2771da78c", "AQAAAAIAAYagAAAAEDT6h3q59THUYxLULmHkuKXiLWSCz8/abKCoIjuvNz1DijUVB34PI1H+lwe1g7QfzA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "98df55a9-42d1-4788-bcf2-c89f589641e1", "AQAAAAIAAYagAAAAEAh8s6nCQqII+zJa5rBMiX5Khd5CpLXF+z7rK9W/NivWwXETU1HyMh8uZ9GlHoxeNw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3e1121c9-e86c-4cc1-9633-75635faab4c4", "AQAAAAIAAYagAAAAEBuvSXFfLnBDDEmUaSX3VRLLof80Af/iSbCAWsDqh1WNRoFpjbaQ/qJLAIss5QVowQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d034a64c-ede2-4fe9-828a-c8aa5ab1b394", "AQAAAAIAAYagAAAAEE3TREUYWl1y26NBzXOhMwOltxMK7i/Q4n/owg0KfYpvk6Z6EXOHa/cqybFOkKRt7A==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Containers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Containers");

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
        }
    }
}
