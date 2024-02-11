using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class AddOrderUI
    {
        private readonly CustomersDataContext _context;

        public AddOrderUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett kund-ID för ordern: ");
            var customerId = int.Parse(Console.ReadLine()!);

            Console.Write("Ange en lista med produktnamn du vill lägga till i ordern: ");
            var productList = Console.ReadLine()!;

            Console.Write("Ange kostnaden för ordern: ");
            var cost = decimal.Parse(Console.ReadLine()!);

            Console.Write("Ange ISO-koden för valutan som ska användas: ");
            var isoCode = Console.ReadLine()!;

            OrderService service = new OrderService(_context);

            var result = await service.CreateOrderAsync(new OrderDto() 
            { 
                CustomerId = customerId,
                ProductList = productList,
                Cost = cost,
                CurrencyISOCode = isoCode
            });

            if (result != false)
            {
                Console.WriteLine($"Ordern har skapats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att skapa ordern.");
            }


        }
    }
}
