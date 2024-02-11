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
    }
}
