using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forum.Migrations
{
    /// <inheritdoc />
    public partial class changemenets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Forums_ThemeId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FilID",
                table: "Posts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreationMessage",
                table: "Posts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreationLastMessage",
                table: "Posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Forums_ThemeId",
                table: "Posts",
                column: "ThemeId",
                principalTable: "Forums",
                principalColumn: "ForumID");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "ThemeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Forums_ThemeId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "DateCreationLastMessage",
                table: "Posts");

            migrationBuilder.AlterColumn<int>(
                name: "ThemeId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FilID",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreationMessage",
                table: "Posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Forums_ThemeId",
                table: "Posts",
                column: "ThemeId",
                principalTable: "Forums",
                principalColumn: "ForumID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Themes_ThemeId",
                table: "Posts",
                column: "ThemeId",
                principalTable: "Themes",
                principalColumn: "ThemeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
