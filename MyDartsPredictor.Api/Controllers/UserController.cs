using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly Bll.Interfaces.IUserSevice _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(Bll.Interfaces.IUserSevice userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving users.");
                return StatusCode(500, "An error occurred while retrieving users.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(int userId)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user.");
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<UserDto>> GetUserByAuthIdAsync()
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            try
            {
                if (uid == null)
                {
                    return NotFound();
                }
                var user = await _userService.GetUserByAuthidAsync(uid);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the user.");
                return StatusCode(500, "An error occurred while retrieving the user.");
            }
        }

        [HttpPost]
        [ActionName(nameof(GetUserByIdAsync))]
        public async Task<ActionResult<UserDto>> CreateUserAsync([FromBody] UserCreate userDto)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            try
            {
                if (uid == null)
                {
                    return NotFound();
                }
                var createdUser = await _userService.CreateUserAsync(userDto, uid);
                return CreatedAtAction(nameof(GetUserByIdAsync), new { userId = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the user.");
                return StatusCode(500, "An error occurred while creating the user.");
            }
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult<UserDto>> UpdateUserAsync(int userId, UserDto userDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userId, userDto);

                if (updatedUser == null)
                {
                    return NotFound();
                }

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the user.");
                return StatusCode(500, "An error occurred while updating the user.");
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUserAsync(int userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the user.");
                return StatusCode(500, "An error occurred while deleting the user.");
            }
        }
    }
}
