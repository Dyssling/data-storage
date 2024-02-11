using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetOneCustomerUI
    {
        private readonly CustomersDataContext _context;

        public GetOneCustomerUI(CustomersDataContext context)
        {
            _context = context;
        }
        public async Task Show()
        {
            Console.Write("Ange ID't för kunden du vill hämta: ");
            var id = int.Parse(Console.ReadLine()!);

            CustomerService service = new CustomerService(_context);

            var customer = await service.GetOneCustomerAsync(id);

            if (customer != null)
            {
                Console.WriteLine($"Förnamn: {customer.FirstName}");
                Console.WriteLine($"Efternamn: {customer.LastName}");
            }
            else
            {
                Console.WriteLine($"Ingen kund hittades.");
            }
        }
    }
}
