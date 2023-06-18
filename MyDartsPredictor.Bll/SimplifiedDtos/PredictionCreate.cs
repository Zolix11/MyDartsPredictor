namespace MyDartsPredictor.Bll.SimplifiedDtos;

public class PredictionCreate
{
    public int GameId { get; set; }
    public int PredictionWinner { get; set; }
    public string PredictionScore { get; set; } = null!;
}
