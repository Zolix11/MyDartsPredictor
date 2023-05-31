namespace MyDartsPredictor.Bll.SimplifiedDtos;

public class GameCreate
{
    public DateTime MatchDate { get; set; }
    public string Player1Name { get; set; } = null!;
    public string Player2Name { get; set; } = null!;

    public int founderId { get; set; }

    public int tournamentId { get; set; }
}
