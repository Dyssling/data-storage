using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class DeleteOrderUI
    {
        private readonly CustomersDataContext _context;

        public DeleteOrderUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ID't för ordern du vill radera: ");
            var id = int.Parse(Console.ReadLine()!);

            OrderService service = new OrderService(_context);

            var order = await service.DeleteOrderAsync(id);

            if (order != false)
            {
                Console.WriteLine($"Ordern har tagits bort.");
            }
            else
            {
                Console.WriteLine($"Ordern kunde inte tas bort.");
            }
        }
    }
}
