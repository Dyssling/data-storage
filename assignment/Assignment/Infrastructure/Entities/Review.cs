using System;
using System.Collections.Generic;

namespace Infrastructure.Entities;

public partial class Review
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public byte Rating { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public virtual ProductReview? ProductReview { get; set; }
}
