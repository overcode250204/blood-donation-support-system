using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class EmergencyBloodRequest
{
    public Guid EmergencyBloodRequestId { get; set; }

    public string PatientName { get; set; } = null!;

    public string PatientRelatives { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string LocationOfPatient { get; set; } = null!;

    public string BloodType { get; set; } = null!;

    public int VolumeMl { get; set; }

    public string? LevelOfUrgency { get; set; }

    public bool? IsFulfill { get; set; }

    public string? Note { get; set; }

    public Guid RegisteredByStaffId { get; set; }

    public DateOnly? RegistrationDate { get; set; }

    public virtual ICollection<DonationEmergency> DonationEmergencies { get; set; } = new List<DonationEmergency>();

    public virtual UserTable RegisteredByStaff { get; set; } = null!;
}
