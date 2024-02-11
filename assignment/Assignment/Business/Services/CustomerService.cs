using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
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
    }
}
