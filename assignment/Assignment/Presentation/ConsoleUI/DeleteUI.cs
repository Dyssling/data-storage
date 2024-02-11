using Infrastructure.Contexts;
using Presentation.ConsoleUI.EntitiesUI;

namespace Presentation.ConsoleUI
{
    public class DeleteUI
    {
        private readonly ProductsDataContext _productsDataContext;
        private readonly CustomersDataContext _customersDataContext;

        public DeleteUI(ProductsDataContext productsDataContext, CustomersDataContext customersDataContext)
        {
            _productsDataContext = productsDataContext;
            _customersDataContext = customersDataContext;
        }
        public async Task Show()
        {
            Console.WriteLine("Välj typen av entitet som du vill radera:");
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
                    DeleteCategoryUI deleteCategoryUI = new DeleteCategoryUI(_productsDataContext!);
                    await deleteCategoryUI.Show();
                    break;
                case "2":
                    DeleteCurrencyUI deleteCurrencyUI = new DeleteCurrencyUI(_customersDataContext!);
                    await deleteCurrencyUI.Show();
                    break;
                case "3":
                    DeleteCustomerUI deleteCustomerUI = new DeleteCustomerUI(_customersDataContext!);
                    await deleteCustomerUI.Show();
                    break;
                case "4":
                    DeleteImageUI deleteImageUI = new DeleteImageUI(_productsDataContext!);
                    await deleteImageUI.Show();
                    break;
                case "5":
                    DeleteOrderUI deleteOrderUI = new DeleteOrderUI(_customersDataContext!);
                    await deleteOrderUI.Show();
                    break;
            }
        }
    }
}
