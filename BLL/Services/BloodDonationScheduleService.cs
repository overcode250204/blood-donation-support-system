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
        public bool AddSchedule(BlooddonationsupportsystemContext context, string addressHospital, DateOnly donationDate, TimeOnly startTime, TimeOnly endTime, out string error, Guid staffId, int amountRegistration)
        {
            error = null;
            // Validate trùng lịch (ngày, địa điểm, giờ giao nhau)
            var exists = context.BloodDonationSchedules.Any(s =>
                s.AddressHospital == addressHospital &&
                s.DonationDate == donationDate &&
                ((startTime < s.EndTime && endTime > s.StartTime))
            );
            if (exists)
            {
                error = "Đã có lịch hiến máu trùng thời gian và địa điểm!";
                return false;
            }
            if (donationDate < DateOnly.FromDateTime(DateTime.Now))
            {
                error = "Không thể thêm lịch hiến máu trong quá khứ!";
                return false;
            }
            if (startTime >= endTime)
            {
                error = "Giờ bắt đầu phải nhỏ hơn giờ kết thúc!";
                return false;
            }
            if (amountRegistration < 0)
            {
                error = "Số lượng người đăng ký phải >= 0!";
                return false;
            }
            var schedule = new BloodDonationSchedule
            {
                BloodDonationScheduleId = Guid.NewGuid(),
                AddressHospital = addressHospital,
                DonationDate = donationDate,
                StartTime = startTime,
                EndTime = endTime,
                AmountRegistration = amountRegistration,  
                EditedByStaff = staffId
            };
            context.BloodDonationSchedules.Add(schedule);
            context.SaveChanges();
            return true;
        }
    }
} 