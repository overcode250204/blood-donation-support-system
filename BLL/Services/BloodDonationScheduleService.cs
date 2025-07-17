using System;
using System.Collections.Generic;
using DAL.Entities;
using DAL.Repositories;
using System.Linq;

namespace BLL.Services
{
    public class BloodDonationScheduleService
    {
        private readonly BloodDonationScheduleRepository _scheduleRepo;
        private readonly BloodDonationRegistrationRepository _registrationRepo;
        public BloodDonationScheduleService(BloodDonationScheduleRepository scheduleRepo, BloodDonationRegistrationRepository registrationRepo)
        {
            _scheduleRepo = scheduleRepo;
            _registrationRepo = registrationRepo;
        }
        public List<BloodDonationSchedule> GetFutureSchedules()
        {
            return _scheduleRepo.GetFutureSchedules();
        }
        public bool IsUserRegistered(Guid userId, Guid scheduleId)
        {
            return _registrationRepo.IsUserRegistered(userId, scheduleId);
        }
        public void Register(Guid userId, Guid scheduleId, DateOnly scheduleDate)
        {
            _registrationRepo.Register(userId, scheduleId, scheduleDate);
        }
        public bool CanRegister(Guid userId, Guid scheduleId, DateOnly scheduleDate, out string reason)
        {
            if (_registrationRepo.IsUserRegistered(userId, scheduleId))
            {
                reason = "Bạn đã đăng ký lịch này rồi!";
                return false;
            }
            if (_registrationRepo.HasPendingRegistration(userId))
            {
                reason = "Bạn đang có đơn đăng ký hiến máu chưa hoàn thành!";
                return false;
            }
            if (_registrationRepo.HasNeverDonated(userId))
            {
                reason = null;
                return true;
            }
            if (!_registrationRepo.IsThreeMonthsSinceLastDonation(userId, scheduleDate))
            {
                reason = "Bạn phải cách lần hiến máu gần nhất ít nhất 3 tháng mới được đăng ký!";
                return false;
            }
            reason = null;
            return true;
        }
        public List<DonationRegistration> GetUserHistory(Guid userId)
        {
            return _registrationRepo.GetUserHistory(userId);
        }
        public void CancelRegistration(Guid registrationId)
        {
            _registrationRepo.CancelRegistration(registrationId);
        }
        public List<DonationRegistration> GetPendingRegistrations()
        {
            return _registrationRepo.GetPendingRegistrations();
        }
        public void AcceptRegistration(Guid registrationId, double height, double weight, string healthStatus, string note)
        {
            var context = new DAL.Entities.BlooddonationsupportsystemContext();
            var reg = context.DonationRegistrations.FirstOrDefault(r => r.DonationRegistrationId == registrationId);
            if (reg != null && reg.Status == "CHƯA HIẾN")
            {
                // Insert healthcheck
                var healthCheck = new DAL.Entities.HealthCheck
                {
                    HealthCheckId = Guid.NewGuid(),
                    Height = height,
                    Weight = weight,
                    HealthStatus = healthStatus,
                    Note = note,
                    DonationRegistrationId = registrationId
                };
                context.HealthChecks.Add(healthCheck);
                reg.Status = "ĐÃ HIẾN"; // hoặc cập nhật sang trạng thái tiếp theo nếu cần
                context.SaveChanges();
            }
        }
    }
} 