namespace MyDartsPredictor.Dal.Entities;

public class UsersInTournament
{
    public int Id { get; set; }
    public TournamentId TournamentId { get; set; }
    public int UserId { get; set; }
    public int EarnedPoints { get; set; }

    // Navigation properties
    public Tournament Tournament { get; set; } = null!;
    public User User { get; set; } = null!;
}