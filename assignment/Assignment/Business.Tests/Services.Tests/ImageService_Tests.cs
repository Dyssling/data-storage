using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services.Tests
{
    public class ImageService_Tests
    {
        private readonly ProductsDataContext _context = new ProductsDataContext(new DbContextOptionsBuilder<ProductsDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        [Fact]
        public async Task CreateImageAsync_ShouldAddImageToDatabase_And_ReturnTrue()
        {
            var service = new ImageService(_context); //Arrange delen, där jag skapar en instans av min service
            ImageDto image = new ImageDto()
            {
                ImageURL = "TestURL"
            };

            bool result = await service.CreateImageAsync(image); //Act delen, där jag skickar in den skapade modellen i Create metoden
            bool falseResult = await service.CreateImageAsync(image); //Jag skickar in en kategori med samma namn igen, vilket bör returnera false

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.False(falseResult); //Denna bör som sagt returnera false, eftersom kategorinamnet redan finns i tabellen
        }

        [Fact]
        public async Task GetOneImageAsync_ShouldGetOneImage_And_ReturnImageDto()
        {
            var service = new ImageService(_context); //Arrange delen
            ImageDto image = new ImageDto()
            {
                ImageURL = "TestURL"
            };
            await service.CreateImageAsync(image); //Lägger till den skapade entiteten i databasen

            var result = await service.GetOneImageAsync("TestURL"); //Act delen, där jag hämtar en entitet efter URLen
            var nullResult = await service.GetOneImageAsync(""); //Här testar jag även en URL som inte bör hittas

            Assert.Equal("TestURL", result.ImageURL); //Här kollar jag så att URlen stämmer, och på så vis vet jag att bilden har hämtats
            Assert.Null(nullResult); //Jag kollar även att det blir null på nullresult
        }

        [Fact]
        public async Task GetAllImagesAsync_ShouldGetAllImages_And_ReturnIEnumerable()
        {
            var service = new ImageService(_context); //Arrange delen
            ImageDto image = new ImageDto()
            {
                ImageURL = "TestURL"
            };
            await service.CreateImageAsync(image);

            var result = await service.GetAllImagesAsync(); //Act delen

            Assert.Single(result); //Jag kollar om listan innehåller ett element
            Assert.Equal("TestURL", result.First().ImageURL); //Och även om URLen stämmer överens
        }

        [Fact]
        public async Task UpdateImageAsync_ShouldUpdateImage_And_ReturnTrue()
        {
            var service = new ImageService(_context); //Arrange delen
            ImageDto image = new ImageDto()
            {
                ImageURL = "TestURL"
            };
            await service.CreateImageAsync(image);
            image.ImageURL = "NewURL"; //Här ändrar jag URLen på bilden

            var result = await service.UpdateImageAsync("TestURL", image); //Act delen, där jag uppdaterar den gamla entiteten med den nya infon
            var getResult = await service.GetOneImageAsync("TestURL"); //Här hämtas entiteten med det gamla namnet, vilket kommer returnera null eftersom den inte bör finnas längre

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.Null(getResult); //Här bör jag som sagt få tillbaka null
        }
    }
}
