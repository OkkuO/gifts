using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace snow_bot.Migrations
{
    /// <inheritdoc />
    public partial class giftMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GiftModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Points = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Coal = table.Column<int>(type: "integer", nullable: false),
                    Ring = table.Column<int>(type: "integer", nullable: false),
                    Socks = table.Column<int>(type: "integer", nullable: false),
                    Bear = table.Column<int>(type: "integer", nullable: false),
                    Dollar = table.Column<int>(type: "integer", nullable: false),
                    Matryoshka = table.Column<int>(type: "integer", nullable: false),
                    Bottle = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftModels", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GiftModels");
        }
    }
}
