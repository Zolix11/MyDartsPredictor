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
    [Route("api/tournaments")]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;
        private readonly IUserSevice _userSevice;

        public TournamentController(ITournamentService tournamentService, IUserSevice userSevice)
        {
            _tournamentService = tournamentService;
            _userSevice = userSevice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTournaments()
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            if (uid == null)
            {
                return NotFound();
            }
            IEnumerable<TournamentDto> tournaments = await _tournamentService.GetAllTournamentsAsync();
            return Ok(tournaments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTournamentById(int id)
        {
            TournamentDto tournament = await _tournamentService.GetTournamentByIdAsync(id);
            if (tournament == null)
            {
                return NotFound();
            }
            return Ok(tournament);
        }

        [HttpPost("{tournamentId}/join")]
        public async Task<IActionResult> JoinPlayerToTournament(int tournamentId)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            if (uid == null)
            {
                return NotFound();
            }
            try
            {
                await _tournamentService.JoinPlayerToTournamentAsync(tournamentId, uid);
                return Ok("Player joined the tournament successfully.");
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
        }



        [HttpPost]
        [ActionName(nameof(GetTournamentById))]
        public async Task<IActionResult> CreateTournament(TournamentCreate tournamentDto)
        {
            var uid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "user_id")?.Value;
            if (uid == null)
            {
                return NotFound();
            }
            try
            {
                var createdTournament = await _tournamentService.CreateTournamentAsync(tournamentDto, uid);
                return CreatedAtAction(nameof(GetTournamentById), new { id = createdTournament.Id }, createdTournament);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTournament(int id, TournamentDto tournamentDto)
        {
            try
            {
                TournamentDto updatedTournament = await _tournamentService.UpdateTournamentAsync(id, tournamentDto);
                return Ok(updatedTournament);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTournament(int id)
        {

            return NotFound();
        }
    }
}
