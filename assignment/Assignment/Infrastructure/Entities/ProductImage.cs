using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class ProductImage
{
    public int ImageId { get; set; }

    public string ArticleNumber { get; set; } = null!;

    public virtual Product ArticleNumberNavigation { get; set; } = null!;

    public virtual Image Image { get; set; } = null!;
}
