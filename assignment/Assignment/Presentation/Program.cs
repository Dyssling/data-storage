using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder();
builder.ConfigureServices(services =>
{
    services.AddDbContext<CustomersDataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\data-storage\assignment\Assignment\Infrastructure\Data\CustomersAndOrders.mdf;Integrated Security=True;Connect Timeout=30"));
    services.AddDbContext<CustomersDataContext>(x => x.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Education\data-storage\assignment\Assignment\Infrastructure\Data\ProductCatalog.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=True"));
});

builder.Build();