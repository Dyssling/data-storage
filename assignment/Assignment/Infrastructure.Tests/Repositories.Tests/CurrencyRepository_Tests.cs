using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tests
{
    public class CurrencyRepository_Tests
    {
        private readonly CustomersDataContext _context = new CustomersDataContext(new DbContextOptionsBuilder<CustomersDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options); //Här skapar jag en ny instans av mitt context. Jag skickar in mina options, som jag får ifrån en ny OptionsBuilder som jag skapar, som gör att mitt context använder sig av en InMemoryDatabase.

        [Fact]
        public async Task CreateAsync_ShouldAddEntityToTable_And_ReturnTrue()
        {
            var repository = new CurrencyRepository(_context); //Arrange delen, där jag gör en ny instans av ett lämpligt repository, och skickar in contextet som använder en InMemoryDatabase
            var entity = new CurrencyEntity() { ISOCode = "Test", Name = "Test" }; //Och här skapar jag en ny entitet med lämpliga värden

            bool result = await repository.CreateAsync(entity); //Act delen, där jag lägger till entiteten i InMemory databasen

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde (som man bör få om entiteten kunde läggas till i databasen)
        }

        [Fact]
        public async Task GetOneAsync_ShouldGetEntityFromTable_And_ReturnEntity()
        {
            var repository = new CurrencyRepository(_context); //Arrange delen
            var entity = new CurrencyEntity() { ISOCode = "Test", Name = "Test" };
            var secondEntity = new CurrencyEntity() { ISOCode = "Test2", Name = "Test" };
            await repository.CreateAsync(entity); //Jag lägger till dessa två entiteter i tabellen
            await repository.CreateAsync(secondEntity);

            var result = await repository.GetOneAsync(x => x.ISOCode == "Test2"); //Act delen, där jag hämtar den andra entiteten från InMemory databasen

            Assert.Equal("Test2", result.ISOCode); //Assert delen, där jag kollar om man får tillbaka ett namn på Test2, vilket man bör få eftersom man vill få tillbaka den andra entiteten
        }

        [Fact]
        public async Task GetAllAsync_ShouldGetEntitiesFromTable_And_ReturnIEnumerable()
        {
            var repository = new CurrencyRepository(_context); //Arrange delen
            var entity = new CurrencyEntity() { ISOCode = "Test", Name = "Test" };
            var secondEntity = new CurrencyEntity() { ISOCode = "Test2", Name = "Test" };
            await repository.CreateAsync(entity);
            await repository.CreateAsync(secondEntity);

            var result = await repository.GetAllAsync(); //Act delen, där jag hämtar entiteterna från InMemory databasen

            Assert.Equal(2, result.Count()); //Assert delen, där jag kollar om man får tillbaka ett count på 2, vilket man bör få eftersom man vill få tillbaka två (alla) entiteter
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_And_ReturnTrue()
        {
            var repository = new CurrencyRepository(_context); //Arrange delen
            var entity = new CurrencyEntity() { ISOCode = "Test", Name = "Test" };
            await repository.CreateAsync(entity);
            var newEntity = new CurrencyEntity() { ISOCode = entity.ISOCode, Name = "NewTest", Orders = entity.Orders }; //Och här skapar jag den nya entiteten, vars värden ska ersätta den gamla entitetens värden. ISOCode och Orders ska inte uppdateras.

            var result = await repository.UpdateAsync(x => x.Name == "Test", newEntity); //Act delen, där jag uppdaterar den gamla entiteten med den nya

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde (vilket man bör få om allt gick som det ska)
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_And_ReturnTrue()
        {
            var repository = new CurrencyRepository(_context); //Arrange delen
            var entity = new CurrencyEntity() { ISOCode = "Test", Name = "Test" };
            await repository.CreateAsync(entity);

            var result = await repository.DeleteAsync(x => x.ISOCode == "Test"); //Act delen, där jag raderar entiteten

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
        }
    }
}
