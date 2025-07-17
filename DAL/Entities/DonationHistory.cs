using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class DonationHistory
{
    public Guid DonationHistoryId { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public string AddressHospital { get; set; } = null!;

    public Guid DonorId { get; set; }

    public Guid DonationRegistrationId { get; set; }

    public virtual DonationRegistration DonationRegistration { get; set; } = null!;

    public virtual UserTable Donor { get; set; } = null!;
}
