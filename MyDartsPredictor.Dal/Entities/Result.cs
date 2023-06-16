namespace MyDartsPredictor.Dal.Entities;

public class Result
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int WinnerPlayer { get; set; }
    public string Score { get; set; } = null!;

    // Navigation property
    public Game Game { get; set; } = null!;
}
