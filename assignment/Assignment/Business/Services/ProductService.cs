using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Business.Services
{
    public class ProductService
    {
        private readonly ProductsDataContext _context;
        private ProductRepository _repository;

        public ProductService(ProductsDataContext context)
        {
            _context = context;
            _repository = new ProductRepository(_context);
        }

        public async Task<bool> CreateProductAsync(ProductDto product)
        {
            var categoryRepository = new CategoryRepository(_context);

            try
            {
                foreach (Category category in product.Categories) //Loopen kollar igenom alla kategorier i den nya produkten
                {
                    var result = await categoryRepository.GetOneAsync(x => x.Name == category.Name); //Här kollar jag om en kategori redan finns med det givna namnet
                    if (result == null) //Om kategorin inte finns
                    {
                        Category newCategory = new Category() { Name = category.Name }; //Så skapar jag kategorin
                        await categoryRepository.CreateAsync(newCategory); //Och lägger till den i databasen
                    }
                }

                Product productEntity = new Product() //ProductDto omvandlas till en entitet
                {
                    ArticleNumber = product.ArticleNumber,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Categories = product.Categories
                };

                await _repository.CreateAsync(productEntity); //Sedan läggs den till i databasen
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false; //Om metoden inte lyckas returneras ett false värde
            }
        }
    }
}
