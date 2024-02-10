using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tests
{
    public class ProductReviewRepository_Tests
    {
        //Är inte stolt över hur jag löste detta, men det viktiga är att metoderna faktiskt fungerar
        private readonly ProductsDataContext _context = new ProductsDataContext(new DbContextOptionsBuilder<ProductsDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options); //Här skapar jag en ny instans av mitt context. Jag skickar in mina options, som jag får ifrån en ny OptionsBuilder som jag skapar, som gör att mitt context använder sig av en InMemoryDatabase.
        private Product _product = new Product() { ArticleNumber = "Test", Name = "Test", Price = 10 };
        private Product _product2 = new Product() { ArticleNumber = "Test2", Name = "Test2", Price = 10 };
        private Review _review = new Review() { Id = 0, CustomerId = 0, Rating = 0 };
        private Review _review2 = new Review() { Id = 1, CustomerId = 1, Rating = 0 };

        [Fact]
        public async Task CreateAsync_ShouldAddEntityToTable_And_ReturnTrue()
        {
            var repository = new ProductReviewRepository(_context); //Arrange delen, där jag gör en ny instans av ett lämpligt repository, och skickar in contextet som använder en InMemoryDatabase
            var entity = new ProductReview() { ArticleNumber = "Test", ReviewId = 0, ArticleNumberNavigation = _product, Review = _review }; //Och här skapar jag en ny entitet med lämpliga värden

            bool result = await repository.CreateAsync(entity); //Act delen, där jag lägger till entiteten i InMemory databasen

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde (som man bör få om entiteten kunde läggas till i databasen)
        }

        [Fact]
        public async Task GetOneAsync_ShouldGetEntityFromTable_And_ReturnEntity()
        {
            var repository = new ProductReviewRepository(_context); //Arrange delen
            var entity = new ProductReview() { ArticleNumber = "Test", ReviewId = 0, ArticleNumberNavigation = _product, Review = _review };
            var secondEntity = new ProductReview() { ArticleNumber = "Test2", ReviewId = 1, ArticleNumberNavigation = _product2, Review = _review2 };
            await repository.CreateAsync(entity); //Jag lägger till dessa två entiteter i tabellen
            await repository.CreateAsync(secondEntity);

            var result = await repository.GetOneAsync(x => x.ArticleNumber == "Test2"); //Act delen, där jag hämtar den andra entiteten från InMemory databasen

            Assert.Equal(1, result.ReviewId); //Assert delen, där jag kollar om man får tillbaka ett Id på 1, vilket man bör få eftersom man vill få tillbaka den andra entiteten
        }

        [Fact]
        public async Task GetAllAsync_ShouldGetEntitiesFromTable_And_ReturnIEnumerable()
        {
            var repository = new ProductReviewRepository(_context); //Arrange delen
            var entity = new ProductReview() { ArticleNumber = "Test", ReviewId = 0, ArticleNumberNavigation = _product, Review = _review };
            var secondEntity = new ProductReview() { ArticleNumber = "Test2", ReviewId = 1, ArticleNumberNavigation = _product2, Review = _review2 };
            await repository.CreateAsync(entity);
            await repository.CreateAsync(secondEntity);

            var result = await repository.GetAllAsync(); //Act delen, där jag hämtar entiteterna från InMemory databasen

            Assert.Equal(2, result.Count()); //Assert delen, där jag kollar om man får tillbaka ett count på 2, vilket man bör få eftersom man vill få tillbaka två (alla) entiteter
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_And_ReturnTrue()
        {
            var repository = new ProductReviewRepository(_context); //Arrange delen
            var entity = new ProductReview() { ArticleNumber = "Test", ReviewId = 0, ArticleNumberNavigation = _product, Review = _review };
            await repository.CreateAsync(entity);
            var newEntity = new ProductReview() { ReviewId = entity.ReviewId, ArticleNumber = "NewTest", ArticleNumberNavigation = entity.ArticleNumberNavigation, Review = entity.Review }; //Och här skapar jag den nya entiteten, vars värden ska ersätta den gamla entitetens värden. ArticleNumber är det enda värdet som ska uppdateras.

            var result = await repository.UpdateAsync(x => x.ArticleNumber == "Test", newEntity); //Act delen, där jag uppdaterar den gamla entiteten med den nya

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde (vilket man bör få om allt gick som det ska)
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_And_ReturnTrue()
        {
            var repository = new ProductReviewRepository(_context); //Arrange delen
            var entity = new ProductReview() { ArticleNumber = "Test", ReviewId = 0, ArticleNumberNavigation = _product, Review = _review };
            await repository.CreateAsync(entity);

            var result = await repository.DeleteAsync(x => x.ArticleNumber == "Test"); //Act delen, där jag raderar entiteten

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
        }
    }
}

