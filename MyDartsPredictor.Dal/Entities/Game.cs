namespace MyDartsPredictor.Dal.Entities;

public class Game
{
    public int Id { get; set; }
    public DateWithTimeZone MatchDate { get; set; }
    public string Player1Name { get; set; } = null!;
    public string Player2Name { get; set; } = null!;
    public TournamentId TournamentId { get; set; }
    public int? ResultId { get; set; }

    // Navigation properties
    public Tournament Tournament { get; set; } = null!;
    public Result? Result { get; set; }
    public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
}
