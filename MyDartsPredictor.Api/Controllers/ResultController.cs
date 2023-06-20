using Microsoft.AspNetCore.Mvc;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Api.Controllers
{
    [ApiController]
    [Route("api/results")]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<ResultDto>> GetResultByGameIdAsync(int gameId)
        {
            try
            {
                var result = await _resultService.GetResultByGameIdAsync(gameId);
                return Ok(result);
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

        [HttpGet("{resultId}")]
        public async Task<ActionResult<ResultDto>> GetResultByIdAsync(int resultId)
        {
            try
            {
                var result = await _resultService.GetResultByIdAsync(resultId);
                return Ok(result);
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
        [ActionName(nameof(GetResultByIdAsync))]
        public async Task<ActionResult<ResultDto>> CreateResultAsync(ResultCreate resultDto)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            if (uid == null)
            {
                return NotFound();
            }
            try
            {
                var createdResult = await _resultService.CreateResultAsync(resultDto, uid);
                return CreatedAtAction(nameof(GetResultByIdAsync), new { id = createdResult.Id }, createdResult);
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

        [HttpPut("{resultId}")]
        public async Task<ActionResult<ResultDto>> UpdateResultAsync(int resultId, ResultCreate resultDto)
        {
            try
            {
                var updatedResult = await _resultService.UpdateResultAsync(resultId, resultDto);
                return Ok(updatedResult);
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

        [HttpDelete("{resultId}")]
        public async Task<ActionResult> DeleteResultAsync(int resultId)
        {
            try
            {
                await _resultService.DeleteResultAsync(resultId);
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
