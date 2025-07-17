using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Certificate
{
    public Guid CertificateId { get; set; }

    public string Title { get; set; } = null!;

    public DateOnly? IssuedAt { get; set; }

    public string? TypeCertificate { get; set; }

    public Guid DonationRegistrationId { get; set; }

    public Guid DonorId { get; set; }

    public virtual DonationRegistration DonationRegistration { get; set; } = null!;

    public virtual UserTable Donor { get; set; } = null!;
}
