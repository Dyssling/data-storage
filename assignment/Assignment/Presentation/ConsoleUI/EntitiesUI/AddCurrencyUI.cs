using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class AddCurrencyUI
    {
        private readonly CustomersDataContext _context;

        public AddCurrencyUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange valutans ISO-kod: ");
            var isoCode = Console.ReadLine()!;

            Console.Write("Ange valutans namn: ");
            var name = Console.ReadLine()!;

            CurrencyService service = new CurrencyService(_context);

            var result = await service.CreateCurrencyAsync(new CurrencyDto() 
            { 
                ISOCode = isoCode,
                Name = name
            });

            if (result != false)
            {
                Console.WriteLine($"Valutan har skapats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att skapa valutan.");
            }


        }
    }
}
