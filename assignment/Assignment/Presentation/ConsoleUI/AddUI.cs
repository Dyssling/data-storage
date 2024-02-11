using Business.Services;
using Infrastructure.Contexts;
using Presentation.ConsoleUI.EntitiesUI;

namespace Presentation.ConsoleUI
{
    public class AddUI
    {
        private readonly ProductsDataContext _productsDataContext;
        private readonly CustomersDataContext _customersDataContext;

        public AddUI(ProductsDataContext productsDataContext, CustomersDataContext customersDataContext)
        {
            _productsDataContext = productsDataContext;
            _customersDataContext = customersDataContext;
        }

        public async Task Show()
        {
            Console.WriteLine("Välj typen av entitet som du vill skapa:");
            Console.WriteLine();
            Console.WriteLine("1: Kategori");
            Console.WriteLine("2: Valuta");
            Console.WriteLine("3: Kund");
            Console.WriteLine("4: Bild");
            Console.WriteLine("5: Order");
            Console.WriteLine("6: Produkt");
            Console.WriteLine("7: Recension");
            Console.WriteLine();

            switch (Console.ReadLine())
            {
                case "1":
                    AddCategoryUI addCategoryUI = new AddCategoryUI(_productsDataContext!);
                    await addCategoryUI.Show();
                    break;
                case "2":
                    AddCurrencyUI addCurrencyUI = new AddCurrencyUI(_customersDataContext!);
                    await addCurrencyUI.Show();
                    break;
                case "3":
                    AddCustomerUI addCustomerUI = new AddCustomerUI(_customersDataContext!);
                    await addCustomerUI.Show();
                    break;
            }
        }
    }
}
