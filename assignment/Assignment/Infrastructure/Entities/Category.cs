using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> ArticleNumbers { get; set; } = new List<Product>();
}
