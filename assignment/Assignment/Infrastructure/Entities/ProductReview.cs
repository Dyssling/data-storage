using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class ProductReview
{
    public int ReviewId { get; set; }

    public string ArticleNumber { get; set; } = null!;

    public virtual Product ArticleNumberNavigation { get; set; } = null!;

    public virtual Review Review { get; set; } = null!;
}
