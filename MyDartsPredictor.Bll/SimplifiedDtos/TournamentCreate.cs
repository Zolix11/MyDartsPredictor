using MyDartsPredictor.Bll.Dtos;

namespace MyDartsPredictor.Bll.SimplifiedDtos
{
    public class TournamentCreate
    {
        public string Name { get; set; } = null!;
        public DateTime Created { get; set; }
        public UserDto FounderUser { get; set; } = null!;

    }
}
