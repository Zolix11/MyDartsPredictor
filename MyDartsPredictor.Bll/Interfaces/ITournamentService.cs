using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface ITournamentService
{
    Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync();
    Task<TournamentDto> GetTournamentByIdAsync(int tournamentId);
    Task<IEnumerable<TournamentDto>> GetTournamentsByUserIdAsync(int userId);
    Task<TournamentDto> CreateTournamentAsync(TournamentCreate tournamentDto);
    Task<TournamentDto> UpdateTournamentAsync(int tournamentId, TournamentDto tournamentDto);
    Task DeleteTournamentAsync(int tournamentId);
}
