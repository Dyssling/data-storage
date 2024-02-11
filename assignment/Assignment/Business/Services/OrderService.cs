using Business.Dtos;
using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;

namespace Business.Services
{
    public class OrderService
    {
        private readonly CustomersDataContext _context;
        private OrderRepository _repository;

        public OrderService(CustomersDataContext context)
        {
            _context = context;
            _repository = new OrderRepository(_context);
        }

        public async Task<bool> CreateOrderAsync(OrderDto order)
        {
            try
            {
                var customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == order.CustomerId); //Här hämtar jag entiteterna som jag sedan vill göra en if-check på
                var currencyEntity = await _context.Currencies.FirstOrDefaultAsync(x => x.ISOCode == order.CurrencyISOCode);

                if (customerEntity != null && currencyEntity != null) //Jag kollar om det ens finns entiteter med de angivna värdena att referera till
                {
                    OrderEntity orderEntity = new OrderEntity() //Dto omvandlas till en entitet
                    {
                        CustomerId = order.CustomerId,
                        ProductList = order.ProductList,
                        Cost = order.Cost,
                        CurrencyISOCode = order.CurrencyISOCode
                    };

                    var createResult = await _repository.CreateAsync(orderEntity); //Sedan läggs entiteten till i databasen
                    return createResult; //Om den lyckades så får man true, annars false
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return false; //Om metoden inte lyckas, eller om en entitet med de angivna värdena inte hittades så returneras ett false värde
        }

        public async Task<OrderDto> GetOneOrderAsync(int id)
        {
            try
            {
                var entity = await _repository.GetOneAsync(x => x.Id == id); //Jag söker efter entiteten med det angivna Id't

                if (entity != null)
                {
                    OrderDto order = new OrderDto()
                    {
                        CustomerId = entity.CustomerId,
                        ProductList = entity.ProductList,
                        Cost = entity.Cost,
                        CurrencyISOCode = entity.CurrencyISOCode
                    };

                    return order;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null!; //Om ingen entitet hittas, eller om något gick snett så returneras ett null värde
        }

        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            try
            {
                var entityList = await _repository.GetAllAsync(); //Jag hämtar listan med entiteter

                if (!entityList.IsNullOrEmpty()) //Kollar om det finns något innehåll i listan
                {
                    var dtoList = new List<OrderDto>();//Sedan skapar jag en lista där Dto varianterna kommer lagras

                    foreach (OrderEntity entity in entityList)
                    {
                        var dto = new OrderDto()
                        {
                            CustomerId = entity.CustomerId,
                            ProductList = entity.ProductList,
                            Cost = entity.Cost,
                            CurrencyISOCode = entity.CurrencyISOCode
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

            return new List<OrderDto>(); //Om listan är tom, eller om något gick snett, så får man tillbaka en tom lista.
        }

        public async Task<bool> UpdateOrderAsync(int id, OrderDto order)
        {
            try
            {
                var getEntity = await _repository.GetOneAsync(x => x.Id == id);

                if (getEntity != null)
                {
                    var orderEntity = new OrderEntity() //Dto omvandlas till en entitet
                    {
                        Id = getEntity.Id,
                        CustomerId = order.CustomerId,
                        ProductList = order.ProductList,
                        Cost = order.Cost,
                        CurrencyISOCode = order.CurrencyISOCode
                    };

                    var updateResult = await _repository.UpdateAsync((x => x.Id == id), orderEntity); //Entiteten med det angivna Id't ersätts med den nya entiteten

                    return updateResult; //Om entiteten hittades så returneras true, annars false
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return false; //Om något gick snett så returneras false
        }
    }
}
