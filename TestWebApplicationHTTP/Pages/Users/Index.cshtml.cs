using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestWebApplicationHTTP.Data;
using TestWebApplicationHTTP.Models;

namespace TestWebApplicationHTTP.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly TestWebApplicationHTTP.Data.ApplicationContext _context;

        public IndexModel(TestWebApplicationHTTP.Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<User> Users { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Users = await _context.Users.ToListAsync();

            //var t = HttpContext.Session.GetString("access_token");
        }
    }
}
