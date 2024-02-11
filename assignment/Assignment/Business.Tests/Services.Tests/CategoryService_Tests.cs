using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services.Tests
{
    public class CategoryService_Tests
    {
        private readonly ProductsDataContext _context = new ProductsDataContext(new DbContextOptionsBuilder<ProductsDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        [Fact]
        public async Task CreateCategoryAsync_ShouldAddCategoryToDatabase_And_ReturnTrue()
        {
            var service = new CategoryService(_context); //Arrange delen, där jag skapar en instans av min service
            CategoryDto category = new CategoryDto()
            {
                Name = "TestName"
            };

            bool result = await service.CreateCategoryAsync(category); //Act delen, där jag skickar in den skapade modellen i Create metoden
            bool falseResult = await service.CreateCategoryAsync(category); //Jag skickar in en kategori med samma namn igen, vilket bör returnera false

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.False(falseResult); //Denna bör som sagt returnera false, eftersom kategorinamnet redan finns i tabellen
        }

        [Fact]
        public async Task GetOneCategoryAsync_ShouldGetOneCategory_And_ReturnCategoryDto()
        {
            var service = new CategoryService(_context); //Arrange delen
            CategoryDto category = new CategoryDto()
            {
                Name = "TestName",
            };
            await service.CreateCategoryAsync(category); //Lägger till den skapade entiteten i databasen

            var result = await service.GetOneCategoryAsync("TestName"); //Act delen, där jag hämtar en entitet efter namnet
            var nullResult = await service.GetOneCategoryAsync(""); //Här testar jag även ett namn som inte bör hittas

            Assert.Equal("TestName", result.Name); //Här kollar jag så att artikelnumret stämmer, och på så vis vet jag att produkten har hämtats
            Assert.Null(nullResult); //Jag kollar även att det blir null på nullresult
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldGetAllCategories_And_ReturnIEnumerable()
        {
            var service = new CategoryService(_context); //Arrange delen
            CategoryDto category = new CategoryDto()
            {
                Name = "TestName"
            };
            await service.CreateCategoryAsync(category);

            var result = await service.GetAllCategoriesAsync(); //Act delen

            Assert.Single(result); //Jag kollar om listan innehåller ett element
            Assert.Equal("TestName", result.First().Name); //Och även om namnet stämmer överens
        }

        [Fact]
        public async Task UpdateCategoryAsync_ShouldUpdateCategory_And_ReturnTrue()
        {
            var service = new CategoryService(_context); //Arrange delen
            CategoryDto category = new CategoryDto()
            {
                Name = "TestName"
            };
            await service.CreateCategoryAsync(category);
            category.Name = "NewArticleNumber"; //Här ändrar jag namnet på kategorin

            var result = await service.UpdateCategoryAsync("TestName", category); //Act delen, där jag uppdaterar den gamla entiteten med den nya infon
            var getResult = await service.GetOneCategoryAsync("TestName"); //Här hämtas entiteten med det gamla namnet, vilket kommer returnera null eftersom den inte bör finnas längre

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.Null(getResult); //Här bör jag som sagt få tillbaka null
        }

        [Fact]
        public async Task DeleteCategoryAsync_ShouldDeleteCategory_And_ReturnTrue()
        {
            var service = new CategoryService(_context); //Arrange delen
            var productService = new ProductService(_context); //Jag skapar även en ProductService instans för att jag vill se så att relationen mellan dessa fungerar som den ska
            CategoryDto category = new CategoryDto()
            {
                Name = "TestName"
            };
            await service.CreateCategoryAsync(category);

            ProductDto product = new ProductDto() //Här skapar jag en produkt som ska använda sig av samma kategori som nyss skapades, plus en ny kategori som också kommer skapas i CreateProduct metoden
            {
                ArticleNumber = "TestArticleNumber",
                Name = "TestName",
                Price = 10,
                Categories = new List<string>() { "TestName", "AnotherTestCategory" }
            };
            await productService.CreateProductAsync(product);

            var result = await service.DeleteCategoryAsync("TestName"); //Act delen, där jag tar bort entiteten med det angivna namnet
            var falseResult = await service.DeleteCategoryAsync(""); //Den bör returnera false, eftersom detta namnet inte finns
            var allCategories = await service.GetAllCategoriesAsync(); //Jag vill även se hur vilka kategorier som nu finns

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
            Assert.False(falseResult); //Och ett false värde på den som inte kunde hittas
            Assert.Equal("AnotherTestCategory", allCategories.First().Name); //Den första kategorin bör nu heta AnotherTestCategory, eftersom den föregående kategorin har tagits bort
        }
    }
}
