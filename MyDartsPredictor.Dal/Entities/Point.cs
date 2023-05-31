namespace MyDartsPredictor.Dal.Entities;

public class Point
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public int PredictionId { get; set; }
    public int ResultId { get; set; }
    public int Points { get; set; }

    // Navigation properties
    public Users User { get; set; } = null!;
    public Games Game { get; set; } = null!;
    public Prediction Prediction { get; set; } = null!;
    public Result Result { get; set; } = null!;
}
