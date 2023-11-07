using Microsoft.AspNetCore.Mvc;
using _0API.Context;
using _0API.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace _0API.Controllers
{
    [Route("api/daio")]
    [ApiController]
    public class daioController : ControllerBase
    {
        private readonly _0DbContext _context;
        private readonly IConfiguration _configuration;
        public daioController(_0DbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration=configuration;   
        }

        // Check user HWID
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("hwid")]
        public async Task<IActionResult> CheckHWID()
        {
            try
            {
                var claims = (ClaimsIdentity)User.Identity;
                var _hwid = claims.FindFirst("hwid")?.Value;
                if (_hwid == null)
                {
                    return Unauthorized();
                }

                var cHWID = await _context.users.FirstOrDefaultAsync(u => u.hwid == _hwid);
                var stats = await _context.stats.FirstOrDefaultAsync();
                if (cHWID == null)
                {
                    var newUser = new user
                    {
                        hwid = _hwid,
                        banned = 0
                    };
                    _context.users.Add(newUser);

                    if (stats != null)
                    {
                        stats.total_users++;
                        stats.active_users++;
                        stats.total_opens++;
                    }
                    await _context.SaveChangesAsync();

                    return new JsonResult(new { banned = false });
                }
                else
                {
                    if (cHWID.banned == 1)
                    {
                        stats.total_opens++;
                        await _context.SaveChangesAsync();

                        return new JsonResult(new { banned = true });
                    }
                    else
                    {
                        if (stats != null)
                        {
                            stats.active_users++;
                            stats.total_opens++;
                        }
                        await _context.SaveChangesAsync();

                        return new JsonResult(new { banned = false });
                    }
                }
            }
            catch
            {
                return Unauthorized();
            }
        }

        // Delete one active user
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("active")]
        public async Task<IActionResult> DelActive()
        {
            try
            {
                var claims = (ClaimsIdentity)User.Identity;
                var _hwid = claims.FindFirst("hwid")?.Value;
                if (_hwid == null)
                {
                    return Unauthorized();
                }

                var stats = await _context.stats.FirstOrDefaultAsync();
                if (stats != null)
                    stats.active_users--;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return Unauthorized();
            }
        }

        // Add one stealer
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        [Route("incste")]
        public async Task<IActionResult> IncrementApp()
        {
            try
            {
                var claims = (ClaimsIdentity)User.Identity;
                var _hwid = claims.FindFirst("hwid")?.Value;
                if (_hwid == null)
                {
                    return Unauthorized();
                }

                var stats = await _context.stats.FirstOrDefaultAsync();
                if (stats != null)
                    stats.total_stealers++;

                await _context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return Unauthorized();
            }
        }

        // Refresh statistics
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("refresh")]
        public async Task<IActionResult> RefreshStatistics()
        {
            try
            {
                var stats = await _context.stats.FirstOrDefaultAsync();
                if (stats == null)
                    return Unauthorized();

                int totalUsers = stats.total_users;
                int totalStealers = stats.total_stealers;
                int totalOpens = stats.total_opens;
                int activeUsers = stats.active_users;

                var rStats = new
                {
                    total_Users = totalUsers,
                    total_Stealers = totalStealers,
                    total_Opens = totalOpens,
                    active_Users = activeUsers
                };

                return Ok(rStats);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
