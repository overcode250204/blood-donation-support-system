using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class UserTable
{
    public Guid UserId { get; set; }

    public string? PasswordHash { get; set; }

    public string? FullName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public double? Longitude { get; set; }

    public double? Latitude { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? BloodType { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public int RoleId { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<BloodDonationSchedule> BloodDonationSchedules { get; set; } = new List<BloodDonationSchedule>();

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<DonationHistory> DonationHistories { get; set; } = new List<DonationHistory>();

    public virtual ICollection<DonationRegistration> DonationRegistrationDonors { get; set; } = new List<DonationRegistration>();

    public virtual ICollection<DonationRegistration> DonationRegistrationScreenedByStaffs { get; set; } = new List<DonationRegistration>();

    public virtual ICollection<EmergencyBloodRequest> EmergencyBloodRequests { get; set; } = new List<EmergencyBloodRequest>();

    public virtual Oauthaccount? Oauthaccount { get; set; }

    public virtual RoleUser Role { get; set; } = null!;
}
