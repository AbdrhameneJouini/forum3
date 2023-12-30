using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forum.Migrations
{
    /// <inheritdoc />
    public partial class InitialUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Forums",
                columns: table => new
                {
                    ForumID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreation = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forums", x => x.ForumID);
                });

            migrationBuilder.CreateTable(
                name: "Themes",
                columns: table => new
                {
                    ThemeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Themes", x => x.ThemeID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pseudonyme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotDePasse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Inscrit = table.Column<bool>(type: "bit", nullable: false),
                    Valide = table.Column<bool>(type: "bit", nullable: false),
                    CheminAvatar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Actif = table.Column<bool>(type: "bit", nullable: false),
                    Admin = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreationMessage = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sujet = table.Column<bool>(type: "bit", nullable: false),
                    MotCle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilID = table.Column<int>(type: "int", nullable: false),
                    ThemeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostID);
                    table.ForeignKey(
                        name: "FK_Posts_Themes_ThemeId",
                        column: x => x.ThemeId,
                        principalTable: "Themes",
                        principalColumn: "ThemeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThemesForums",
                columns: table => new
                {
                    ForumsForumID = table.Column<int>(type: "int", nullable: false),
                    ThemesThemeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThemesForums", x => new { x.ForumsForumID, x.ThemesThemeID });
                    table.ForeignKey(
                        name: "FK_ThemesForums_Forums_ForumsForumID",
                        column: x => x.ForumsForumID,
                        principalTable: "Forums",
                        principalColumn: "ForumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThemesForums_Themes_ThemesThemeID",
                        column: x => x.ThemesThemeID,
                        principalTable: "Themes",
                        principalColumn: "ThemeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FollowedMessages",
                columns: table => new
                {
                    postId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    Lu = table.Column<bool>(type: "bit", nullable: false),
                    Archive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FollowedMessages", x => new { x.postId, x.userId });
                    table.ForeignKey(
                        name: "FK_FollowedMessages_Posts_postId",
                        column: x => x.postId,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FollowedMessages_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReferences",
                columns: table => new
                {
                    ReferencedPostsPostID = table.Column<int>(type: "int", nullable: false),
                    ReferencingPostsPostID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReferences", x => new { x.ReferencedPostsPostID, x.ReferencingPostsPostID });
                    table.ForeignKey(
                        name: "FK_PostReferences_Posts_ReferencedPostsPostID",
                        column: x => x.ReferencedPostsPostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReferences_Posts_ReferencingPostsPostID",
                        column: x => x.ReferencingPostsPostID,
                        principalTable: "Posts",
                        principalColumn: "PostID");
                });

            migrationBuilder.CreateTable(
                name: "UsersPosts",
                columns: table => new
                {
                    PostsPostID = table.Column<int>(type: "int", nullable: false),
                    UsersUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersPosts", x => new { x.PostsPostID, x.UsersUserID });
                    table.ForeignKey(
                        name: "FK_UsersPosts_Posts_PostsPostID",
                        column: x => x.PostsPostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersPosts_Users_UsersUserID",
                        column: x => x.UsersUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FollowedMessages_userId",
                table: "FollowedMessages",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReferences_ReferencingPostsPostID",
                table: "PostReferences",
                column: "ReferencingPostsPostID");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ThemeId",
                table: "Posts",
                column: "ThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_ThemesForums_ThemesThemeID",
                table: "ThemesForums",
                column: "ThemesThemeID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersPosts_UsersUserID",
                table: "UsersPosts",
                column: "UsersUserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FollowedMessages");

            migrationBuilder.DropTable(
                name: "PostReferences");

            migrationBuilder.DropTable(
                name: "ThemesForums");

            migrationBuilder.DropTable(
                name: "UsersPosts");

            migrationBuilder.DropTable(
                name: "Forums");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Themes");
        }
    }
}
