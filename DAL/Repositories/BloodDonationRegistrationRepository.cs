using System;
using System.Linq;
using DAL.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class BloodDonationRegistrationRepository
    {
        private readonly BlooddonationsupportsystemContext _context;
        public BloodDonationRegistrationRepository(BlooddonationsupportsystemContext context)
        {
            _context = context;
        }
        public bool IsUserRegistered(Guid userId, Guid scheduleId)
        {
            return _context.DonationRegistrations.Any(r => r.DonorId == userId && r.BloodDonationScheduleId == scheduleId);
        }
        public void Register(Guid userId, Guid scheduleId, DateOnly scheduleDate)
        {
            var reg = new DonationRegistration
            {
                DonationRegistrationId = Guid.NewGuid(),
                RegistrationDate = DateOnly.FromDateTime(DateTime.Now),
                DonorId = userId,
                BloodDonationScheduleId = scheduleId,
                Status = "CHƯA HIẾN",
                StartDate = scheduleDate,
                EndDate = scheduleDate
            };
            _context.DonationRegistrations.Add(reg);
            _context.SaveChanges();
        }
        public bool HasPendingRegistration(Guid userId)
        {
            // Đơn chưa hiến là status khác 'ĐÃ HIẾN' và khác 'HỦY'
            return _context.DonationRegistrations.Any(r => r.DonorId == userId && r.Status != "ĐÃ HIẾN" && r.Status != "HỦY");
        }
        public bool IsThreeMonthsSinceLastDonation(Guid userId, DateOnly newScheduleDate)
        {
            // Tìm lần hiến gần nhất (status = 'ĐÃ HIẾN')
            var lastDonation = _context.DonationRegistrations
                .Where(r => r.DonorId == userId && r.Status == "ĐÃ HIẾN" && r.DateCompleteDonation != null)
                .OrderByDescending(r => r.DateCompleteDonation)
                .FirstOrDefault();
            if (lastDonation == null || lastDonation.DateCompleteDonation == null)
                return true; // Chưa từng hiến
            var lastDate = lastDonation.DateCompleteDonation.Value;
            return lastDate.AddMonths(3) <= newScheduleDate;
        }
        public List<DonationRegistration> GetUserHistory(Guid userId)
        {
            return _context.DonationRegistrations
                .Include(r => r.BloodDonationSchedule)
                .Where(r => r.DonorId == userId)
                .OrderByDescending(r => r.RegistrationDate)
                .ToList();
        }
        public void CancelRegistration(Guid registrationId)
        {
            var reg = _context.DonationRegistrations.FirstOrDefault(r => r.DonationRegistrationId == registrationId);
            if (reg != null && reg.Status == "CHƯA HIẾN")
            {
                reg.Status = "HỦY";
                _context.SaveChanges();
            }
        }
        public bool HasNeverDonated(Guid userId)
        {
            return !_context.DonationRegistrations.Any(r => r.DonorId == userId && r.Status == "ĐÃ HIẾN");
        }
        public List<DonationRegistration> GetPendingRegistrations()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);
            return _context.DonationRegistrations
                .Include(r => r.Donor)
                .Include(r => r.BloodDonationSchedule)
                .Where(r => r.Status == "CHƯA HIẾN" && r.BloodDonationSchedule.DonationDate == today)
                .OrderBy(r => r.RegistrationDate)
                .ToList();
        }
    }
} 