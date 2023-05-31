using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IPredictionService
{
    Task<PredictionDto> GetPredictionByIdAsync(int predictionId);
    Task<PredictionDto> CreatePredictionAsync(PredictionCreate predictionDto);
    Task<PredictionDto> UpdatePredictionAsync(int predictionId, PredictionCreate predictionDto);
}
