using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApplicationHTTP.Data;
using TestWebApplicationHTTP.Models;

namespace TestWebApplicationHTTP.Pages
{
    public class AdminPageModel : PageModel
    {
        private readonly ApplicationContext _context;

        public AdminPageModel(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<AvgParkingTime> ParkingTimes { get; set; } = default!;

        public void OnGet()
        {
            ParkingTimes = _context.AvgParkingTimes.ToList(); // Загрузите данные из представления
        }
    }
}
