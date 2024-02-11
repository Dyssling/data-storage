using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services.Tests
{
    public class OrderService_Tests
    {
        private readonly CustomersDataContext _context = new CustomersDataContext(new DbContextOptionsBuilder<CustomersDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        [Fact]
        public async Task CreateOrderAsync_ShouldAddOrderToDatabase_And_ReturnTrue()
        {
            var service = new OrderService(_context); //Arrange delen, där jag skapar en instans av min service
            var customerService = new CustomerService(_context);
            var currencyService = new CurrencyService(_context);

            CustomerDto customer = new CustomerDto() //Skapar en kund, eftersom en order är beroende av ett CustomerId
            {
                FirstName = "Test",
                LastName = "Testsson"
            };

            await customerService.CreateCustomerAsync(customer); //Lägger till den i databasen

            CurrencyDto currency = new CurrencyDto() //Samma sak med currency
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };

            await currencyService.CreateCurrencyAsync(currency);

            OrderDto order = new OrderDto()
            {
                CustomerId = 1,
                ProductList = "TestProducts",
                Cost = 10,
                CurrencyISOCode = "TestISO"
            };

            bool result = await service.CreateOrderAsync(order); //Act delen, där jag skickar in den skapade modellen i Create metoden

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
        }

        [Fact]
        public async Task GetOneOrderAsync_ShouldGetOneOrder_And_ReturnOrderDto()
        {
            var service = new OrderService(_context); //Arrange delen
            var customerService = new CustomerService(_context);
            var currencyService = new CurrencyService(_context);


            CustomerDto customer = new CustomerDto() //Skapar en kund, eftersom en order är beroende av ett CustomerId
            {
                FirstName = "Test",
                LastName = "Testsson"
            };

            await customerService.CreateCustomerAsync(customer); //Lägger till den i databasen

            CurrencyDto currency = new CurrencyDto() //Samma sak med currency
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };

            await currencyService.CreateCurrencyAsync(currency);

            OrderDto order = new OrderDto()
            {
                CustomerId = 1,
                ProductList = "TestProducts",
                Cost = 10,
                CurrencyISOCode = "TestISO"
            };
            await service.CreateOrderAsync(order); //Lägger till den skapade entiteten i databasen

            var result = await service.GetOneOrderAsync(1); //Act delen, där jag hämtar en entitet efter Id
            var nullResult = await service.GetOneOrderAsync(2); //Här testar jag även ett Id som inte bör hittas

            Assert.Equal(1, result.CustomerId); //Här kollar jag så att namnet stämmer, och på så vis vet jag att entiteten har hämtats
            Assert.Null(nullResult); //Jag kollar även att det blir null på nullresult
        }

        [Fact]
        public async Task GetAllOrdersAsync_ShouldGetAllOrders_And_ReturnIEnumerable()
        {
            var service = new OrderService(_context); //Arrange delen
            var customerService = new CustomerService(_context);
            var currencyService = new CurrencyService(_context);

            CustomerDto customer = new CustomerDto() //Skapar en kund, eftersom en order är beroende av ett CustomerId
            {
                FirstName = "Test",
                LastName = "Testsson"
            };

            await customerService.CreateCustomerAsync(customer); //Lägger till den i databasen

            CurrencyDto currency = new CurrencyDto() //Samma sak med currency
            {
                ISOCode = "TestISO",
                Name = "TestName"
            };

            await currencyService.CreateCurrencyAsync(currency);

            OrderDto order = new OrderDto()
            {
                CustomerId = 1,
                ProductList = "TestProducts",
                Cost = 10,
                CurrencyISOCode = "TestISO"
            };
            await service.CreateOrderAsync(order);

            var result = await service.GetAllOrdersAsync(); //Act delen

            Assert.Single(result); //Jag kollar om listan innehåller ett element
            Assert.Equal(1, result.First().CustomerId); //Och även om CustomerId stämmer överens
        }
    }
}
