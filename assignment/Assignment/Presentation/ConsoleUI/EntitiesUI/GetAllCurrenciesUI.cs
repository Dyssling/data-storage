using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetAllCurrenciesUI
    {
        private readonly CustomersDataContext _context;

        public GetAllCurrenciesUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            CurrencyService service = new CurrencyService(_context);

            var currencyList = await service.GetAllCurrenciesAsync();

            foreach (CurrencyDto currency in currencyList)
            {
                Console.WriteLine(currency.Name);
            }
        }
    }
}
