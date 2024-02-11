using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services.Tests
{
    public class CustomerService_Tests
    {
        private readonly CustomersDataContext _context = new CustomersDataContext(new DbContextOptionsBuilder<CustomersDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        [Fact]
        public async Task CreateCustomerAsync_ShouldAddCustomerToDatabase_And_ReturnTrue()
        {
            var service = new CustomerService(_context); //Arrange delen, där jag skapar en instans av min service
            CustomerDto customer = new CustomerDto()
            {
                FirstName = "Test",
                LastName = "Testsson"
            };

            bool result = await service.CreateCustomerAsync(customer); //Act delen, där jag skickar in den skapade modellen i Create metoden

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
        }

        [Fact]
        public async Task GetOneCustomerAsync_ShouldGetOneCustomer_And_ReturnCustomerDto()
        {
            var service = new CustomerService(_context); //Arrange delen
            CustomerDto customer = new CustomerDto()
            {
                FirstName = "Test",
                LastName = "Testsson"
            };
            await service.CreateCustomerAsync(customer); //Lägger till den skapade entiteten i databasen

            var result = await service.GetOneCustomerAsync(1); //Act delen, där jag hämtar en entitet efter Id
            var nullResult = await service.GetOneCustomerAsync(2); //Här testar jag även ett Id som inte bör hittas

            Assert.Equal("Test", result.FirstName); //Här kollar jag så att namnet stämmer, och på så vis vet jag att entiteten har hämtats
            Assert.Null(nullResult); //Jag kollar även att det blir null på nullresult
        }
    }
}
