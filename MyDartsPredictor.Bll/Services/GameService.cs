using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Services;

public class GameService : IGameService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public GameService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<GameDto> GetGameByIdAsync(int gameId)
    {
        var game = await _dbContext.Games
            .Include(p => p.Tournament).ThenInclude(p => p.FounderUser)
            .FirstOrDefaultAsync(p => p.Id == gameId);

        if (game == null)
        {
            throw new NotFoundException($"Game with ID {gameId} not found");
        }

        var gameDto = _mapper.Map<GameDto>(game);
        return gameDto;
    }

    public async Task<GameDto> CreateGameAsync(GameCreate gameDto, string uid)
    {

        var user = await _dbContext.Users.Where(p => p.AuthUID == uid).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException($"User with ID user not found");
        }

        var tournament = await _dbContext
            .Tournaments
            .Include(t => t.FounderUser)
            .FirstOrDefaultAsync(t => t.Id == gameDto.tournamentId);
        if (tournament == null)
        {
            throw new NotFoundException($"Tournament with ID {gameDto.tournamentId} not found");

        }

        if (tournament.FounderUser.Id != user.Id)
        {
            throw new ConflictException($"Founder id{user.Id} is not the founder of the tournament");
        }

        var newGame = new Game
        {
            Player1Name = gameDto.Player1Name,
            Player2Name = gameDto.Player2Name,
            MatchDate = gameDto.MatchDate,
            TournamentId = gameDto.tournamentId,
        };
        _dbContext.Games.Add(newGame);
        await _dbContext.SaveChangesAsync();

        var addedGame = await _dbContext.Games.FindAsync(newGame.Id);
        var createdGameDto = _mapper.Map<GameDto>(addedGame);
        return createdGameDto;
    }

    public async Task DeleteGameAsync(int gameId, string uid)
    {
        var user = await _dbContext.Users.Where(p => p.AuthUID == uid).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException($"User with ID user not found");

        }
        var game = await _dbContext.Games
            .Include(p => p.Tournament)
            .ThenInclude(p => p.FounderUser)
            .FirstOrDefaultAsync(p => p.Id == gameId);
        if (game != null)
        {
            if (game.Tournament.FounderUserId != user.Id)
            {
                throw new NotFoundException($"Game with ID {gameId} not found");
            }
            var predictions = await _dbContext.Predictions
                .Where(p => p.GameId == gameId)
                .ToListAsync();

            var result = await _dbContext.Results.FirstOrDefaultAsync(p => p.GameId == gameId);
            if (result != null)
            {
                _dbContext.Results.Remove(result);

            }
            foreach (var prediction in predictions)
            {
                var userInTournament = await _dbContext.UsersInTournaments
                    .FirstOrDefaultAsync(u => u.UserId == prediction.UserId && u.TournamentId == prediction.Game.TournamentId);

                if (userInTournament != null && result != null)
                {
                    if (prediction.PredictionWinner == result.WinnerPlayer)
                    {
                        userInTournament.EarnedPoints -= 5;
                    }
                }

                _dbContext.Predictions.Remove(prediction);
            }

            _dbContext.Games.Remove(game);

            await _dbContext.SaveChangesAsync();
        }
    }

}
