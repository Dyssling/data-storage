using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<CurrencyDto> GetOneCurrencyAsync(string isoCode)
        {
            try
            {
                var entity = await _repository.GetOneAsync(x => x.ISOCode == isoCode); //Jag söker efter entiteten med den angivna ISOCode

                if (entity != null)
                {
                    CurrencyDto currency = new CurrencyDto()
                    {
                        ISOCode = entity.ISOCode,
                        Name = entity.Name
                    };

                    return currency;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om ingen entitet hittas, eller om något gick snett så returneras ett null värde
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync()
        {
            try
            {
                var entityList = await _repository.GetAllAsync(); //Jag hämtar listan med entiteter

                if (!entityList.IsNullOrEmpty()) //Kollar om det finns något innehåll i listan
                {
                    var dtoList = new List<CurrencyDto>();//Sedan skapar jag en lista där Dto varianterna kommer lagras

                    foreach (CurrencyEntity entity in entityList)
                    {
                        var dto = new CurrencyDto()
                        {
                            ISOCode = entity.ISOCode,
                            Name = entity.Name
                        };

                        dtoList.Add(dto); //Slutligen läggs Dton till i Dto listan
                    }

                    return dtoList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<CurrencyDto>(); //Om listan är tom, eller om något gick snett, så får man tillbaka en tom lista.
        }

        public async Task<bool> UpdateCurrencyAsync(string isoCode, CurrencyDto currency)
        {
            try
            {
                var getEntity = await _repository.GetOneAsync(x => x.ISOCode == isoCode);

                if (getEntity != null)
                {
                    var currencyEntity = new CurrencyEntity() //Dto omvandlas till en entitet
                    {
                        ISOCode = getEntity.ISOCode,
                        Name = currency.Name,
                        Orders = getEntity.Orders
                    };

                    var updateResult = await _repository.UpdateAsync((x => x.ISOCode == isoCode), currencyEntity); //Entiteten med den angivna ISOCoden ersätts med den nya entiteten

                    return updateResult; //Om entiteten hittades så returneras true, annars false
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false; //Om något gick snett så returneras false
        }

        public async Task<bool> DeleteCurrencyAsync(string isoCode)
        {
            try
            {
                var result = await _repository.DeleteAsync(x => x.ISOCode == isoCode);

                return result; //Om entiteten kunde hittas så returneras true, annars false
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Och om något gick snett så returneras false
        }
    }
}
