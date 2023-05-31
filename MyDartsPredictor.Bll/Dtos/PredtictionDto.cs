namespace MyDartsPredictor.Bll.Dtos;

public class PredictionDto
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int UserId { get; set; }
    public int PredictionWinner { get; set; }
    public int PredictionScore { get; set; }
    public GameDto Game { get; set; } = null!;
    public UserDto User { get; set; } = null!;
}
