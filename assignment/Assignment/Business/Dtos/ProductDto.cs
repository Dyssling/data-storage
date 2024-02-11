using Infrastructure.Entities;

namespace Business.Dtos
{
    public class ProductDto
    {
        public string ArticleNumber { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public ICollection<string> Categories { get; set; } = new List<string>();

        public ICollection<string> Images { get; set; } = new List<string>();
    }
}
