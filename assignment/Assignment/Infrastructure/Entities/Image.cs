using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Image
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = null!;

    public virtual ICollection<Product> ArticleNumbers { get; set; } = new List<Product>();
}
