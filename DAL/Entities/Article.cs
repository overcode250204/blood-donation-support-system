using System;
using System.Collections.Generic;

namespace DAL.Entities;

public partial class Article
{
    public Guid ArticleId { get; set; }

    public string Title { get; set; } = null!;

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Status { get; set; }

    public string? ImageUrl { get; set; }

    public string ArticleType { get; set; } = null!;

    public Guid CreatedByAdminId { get; set; }

    public virtual UserTable CreatedByAdmin { get; set; } = null!;
}
