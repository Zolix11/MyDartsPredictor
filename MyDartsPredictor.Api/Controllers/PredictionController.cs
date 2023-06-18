using Microsoft.AspNetCore.Mvc;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Api.Controllers
{
    [ApiController]
    [Route("api/predictions")]
    public class PredictionController : ControllerBase
    {
        private readonly IPredictionService _predictionService;

        public PredictionController(IPredictionService predictionService)
        {
            _predictionService = predictionService;
        }

        [HttpGet("{predictionId}")]
        public async Task<ActionResult<PredictionDto>> GetPredictionByIdAsync(int predictionId)
        {
            try
            {
                var prediction = await _predictionService.GetPredictionByIdAsync(predictionId);
                return Ok(prediction);
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

        [HttpGet("game/{gameId}")]
        public async Task<ActionResult<IEnumerable<PredictionDto>>> GetPredictionsByGameAsync(int gameId)
        {
            try
            {
                var predictions = await _predictionService.GetPredictionsByGameAsync(gameId);
                return Ok(predictions);
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
        [ActionName(nameof(GetPredictionByIdAsync))]
        public async Task<ActionResult<PredictionDto>> CreatePredictionAsync(PredictionCreate predictionDto)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            if (uid == null)
            {
                return NotFound();
            }
            try
            {
                var createdPrediction = await _predictionService.CreatePredictionAsync(predictionDto, uid);
                return CreatedAtAction(nameof(GetPredictionByIdAsync), new { id = createdPrediction.Id }, createdPrediction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{predictionId}")]
        public async Task<ActionResult<PredictionDto>> UpdatePredictionAsync(int predictionId, PredictionCreate predictionDto)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            if (uid == null)
            {
                return NotFound();
            }
            try
            {
                var updatedPrediction = await _predictionService.UpdatePredictionAsync(predictionId, predictionDto, uid);
                return Ok(updatedPrediction);
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
