using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace forum.Migrations
{
    /// <inheritdoc />
    public partial class newAbonneUsersPosts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AbonneUsersPosts",
                columns: table => new
                {
                    AbonnePostsPostID = table.Column<int>(type: "int", nullable: false),
                    AbonneUsersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbonneUsersPosts", x => new { x.AbonnePostsPostID, x.AbonneUsersId });
                    table.ForeignKey(
                        name: "FK_AbonneUsersPosts_AspNetUsers_AbonneUsersId",
                        column: x => x.AbonneUsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbonneUsersPosts_Posts_AbonnePostsPostID",
                        column: x => x.AbonnePostsPostID,
                        principalTable: "Posts",
                        principalColumn: "PostID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbonneUsersPosts_AbonneUsersId",
                table: "AbonneUsersPosts",
                column: "AbonneUsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AbonneUsersPosts");
        }
    }
}
