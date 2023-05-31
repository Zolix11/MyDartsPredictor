using MyDartsPredictor.Bll.Dtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IResultService
{
    Task<ResultDto> GetResultByGameIdAsync(int gameId);
    Task<ResultDto> GetResultByIdAsync(int resultId);
    Task<ResultDto> CreateResultAsync(ResultDto resultDto);
    Task<ResultDto> UpdateResultAsync(int resultId, ResultDto resultDto);
    Task DeleteResultAsync(int resultId);
}
