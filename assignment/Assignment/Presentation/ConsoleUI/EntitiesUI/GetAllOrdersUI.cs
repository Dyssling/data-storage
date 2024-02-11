using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;
using System;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class GetAllOrdersUI
    {
        private readonly CustomersDataContext _context;

        public GetAllOrdersUI(CustomersDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            OrderService service = new OrderService(_context);

            var orderList = await service.GetAllOrdersAsync();

            foreach (OrderDto order in orderList)
            {
                Console.WriteLine($"{order.ProductList} för {order.Cost} {order.CurrencyISOCode}");
            }
        }
    }
}
