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
            var tournaments = await _dbContext.Tournaments
                 .Include(u => u.FounderUser)
                 .Include(u => u.UsersInTournament).ThenInclude(u => u.User)
                 .Include(p => p.Games)
                 .ToListAsync();

            var tournamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(tournaments);

            foreach (var tournamentDto in tournamentDtos)
            {
                var listUsersWithPoints = await _dbContext.UsersInTournaments
                    .Where(p => p.TournamentId == tournamentDto.Id)
                    .ToListAsync();

                foreach (var userWithPoints in listUsersWithPoints)
                {
                    var userWithPointsDto = _mapper.Map<UserWithPointsDto>(userWithPoints);
                    tournamentDto.UsersWithPoints.Add(userWithPointsDto);
                }
            }

            return tournamentDtos;
        }

        public async Task<TournamentDto> GetTournamentByIdAsync(int tournamentId)
        {
            var tournament = await _dbContext.Tournaments
                 .Include(u => u.FounderUser)
                .Include(u => u.UsersInTournament).ThenInclude(u => u.User)
                .Include(p => p.Games)
                .FirstOrDefaultAsync(t => t.Id == tournamentId);
            var tournamentDto = _mapper.Map<TournamentDto>(tournament);

            var listUsersWithPoints = await _dbContext.UsersInTournaments
                .Where(p => p.TournamentId == tournamentId)
                .ToListAsync();

            foreach (var userWithPoints in listUsersWithPoints)
            {
                var userWithPointsDto = _mapper.Map<UserWithPointsDto>(userWithPoints);
                tournamentDto.UsersWithPoints.Add(userWithPointsDto);
            }
            return tournamentDto;
        }

        public async Task<TournamentDto> CreateTournamentAsync(TournamentCreate tournamentCreateDto)
        {

            var founderUser = await _dbContext.Users.FindAsync(tournamentCreateDto.founderUserId);
            if (founderUser == null)
            {
                throw new NotFoundException("User not found.");
            }

            var tournament = new Tournament
            {
                Name = tournamentCreateDto.Name,
                FoundationTime = DateTime.UtcNow,
                FounderUserId = founderUser.Id
            };

            _dbContext.Tournaments.Add(tournament);
            await _dbContext.SaveChangesAsync();

            var addedTournament = await _dbContext.Tournaments
                   .Include(t => t.FounderUser)
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

        public async Task JoinPlayerToTournamentAsync(int tournamentId, int playerId)
        {
            var tournament = await _dbContext
                .Tournaments
                .Include(f => f.FounderUser)
                .Include(u => u.UsersInTournament)
                       .ThenInclude(u => u.User)
                .FirstOrDefaultAsync(p => p.Id == tournamentId);

            if (tournament == null)
            {
                throw new NotFoundException("Tournament not found.");

            }
            var player = await _dbContext.Users.FindAsync(playerId);
            if (player == null)
            {
                throw new NotFoundException("User not found.");
            }

            var userAlreadyIn = tournament.UsersInTournament.FirstOrDefault(p => p.UserId == playerId);
            if (userAlreadyIn != null)
            {
                throw new ConflictException($"Player with ID {playerId} is already in tournament : {tournament.Name}.");
            }

            var tournamentDto = _mapper.Map<TournamentDto>(tournament);

            var newUserInTournament = new UsersInTournament
            {
                TournamentId = tournamentId,
                UserId = playerId,
                EarnedPoints = 0,
            };

            _dbContext.UsersInTournaments.Add(newUserInTournament);
            await _dbContext.SaveChangesAsync();
        }
    }
}
