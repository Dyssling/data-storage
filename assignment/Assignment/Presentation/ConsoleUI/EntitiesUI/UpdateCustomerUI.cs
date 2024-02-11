using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class UpdateCustomerUI
    {
        private readonly CustomersDataContext _context;

        public UpdateCustomerUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett ID vars kund du vill uppdatera: ");
            var id = int.Parse(Console.ReadLine()!);

            Console.Write("Ange det nya förnamnet: ");
            var newFirstName = Console.ReadLine()!;

            Console.Write("Ange det nya efternamnet: ");
            var newLastName = Console.ReadLine()!;

            CustomerService service = new CustomerService(_context);

            var result = await service.UpdateCustomerAsync(id, new CustomerDto()
            {
                FirstName = newFirstName,
                LastName = newLastName
            });

            if (result != false)
            {
                Console.WriteLine($"Kunden har uppdaterats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att uppdatera kunden.");
            }


        }
    }
}
