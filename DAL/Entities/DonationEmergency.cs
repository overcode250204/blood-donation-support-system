using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class DonationEmergency
{
    public Guid DonationEmergencyId { get; set; }

    public DateOnly? AssignedDate { get; set; }

    public Guid? EmergencyBloodRequestId { get; set; }

    public Guid DonationRegistrationId { get; set; }

    public virtual DonationRegistration DonationRegistration { get; set; } = null!;

    public virtual EmergencyBloodRequest? EmergencyBloodRequest { get; set; }
}
