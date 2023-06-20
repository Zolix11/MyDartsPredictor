namespace MyDartsPredictor.Bll.Dtos;

public class TournamentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime FoundationTime { get; set; }
    public int FounderUserId { get; set; }
    public List<GameDto> Games { get; set; } = new List<GameDto>();
    public List<UserWithPointsDto> UsersWithPoints { get; set; } = new List<UserWithPointsDto>();
}
