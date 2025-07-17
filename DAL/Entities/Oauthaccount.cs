using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Oauthaccount
{
    public Guid OauthaccountId { get; set; }

    public string Provider { get; set; } = null!;

    public string ProviderUserId { get; set; } = null!;

    public string Account { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public Guid? UserId { get; set; }

    public virtual UserTable? User { get; set; }
}
