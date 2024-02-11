using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class AddCustomerUI
    {
        private readonly CustomersDataContext _context;

        public AddCustomerUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett förnamn: ");
            var firstName = Console.ReadLine()!;

            Console.Write("Ange ett efternamn: ");
            var lastName = Console.ReadLine()!;

            CustomerService service = new CustomerService(_context);

            var result = await service.CreateCustomerAsync(new CustomerDto() 
            { 
                FirstName = firstName,
                LastName = lastName
            });

            if (result != false)
            {
                Console.WriteLine($"Kunden har skapats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att skapa kunden.");
            }


        }
    }
}
