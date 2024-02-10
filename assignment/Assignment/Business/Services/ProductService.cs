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
                ICollection<Category> categories = new List<Category>();//Jag skapar en ny lista där kategorierna som tillhör kategorinamnen som är givna i produkten kommer lagras

                foreach (string categoryName in product.Categories) //Loopen kollar igenom alla givna kategorinamn i den nya produkten
                {
                    var result = await categoryRepository.GetOneAsync(x => x.Name == categoryName); //Här kollar jag om en kategori redan finns med det givna namnet
                    if (result == null) //Om kategorin inte finns
                    {
                        Category newCategory = new Category() { Name = categoryName }; //Så skapar jag kategorin
                        await categoryRepository.CreateAsync(newCategory); //Och lägger till den i databasen
                        result = await categoryRepository.GetOneAsync(x => x.Name == categoryName); //Och sedan hämtas en result/entitet igen (denna gången finns den)
                    }
                    categories.Add(result); //Här lagras kategorin i den "interna" listan som jag skapade ovan
                }

                Product productEntity = new Product() //ProductDto omvandlas till en entitet
                {
                    ArticleNumber = product.ArticleNumber,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Categories = categories //Här hämtas den "interna" kategorilistan som tidigare skapades
                };

                await _repository.CreateAsync(productEntity); //Sedan läggs Produkt entiteten till i databasen
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
