using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class UpdateOrderUI
    {
        private readonly CustomersDataContext _context;

        public UpdateOrderUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett ID vars order du vill uppdatera: ");
            var id = int.Parse(Console.ReadLine()!);

            Console.Write("Ange det nya kund-ID't: ");
            var newCustomerId = int.Parse(Console.ReadLine()!);

            Console.Write("Ange de nya produkterna: ");
            var newProductList = Console.ReadLine()!;

            Console.Write("Ange den nya kostnaden: ");
            var newCost = decimal.Parse(Console.ReadLine()!);

            Console.Write("Ange den nya ISO-koden för valutan: ");
            var newCurrency = Console.ReadLine()!;

            OrderService service = new OrderService(_context);

            var result = await service.UpdateOrderAsync(id, new OrderDto()
            {
                CustomerId = newCustomerId,
                ProductList = newProductList,
                Cost = newCost,
                CurrencyISOCode = newCurrency
            });

            if (result != false)
            {
                Console.WriteLine($"Ordern har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att uppdatera ordern.");
            }


        }
    }
}
