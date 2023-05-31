using MyDartsPredictor.Bll.Dtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IPointService
{
    Task<IEnumerable<PointDto>> GetPointsByUserIdAsync(int userId);
    Task<IEnumerable<PointDto>> GetPointsByGameIdAsync(int gameId);
    Task<PointDto> GetPointByIdAsync(int pointId);
    Task<PointDto> CreatePointAsync(PointDto pointDto);
    Task<PointDto> UpdatePointAsync(int pointId, PointDto pointDto);
    Task DeletePointAsync(int pointId);
}
