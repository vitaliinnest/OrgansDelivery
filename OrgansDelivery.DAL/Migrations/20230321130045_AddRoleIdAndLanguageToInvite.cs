using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrgansDelivery.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleIdAndLanguageToInvite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invites_AspNetRoles_RoleId",
                table: "Invites");

            migrationBuilder.DropIndex(
                name: "IX_Invites_RoleId",
                table: "Invites");

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Invites",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "Invites");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("9b1b1aa5-f517-401f-adf4-2d37c9f35f9a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e45d052b-fd6c-4965-80ad-b575ecffcdca", "AQAAAAIAAYagAAAAEKtpauWh+ctn9hTvggDLngtxZCIUTbsNHVIeBMP7x5eCxCVjhjS7F3rK0/jXQv+u/w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("da5bec95-daf8-41d6-b980-69091c89532a"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "79448760-bf6a-45e6-9c1c-3ee1597519d6", "AQAAAAIAAYagAAAAEDtvy3SY1dKME2WfvgHjb4UHjXJuHif2eXk5jbLi30HOMbff47MW3UGYq9U1pbpRtw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f7355727-a103-41fe-a1a8-a543b9682605"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "10067289-a393-4262-834a-8e53edec2c90", "AQAAAAIAAYagAAAAEHdSyHhvugYoZFkcrejPTn1cTjsCOASPMuQVcmOdWw4NY8U6hbed4CtPMnu1OiLSug==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f8243ba0-d6b5-46f5-9eaf-ff8da1f7e320"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c870f62d-7ee0-4a21-8d5e-de19562915ea", "AQAAAAIAAYagAAAAEGKY6NGJW41VTp3MHOVflW7L4C57V+7/R0O5XSOW5vd5HcQvmNZkPQ3LSJ1rZOQUFQ==" });

            migrationBuilder.CreateIndex(
                name: "IX_Invites_RoleId",
                table: "Invites",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invites_AspNetRoles_RoleId",
                table: "Invites",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
