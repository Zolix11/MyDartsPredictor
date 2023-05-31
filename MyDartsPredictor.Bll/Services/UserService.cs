using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;
using MyDartsPredictor.Dal.Entities;

namespace MyDartsPredictor.Bll.Services
{
    public class UserService : IUserSevice
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public UserService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _dbContext.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(UserCreate userCreationDto)
        {
            var user = new Users
            {
                Name = userCreationDto.Name,
                AzureAdB2CId = "id",
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            var addedUser = await _dbContext.Users.FindAsync(user.Id);
            var userDto = _mapper.Map<UserDto>(addedUser);
            return userDto;
        }

        public async Task<UserDto> UpdateUserAsync(int userId, UserDto userDto)
        {
            var existingUser = await _dbContext.Users.FindAsync(userId);

            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {userId} not found");
            }

            // Update the properties of the existing user entity
            existingUser.Name = userDto.Name;
            // Update other properties as needed

            await _dbContext.SaveChangesAsync();
            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task DeleteUserAsync(int userId)
        {
            var existingUser = await _dbContext.Users.FindAsync(userId);

            if (existingUser == null)
            {
                throw new NotFoundException($"User with ID {userId} not found");
            }

            _dbContext.Users.Remove(existingUser);
            await _dbContext.SaveChangesAsync();
        }
    }
}
