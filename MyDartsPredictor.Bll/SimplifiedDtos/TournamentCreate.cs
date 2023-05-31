namespace MyDartsPredictor.Bll.SimplifiedDtos
{
    public class TournamentCreate
    {
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public int founderUserId { get; set; }

    }
}
