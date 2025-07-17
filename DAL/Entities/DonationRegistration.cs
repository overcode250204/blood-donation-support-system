using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class DonationRegistration
{
    public Guid DonationRegistrationId { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public DateOnly? DateCompleteDonation { get; set; }

    public string? Status { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public Guid? ScreenedByStaffId { get; set; }

    public Guid DonorId { get; set; }

    public Guid? BloodDonationScheduleId { get; set; }

    public virtual BloodDonationSchedule? BloodDonationSchedule { get; set; }

    public virtual Certificate? Certificate { get; set; }

    public virtual ICollection<DonationEmergency> DonationEmergencies { get; set; } = new List<DonationEmergency>();

    public virtual ICollection<DonationHistory> DonationHistories { get; set; } = new List<DonationHistory>();

    public virtual DonationProcess? DonationProcess { get; set; }

    public virtual UserTable Donor { get; set; } = null!;

    public virtual HealthCheck? HealthCheck { get; set; }

    public virtual UserTable? ScreenedByStaff { get; set; }
}
