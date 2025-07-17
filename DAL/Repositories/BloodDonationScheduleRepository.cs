using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;

namespace DAL.Repositories
{
    public class BloodDonationScheduleRepository
    {
        private readonly BlooddonationsupportsystemContext _context;
        public BloodDonationScheduleRepository(BlooddonationsupportsystemContext context)
        {
            _context = context;
        }
        public List<BloodDonationSchedule> GetFutureSchedules()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return _context.BloodDonationSchedules
                .Where(s => s.DonationDate >= today)
                .OrderBy(s => s.DonationDate)
                .ToList();
        }
    }
} 