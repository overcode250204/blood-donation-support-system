using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class BloodInventory
{
    public string BloodTypeId { get; set; } = null!;

    public int? TotalVolumeMl { get; set; }

    public virtual ICollection<DonationProcess> DonationProcesses { get; set; } = new List<DonationProcess>();
}
