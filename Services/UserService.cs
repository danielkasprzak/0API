using _0API.Data;
using _0API.Models;
using Microsoft.EntityFrameworkCore;

namespace _0API.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> CheckAndAddUserAsync(string hwid)
        {
            var user = await _context.Users.OrderBy(u => u.Id).FirstOrDefaultAsync(u => u.HWID == hwid);
            if (user == null)
            {
                user = new User { HWID = hwid, IsBanned = 0 };
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            return user;
        }

        public async Task<bool> IsUserBannedAsync(string hwid)
        {
            var user = await _context.Users.OrderBy(u => u.Id).FirstOrDefaultAsync(u => u.HWID == hwid);
            return user != null && user.IsBanned == 1;
        }

        public async Task UpdateHeartbeatAsync(string hwid)
        {
            var user = await _context.Users.OrderBy(u => u.Id).FirstOrDefaultAsync(u => u.HWID == hwid);
            if (user != null)
            {
                user.LastActivity = DateTime.UtcNow;
                Console.WriteLine(user.LastActivity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetActiveUserCountAsync()
        {
            var fiveMinutesAgo = DateTime.UtcNow.AddMinutes(-5);
            return await _context.Users
                .Where(u => u.LastActivity > fiveMinutesAgo)
                .CountAsync();
        }

        public async Task<int> GetTotalUserCountAsync()
        {
            return await _context.Users.CountAsync();
        }
    }
}
