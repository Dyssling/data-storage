using Infrastructure.Contexts;
using Presentation.ConsoleUI.EntitiesUI;

namespace Presentation.ConsoleUI
{
    public class GetUI
    {
        private readonly ProductsDataContext _productsDataContext;
        private readonly CustomersDataContext _customersDataContext;

        public GetUI(ProductsDataContext productsDataContext, CustomersDataContext customersDataContext)
        {
            _productsDataContext = productsDataContext;
            _customersDataContext = customersDataContext;
        }
        public async Task Show()
        {
            Console.WriteLine("Välj typen av entitet som du vill hämta en utav:");
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
                    GetOneCategoryUI getOneCategoryUI = new GetOneCategoryUI(_productsDataContext!);
                    await getOneCategoryUI.Show();
                    break;
                case "2":
                    GetOneCurrencyUI getOneCurrencyUI = new GetOneCurrencyUI(_customersDataContext!);
                    await getOneCurrencyUI.Show();
                    break;
            }
        }
    }
}
