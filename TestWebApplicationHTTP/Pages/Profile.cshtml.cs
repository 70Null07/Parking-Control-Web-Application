using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApplicationHTTP.Data;
using TestWebApplicationHTTP.Models;

namespace TestWebApplicationHTTP.Pages
{
    public class ProfileModel : PageModel
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string PhoneNumber { get; set; }
        public string NewPassword { get; set; }

        [BindProperty]
        public User User { get; set; } /*= default!;*/

        [BindProperty]
        public UserInfo UserInfo { get; set; }

        private readonly ApplicationContext _context;

        public ProfileModel(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            IEnumerable<KeyValuePair<string, string>> user_login = Request.Cookies.Where(x => x.Key == "username");

            if (user_login.FirstOrDefault().Key != null)
            {
                string email = _context.Users.Where(x => x.Login == user_login.First().Value).First().Email;

                UserInfo? currentUserInfo = _context.UsersInfo.Where(x => x.Email == email).FirstOrDefault();

                UserInfo = new UserInfo();

                if (currentUserInfo != null)
                {
                    UserInfo.Name = currentUserInfo.Name;
                    UserInfo.SurName = currentUserInfo.SurName;
                    UserInfo.Phone = currentUserInfo.Phone;
                    UserInfo.Email = email;
                }
                else
                {
                    UserInfo.Name = "Имя не установлено";
                    UserInfo.SurName = "Фамилия не установлена";
                    UserInfo.Email = email;
                    UserInfo.Phone = "";
                    UserInfo.NumberPlate = "";

                    _context.UsersInfo.Add(UserInfo);
                    _context.SaveChanges();
                }
                return Page();
            }
            return RedirectPermanent("./authorization");
        }

        public IActionResult OnPostSaveInfo()
        {
            IEnumerable<KeyValuePair<string, string>> user_login = Request.Cookies.Where(x => x.Key == "username");
            string email = _context.Users.Where(x => x.Login == user_login.First().Value).First().Email;

            if (UserInfo.Email == null)
            {
                UserInfo.Email = email;
            }

            UserInfo? currentUserInfo = _context.UsersInfo.Where(x => x.Email == email).FirstOrDefault();
            currentUserInfo.Name = UserInfo.Name;
            currentUserInfo.SurName = UserInfo.SurName;
            currentUserInfo.Phone = UserInfo.Phone;

            //_context.UsersInfo.Add(UserInfo);
            _context.SaveChanges();

            return Page();
        }

        public IActionResult OnPostExit()
        {
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("jwtToken");

            return RedirectPermanent("./authorization");
        }
    }
}
