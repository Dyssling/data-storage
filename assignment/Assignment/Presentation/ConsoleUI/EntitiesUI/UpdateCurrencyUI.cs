using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class UpdateCurrencyUI
    {
        private readonly CustomersDataContext _context;

        public UpdateCurrencyUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange en ISO-kod vars valuta du vill uppdatera: ");
            var isoCode = Console.ReadLine()!;

            Console.Write("Ange det nya valutanamnet: ");
            var newName = Console.ReadLine()!;

            CurrencyService service = new CurrencyService(_context);

            var result = await service.UpdateCurrencyAsync(isoCode, new CurrencyDto()
            {
                Name = newName
            });

            if (result != false)
            {
                Console.WriteLine($"Valutan har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att uppdatera valutan.");
            }


        }
    }
}
