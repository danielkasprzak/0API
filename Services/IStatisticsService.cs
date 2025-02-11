using _0API.Models;

namespace _0API.Services
{
    public interface IStatisticsService
    {
        Task<Statistics> GetStatisticsAsync();
        Task IncrementTotalPentestsAsync();
        Task IncrementTotalOpensAsync();
    }
}
