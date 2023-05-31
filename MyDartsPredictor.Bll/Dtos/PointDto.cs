namespace MyDartsPredictor.Bll.Dtos;

public class PointDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int GameId { get; set; }
    public int PredictionId { get; set; }
    public int ResultId { get; set; }
    public int Points { get; set; }
    public UserDto User { get; set; } = null!;
    public GameDto Game { get; set; } = null!;
    public PredictionDto Prediction { get; set; } = null!;
    public ResultDto Result { get; set; } = null!;
}
