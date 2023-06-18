using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface ITournamentService
{
    Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync();
    Task<TournamentDto> GetTournamentByIdAsync(int tournamentId);
    Task<IEnumerable<TournamentDto>> GetTournamentsByUserIdAsync(string uid);
    Task JoinPlayerToTournamentAsync(int tournamentId, string uid);
    Task<TournamentDto> CreateTournamentAsync(TournamentCreate tournamentDto, string uid);
    Task<TournamentDto> UpdateTournamentAsync(int tournamentId, TournamentDto tournamentDto);
    Task DeleteTournamentAsync(int tournamentId);
}
