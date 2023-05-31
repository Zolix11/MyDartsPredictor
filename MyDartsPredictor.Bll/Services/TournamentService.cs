using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;


        public TournamentService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TournamentDto>> GetAllTournamentsAsync()
        {
            var tournaments = await _dbContext.Tournaments.ToListAsync();
            var tournamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return tournamentDtos;
        }

        public async Task<TournamentDto> GetTournamentByIdAsync(int tournamentId)
        {
            var tournament = await _dbContext.Tournaments.FindAsync(tournamentId);
            await Console.Out.WriteLineAsync(tournament.ToString());
            var tournamentDto = _mapper.Map<TournamentDto>(tournament);
            return tournamentDto;
        }

        public async Task<TournamentDto> CreateTournamentAsync(TournamentCreate tournamentCreateDto)
        {
            var tournament = new Tournament
            {
                Name = tournamentCreateDto.Name,
                FoundationTime = DateTime.UtcNow,
                FounderUserId = tournamentCreateDto.FounderUser.Id
            };

            _dbContext.Tournaments.Add(tournament);
            await _dbContext.SaveChangesAsync();

            var addedTournament = await _dbContext.Tournaments
                   .Include(t => t.FounderUser)
                   .Include(t => t.Games)
                   .ThenInclude(g => g.Result)
                   .FirstOrDefaultAsync(t => t.Id == tournament.Id);

            return _mapper.Map<TournamentDto>(addedTournament);
        }

        public async Task<TournamentDto> UpdateTournamentAsync(int tournamentId, TournamentDto tournamentDto)
        {
            var existingTournament = await _dbContext.Tournaments.FindAsync(tournamentId);
            if (existingTournament == null)
            {
                throw new NotFoundException();
            }

            _mapper.Map(tournamentDto, existingTournament);
            await _dbContext.SaveChangesAsync();

            return tournamentDto;
        }

        public async Task DeleteTournamentAsync(int tournamentId)
        {
            var tournament = await _dbContext.Tournaments.FindAsync(tournamentId);
            if (tournament == null)
            {
                throw new NotFoundException();
            }

            _dbContext.Tournaments.Remove(tournament);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TournamentDto>> GetTournamentsByUserIdAsync(int userId)
        {
            var tournaments = await _dbContext.Tournaments
                .Where(t => t.FounderUserId == userId)
                .ToListAsync();

            var tournamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);
            return tournamentDtos;
        }



    }
}
