using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestWebApplicationHTTP.Data;
using TestWebApplicationHTTP.Models;

namespace TestWebApplicationHTTP.Pages.Places
{
    public class IndexModel : PageModel
    {
        private readonly TestWebApplicationHTTP.Data.ApplicationContext _context;

        public IndexModel(TestWebApplicationHTTP.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Place> Place { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Place = await _context.Places.ToListAsync();
        }
    }
}
