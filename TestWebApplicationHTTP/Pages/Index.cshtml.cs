using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebApplicationHTTP.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public string AuthSwitch { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            //var token = Request.Cookies.Where(x => x.Key == "jwtToken");
            //if (token.FirstOrDefault().Key != null)
            //{
            //    Response.Headers.Add("Authorization", "Bearer " + token.FirstOrDefault().Value);
            //}

            IEnumerable<KeyValuePair<string, string>> user_login = Request.Cookies.Where(x => x.Key == "username");

            if (user_login.FirstOrDefault().Key != null)
            {
                AuthSwitch = "Профиль";
                //return RedirectPermanent("./profile");
            }
            else
            {
                AuthSwitch = "Авторизация";
            }

            return Page();
        }
    }
}
