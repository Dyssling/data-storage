namespace Business.Dtos
{
    public class OrderDto
    {
        public int CustomerId { get; set; }
        public string ProductList { get; set; } = null!;
        public decimal Cost { get; set; }
        public string CurrencyISOCode { get; set; } = null!;
    }
}
