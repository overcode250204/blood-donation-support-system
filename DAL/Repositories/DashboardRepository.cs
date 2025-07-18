using DAL.Entities;
using Microsoft.EntityFrameworkCore;  // <-- thêm dòng này
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class DashboardRepository
    {
        private readonly BlooddonationsupportsystemContext _context;

        public DashboardRepository(BlooddonationsupportsystemContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.UserTables.CountAsync();
        }

        public async Task<Dictionary<string, int>> GetDonationStatusCountsAsync()
        {
            return await _context.DonationRegistrations
                .GroupBy(dr => dr.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.Status ?? "Unknown", v => v.Count);
        }

        public async Task<Dictionary<string, int>> GetBloodInventoryAsync()
        {
            return await _context.BloodInventories
                .ToDictionaryAsync(b => b.BloodTypeId, b => b.TotalVolumeMl ?? 0);
        }

        public async Task<Dictionary<string, int>> GetEmergencyRequestsAsync()
        {
            return await _context.EmergencyBloodRequests
                .GroupBy(er => er.LevelOfUrgency)
                .Select(g => new { Urgency = g.Key, Count = g.Count() })
                .ToDictionaryAsync(k => k.Urgency ?? "Unknown", v => v.Count);
        }
    }
}
