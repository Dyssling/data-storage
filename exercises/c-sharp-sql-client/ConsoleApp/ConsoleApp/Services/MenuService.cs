using ConsoleApp.Repositories;

namespace ConsoleApp.Services
{
    public class MenuService
    {
        public void Menu()
        {
            Console.WriteLine("First name: ");
            var firstName = Console.ReadLine();

            Console.WriteLine("Last name: ");
            var lastName = Console.ReadLine();

            var customerService = new CustomerService();

            customerService.AddCustomer(customerService.RegisterCustomer(firstName!, lastName!));

            Console.ReadKey();
        }
    }
}
