﻿using Infrastructure.Contexts;
using Presentation.ConsoleUI.EntitiesUI;

namespace Presentation.ConsoleUI
{
    public class UpdateUI
    {
        private readonly ProductsDataContext _productsDataContext;
        private readonly CustomersDataContext _customersDataContext;

        public UpdateUI(ProductsDataContext productsDataContext, CustomersDataContext customersDataContext)
        {
            _productsDataContext = productsDataContext;
            _customersDataContext = customersDataContext;
        }

        public async Task Show()
        {
            Console.WriteLine("Välj typen av entitet som du vill uppdatera:");
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
                    UpdateCategoryUI updateCategoryUI = new UpdateCategoryUI(_productsDataContext!);
                    await updateCategoryUI.Show();
                    break;
                case "2":
                    UpdateCurrencyUI updateCurrencyUI = new UpdateCurrencyUI(_customersDataContext!);
                    await updateCurrencyUI.Show();
                    break;
                case "3":
                    UpdateCustomerUI updateCustomerUI = new UpdateCustomerUI(_customersDataContext!);
                    await updateCustomerUI.Show();
                    break;
                case "4":
                    UpdateImageUI updateImageUI = new UpdateImageUI(_productsDataContext!);
                    await updateImageUI.Show();
                    break;
                case "5":
                    UpdateOrderUI updateOrderUI = new UpdateOrderUI(_customersDataContext!);
                    await updateOrderUI.Show();
                    break;
                case "6":
                    UpdateProductUI updateProductUI = new UpdateProductUI(_productsDataContext!);
                    await updateProductUI.Show();
                    break;
                case "7":
                    UpdateReviewUI updateReviewUI = new UpdateReviewUI(_productsDataContext!);
                    await updateReviewUI.Show();
                    break;
            }
        }
    }
}
