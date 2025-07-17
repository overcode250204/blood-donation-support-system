using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class HealthCheck
{
    public Guid HealthCheckId { get; set; }

    public double Height { get; set; }

    public double Weight { get; set; }

    public string? HealthStatus { get; set; }

    public string? Note { get; set; }

    public Guid DonationRegistrationId { get; set; }

    public virtual DonationRegistration DonationRegistration { get; set; } = null!;
}
