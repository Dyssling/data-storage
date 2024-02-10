using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services.Tests
{
    public class ProductService_Tests
    {
        private readonly ProductsDataContext _context = new ProductsDataContext(new DbContextOptionsBuilder<ProductsDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        [Fact]
        public async Task CreateProductAsync_ShouldAddProductAndCategoryToDatabase_And_ReturnTrue()
        {
            var service = new ProductService(_context); //Arrange delen, där jag skapar en instans av min service
            var categoryRepository = new CategoryRepository(_context);
            ProductDto product = new ProductDto()
            {
                ArticleNumber = "TestArticleNumber",
                Name = "TestName",
                Price = 10,
                Categories = new List<string>() { "TestCategory" }
            };

            bool result = await service.CreateProductAsync(product); //Act delen, där jag skickar in den skapade modellen i Create metoden
            Category category = await categoryRepository.GetOneAsync(x => x.Name == "TestCategory"); //Jag hämtar även den skapade test-kategorin

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.Equal("TestCategory", category.Name); //Jag kollar även om kategorin har skapats, genom att jämföra namnen
        }
    }
}
