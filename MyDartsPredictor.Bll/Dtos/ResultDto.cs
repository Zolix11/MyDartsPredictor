namespace MyDartsPredictor.Bll.Dtos;

public class ResultDto
{
    public int Id { get; set; }
    public int GameId { get; set; }
    public int WinnerPlayer { get; set; }
    public string Score { get; set; } = null!;
    public GameDto Game { get; set; } = null!;
}
