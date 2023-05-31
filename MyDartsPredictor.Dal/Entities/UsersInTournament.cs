namespace MyDartsPredictor.Dal.Entities;

public class UsersInTournament
{
    public int Id { get; set; }
    public int TournamentId { get; set; }
    public int UserId { get; set; }
    public int EarnedPoints { get; set; }

    // Navigation properties
    public Tournament Tournament { get; set; } = null!;
    public Users User { get; set; } = null!;
}