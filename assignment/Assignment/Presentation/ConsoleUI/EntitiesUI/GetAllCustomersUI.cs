using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetAllCustomersUI
    {
        private readonly CustomersDataContext _context;

        public GetAllCustomersUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            CustomerService service = new CustomerService(_context);

            var customerList = await service.GetAllCustomersAsync();

            foreach (CustomerDto customer in customerList)
            {
                Console.WriteLine($"{customer.FirstName} {customer.LastName}");
            }
        }
    }
}
