using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetAllReviewsUI
    {
        private readonly ProductsDataContext _context;

        public GetAllReviewsUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            ReviewService service = new ReviewService(_context);

            var reviewList = await service.GetAllReviewsAsync();

            foreach (ReviewDto review in reviewList)
            {
                Console.WriteLine($"Kund {review.CustomerId} med betyget {review.Rating}/5: {review.Content}");
            }
        }
    }
}
