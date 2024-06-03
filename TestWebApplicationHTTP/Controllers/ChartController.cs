using Microsoft.AspNetCore.Mvc;
using TestWebApplicationHTTP.Data;
using TestWebApplicationHTTP.Models;

namespace TestWebApplicationHTTP.Controllers
{
    public class ChartController : Controller
    {
        private readonly ApplicationContext _context;

        public ChartController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult AdminPage(DateTime startDate, DateTime endDate)
        {
            var data = _context.AvgParkingTimes
                .Where(x => x.Date >= startDate && x.Date <= endDate)
                .ToList();
            return Json(data);
        }
    }
}
