using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_handin3.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "times",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    year = table.Column<int>(nullable: false),
                    day = table.Column<int>(nullable: false),
                    month = table.Column<int>(nullable: false),
                    hour = table.Column<int>(nullable: false),
                    minutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_times", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    WeatherObservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timeid = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Temperature = table.Column<decimal>(nullable: false),
                    Humidity = table.Column<int>(nullable: false),
                    AirPressure = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.WeatherObservationId);
                    table.ForeignKey(
                        name: "FK_Weather_times_Timeid",
                        column: x => x.Timeid,
                        principalTable: "times",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weather_Timeid",
                table: "Weather",
                column: "Timeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");

            migrationBuilder.DropTable(
                name: "times");
        }
    }
}
