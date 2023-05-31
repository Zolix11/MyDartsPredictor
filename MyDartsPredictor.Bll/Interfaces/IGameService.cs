using MyDartsPredictor.Bll.Dtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IGameService
{
    Task<IEnumerable<GameDto>> GetGamesByTournamentIdAsync(int tournamentId);
    Task<GameDto> GetGameByIdAsync(int gameId);
    Task<GameDto> CreateGameAsync(GameDto gameDto);
    Task<GameDto> UpdateGameAsync(int gameId, GameDto gameDto);
    Task DeleteGameAsync(int gameId);
}
