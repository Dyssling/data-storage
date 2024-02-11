using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
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

        public async Task<ReviewDto> GetOneReviewAsync(int id) //Här behöver man Id't på reviewn
        {
            try
            {
                var entity = await _repository.GetOneAsync(x => x.Id == id); //Jag söker efter entiteten med det angivna IDt

                if (entity != null)
                {
                    ReviewDto review = new ReviewDto()
                    {
                        CustomerId = entity.CustomerId,
                        Rating = entity.Rating,
                        Title = entity.Title,
                        Content = entity.Content
                    };

                    return review;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om ingen entitet hittas, eller om något gick snett så returneras ett null värde
        }

        public async Task<IEnumerable<ReviewDto>> GetAllReviewsAsync()
        {
            try
            {
                var entityList = await _repository.GetAllAsync(); //Jag hämtar listan med entiteter

                if (!entityList.IsNullOrEmpty()) //Kollar om det finns något innehåll i listan
                {
                    var dtoList = new List<ReviewDto>();//Sedan skapar jag en lista där Dto varianterna kommer lagras

                    foreach (Review entity in entityList)
                    {
                        var dto = new ReviewDto()
                        {
                            CustomerId = entity.CustomerId,
                            Rating = entity.Rating,
                            Title = entity.Title,
                            Content = entity.Content
                        };

                        dtoList.Add(dto); //Slutligen läggs Dton till i Dto listan
                    }

                    return dtoList;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return new List<ReviewDto>(); //Om listan är tom, eller om något gick snett, så får man tillbaka en tom lista.
        }

        public async Task<bool> UpdateReviewAsync(int id, ReviewDto review)
        {
            try
            {
                var getEntity = await _repository.GetOneAsync(x => x.Id == id);

                var reviewEntity = new Review() //Dto omvandlas till en entitet
                {
                    Id = getEntity.Id,
                    CustomerId = review.CustomerId,
                    Rating = review.Rating,
                    Title = review.Title,
                    Content = review.Content,
                    ArticleNumbers = getEntity.ArticleNumbers
                };

                var updateResult = await _repository.UpdateAsync((x => x.Id == id), reviewEntity); //Entiteten med det angivna Id't ersätts med med nya entiteten

                return updateResult; //Om entiteten hittades så returneras true, annars false
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false; //Om något gick snett så returneras false
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            try
            {
                var result = await _repository.DeleteAsync(x => x.Id == id);

                return result; //Om entiteten kunde hittas så returneras true, annars false
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Och om något gick snett så returneras false
        }
    }
}
