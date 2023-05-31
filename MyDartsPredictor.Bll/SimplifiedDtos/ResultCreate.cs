namespace MyDartsPredictor.Bll.SimplifiedDtos;

public class ResultCreate
{
    public int userId { get; set; }
    public int GameId { get; set; }
    public int WinnerPlayer { get; set; }
    public string Score { get; set; } = null!;
}
