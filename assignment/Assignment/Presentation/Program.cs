using Business.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleUI;

var builder = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddDbContext<CustomersDataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\data-storage\assignment\Assignment\Infrastructure\Data\CustomersAndOrders.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddDbContext<ProductsDataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\data-storage\assignment\Assignment\Infrastructure\Data\ProductCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));

    services.AddScoped<CategoryRepository>();
    services.AddScoped<CurrencyRepository>();
    services.AddScoped<CustomerRepository>();
    services.AddScoped<ImageRepository>();
    services.AddScoped<OrderRepository>();
    services.AddScoped<ProductRepository>();
    services.AddScoped<ReviewRepository>();

    services.AddScoped<CategoryService>();
    services.AddScoped<CurrencyService>();
    services.AddScoped<CustomerService>();
    services.AddScoped<ImageService>();
    services.AddScoped<OrderService>();
    services.AddScoped<ProductService>();
    services.AddScoped<ReviewService>();

    services.AddSingleton<MenuUI>();
}).Build();

var menuUI = builder.Services.GetRequiredService<MenuUI>();
await menuUI.Show();