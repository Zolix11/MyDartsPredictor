using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Bll.Interfaces;

public interface IUserSevice
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> GetUserByIdAsync(int userId);
    Task<UserDto> CreateUserAsync(UserCreate userDto);
    Task<UserDto> UpdateUserAsync(int userId, UserDto userDto);
    Task DeleteUserAsync(int userId);
}
