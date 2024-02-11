using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Business.Services
{
    public class CurrencyService
    {
        private readonly CustomersDataContext _context;
        private CurrencyRepository _repository;

        public CurrencyService(CustomersDataContext context)
        {
            _context = context;
            _repository = new CurrencyRepository(_context);
        }

        public async Task<bool> CreateCurrencyAsync(CurrencyDto currency)
        {
            try
            {
                CurrencyEntity currencyEntity = new CurrencyEntity() //Dto omvandlas till en entitet
                {
                    ISOCode = currency.ISOCode,
                    Name = currency.Name
                };

                var createResult = await _repository.CreateAsync(currencyEntity); //Sedan läggs entiteten till i databasen
                return createResult; //Om den lyckades så får man true, annars false

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Om metoden inte lyckas, eller om en kategori redan finns så returneras ett false värde
        }
    }
}
