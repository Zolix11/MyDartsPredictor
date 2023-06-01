using Microsoft.AspNetCore.Mvc;
using MyDartsPredictor.Bll.Dtos;
using MyDartsPredictor.Bll.Expections;
using MyDartsPredictor.Bll.Interfaces;
using MyDartsPredictor.Bll.SimplifiedDtos;

namespace MyDartsPredictor.Api.Controllers
{
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

        [HttpPost("{tournamentId}/join/{playerId}")]
        public async Task<IActionResult> JoinPlayerToTournament(int tournamentId, int playerId)
        {
            try
            {
                await _tournamentService.JoinPlayerToTournamentAsync(tournamentId, playerId);
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
            try
            {
                var createdTournament = await _tournamentService.CreateTournamentAsync(tournamentDto);
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
        public async Task<IActionResult> DeleteTournament(int id, int userId)
        {
            try
            {
                TournamentDto tournament = await _tournamentService.GetTournamentByIdAsync(id);
                if (tournament != null && tournament.FounderUser.Id == userId)
                {
                    await _tournamentService.DeleteTournamentAsync(id);
                    return NoContent();

                }
                return Unauthorized("You are not the founder user");
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
