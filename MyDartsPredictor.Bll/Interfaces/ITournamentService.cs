using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Interfaces;

public interface ITournamentService
{
    Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync();
    Task<TournamentDto> GetTournamentByIdAsync(TournamentId tournamentId);
    Task<IEnumerable<TournamentDto>> GetTournamentsByUserIdAsync(string uid);
    Task JoinPlayerToTournamentAsync(TournamentId tournamentId, string uid);
    Task<TournamentDto> CreateTournamentAsync(TournamentCreate tournamentDto, string uid);
    Task<TournamentDto> UpdateTournamentAsync(TournamentId tournamentId, TournamentDto tournamentDto);
    Task DeleteTournamentAsync(int tournamentId);
}
