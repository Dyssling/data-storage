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

        [Fact]
        public async Task GetAllCustomersAsync_ShouldGetAllCustomers_And_ReturnIEnumerable()
        {
            var service = new CustomerService(_context); //Arrange delen
            CustomerDto customer = new CustomerDto()
            {
                FirstName = "Test",
                LastName = "Testsson"
            };
            await service.CreateCustomerAsync(customer);

            var result = await service.GetAllCustomersAsync(); //Act delen

            Assert.Single(result); //Jag kollar om listan innehåller ett element
            Assert.Equal("Test", result.First().FirstName); //Och även om namnet stämmer överens
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldUpdateCustomer_And_ReturnTrue()
        {
            var service = new CustomerService(_context); //Arrange delen
            CustomerDto customer = new CustomerDto()
            {
                FirstName = "Test",
                LastName = "Testsson"
            };
            await service.CreateCustomerAsync(customer);
            customer.FirstName = "NewTest"; //Här ändrar jag namnet

            var result = await service.UpdateCustomerAsync(1, customer); //Act delen, där jag uppdaterar den gamla entiteten med den nya infon
            var getResult = await service.GetOneCustomerAsync(1); //Jag hämtar även entiteten

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.Equal("NewTest", getResult.FirstName); //Här bör jag få det nya värdet
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldDeleteCustomer_And_ReturnTrue()
        {
            var service = new CustomerService(_context); //Arrange delen

            CustomerDto customer = new CustomerDto()
            {
                FirstName = "Test",
                LastName = "Testsson"
            };
            await service.CreateCustomerAsync(customer);

            var result = await service.DeleteCustomerAsync(1); //Act delen, där jag tar bort entiteten med det angivna Id't
            var falseResult = await service.DeleteCustomerAsync(1); //Den bör returnera false, eftersom detta Id inte finns längre
            var allCategories = await service.GetAllCustomersAsync(); //Jag vill även se hur vilka entiteter som nu finns

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
            Assert.False(falseResult); //Och ett false värde på den som inte kunde hittas
            Assert.Empty(allCategories); //Listan med entiteter bör nu vara tom
        }
    }
}
