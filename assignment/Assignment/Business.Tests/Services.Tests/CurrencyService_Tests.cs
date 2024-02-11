using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services.Tests
{
    public class CurrencyService_Tests
    {
        private readonly CustomersDataContext _context = new CustomersDataContext(new DbContextOptionsBuilder<CustomersDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        [Fact]
        public async Task CreateCurrencyAsync_ShouldAddCurrencyToDatabase_And_ReturnTrue()
        {
            var service = new CurrencyService(_context); //Arrange delen, där jag skapar en instans av min service
            CurrencyDto currency = new CurrencyDto()
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };

            bool result = await service.CreateCurrencyAsync(currency); //Act delen, där jag skickar in den skapade modellen i Create metoden

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
        }

        [Fact]
        public async Task GetOneCurrencyAsync_ShouldGetOneCurrency_And_ReturnCurrencyDto()
        {
            var service = new CurrencyService(_context); //Arrange delen
            CurrencyDto currency = new CurrencyDto()
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };
            await service.CreateCurrencyAsync(currency); //Lägger till den skapade entiteten i databasen

            var result = await service.GetOneCurrencyAsync("TestISO"); //Act delen, där jag hämtar en entitet efter ISOCode
            var nullResult = await service.GetOneCurrencyAsync(""); //Här testar jag även en ISOCode som inte bör hittas

            Assert.Equal("TestISO", result.ISOCode); //Här kollar jag så att ISOCode stämmer, och på så vis vet jag att entiteten har hämtats
            Assert.Null(nullResult); //Jag kollar även att det blir null på nullresult
        }

        [Fact]
        public async Task GetAllCurrenciesAsync_ShouldGetAllCurrencies_And_ReturnIEnumerable()
        {
            var service = new CurrencyService(_context); //Arrange delen
            CurrencyDto currency = new CurrencyDto()
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };
            await service.CreateCurrencyAsync(currency);

            var result = await service.GetAllCurrenciesAsync(); //Act delen

            Assert.Single(result); //Jag kollar om listan innehåller ett element
            Assert.Equal("TestISO", result.First().ISOCode); //Och även om ISOCode stämmer överens
        }

        [Fact]
        public async Task UpdateCurrencyAsync_ShouldUpdateCurrency_And_ReturnTrue()
        {
            var service = new CurrencyService(_context); //Arrange delen
            CurrencyDto currency = new CurrencyDto()
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };
            await service.CreateCurrencyAsync(currency);
            currency.Name = "NewTestName"; //Här ändrar jag namnet

            var result = await service.UpdateCurrencyAsync("TestISO", currency); //Act delen, där jag uppdaterar den gamla entiteten med den nya infon
            var getResult = await service.GetOneCurrencyAsync("TestISO"); //Jag hämtar även entiteten

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.Equal("NewTestName", getResult.Name); //Här bör jag få det nya värdet
        }

        [Fact]
        public async Task DeleteCurrencyAsync_ShouldDeleteCurrency_And_ReturnTrue()
        {
            var service = new CurrencyService(_context); //Arrange delen

            CurrencyDto currency = new CurrencyDto()
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };
            await service.CreateCurrencyAsync(currency);

            var result = await service.DeleteCurrencyAsync("TestISO"); //Act delen, där jag tar bort entiteten med den angivna ISOCoden
            var falseResult = await service.DeleteCurrencyAsync("TestISO"); //Den bör returnera false, eftersom denna ISOCode inte finns längre
            var allCurrencies = await service.GetAllCurrenciesAsync(); //Jag vill även se vilka entiteter som nu finns

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
            Assert.False(falseResult); //Och ett false värde på den som inte kunde hittas
            Assert.Empty(allCurrencies); //Listan med entiteter bör nu vara tom
        }
    }
}
