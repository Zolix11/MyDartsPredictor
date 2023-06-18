using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("{gameId}")]
        public async Task<ActionResult<GameDto>> GetGameByIdAsync(int gameId)
        {
            try
            {
                var game = await _gameService.GetGameByIdAsync(gameId);
                return Ok(game);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        [ActionName(nameof(GetGameByIdAsync))]
        public async Task<ActionResult<GameDto>> CreateGameAsync(GameCreate gameDto)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

            try
            {
                var createdGame = await _gameService.CreateGameAsync(gameDto, uid);
                return CreatedAtAction(nameof(GetGameByIdAsync), new { id = createdGame.Id }, createdGame);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("{gameId}")]
        public async Task<ActionResult> DeleteGameAsync(int gameId, [FromBody] int founderUserId)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;

            try
            {
                await _gameService.DeleteGameAsync(gameId, uid);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
