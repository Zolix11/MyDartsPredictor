namespace MyDartsPredictor.Dal.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string AuthUID { get; set; } = null!;

        // Navigation properties
        public ICollection<UsersInTournament> UsersInTournaments { get; set; } = new List<UsersInTournament>();
        public ICollection<Prediction> Predictions { get; set; } = new List<Prediction>();
    }
}