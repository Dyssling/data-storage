using Business.Services;
using Infrastructure.Contexts;
using Presentation.ConsoleUI.EntitiesUI;

namespace Presentation.ConsoleUI
{
    public class MenuUI
    {
        private readonly ProductsDataContext _productsDataContext;
        private readonly CustomersDataContext _customersDataContext;

        public MenuUI(ProductsDataContext productsDataContext, CustomersDataContext customersDataContext)
        {
            _productsDataContext = productsDataContext;
            _customersDataContext = customersDataContext;
        }
        public async Task Show()
        {
            Console.WriteLine("Vad vill du göra?");
            Console.WriteLine();
            Console.WriteLine("1: Skapa en entitet");
            Console.WriteLine("2: Hämta en entitet");
            Console.WriteLine("3: Hämta alla entiteter av en viss typ");
            Console.WriteLine("4: Uppdatera en entitet");
            Console.WriteLine("5: Radera en entitet");
            Console.WriteLine();

            switch (Console.ReadLine()){
                case "1":
                    AddUI addUI = new AddUI(_productsDataContext, _customersDataContext);
                    await addUI.Show();
                    break;
                case "2":
                    GetUI getUI = new GetUI(_productsDataContext, _customersDataContext);
                    await getUI.Show();
                    break;
                case "3":
                    GetAllUI getAllUI = new GetAllUI(_productsDataContext, _customersDataContext);
                    await getAllUI.Show();
                    break;
                case "4":
                    UpdateUI updateUI = new UpdateUI(_productsDataContext, _customersDataContext);
                    await updateUI.Show();
                    break;
                case "5":
                    DeleteUI deleteUI = new DeleteUI(_productsDataContext, _customersDataContext);
                    await deleteUI.Show();
                    break;
            }
        }
    }
}
