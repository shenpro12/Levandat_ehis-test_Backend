using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "menu",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "varchar(50)", nullable: false),
                    lft = table.Column<int>(type: "int", nullable: false),
                    rgt = table.Column<int>(type: "int", nullable: false),
                    parentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_menu", x => x.ID);
                    table.ForeignKey(
                        name: "FK_menu_menu_parentID",
                        column: x => x.parentID,
                        principalTable: "menu",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_menu_parentID",
                table: "menu",
                column: "parentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "menu");
        }
    }
}
