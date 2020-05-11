using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_handin3.Migrations
{
    public partial class fixingTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Temperatue",
                table: "Weather");

            migrationBuilder.AddColumn<decimal>(
                name: "Temperature",
                table: "Weather",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Timeentry",
                table: "Weather",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Time",
                columns: table => new
                {
                    entry = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    day = table.Column<string>(nullable: true),
                    month = table.Column<string>(nullable: true),
                    hour = table.Column<string>(nullable: true),
                    minutes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time", x => x.entry);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Weather_Timeentry",
                table: "Weather",
                column: "Timeentry");

            migrationBuilder.AddForeignKey(
                name: "FK_Weather_Time_Timeentry",
                table: "Weather",
                column: "Timeentry",
                principalTable: "Time",
                principalColumn: "entry",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weather_Time_Timeentry",
                table: "Weather");

            migrationBuilder.DropTable(
                name: "Time");

            migrationBuilder.DropIndex(
                name: "IX_Weather_Timeentry",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Temperature",
                table: "Weather");

            migrationBuilder.DropColumn(
                name: "Timeentry",
                table: "Weather");

            migrationBuilder.AddColumn<decimal>(
                name: "Temperatue",
                table: "Weather",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
