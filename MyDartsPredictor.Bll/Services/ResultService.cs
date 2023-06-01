using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Services;

public class ResultService : IResultService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public ResultService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ResultDto> GetResultByGameIdAsync(int gameId)
    {
        var result = await _dbContext.Results.FirstOrDefaultAsync(r => r.GameId == gameId);

        if (result == null)
        {
            throw new NotFoundException($"Result for game with ID {gameId} not found");
        }

        var resultDto = _mapper.Map<ResultDto>(result);
        return resultDto;
    }

    public async Task<ResultDto> GetResultByIdAsync(int resultId)
    {
        var result = await _dbContext.Results.FindAsync(resultId);

        if (result == null)
        {
            throw new NotFoundException($"Result with ID {resultId} not found");
        }

        var resultDto = _mapper.Map<ResultDto>(result);
        return resultDto;
    }

    public async Task<ResultDto> CreateResultAsync(ResultCreate resultDto)
    {
        var game = await _dbContext.Games
            .Include(p => p.Tournament)
            .Include(p => p.Result)
            .FirstOrDefaultAsync(p => p.Id == resultDto.GameId);
        if (game == null)
        {
            throw new NotFoundException($"No game found for id {resultDto.GameId} result");
        }
        if (game.Tournament.FounderUserId != resultDto.userId)
        {
            throw new NotFoundException($"You are not the founder of this tournament");
        }
        if (game.Result != null)
        {
            throw new NotFoundException($"There is already a result for that game");
        }
        var newResult = new Result
        {
            Game = game,
            WinnerPlayer = resultDto.WinnerPlayer,
            Score = resultDto.Score,
        };
        _dbContext.Results.Add(newResult);
        await _dbContext.SaveChangesAsync();

        var resultId = newResult.Id;

        if (game != null)
        {
            game.ResultId = resultId;
            await _dbContext.SaveChangesAsync();
        }

        var predictions = await _dbContext.Predictions
         .Where(p => p.GameId == resultDto.GameId)
         .ToListAsync();

        foreach (var prediction in predictions)
        {
            var userInTournament = await _dbContext.UsersInTournaments
                .FirstOrDefaultAsync(u => u.UserId == prediction.UserId && u.TournamentId == game.TournamentId);

            if (userInTournament != null)
            {
                if (prediction.PredictionWinner == resultDto.WinnerPlayer)
                {
                    userInTournament.EarnedPoints += 5;
                }
            }
        }

        await _dbContext.SaveChangesAsync();
        var newResultDto = _mapper.Map<ResultDto>(newResult);
        return newResultDto;
    }

    public async Task<ResultDto> UpdateResultAsync(int resultId, ResultCreate resultDto)
    {
        var existingResult = await _dbContext.Results.FindAsync(resultId);

        if (existingResult == null)
        {
            throw new NotFoundException($"Result with ID {resultId} not found");
        }

        existingResult.Score = resultDto.Score;
        existingResult.WinnerPlayer = resultDto.WinnerPlayer;

        await _dbContext.SaveChangesAsync();

        var updatedResultDto = _mapper.Map<ResultDto>(existingResult);
        return updatedResultDto;
    }

    public async Task DeleteResultAsync(int resultId)
    {
        var existingResult = await _dbContext.Results.Include(p => p.Game).FirstOrDefaultAsync(p => p.Id == resultId);

        if (existingResult == null)
        {
            throw new NotFoundException($"Result with ID {resultId} not found");
        }
        var gamesToUpdate = await _dbContext.Games
            .Where(g => g.ResultId == existingResult.Id)
            .ToListAsync();

        // Update the ResultId to null for each game
        foreach (var game in gamesToUpdate)
        {
            game.ResultId = null;
        }

        existingResult.Game.Result = null;
        existingResult.Game.ResultId = null;
        var gameId = existingResult.GameId;

        _dbContext.Results.Remove(existingResult);
        await _dbContext.SaveChangesAsync();

        var predictions = await _dbContext.Predictions
            .Where(p => p.GameId == gameId)
            .ToListAsync();

        foreach (var prediction in predictions)
        {
            var userInTournament = await _dbContext.UsersInTournaments
                .FirstOrDefaultAsync(u => u.UserId == prediction.UserId && u.TournamentId == prediction.Game.TournamentId);

            if (userInTournament != null)
            {
                if (prediction.PredictionWinner == existingResult.WinnerPlayer)
                {
                    userInTournament.EarnedPoints -= 5;
                }
            }
        }

        await _dbContext.SaveChangesAsync();

    }
}
