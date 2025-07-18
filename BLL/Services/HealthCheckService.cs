using System;
using DAL.Entities;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services
{
    public class HealthCheckService
    {
        private readonly BlooddonationsupportsystemContext _context;
        public HealthCheckService(BlooddonationsupportsystemContext context)
        {
            _context = context;
        }
        public void AddHealthCheck(Guid registrationId, double height, double weight, string healthStatus, string note)
        {
            var reg = _context.DonationRegistrations.FirstOrDefault(r => r.DonationRegistrationId == registrationId);
            if (reg == null)
                throw new Exception("Không tìm thấy đơn đăng ký!");
            var healthCheck = new HealthCheck
            {
                HealthCheckId = Guid.NewGuid(),
                Height = height,
                Weight = weight,
                HealthStatus = healthStatus,
                Note = note,
                DonationRegistrationId = registrationId
            };
            _context.HealthChecks.Add(healthCheck);
            reg.Status = "ĐÃ HIẾN";
            _context.SaveChanges();
        }

        public List<DonationRegistration> GetAcceptedRegistrationsWithoutHealthCheck()
        {
            return _context.DonationRegistrations
                .Include(r => r.Donor)
                .Include(r => r.BloodDonationSchedule)
                .Where(r => r.Status == "CHẤP NHẬN" && r.HealthCheck == null)
                .OrderBy(r => r.RegistrationDate)
                .ToList();
        }
    }
} 