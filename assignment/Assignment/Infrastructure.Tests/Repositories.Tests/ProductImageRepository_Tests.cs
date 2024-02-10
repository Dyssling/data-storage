using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.Repositories.Tests
{
    public class ProductImageRepository_Tests
    {
        //Är inte stolt över hur jag löste detta, men det viktiga är att metoderna faktiskt fungerar
        private readonly ProductsDataContext _context = new ProductsDataContext(new DbContextOptionsBuilder<ProductsDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options); //Här skapar jag en ny instans av mitt context. Jag skickar in mina options, som jag får ifrån en ny OptionsBuilder som jag skapar, som gör att mitt context använder sig av en InMemoryDatabase.
        private Product _product = new Product() { ArticleNumber = "Test", Name = "Test", Price = 10};
        private Product _product2 = new Product() { ArticleNumber = "Test2", Name = "Test2", Price = 10 };
        private Image _image = new Image() { Id = 0, ImageUrl="" };
        private Image _image2 = new Image() { Id = 1, ImageUrl = "" };

        [Fact]
        public async Task CreateAsync_ShouldAddEntityToTable_And_ReturnTrue()
        {
            var repository = new ProductImageRepository(_context); //Arrange delen, där jag gör en ny instans av ett lämpligt repository, och skickar in contextet som använder en InMemoryDatabase
            var entity = new ProductImage() { ArticleNumber = "Test", ImageId = 0, ArticleNumberNavigation = _product, Image = _image }; //Och här skapar jag en ny entitet med lämpliga värden

            bool result = await repository.CreateAsync(entity); //Act delen, där jag lägger till entiteten i InMemory databasen

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde (som man bör få om entiteten kunde läggas till i databasen)
        }

        [Fact]
        public async Task GetOneAsync_ShouldGetEntityFromTable_And_ReturnEntity()
        {
            var repository = new ProductImageRepository(_context); //Arrange delen
            var entity = new ProductImage() { ArticleNumber = "Test", ImageId = 0, ArticleNumberNavigation = _product, Image = _image };
            var secondEntity = new ProductImage() { ArticleNumber = "Test2", ImageId = 1, ArticleNumberNavigation = _product2, Image = _image2 };
            await repository.CreateAsync(entity); //Jag lägger till dessa två entiteter i tabellen
            await repository.CreateAsync(secondEntity);

            var result = await repository.GetOneAsync(x => x.ArticleNumber == "Test2"); //Act delen, där jag hämtar den andra entiteten från InMemory databasen

            Assert.Equal(1, result.ImageId); //Assert delen, där jag kollar om man får tillbaka ett Id på 1, vilket man bör få eftersom man vill få tillbaka den andra entiteten
        }

        [Fact]
        public async Task GetAllAsync_ShouldGetEntitiesFromTable_And_ReturnIEnumerable()
        {
            var repository = new ProductImageRepository(_context); //Arrange delen
            var entity = new ProductImage() { ArticleNumber = "Test", ImageId = 0, ArticleNumberNavigation = _product, Image = _image };
            var secondEntity = new ProductImage() { ArticleNumber = "Test2", ImageId = 1, ArticleNumberNavigation = _product2, Image = _image2 };
            await repository.CreateAsync(entity);
            await repository.CreateAsync(secondEntity);

            var result = await repository.GetAllAsync(); //Act delen, där jag hämtar entiteterna från InMemory databasen

            Assert.Equal(2, result.Count()); //Assert delen, där jag kollar om man får tillbaka ett count på 2, vilket man bör få eftersom man vill få tillbaka två (alla) entiteter
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateEntity_And_ReturnTrue()
        {
            var repository = new ProductImageRepository(_context); //Arrange delen
            var entity = new ProductImage() { ArticleNumber = "Test", ImageId = 0, ArticleNumberNavigation = _product, Image = _image };
            await repository.CreateAsync(entity);
            var newEntity = new ProductImage() { ImageId = entity.ImageId, ArticleNumber = "NewTest", ArticleNumberNavigation = entity.ArticleNumberNavigation, Image = entity.Image }; //Och här skapar jag den nya entiteten, vars värden ska ersätta den gamla entitetens värden. ArticleNumber är det enda värdet som ska uppdateras.

            var result = await repository.UpdateAsync(x => x.ArticleNumber == "Test", newEntity); //Act delen, där jag uppdaterar den gamla entiteten med den nya

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde (vilket man bör få om allt gick som det ska)
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEntity_And_ReturnTrue()
        {
            var repository = new ProductImageRepository(_context); //Arrange delen
            var entity = new ProductImage() { ArticleNumber = "Test", ImageId = 0, ArticleNumberNavigation = _product, Image = _image };
            await repository.CreateAsync(entity);

            var result = await repository.DeleteAsync(x => x.ArticleNumber == "Test"); //Act delen, där jag raderar entiteten

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
        }
    }
}
