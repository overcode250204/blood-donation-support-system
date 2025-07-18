using DAL.DTOs;
using DAL.Repositories;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class DashboardService
    {
        private readonly DashboardRepository _dashboardRepo;

        public DashboardService(DashboardRepository dashboardRepo)
        {
            _dashboardRepo = dashboardRepo;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var totalUsers = await _dashboardRepo.GetTotalUsersAsync();
            var donationStatusCounts = await _dashboardRepo.GetDonationStatusCountsAsync();
            var bloodInventory = await _dashboardRepo.GetBloodInventoryAsync();
            var emergencyRequests = await _dashboardRepo.GetEmergencyRequestsAsync();

            return new DashboardStatsDto
            {
                TotalUsers = totalUsers,
                DonationStatusCounts = donationStatusCounts,
                BloodInventory = bloodInventory,
                EmergencyRequests = emergencyRequests
            };
        }
    }
}
