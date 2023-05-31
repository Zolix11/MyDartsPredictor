namespace MyDartsPredictor.Bll.Dtos;

public class GameDto
{
    public int Id { get; set; }
    public DateTime MatchDate { get; set; }
    public string Player1Name { get; set; } = null!;
    public string Player2Name { get; set; } = null!;
    public TournamentDto Tournament { get; set; } = null!;
    public ResultDto? Result { get; set; }
    public List<PredictionDto> Predictions { get; set; } = new List<PredictionDto>();
    public List<PointDto> Points { get; set; } = new List<PointDto>();
}
