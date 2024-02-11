using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class DeleteCustomerUI
    {
        private readonly CustomersDataContext _context;

        public DeleteCustomerUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ID't för kunden du vill radera: ");
            var id = int.Parse(Console.ReadLine()!);

            CustomerService service = new CustomerService(_context);

            var customer = await service.DeleteCustomerAsync(id);

            if (customer != false)
            {
                Console.WriteLine($"Kunden har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Kunden kunde inte tas bort.");
            }
        }
    }
}
