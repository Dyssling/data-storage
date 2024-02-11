using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetAllImagesUI
    {
        private readonly ProductsDataContext _context;

        public GetAllImagesUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            ImageService service = new ImageService(_context);

            var imageList = await service.GetAllImagesAsync();

            foreach (ImageDto image in imageList)
            {
                Console.WriteLine(image.ImageURL);
            }
        }
    }
}
