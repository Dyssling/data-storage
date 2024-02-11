using Business.Dtos;
using Business.Services;
using Infrastructure.Contexts;

namespace Presentation.ConsoleUI.EntitiesUI
{
    public class AddReviewUI
    {
        private readonly ProductsDataContext _context;

        public AddReviewUI(ProductsDataContext context)
        {
            _context = context;

        }
        public async Task Show()
        {
            Console.Write("Ange ett kund-ID som tillhör recensionen: ");
            var customerId = int.Parse(Console.ReadLine()!);

            Console.Write("Ange ett betyg mellan 1-5: ");
            var rating = byte.Parse(Console.ReadLine()!);

            Console.Write("Ange ett meddelande: ");
            var content = Console.ReadLine()!;


            ReviewService service = new ReviewService(_context);

            var result = await service.CreateReviewAsync(new ReviewDto() 
            { 
                CustomerId = customerId,
                Rating = rating,
                Content = content
            });

            if (result != false)
            {
                Console.WriteLine($"Recensionen har skapats.");
            }
            else
            {
                Console.WriteLine($"Det gick inte att skapa recensionen.");
            }


        }
    }
}
