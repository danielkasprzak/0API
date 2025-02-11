using _0API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _0API.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IStatisticsService _statisticsService;

        public UserController(IUserService userService, IStatisticsService statisticsService)
        {
            _userService = userService;
            _statisticsService = statisticsService;
        }

        [HttpPost("check")]
        public async Task<IActionResult> CheckUser([FromBody] string hwid)
        {
            if (string.IsNullOrEmpty(hwid))
            {
                return BadRequest("HWID is required.");
            }

            var user = await _userService.CheckAndAddUserAsync(hwid);
            if (await _userService.IsUserBannedAsync(hwid))
            {
                return BadRequest("User is banned.");
            }

            await _statisticsService.IncrementTotalOpensAsync();
            return Ok("User is allowed.");
        }

        [HttpPost("heartbeat")]
        public async Task<IActionResult> SendHeartbeat([FromBody] string hwid)
        {
            await _userService.UpdateHeartbeatAsync(hwid);
            return Ok();
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetActiveUserCount()
        {
            var activeUsers = await _userService.GetActiveUserCountAsync();
            return Ok(new { ActiveUsers = activeUsers });
        }

        [HttpGet("total")]
        public async Task<IActionResult> GetTotalUserCount()
        {
            var totalUsers = await _userService.GetTotalUserCountAsync();
            return Ok(new { TotalUsers = totalUsers });
        }
    }
}
