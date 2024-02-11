namespace Business.Dtos
{
    public class ReviewDto
    {
        public int CustomerId { get; set; }

        public byte Rating { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }
    }
}
