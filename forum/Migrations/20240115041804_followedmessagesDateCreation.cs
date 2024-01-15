using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forum.Migrations
{
    /// <inheritdoc />
    public partial class followedmessagesDateCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowedMessages",
                table: "FollowedMessages");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatioDateTime",
                table: "FollowedMessages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowedMessages",
                table: "FollowedMessages",
                columns: new[] { "postId", "userId", "CreatioDateTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FollowedMessages",
                table: "FollowedMessages");

            migrationBuilder.DropColumn(
                name: "CreatioDateTime",
                table: "FollowedMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FollowedMessages",
                table: "FollowedMessages",
                columns: new[] { "postId", "userId" });
        }
    }
}
