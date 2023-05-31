using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IGameService
{
    Task<GameDto> GetGameByIdAsync(int gameId);
    Task<GameDto> CreateGameAsync(GameCreate gameDto);
    Task DeleteGameAsync(int gameId);
}
