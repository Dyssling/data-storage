using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetOneOrderUI
    {
        private readonly CustomersDataContext _context;

        public GetOneOrderUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ID't för ordern du vill hämta: ");
            var id = int.Parse(Console.ReadLine()!);

            OrderService service = new OrderService(_context);

            var order = await service.GetOneOrderAsync(id);

            if (order != null)
            {
                Console.WriteLine($"Kund-ID: {order.CustomerId}");
                Console.WriteLine($"Produkter: {order.ProductList}");
                Console.WriteLine($"Kostnad: {order.Cost}");
                Console.WriteLine($"Valuta: {order.CurrencyISOCode}");
            }
            else
            {
                Console.WriteLine($"Ingen kategori hittades.");
            }
        }
    }
}
