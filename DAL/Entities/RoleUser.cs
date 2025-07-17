using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class RoleUser
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserTable> UserTables { get; set; } = new List<UserTable>();
}
