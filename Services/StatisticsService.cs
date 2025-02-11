using _0API.Data;
using _0API.Models;
using Microsoft.EntityFrameworkCore;

namespace _0API.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly AppDbContext _context;

        public StatisticsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Statistics> GetStatisticsAsync()
        {
            var statistics = await _context.Statistics.FirstOrDefaultAsync();
            if (statistics == null)
            {
                statistics = new Statistics
                {
                    TotalPentests = 0,
                    TotalOpens = 0,
                };
                _context.Statistics.Add(statistics);
                await _context.SaveChangesAsync();
            }

            return statistics;
        }

        public async Task IncrementTotalPentestsAsync()
        {
            var statistics = await GetStatisticsAsync();
            statistics.TotalPentests++;
            await _context.SaveChangesAsync();
        }

        public async Task IncrementTotalOpensAsync()
        {
            var statistics = await GetStatisticsAsync();
            statistics.TotalOpens++;
            await _context.SaveChangesAsync();
        }
    }
}
