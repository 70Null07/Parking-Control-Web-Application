using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestWebApplicationHTTP.Data;
using TestWebApplicationHTTP.Models;
using TestWebApplicationHTTP.Controllers;
using NuGet.Protocol;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Razor.Language;
using System.Net.Http;

namespace TestWebApplicationHTTP.Pages
{
    public class RegistrationModel : PageModel
    {
        public bool ShowSuccessMessage { get; set; }

        [TempData]
        public string RegisterStatusMessage { get; set; }

        private readonly ApplicationContext _context;

        public RegistrationModel(ApplicationContext context)
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

        [BindProperty]
        public User User { get; set; } /*= default!;*/

        [BindProperty]
        public string ConfirmationCodeFromUser { get; set; }

        [BindProperty]
        public string ErrorMessage { get; set; }

        public IActionResult OnPostRequestCode()
        {
            if (User.Email == null || !User.Email.Contains('@'))
            {
                ErrorMessage = "��������� ������������ ��������� ������ � ��������� �������";
                return Page();
            }

            List<User> userWithEmail = _context.Users.Where(x => x.Email == User.Email).ToList();

            if (userWithEmail.Count > 0)
            {
                ErrorMessage = "������������ � ��������� ������ ��� ����������," +
                    " ������������� ��� ������� ������ �����";
                return Page();
            }

            string ConfirmationCode = new Random().Next(100000, 999999).ToString();

            string template = "<html>\r\n<div style=\"background-image: linear-gradient(to right, rgba(45,84,41,0.63), rgba(5,150,48,0.33));\">\r\n    <img src=\"http://217.144.172.106/images/carparkinglogogreen-blue.png\" alt=\"\" data-image-width=\"1024\" data-image-height=\"1024\" style=\"width: 103px;height: 103px;\">\r\n\r\n    <table width=\"100%\" cellpadding=\"5\" cellspacing=\"0\" style=\"empty-cells: show;\">\r\n        <tr>\r\n            <td>&nbsp;</td>\r\n            <td style=\"text-align: right;\">������ �������</td>\r\n            <td style=\"text-align: left;\">������</td>\r\n        </tr>\r\n        <tr>\r\n            <td style=\"width: 10%\"> </td>\r\n            <td style=\"width: 70%\">\r\n                <div style=\"color: #111111;background-color: #e5e5e5;display: flex;text-align: left;width: 800px;height: auto;border-radius: 10px !important;\">\r\n                    <div style=\"flex: 1;max-width: 100%;transition-duration: inherit;padding: 30px;\">\r\n                        <h3 style=\"font-weight: 400;font-size: 2.25rem;line-height: 1.2;margin-top: 0;margin-bottom: 0.5rem;font-family: Roboto,sans-serif;color: inherit;text-align: center;margin: 0 auto;\">��� �������������:</h3>\r\n                        <div style=\"display: flex;color: #ffffff;background-color: #304937;text-align: left;min-height: 71px;width: 491px;height: auto;background-image: none;margin: 16px auto 0;border-radius: 10px !important;\">\r\n                            <div style=\"flex: 1;max-width: 100%;transition-duration: inherit;padding-left: 30px;padding-right: 30px;\">\r\n                                <h3 style=\"font-weight: 400;font-size: 2.25rem;line-height: 1.2;margin-bottom: 0.5rem;font-family: Roboto,sans-serif;color: inherit;text-align: center;margin: 14px auto 0;\">" +
                ConfirmationCode +
                "</h3>\r\n</div>\r\n</div>\r\n</div>\r\n</div>\r\n</td>\r\n            <td style=\"width: 10%\"> </td>\r\n        </tr>\r\n        <tr>\r\n            <td style=\"width: 10%\"> </td>\r\n            <td style=\"width: 70%\">\r\n                <p style=\"font-size: 0.875rem;width: 800px;\">\r\n                    ��� ������ ������������ �������������. ����������, �� ��������� �� ����. �� �������� ��� ��c���, ������ ��� ���������������� ������� �� ����� parkingcontrolcomplex.ru<br>\r\n                    <br>� 2023 �������� ���������� ���������\r\n                </p>\r\n            </td>\r\n            <td style=\"width: 10%\"> </td>\r\n        </tr>\r\n    </table>\r\n    <br>\r\n</div>\r\n</html>";

            SmtpClient client = new()
            {
                // IP-����� SMTP-������� ��� ���������� ����������
                Host = "31.31.196.98",
                // ���� ��� �������� ��������� ��� ����������
                Port = 25,
                // ���������� ����������� ���� SSL
                EnableSsl = false,
                // ������� ������ ��� �������������� �� SMTP-�������
                Credentials = new NetworkCredential("dmitryba@parkingcontrolcomplex.ru", "R8g6hxM8zFX-QJq")
            };

            MailMessage msg = new()
            {
                // ����� ����������� ������
                From = new MailAddress("dmitryba@parkingcontrolcomplex.ru"),
                // ���� ������
                Subject = "��� ������������� ��� �����������",
                // ����� ������, � ����� ������ template ��� html ��������
                Body = template,
                // ����������� ������, ��� html ��������
                IsBodyHtml = true
            };

            // ���������� ���������� ������
            msg.To.Add(new MailAddress(User.Email));

            // �������� ������ � ������� SmtpClient
            client.Send(msg);

            // ���������� ���������������� ���� �������������
            HttpContext.Session.SetString("ConfirmationCode", ConfirmationCode);
            return Page();
        }

        public IActionResult OnPostRegister()
        {
            User.Permissions = "Default";
            User.LastLoginTime = DateTime.Now;
            User.AccessExpiresTime = new DateTime(2024, 12, 31);

            if (Request.Form["text"] != HttpContext.Session.GetString("ConfirmationCode"))
                return Page();

            _context.Users.Add(User);
            _context.SaveChanges();

            var tokenController = new UsersAccountController();
            JsonResult result = (JsonResult)tokenController.Token(User.Login, User.Password, User.Email, User.Permissions, User.Permissions, DateTime.UtcNow);
            string accessToken = result.Value.GetType().GetProperty("access_token").GetValue(result.Value, null).ToString();
            string username = result.Value.GetType().GetProperty("username").GetValue(result.Value, null).ToString();

            // �������� ������� CookieOptions ��� ��������� ���������� ����
            var cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(30), // ������������� ���� �������� ���� �� 30 ����
                Secure = true, // ������������� ���� Secure ��� �������� ���� ������ �� HTTPS
                HttpOnly = true // ������������� ���� HttpOnly ��� ������ �� ������� ����� JavaScript
            };

            // ������������� ���������� ���� � JWT ������� � ������ ������������
            Response.Cookies.Append("jwtToken", accessToken, cookieOptions);
            Response.Cookies.Append("username", username, cookieOptions);

            TempData["RegisterStatusMessage"] = "����������� ������ �������!";

            ShowSuccessMessage = true;

            return Page();
            //return RedirectPermanent("/Users/Index");
        }
    }
}
