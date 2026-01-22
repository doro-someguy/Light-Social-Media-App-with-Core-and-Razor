using System.Diagnostics;
using System.Linq;
using App.Models;
using Microsoft.AspNetCore.Mvc;

namespace App.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<TopicsController> _logger;
        private readonly ForumDB _db;

        public AccountController(ILogger<TopicsController> logger, ForumDB db)
        {
            _logger = logger;
            _db = db;
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Login()
        {
            if (Request.Method == "GET")
            {
                return View();
            }

            if (Request.Method == "POST")
            {
                var form = await Request.ReadFormAsync();
                string username = form["username"];
                string password = form["password"];

                if (username == "") {
                    ViewData["message"] = "The username field is empty!";
                    return View();
                }
                if (password == "") {
                    ViewData["message"] = "The password field is empty!";
                    return View();
                }

                var searchedAccount = _db.accounts.FirstOrDefault(a => a.username == username && a.password == password);
                if (searchedAccount is null)
                {
                    ViewData["message"] = "Account does not exist or wrong password!";
                    return View();
                } 
                else
                {
                    ViewData["message"] = "You have been connected successfully!";
                    HttpContext.Session.SetString("IsLogged", "yes");
                    HttpContext.Session.SetString("AccountUser", username);
                    return View();
                }

            }
            return View();
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Signup()
        {
            if (Request.Method == "GET")
            {
                return View();
            }

            if (Request.Method == "POST")
            {
                var form = await Request.ReadFormAsync();
                string username = form["username"];
                string email = form["email"];
                string birthDate = form["birthDate"];
                string password = form["password"];
                string confirmPassword = form["confirmPassword"];
                var searchedAccount = _db.accounts.FirstOrDefault(a => a.username == username && a.password == password);
                if (searchedAccount is not null)
                {
                    if(username == searchedAccount.username)
                    {
                        ViewData["deniedMessage"] = "Username is already registered!";
                        return View();
                    }
                }
                if (username == "") {
                    ViewData["deniedMessage"] = "You didn't fill in the username field!";
                    return View();
                }
                if (email == "") {
                    ViewData["deniedMessage"] = "You didn't fill in the email field!";
                    return View();
                }
                if (password == "") {
                    ViewData["deniedMessage"] = "You didn't fill in the password field!";
                    return View();
                }
                if (confirmPassword == "") {
                    ViewData["deniedMessage"] = "You didn't fill in the confirm password field!";
                    return View();
                }
                else
                {
                    var account = new Account
                    {
                        username = username,
                        email = email,
                        password = password
                    };
                    _db.accounts.Add(account);
                    _db.SaveChanges();
                    ViewData["okMessage"] = "The new account has been created successfully!";
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            string accountUser = HttpContext.Session.GetString("AccountUser");
            if (accountUser is null)
            {
                ViewData["message"] = "You can't log out if you're not already logged in!";
                return View();
            } else
            {
                HttpContext.Session.Remove("AccountUser");
                ViewData["message"] = "You have been logged out!";
                return View();
            }
            return View();
        }
    }
}