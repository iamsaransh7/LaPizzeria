using LaPizzeria.Models;
using LaPizzeria.Services.Interfaces;
using LaPizzeria.UI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace LaPizzeria.UI.Controllers
{
    public class AccountController : BaseController
    {
        IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            if (CurrentUser != null && CurrentUser.Roles.Contains("Admin")) 
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            else if(CurrentUser != null && CurrentUser.Roles.Contains("User")) 
            {
                return RedirectToAction("Index", "Home", new { area = "User" });
            }
            return View();
        }

        // Generate JWT Token
        private void GenerateTicket(UserModel user) 
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, string.Join(",", user.Roles)),
                new Claim(ClaimTypes.UserData, JsonSerializer.Serialize(user))
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
            };

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, properties);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model, string? returnUrl)
        {
            if (ModelState.IsValid) 
            {
                UserModel user = _authService.ValidateUser(model.Email, model.Password);

                if (user != null)
                {
                    GenerateTicket(user);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    if (user.Roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home", new { area = "User" });
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Invalid Email or Password");
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
