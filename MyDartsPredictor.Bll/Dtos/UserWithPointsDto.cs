namespace MyDartsPredictor.Bll.Dtos;

public class UserWithPointsDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = null!;
    public int EarnedPoints { get; set; }
}
