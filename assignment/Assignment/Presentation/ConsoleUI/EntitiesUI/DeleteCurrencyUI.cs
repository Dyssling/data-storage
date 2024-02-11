using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class DeleteCurrencyUI
    {
        private readonly CustomersDataContext _context;

        public DeleteCurrencyUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ISO-koden för valutan du vill radera: ");
            var isoCode = Console.ReadLine()!;

            CurrencyService service = new CurrencyService(_context);

            var currency = await service.DeleteCurrencyAsync(isoCode);

            if (currency != false)
            {
                Console.WriteLine($"Valutan har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Valutan kunde inte tas bort.");
            }
        }
    }
}
