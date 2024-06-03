using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TestWebApplicationHTTP.Data;
using TestWebApplicationHTTP.Models;
using TestWebApplicationHTTP.Controllers;

namespace TestWebApplicationHTTP.Pages
{
    public class AuthorizationModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

        private readonly ApplicationContext _context;

        public AuthorizationModel(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            IEnumerable<KeyValuePair<string, string>> user_login = Request.Cookies.Where(x => x.Key == "username");

            if (user_login.FirstOrDefault().Key != null)
            {
                return RedirectPermanent("./profile");
            }

            return Page();
        }

        public IActionResult OnPostAuth()
        {
            if (User.Login != null && User.Password != null)
            {
                List<User> users = _context.Users.Where(x => x.Login == User.Login || x.Email == User.Login).ToList();

                if (users.Count == 0 || users.First().Password != User.Password)
                {
                    return Page();
                }

                var tokenController = new UsersAccountController();
                JsonResult result = (JsonResult)tokenController.Token(users.First().Login, User.Password, users.First().Email, users.First().Permissions, users.First().Permissions, DateTime.UtcNow);
                string accessToken = result.Value.GetType().GetProperty("access_token").GetValue(result.Value, null).ToString();
                string username = result.Value.GetType().GetProperty("username").GetValue(result.Value, null).ToString();

                // �������� ������� CookieOptions ��� ��������� ���������� ����
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(30), // ������������� ���� �������� ���� �� 30 ����
                    Secure = true, // ������������� ���� Secure ��� �������� ���� ������ �� HTTPS
                    HttpOnly = true // ������������� ���� HttpOnly ��� ������ �� ������� ����� JavaScript
                };

                //Response.Headers.Append("Bearer ", accessToken);

                // ������������� ���������� ���� � JWT ������� � ������ ������������
                Response.Cookies.Append("jwtToken", accessToken, cookieOptions);
                Response.Cookies.Append("username", username, cookieOptions);

                return RedirectPermanent("/profile");
            }

            return Page();
        }

        //public void OnPost()
        //{
        //    Message = $"������� �����������: {userClass.userName}, {userClass.userPassword}.";
        //}
    }
}
