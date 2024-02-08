using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Product
{
    public string ArticleNumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
