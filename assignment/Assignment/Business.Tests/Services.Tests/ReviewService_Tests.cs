using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Business.Tests.Services.Tests
{
    public class ReviewService_Tests
    {
        private readonly ProductsDataContext _context = new ProductsDataContext(new DbContextOptionsBuilder<ProductsDataContext>().UseInMemoryDatabase($"{Guid.NewGuid()}").Options);

        [Fact]
        public async Task CreateReviewAsync_ShouldAddReviewToDatabase_And_ReturnTrue()
        {
            var service = new ReviewService(_context); //Arrange delen, där jag skapar en instans av min service
            ReviewDto review = new ReviewDto()
            {
                CustomerId = 1,
                Rating = 5,
                Title = "TestTitle",
                Content = "TestContent"
            };

            bool result = await service.CreateReviewAsync(review); //Act delen, där jag skickar in den skapade modellen i Create metoden

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
        }

        [Fact]
        public async Task GetOneReviewAsync_ShouldGetOneReview_And_ReturnReviewDto()
        {
            var service = new ReviewService(_context); //Arrange delen
            ReviewDto review = new ReviewDto()
            {
                CustomerId = 1,
                Rating = 5,
                Title ="TestTitle",
                Content = "TestContent"
            };
            await service.CreateReviewAsync(review); //Lägger till den skapade entiteten i databasen

            var result = await service.GetOneReviewAsync(1); //Act delen, där jag hämtar en entitet efter Id
            var nullResult = await service.GetOneReviewAsync(2); //Här testar jag även ett Id som inte bör hittas

            Assert.Equal("TestTitle", result.Title); //Här kollar jag så att värdena stämmer, och på så vis vet jag att reviewn har hämtats
            Assert.Null(nullResult); //Jag kollar även att det blir null på nullresult
        }

        [Fact]
        public async Task GetAllReviewsAsync_ShouldGetAllReviews_And_ReturnIEnumerable()
        {
            var service = new ReviewService(_context); //Arrange delen
            ReviewDto review = new ReviewDto()
            {
                CustomerId = 1,
                Rating = 5,
                Title = "TestTitle",
                Content = "TestContent"
            };
            await service.CreateReviewAsync(review);

            var result = await service.GetAllReviewsAsync(); //Act delen

            Assert.Single(result); //Jag kollar om listan innehåller ett element
            Assert.Equal(1, result.First().CustomerId); //Och även om CustomerId stämmer överens
        }

        [Fact]
        public async Task UpdateReviewAsync_ShouldUpdateReview_And_ReturnTrue()
        {
            var service = new ReviewService(_context); //Arrange delen
            ReviewDto review = new ReviewDto()
            {
                CustomerId = 1,
                Rating = 5,
                Title = "TestTitle",
                Content = "TestContent"
            };
            await service.CreateReviewAsync(review);
            review.Title = "NewTitle"; //Här ändrar jag titeln

            var result = await service.UpdateReviewAsync(1, review); //Act delen, där jag uppdaterar den gamla entiteten med den nya infon
            var getResult = await service.GetOneReviewAsync(1); //Hämtar den nya entiteten
            var resultTitle = getResult.Title; //Och dess titel

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka true, vilket man bör få om allt gick som tänkt
            Assert.Equal("NewTitle", resultTitle); //Och jag kollar så att titeln har uppdaterats
        }

        [Fact]
        public async Task DeleteReviewAsync_ShouldDeleteReview_And_ReturnTrue()
        {
            var service = new ReviewService(_context); //Arrange delen
            var productService = new ProductService(_context); //Jag skapar även en ProductService instans för att jag vill se så att relationen mellan dessa fungerar som den ska
            ReviewDto review = new ReviewDto()
            {
                CustomerId = 1,
                Rating = 5,
                Title = "TestTitle",
                Content = "TestContent"
            };
            await service.CreateReviewAsync(review);

            ProductDto product = new ProductDto() //Här skapar jag en produkt som ska använda sig av samma kategori som nyss skapades, plus en ny kategori som också kommer skapas i CreateProduct metoden
            {
                ArticleNumber = "TestArticleNumber",
                Name = "TestName",
                Price = 10,
                Categories = new List<string>() { "TestCategory" },
                Images = new List<string>() { "TestURL" },
                Reviews = new List<int>() { 1 }
            };
            await productService.CreateProductAsync(product);

            var result = await service.DeleteReviewAsync(1); //Act delen, där jag tar bort entiteten med det angivna Id't
            var falseResult = await service.DeleteReviewAsync(2); //Den bör returnera false, eftersom detta Id inte finns
            var allReviews = await service.GetAllReviewsAsync(); //Jag vill även se hur vilka reviews

            Assert.True(result); //Assert delen, där jag kollar om man får tillbaka ett true värde
            Assert.False(falseResult); //Och ett false värde på den som inte kunde hittas
            Assert.Empty(allReviews); //Och review listan bör vara tom
        }
    }
}
