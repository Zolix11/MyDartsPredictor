namespace MyDartsPredictor.Bll.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public List<TournamentDto> Tournaments { get; set; } = new List<TournamentDto>();

    public List<PredictionDto> Predictions { get; set; } = new List<PredictionDto>();
}
