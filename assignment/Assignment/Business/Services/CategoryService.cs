using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Business.Services
{
    public class CategoryService
    {
        private readonly ProductsDataContext _context;
        private CategoryRepository _repository;

        public CategoryService(ProductsDataContext context)
        {
            _context = context;
            _repository = new CategoryRepository(_context);
        }

        public async Task<bool> CreateCategoryAsync(CategoryDto category)
        {
            try
            {
                var getResult = await _repository.GetOneAsync(x => x.Name == category.Name);
                if (getResult == null) //Om kategorin hittades i databasen så ska en ny kategori med samma namn INTE skapas, så bara när resultatet blev null ska den skapas.
                {
                    Category categoryEntity = new Category() //CategoryDto omvandlas till en entitet
                    {
                        Name = category.Name
                    };

                    var createResult = await _repository.CreateAsync(categoryEntity); //Sedan läggs Category entiteten till i databasen
                    return createResult; //Om den lyckades så får man true, annars false
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Om metoden inte lyckas, eller om en kategori redan finns så returneras ett false värde
        }

        public async Task<CategoryDto> GetOneCategoryAsync(string name)
        {
            try
            {
                var entity = await _repository.GetOneAsync(x => x.Name == name); //Jag söker efter entiteten med det angivna namnet

                if (entity != null)
                {
                    CategoryDto category = new CategoryDto()
                    {
                        Name = entity.Name,
                    };

                    return category;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om ingen kategori hittas, eller om något gick snett så returneras ett null värde
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            try
            {
                var entityList = await _repository.GetAllAsync(); //Jag hämtar listan med entiteter

                if (!entityList.IsNullOrEmpty()) //Kollar om det finns något innehåll i listan
                {
                    var dtoList = new List<CategoryDto>();//Sedan skapar jag en lista där Dto varianterna kommer lagras

                    foreach (Category entity in entityList)
                    {
                        var dto = new CategoryDto()
                        {
                            Name = entity.Name
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

            return new List<CategoryDto>(); //Om listan är tom, eller om något gick snett, så får man tillbaka en tom lista.
        }

        public async Task<bool> UpdateCategoryAsync(string name, CategoryDto category)
        {
            try
            {
                var getEntity = await _repository.GetOneAsync(x => x.Name == name);

                var categoryEntity = new Category() //Dto omvandlas till en entitet
                {
                    Id = getEntity.Id,
                    Name = category.Name,
                    ArticleNumbers = getEntity.ArticleNumbers
                };

                var updateResult = await _repository.UpdateAsync((x => x.Name == name), categoryEntity); //Entiteten med det angivna namnet (alltså det gamla) ersätts med med nya entiteten

                return updateResult; //Om entiteten hittades så returneras true, annars false
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false; //Om något gick snett så returneras false
        }

        public async Task<bool> DeleteCategoryAsync(string name)
        {
            try
            {
                var result = await _repository.DeleteAsync(x => x.Name == name);

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
