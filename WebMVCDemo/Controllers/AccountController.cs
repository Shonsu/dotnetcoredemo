using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebMVCDemo.Services;

namespace WebMVCDemo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public AccountController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(string firstname, string password, string ReturnUrl)
        {
            System.Console.WriteLine("firstname: {0}", firstname);
            System.Console.WriteLine("pass: {0}", password);
            var employee = _employeeService.GetEmployee(firstname);
            System.Console.WriteLine("Employee:{0}", employee);
            if ((employee == null) || !BCrypt.Net.BCrypt.Verify(password, employee.Hash))
            {
                return RedirectToAction("Login", new { ReturnUrl });
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Name, employee.FirstName),
                new Claim("IsAdmin", employee.isAdmin.ToString())
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            if (string.IsNullOrEmpty(ReturnUrl))
            {
                return RedirectToAction("Index", "Home1");
            }
            else
            {
                return LocalRedirect(ReturnUrl);
            }
        }

        [HttpGet]
        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme
            );
            return RedirectToAction("Index", "Home1");
        }
    }
}