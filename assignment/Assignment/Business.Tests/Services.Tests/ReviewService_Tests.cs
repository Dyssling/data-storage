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
    }
}
