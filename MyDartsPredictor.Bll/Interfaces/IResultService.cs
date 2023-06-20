using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IResultService
{
    Task<ResultDto> GetResultByGameIdAsync(int gameId);
    Task<ResultDto> GetResultByIdAsync(int resultId);
    Task<ResultDto> CreateResultAsync(ResultCreate resultDto, string uid);
    Task<ResultDto> UpdateResultAsync(int resultId, ResultCreate resultDto);
    Task DeleteResultAsync(int resultId);
}
