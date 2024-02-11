using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Business.Services
{
    public class ReviewService
    {
        private readonly ProductsDataContext _context;
        private ReviewRepository _repository;

        public ReviewService(ProductsDataContext context)
        {
            _context = context;
            _repository = new ReviewRepository(_context);
        }

        public async Task<bool> CreateReviewAsync(ReviewDto review)
        {
            try
            {
                Review reviewEntity = new Review() //Dto omvandlas till en entitet
                {
                    CustomerId = review.CustomerId,
                    Rating = review.Rating,
                    Title = review.Title,
                    Content = review.Content
                };

                var createResult = await _repository.CreateAsync(reviewEntity); //Sedan läggs entiteten till i databasen
                return createResult; //Om den lyckades så får man true, annars false

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Om metoden inte lyckas, eller om en kategori redan finns så returneras ett false värde
        }
    }
}
