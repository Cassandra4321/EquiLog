using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EquiLog.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddHorseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Horses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EmergencyContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoRiderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    HoofStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pasture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Blanket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlyMask = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Boots = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurnoutInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IntakeInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedingInstructions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OtherInfo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Horses_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Horses_OwnerId",
                table: "Horses",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Horses");
        }
    }
}
