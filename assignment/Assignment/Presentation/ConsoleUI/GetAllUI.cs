﻿using Infrastructure.Contexts;
using Presentation.ConsoleUI.EntitiesUI;

namespace Presentation.ConsoleUI
{
    public class GetAllUI
    {
        private readonly ProductsDataContext _productsDataContext;
        private readonly CustomersDataContext _customersDataContext;

        public GetAllUI(ProductsDataContext productsDataContext, CustomersDataContext customersDataContext)
        {
            _productsDataContext = productsDataContext;
            _customersDataContext = customersDataContext;
        }
        public async Task Show()
        {
            Console.WriteLine("Välj typen av entitet som du vill hämta alla utav:");
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
                    GetAllCategoriesUI getAllCategoriesUI = new GetAllCategoriesUI(_productsDataContext!);
                    await getAllCategoriesUI.Show();
                    break;
                case "2":
                    GetAllCurrenciesUI getAllCurrenciesUI = new GetAllCurrenciesUI(_customersDataContext!);
                    await getAllCurrenciesUI.Show();
                    break;
                case "3":
                    GetAllCustomersUI getAllCustomersUI = new GetAllCustomersUI(_customersDataContext!);
                    await getAllCustomersUI.Show();
                    break;
                case "4":
                    GetAllImagesUI getAllImagesUI = new GetAllImagesUI(_productsDataContext!);
                    await getAllImagesUI.Show();
                    break;
                case "5":
                    GetAllOrdersUI getAllOrdersUI = new GetAllOrdersUI(_customersDataContext!);
                    await getAllOrdersUI.Show();
                    break;
                case "6":
                    GetAllProductsUI getAllProductsUI = new GetAllProductsUI(_productsDataContext!);
                    await getAllProductsUI.Show();
                    break;
                case "7":
                    GetAllReviewsUI getAllReviewsUI = new GetAllReviewsUI(_productsDataContext!);
                    await getAllReviewsUI.Show();
                    break;
            }
        }
    }
}

