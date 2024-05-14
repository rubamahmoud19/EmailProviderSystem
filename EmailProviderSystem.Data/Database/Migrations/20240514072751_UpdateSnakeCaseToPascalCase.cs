using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmailProviderSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSnakeCaseToPascalCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Folders_Folder_Id",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_User_Email",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "User_Email",
                table: "Folders",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_User_Email",
                table: "Folders",
                newName: "IX_Folders_UserEmail");

            migrationBuilder.RenameColumn(
                name: "Folder_Id",
                table: "Emails",
                newName: "FolderId");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_Folder_Id",
                table: "Emails",
                newName: "IX_Emails_FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Folders_FolderId",
                table: "Emails",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "FolderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_UserEmail",
                table: "Folders",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Folders_FolderId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_UserEmail",
                table: "Folders");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Folders",
                newName: "User_Email");

            migrationBuilder.RenameIndex(
                name: "IX_Folders_UserEmail",
                table: "Folders",
                newName: "IX_Folders_User_Email");

            migrationBuilder.RenameColumn(
                name: "FolderId",
                table: "Emails",
                newName: "Folder_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Emails_FolderId",
                table: "Emails",
                newName: "IX_Emails_Folder_Id");

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
    }
}
