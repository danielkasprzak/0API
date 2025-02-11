using _0API.Models;

namespace _0API.Services
{
    public interface IUserService
    {
        Task<User> CheckAndAddUserAsync(string hwid);
        Task<bool> IsUserBannedAsync(string hwid);
        Task UpdateHeartbeatAsync(string hwid);
        Task<int> GetActiveUserCountAsync();
        Task<int> GetTotalUserCountAsync();
    }
}
