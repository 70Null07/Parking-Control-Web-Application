using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestWebApplicationHTTP.Pages
{
    public class SchemeModel : PageModel
    {
        [BindProperty]
        public string CurrentLoad { get; set; }

        public string AuthSwitch { get; set; }

        public void OnGet()
        {
            IEnumerable<KeyValuePair<string, string>> user_login = Request.Cookies.Where(x => x.Key == "username");

            if (user_login.FirstOrDefault().Key != null)
            {
                AuthSwitch = "�������";
                //return RedirectPermanent("./profile");
            }
            else
            {
                AuthSwitch = "�����������";
            }

            CurrentLoad = System.IO.File.ReadAllText("C:\\temp\\plTaken.txt");
        }
    }
}
