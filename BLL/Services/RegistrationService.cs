using System;
using DAL.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class RegistrationService
    {
        private readonly BlooddonationsupportsystemContext _context;
        public RegistrationService(BlooddonationsupportsystemContext context)
        {
            _context = context;
        }
        public void AcceptRegistration(Guid registrationId)
        {
            var reg = _context.DonationRegistrations.FirstOrDefault(r => r.DonationRegistrationId == registrationId);
            if (reg != null && reg.Status == "CHƯA HIẾN")
            {
                reg.Status = "CHẤP NHẬN";
                _context.SaveChanges();
            }
        }

        public List<DonationRegistration> GetRegistrationsHealthCheckPassed()
        {
            return _context.DonationRegistrations
                .Include(r => r.Donor)
                .Include(r => r.BloodDonationSchedule)
                .Include(r => r.HealthCheck)
                .Where(r => r.HealthCheck != null && r.HealthCheck.HealthStatus == "ĐẠT" && r.DonationProcess == null)
                .OrderBy(r => r.RegistrationDate)
                .ToList();
        }
    }
} 