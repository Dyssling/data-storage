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

            var result = await service.GetOneReviewAsync(1); //Act delen, där jag hämtar en entitet efter CustomerId
            var nullResult = await service.GetOneReviewAsync(2); //Här testar jag även ett CustomerId som inte bör hittas

            Assert.Equal(1, result.CustomerId); //Här kollar jag så att värdena stämmer, och på så vis vet jag att reviewn har hämtats
            Assert.Null(nullResult); //Jag kollar även att det blir null på nullresult
        }
    }
}
