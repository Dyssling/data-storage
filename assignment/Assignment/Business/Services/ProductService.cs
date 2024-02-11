using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Linq.Expressions;

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

                var createResult = await _repository.CreateAsync(productEntity); //Sedan läggs Produkt entiteten till i databasen
                return createResult; //Om den lyckades så får man true, annars false
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false; //Om metoden inte lyckas returneras ett false värde
            }
        }

        public async Task<ProductDto> GetOneProductAsync(string articleNumber)
        {
            try
            {
                var entity = await _repository.GetOneAsync(x => x.ArticleNumber == articleNumber); //Jag söker efter entiteten med det angivna artikelnumret

                if (entity != null)
                {
                    ICollection<string> categories = new List<string>(); //Skapar en lista där produktens kategorier kommer lagras

                    foreach (Category category in entity.Categories) //Varje kategorinamn läggs till i listan
                    {
                        categories.Add(category.Name);
                    }

                    ProductDto product = new ProductDto()
                    {
                        ArticleNumber = entity.ArticleNumber,
                        Name = entity.Name,
                        Description = entity.Description,
                        Price = entity.Price,
                        Categories = categories //Här sätts kategorilistan in som en property
                    };

                    return product;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om ingen produkt hittas så returneras ett null värde
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            try
            {
                var entityList = await _repository.GetAllAsync(); //Jag hämtar listan med entiteter

                if (!entityList.IsNullOrEmpty()) //Kollar om det finns något innehåll i listan
                {
                    var dtoList = new List<ProductDto>();//Sedan skapar jag en lista där Dto varianterna kommer lagras

                    foreach (Product entity in entityList)
                    {
                        var categoryList = new List<string>(); //Här skapas en tom lista som kategorinamnen kommer lagras i

                        foreach (Category category in entity.Categories)
                        {
                            categoryList.Add(category.Name); //Kategorinamnet läggs till i listan
                        }

                        var dto = new ProductDto()
                        {
                            ArticleNumber = entity.ArticleNumber,
                            Name = entity.Name,
                            Description = entity.Description,
                            Price = entity.Price,
                            Categories = categoryList //Kategorilistan sätts som en property
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

            return new List<ProductDto>(); //Om listan är tom, eller om något gick snett, så får man tillbaka en tom lista.
        }

        public async Task<bool> UpdateProductAsync(string articleNumber, ProductDto product)
        {
            var categoryRepository = new CategoryRepository(_context);
            ICollection<Category> categories = new List<Category>();

            try
            {
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

                var productEntity = new Product() //ProductDto omvandlas till en entitet
                {
                    ArticleNumber = articleNumber, //Artikelnumret får inte ändras
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Categories = categories //Här hämtas den "interna" kategorilistan som tidigare skapades
                };

                var updateResult = await _repository.UpdateAsync((x => x.ArticleNumber == articleNumber), productEntity); //Entiteten med det angivna artikelnumret (alltså det gamla) ersätts med med nya entiteten

                return updateResult; //Om entiteten hittades så returneras true, annars false
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false; //Om något gick snett så returneras false
        }

        public async Task<bool> DeleteProductAsync(string articleNumber)
        {
            try
            {
                var result = await _repository.DeleteAsync(x => x.ArticleNumber == articleNumber);

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
