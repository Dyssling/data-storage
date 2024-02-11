using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Business.Services
{
    public class ImageService
    {
        private readonly ProductsDataContext _context;
        private ImageRepository _repository;

        public ImageService(ProductsDataContext context)
        {
            _context = context;
            _repository = new ImageRepository(_context);
        }

        public async Task<bool> CreateImageAsync(ImageDto image)
        {
            try
            {
                var getResult = await _repository.GetOneAsync(x => x.ImageUrl == image.ImageURL);
                if (getResult == null) //Om bilden med samma URL hittades i databasen så ska en ny bild med samma URL INTE skapas, så bara när resultatet blir null ska den skapas.
                {
                    Image imageEntity = new Image() //ImageDto omvandlas till en entitet
                    {
                        ImageUrl = image.ImageURL
                    };

                    var createResult = await _repository.CreateAsync(imageEntity); //Sedan läggs entiteten till i databasen
                    return createResult; //Om den lyckades så får man true, annars false
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Om metoden inte lyckas, eller om en kategori redan finns så returneras ett false värde
        }

        public async Task<ImageDto> GetOneImageAsync(string imageURL)
        {
            try
            {
                var entity = await _repository.GetOneAsync(x => x.ImageUrl == imageURL); //Jag söker efter entiteten med den angivna URLen

                if (entity != null)
                {
                    ImageDto image = new ImageDto()
                    {
                        ImageURL = entity.ImageUrl
                    };

                    return image;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om ingen entitet hittas, eller om något gick snett så returneras ett null värde
        }

        public async Task<IEnumerable<ImageDto>> GetAllImagesAsync()
        {
            try
            {
                var entityList = await _repository.GetAllAsync(); //Jag hämtar listan med entiteter

                if (!entityList.IsNullOrEmpty()) //Kollar om det finns något innehåll i listan
                {
                    var dtoList = new List<ImageDto>();//Sedan skapar jag en lista där Dto varianterna kommer lagras

                    foreach (Image entity in entityList)
                    {
                        var dto = new ImageDto()
                        {
                            ImageURL = entity.ImageUrl
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

            return new List<ImageDto>(); //Om listan är tom, eller om något gick snett, så får man tillbaka en tom lista.
        }

        public async Task<bool> UpdateImageAsync(string imageURL, ImageDto image)
        {
            try
            {
                var getEntity = await _repository.GetOneAsync(x => x.ImageUrl == imageURL);

                if (getEntity != null)
                {
                    var imageEntity = new Image() //Dto omvandlas till en entitet
                    {
                        Id = getEntity.Id,
                        ImageUrl = image.ImageURL,
                        ArticleNumbers = getEntity.ArticleNumbers
                    };

                    var updateResult = await _repository.UpdateAsync((x => x.ImageUrl == imageURL), imageEntity); //Entiteten med det angivna namnet (alltså det gamla) ersätts med med nya entiteten

                    return updateResult; //Om entiteten hittades så returneras true, annars false
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false; //Om något gick snett så returneras false
        }

        public async Task<bool> DeleteImageAsync(string imageURL)
        {
            try
            {
                var result = await _repository.DeleteAsync(x => x.ImageUrl == imageURL);

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
