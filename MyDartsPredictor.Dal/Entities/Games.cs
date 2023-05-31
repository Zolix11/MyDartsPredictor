namespace MyDartsPredictor.Dal.Entities;

public class Games
{
    public int Id { get; set; }
    public DateTime MatchDate { get; set; }
    public string Player1Name { get; set; } = null!;
    public string Player2Name { get; set; } = null!;
    public int TournamentId { get; set; }

    // Navigation properties
    public Tournament Tournament { get; set; } = null!;
    public Result? Result { get; set; }
    public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    public ICollection<Point> Points { get; set; } = new List<Point>();
}
