using ConsoleApp.Entities;
using ConsoleApp.Models;
using ConsoleApp.Repositories;

namespace ConsoleApp.Services
{
    public class CustomerService
    {
        public CustomerRegistration RegisterCustomer(string firstName, string lastName)
        {
            var customer = new CustomerRegistration() {
                FirstName = firstName,
                LastName = lastName
            };

            return customer;
        }

        public void AddCustomer(CustomerRegistration customer)
        {
            var customerEntity =  new CustomerEntity() {
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            var customerRepository = new CustomerRepository();

            customerRepository.Create(customerEntity, "INSERT INTO Customers VALUES (@FirstName, @LastName)");
        }

    }
}
