using MyDartsPredictor.Bll.Dtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IPredictionService
{
    Task<IEnumerable<PredictionDto>> GetPredictionsByUserIdAsync(int userId);
    Task<PredictionDto> GetPredictionByIdAsync(int predictionId);

    Task<IEnumerable<PredictionDto>> GetPredictionsByGameId(int gameId);
    Task<PredictionDto> CreatePredictionAsync(PredictionDto predictionDto);
    Task<PredictionDto> UpdatePredictionAsync(int predictionId, PredictionDto predictionDto);
    Task DeletePredictionAsync(int predictionId);
}
