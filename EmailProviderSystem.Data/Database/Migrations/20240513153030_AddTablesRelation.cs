using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailProviderSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_UserEmail",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_UserEmail",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Folders");

            migrationBuilder.AlterColumn<string>(
                name: "HashPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "User_Email",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Folder_Id",
                table: "Emails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Folders_User_Email",
                table: "Folders",
                column: "User_Email");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_Folder_Id",
                table: "Emails",
                column: "Folder_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Folders_Folder_Id",
                table: "Emails",
                column: "Folder_Id",
                principalTable: "Folders",
                principalColumn: "FolderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_User_Email",
                table: "Folders",
                column: "User_Email",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Folders_Folder_Id",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_User_Email",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_User_Email",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Emails_Folder_Id",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "Folder_Id",
                table: "Emails");

            migrationBuilder.AlterColumn<string>(
                name: "HashPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "User_Email",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Folders",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Folders",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "From",
                table: "Emails",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_UserEmail",
                table: "Folders",
                column: "UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_UserEmail",
                table: "Folders",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email");
        }
    }
}
