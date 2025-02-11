using _0API.Models;
using _0API.Services;
using Microsoft.AspNetCore.Mvc;

namespace _0API.Controllers
{
    [ApiController]
    [Route("statistics")]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet]
        public async Task<ActionResult<Statistics>> GetStatistics()
        {
            var statistics = await _statisticsService.GetStatisticsAsync();
            return Ok(statistics);
        }

        [HttpPost("increment-pentests")]
        public async Task<IActionResult> IncrementTotalPentests()
        {
            await _statisticsService.IncrementTotalPentestsAsync();
            return NoContent();
        }
    }
}
