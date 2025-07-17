using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class BloodDonationSchedule
{
    public Guid BloodDonationScheduleId { get; set; }

    public string AddressHospital { get; set; } = null!;

    public DateOnly DonationDate { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int AmountRegistration { get; set; }

    public Guid EditedByStaff { get; set; }

    public virtual ICollection<DonationRegistration> DonationRegistrations { get; set; } = new List<DonationRegistration>();

    public virtual UserTable EditedByStaffNavigation { get; set; } = null!;
}
