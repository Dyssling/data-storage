using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Business.Services
{
    public class CustomerService
    {
        private readonly CustomersDataContext _context;
        private CustomerRepository _repository;

        public CustomerService(CustomersDataContext context)
        {
            _context = context;
            _repository = new CustomerRepository(_context);
        }

        public async Task<bool> CreateCustomerAsync(CustomerDto customer)
        {
            try
            {
                CustomerEntity customerEntity = new CustomerEntity() //Dto omvandlas till en entitet
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName
                };

                var createResult = await _repository.CreateAsync(customerEntity); //Sedan läggs entiteten till i databasen
                return createResult; //Om den lyckades så får man true, annars false
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Om metoden inte lyckas, eller om en kategori redan finns så returneras ett false värde
        }

        public async Task<CustomerDto> GetOneCustomerAsync(int id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(x => x.Id == id); //Jag söker efter entiteten med det angivna Id't

                if (entity != null)
                {
                    CustomerDto customer = new CustomerDto()
                    {
                        FirstName = entity.FirstName,
                        LastName = entity.LastName
                    };

                    return customer;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om ingen entitet hittas, eller om något gick snett så returneras ett null värde
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomersAsync()
        {
            try
            {
                var entityList = await _repository.GetAllAsync(); //Jag hämtar listan med entiteter

                if (!entityList.IsNullOrEmpty()) //Kollar om det finns något innehåll i listan
                {
                    var dtoList = new List<CustomerDto>();//Sedan skapar jag en lista där Dto varianterna kommer lagras

                    foreach (CustomerEntity entity in entityList)
                    {
                        var dto = new CustomerDto()
                        {
                            FirstName = entity.FirstName,
                            LastName = entity.LastName
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

            return new List<CustomerDto>(); //Om listan är tom, eller om något gick snett, så får man tillbaka en tom lista.
        }

        public async Task<bool> UpdateCustomerAsync(int id, CustomerDto customer)
        {
            try
            {
                var getEntity = await _repository.GetOneAsync(x => x.Id == id);

                var customerEntity = new CustomerEntity() //Dto omvandlas till en entitet
                {
                    Id = getEntity.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Orders = getEntity.Orders
                };

                var updateResult = await _repository.UpdateAsync((x => x.Id == id), customerEntity); //Entiteten med det angivna Id't ersätts med med nya entiteten

                return updateResult; //Om entiteten hittades så returneras true, annars false
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false; //Om något gick snett så returneras false
        }

        public async Task<bool> DeleteCustomerAsync(int id)
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
