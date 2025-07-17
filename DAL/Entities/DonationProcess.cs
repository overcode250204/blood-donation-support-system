using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class DonationProcess
{
    public Guid DonationProcessId { get; set; }

    public string BloodTest { get; set; } = null!;

    public int VolumeMl { get; set; }

    public string? Status { get; set; }

    public Guid DonationRegistrationId { get; set; }

    public string? BloodTypeId { get; set; }

    public virtual BloodInventory? BloodType { get; set; }

    public virtual DonationRegistration DonationRegistration { get; set; } = null!;
}
