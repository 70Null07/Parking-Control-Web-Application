using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebApplicationHTTP.Pages
{
    public class registration_confirmModel : PageModel
    {
        [BindProperty]
        public int UniqueCode { get; set; } = default!;

        public void OnGet()
        {
        }
    }
}
