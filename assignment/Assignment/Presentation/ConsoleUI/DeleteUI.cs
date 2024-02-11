namespace Presentation.ConsoleUI
{
    public class DeleteUI
    {
        public void Show()
        {
            Console.WriteLine("Välj typen av entitet som du vill radera:");
            Console.WriteLine("1: Kategori");
            Console.WriteLine("2: Valuta");
            Console.WriteLine("3: Kund");
            Console.WriteLine("4: Bild");
            Console.WriteLine("5: Order");
            Console.WriteLine("6: Produkt");
            Console.WriteLine("7: Recension");
        }
    }
}
