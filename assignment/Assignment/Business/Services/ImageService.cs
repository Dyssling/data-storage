using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
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
    }
}
