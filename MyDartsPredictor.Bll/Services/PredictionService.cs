using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Services;

public class PredictionService : IPredictionService
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public PredictionService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PredictionDto> CreatePredictionAsync(PredictionCreate predictionDto, string uid)
    {
        var user = await _dbContext.Users.Where(p => p.AuthUID == uid).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException($"User with ID user not found");

        }

        var game = await _dbContext.Games.FindAsync(predictionDto.GameId);
        if (game == null)
        {
            throw new NotFoundException($"Game with ID {predictionDto.GameId} not found");
        }

        var newPrediction = new Prediction
        {
            GameId = game.Id,
            UserId = user.Id,
            PredictionScore = predictionDto.PredictionScore,
            PredictionWinner = predictionDto.PredictionWinner,
        };
        _dbContext.Predictions.Add(newPrediction);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<PredictionDto>(newPrediction);
    }

    public async Task<PredictionDto> GetPredictionByIdAsync(int predictionId)
    {
        var prediction = await _dbContext.Predictions
            .Include(p => p.Game).ThenInclude(g => g.Tournament)
            .Include(p => p.User).ThenInclude(p => p.Predictions)
            .SingleOrDefaultAsync(p => p.Id == predictionId);
        if (prediction == null)
        {
            throw new NotFoundException("Prediction not found");
        }
        var predictionDto = _mapper.Map<PredictionDto>(prediction);
        return predictionDto;

    }

    public async Task<IEnumerable<PredictionDto>> GetPredictionsByGameAsync(int gameId)
    {
        var predictions = await _dbContext.Predictions
            .Include(p => p.User)
            .Include(p => p.Game)
            .Where(p => p.GameId == gameId)
            .ToListAsync();

        return _mapper.Map<IEnumerable<PredictionDto>>(predictions);
    }

    public async Task<PredictionDto> UpdatePredictionAsync(int predictionId, PredictionCreate changedDto, string uid)
    {
        var user = await _dbContext.Users.Where(p => p.AuthUID == uid).FirstOrDefaultAsync();

        if (user == null)
        {
            throw new NotFoundException($"User with ID user not found");

        }
        var existingPrediction = await _dbContext.Predictions
            .Include(p => p.User)
            .Include(p => p.Game)
            .FirstOrDefaultAsync(p => p.Id == predictionId);
        if (existingPrediction == null)
        {
            throw new NotFoundException("Not found prediction");
        }

        if (user.Id != existingPrediction.UserId)
        {
            throw new NotFoundException("Not found prediction");
        }

        existingPrediction.PredictionWinner = changedDto.PredictionWinner;
        existingPrediction.PredictionScore = changedDto.PredictionScore;

        var updatedPrediction = _mapper.Map<PredictionDto>(existingPrediction);
        await _dbContext.SaveChangesAsync();
        return updatedPrediction;
    }

}
