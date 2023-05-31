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

        [HttpPost]
        public async Task<ActionResult<PredictionDto>> CreatePredictionAsync(PredictionCreate predictionDto)
        {
            try
            {
                var createdPrediction = await _predictionService.CreatePredictionAsync(predictionDto);
                return Ok(createdPrediction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("{predictionId}")]
        public async Task<ActionResult<PredictionDto>> UpdatePredictionAsync(int predictionId, PredictionCreate predictionDto)
        {
            try
            {
                var updatedPrediction = await _predictionService.UpdatePredictionAsync(predictionId, predictionDto);
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
