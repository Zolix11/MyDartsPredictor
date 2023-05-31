using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDartsPredictor.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AzureAdB2CId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FoundationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FounderUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_Users_FounderUserId",
                        column: x => x.FounderUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersInTournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    EarnedPoints = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersInTournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsersInTournaments_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersInTournaments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersInTournaments_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Player1Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TournamentId = table.Column<int>(type: "int", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Tournaments_TournamentId",
                        column: x => x.TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Predictions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PredictionWinner = table.Column<int>(type: "int", nullable: false),
                    PredictionScore = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Predictions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Predictions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Predictions_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Predictions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Predictions_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    WinnerPlayer = table.Column<int>(type: "int", nullable: false),
                    Score = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Points",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GameId = table.Column<int>(type: "int", nullable: false),
                    PredictionId = table.Column<int>(type: "int", nullable: false),
                    ResultId = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: false),
                    GamesId = table.Column<int>(type: "int", nullable: true),
                    UsersId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Points", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Points_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Points_Games_GamesId",
                        column: x => x.GamesId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Points_Predictions_PredictionId",
                        column: x => x.PredictionId,
                        principalTable: "Predictions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Points_Results_ResultId",
                        column: x => x.ResultId,
                        principalTable: "Results",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Points_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Points_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Games_ResultId",
                table: "Games",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TournamentId",
                table: "Games",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_GameId",
                table: "Points",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_GamesId",
                table: "Points",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_PredictionId",
                table: "Points",
                column: "PredictionId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_ResultId",
                table: "Points",
                column: "ResultId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_UserId",
                table: "Points",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Points_UsersId",
                table: "Points",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_GameId",
                table: "Predictions",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_GamesId",
                table: "Predictions",
                column: "GamesId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_UserId",
                table: "Predictions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Predictions_UsersId",
                table: "Predictions",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_GameId",
                table: "Results",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_FounderUserId",
                table: "Tournaments",
                column: "FounderUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInTournaments_TournamentId",
                table: "UsersInTournaments",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInTournaments_UserId",
                table: "UsersInTournaments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersInTournaments_UsersId",
                table: "UsersInTournaments",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Results_ResultId",
                table: "Games",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Results_ResultId",
                table: "Games");

            migrationBuilder.DropTable(
                name: "Points");

            migrationBuilder.DropTable(
                name: "UsersInTournaments");

            migrationBuilder.DropTable(
                name: "Predictions");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
