namespace MyDartsPredictor.Dal.Entities;

public readonly record struct TournamentId(int Value);
public class Tournament
{
    public TournamentId Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime FoundationTime { get; set; }
    public int FounderUserId { get; set; }

    // Navigation property
    public User FounderUser { get; set; } = null!;
    public ICollection<Game> Games { get; set; } = new List<Game>();
    public ICollection<UsersInTournament> UsersInTournament { get; set; } = new List<UsersInTournament>();
}
