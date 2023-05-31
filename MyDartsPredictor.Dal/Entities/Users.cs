namespace MyDartsPredictor.Dal.Entities
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string AzureAdB2CId { get; set; }

        // Navigation properties
        public ICollection<UsersInTournament> UsersInTournaments { get; set; } = new List<UsersInTournament>();
        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}