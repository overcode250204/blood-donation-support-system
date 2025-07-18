using System;
using DAL.Entities;
using System.Linq;

namespace BLL.Services
{
    public class ProcessService
    {
        private readonly BlooddonationsupportsystemContext _context;
        public ProcessService(BlooddonationsupportsystemContext context)
        {
            _context = context;
        }
        public void AddProcess(Guid registrationId, string bloodTest, int volume, string status, string bloodTypeId)
        {
            var reg = _context.DonationRegistrations.FirstOrDefault(r => r.DonationRegistrationId == registrationId);
            if (reg == null)
                throw new Exception("Không tìm thấy đơn đăng ký!");
            var process = new DonationProcess
            {
                DonationProcessId = Guid.NewGuid(),
                BloodTest = bloodTest,
                VolumeMl = volume,
                Status = status,
                BloodTypeId = bloodTypeId,
                DonationRegistrationId = registrationId
            };
            _context.DonationProcesses.Add(process);
            _context.SaveChanges();
        }
    }
} 