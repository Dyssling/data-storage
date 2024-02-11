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

        [Fact]
        public async Task GetOneProductAsync_ShouldGetOneProduct_And_ReturnProductDto()
        {
            var service = new ProductService(_context); //Arrange delen
            ProductDto product = new ProductDto()
            {
                ArticleNumber = "TestArticleNumber",
                Name = "TestName",
                Price = 10,
                Categories = new List<string>() { "TestCategory" }
            };
            await service.CreateProductAsync(product); //Lägger till den skapade produkten i databasen

            var result = await service.GetOneProductAsync("TestArticleNumber"); //Act delen, där jag hämtar en produkt efter artikelnumret
            var nullresult = await service.GetOneProductAsync(""); //Här testar jag även ett artikelnummer som inte bör hittas

            Assert.Equal("TestArticleNumber", result.ArticleNumber); //Här kollar jag så att artikelnumret stämmer, och på så vis vet jag att produkten har hämtats
            Assert.Null(nullresult); //Jag kollar även att det blir null på nullresult
        }

        [Fact]
        public async Task GetAllProductsAsync_ShouldGetAllProducts_And_ReturnIEnumerable()
        {
            var service = new ProductService(_context); //Arrange delen
            ProductDto product = new ProductDto()
            {
                ArticleNumber = "TestArticleNumber",
                Name = "TestName",
                Price = 10,
                Categories = new List<string>() { "TestCategory" }
            };
            await service.CreateProductAsync(product);

            var result = await service.GetAllProductsAsync(); //Act delen

            Assert.Single(result); //Jag kollar om listan innehåller ett element
            Assert.Equal("TestCategory", result.First().Categories.First()); //Och även om kategorilistan stämmer överens
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProductAndCreateNewCategoryIfNeeded_And_ReturnTrue()
        {
            var service = new ProductService(_context); //Arrange delen
            var categoryRepository = new CategoryRepository(_context);
            ProductDto product = new ProductDto()
            {
                ArticleNumber = "TestArticleNumber",
                Name = "TestName",
                Price = 10,
                Categories = new List<string>() { "TestCategory" }
            };
            await service.CreateProductAsync(product);
            product.ArticleNumber = "NewArticleNumber"; //Här ändrar jag artikelnumret på produkten, vilket man inte får göra
            product.Categories.Add("NewCategory"); //Och lägger till en ny kategori i produktens kategori-lista

            var result = await service.UpdateProductAsync("TestArticleNumber", product); //Act delen, där jag uppdaterar den gamla produkten med den nya infon
            var getResult = await service.GetOneProductAsync("NewArticleNumber"); //Här hämtas entiteten med det nya artikelnumret, vilket kommer returnera null eftersom man inte får ändra artikelnumret
            Category category = await categoryRepository.GetOneAsync(x => x.Name == "NewCategory"); //Jag hämtar även den nya kategorin

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.Null(getResult); //Här bör jag som sagt få tillbaka null
            Assert.Equal("NewCategory", category.Name); //Jag kollar även om kategorin har skapats, genom att jämföra namnen
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldDeleteProduct_And_ReturnTrue()
        {
            var service = new ProductService(_context); //Arrange delen
            ProductDto product = new ProductDto()
            {
                ArticleNumber = "TestArticleNumber",
                Name = "TestName",
                Price = 10,
                Categories = new List<string>() { "TestCategory" }
            };
            await service.CreateProductAsync(product);

            var result = await service.DeleteProductAsync("TestArticleNumber"); //Act delen, där jag tar bort entiteten med det angivna artikelnumret
            var falseResult = await service.DeleteProductAsync(""); //Den bör returnera false, eftersom detta artikelnumret inte finns

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
            Assert.False(falseResult); //Och ett false värde på den som inte kunde hittas
        }
    }
}
