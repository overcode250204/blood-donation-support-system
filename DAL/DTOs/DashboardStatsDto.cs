using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs
{
    public class DashboardStatsDto
    {
        public int TotalUsers { get; set; }
        public Dictionary<string, int> DonationStatusCounts { get; set; }
        public Dictionary<string, int> BloodInventory { get; set; }
        public Dictionary<string, int> EmergencyRequests { get; set; }
    }
}
