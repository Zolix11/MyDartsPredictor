namespace MyDartsPredictor.Bll.Dtos;

public class GameDto
{

    public int Id { get; set; }
    public DateTime MatchDate { get; set; }
    public string Player1Name { get; set; } = null!;
    public string Player2Name { get; set; } = null!;

}
