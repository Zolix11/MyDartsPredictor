namespace MyDartsPredictor.Dal.Entities;

public class Tournament
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime FoundationTime { get; set; }
    public int FounderUserId { get; set; }

    // Navigation property
    public Users FounderUser { get; set; } = null!;
    public ICollection<Games> Games { get; set; } = new List<Games>();
    public ICollection<UsersInTournament> UsersInTournament { get; set; } = new List<UsersInTournament>();
}
