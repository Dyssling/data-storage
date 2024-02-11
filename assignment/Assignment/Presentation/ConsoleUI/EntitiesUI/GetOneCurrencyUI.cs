using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetOneCurrencyUI
    {
        private readonly CustomersDataContext _context;

        public GetOneCurrencyUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ISO-koden för valutan du vill hämta: ");
            var isoCode = Console.ReadLine()!;

            CurrencyService service = new CurrencyService(_context);

            var currency = await service.GetOneCurrencyAsync(isoCode);

            if (currency != null)
            {
                Console.WriteLine($"ISO-kod: {currency.ISOCode}");
                Console.WriteLine($"Namn: {currency.Name}");
            }
            else
            {
                Console.WriteLine($"Ingen valuta hittades.");
            }
        }
    }
}
