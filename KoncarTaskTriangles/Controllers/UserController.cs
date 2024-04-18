using KoncarTaskTriangles.Models;
using KoncarTaskTriangles.Models.ViewModels;
using KoncarTaskTriangles.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using KoncarTaskTriangles.Helpers;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using KoncarTaskTriangles.Domain;

namespace KoncarTaskTriangles.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public  UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm] RegisterViewModel registerViewModel)
        {

            if (ModelState.IsValid)
            {
                if (_userService.GetUserByFilter(i => i.Username == registerViewModel.Username) != null)
                {
                    ModelState.AddModelError("", "Username taken.");
                }
                UserModel user = new UserModel
                {
                    Username = registerViewModel.Username,
                    Password = new Helpers.PasswordEncode().Encoder(registerViewModel.Password),
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                };
                var id = _userService.InsertUser(user);
                
                return RedirectToAction("Index", "Triangle", new { logedUserId = id });
            }
            else
            {
                ModelState.AddModelError("", "Register form data is not valid.");
            }
            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Login([FromForm]LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                LoginResponse logedUser = await _userService.GetLoginResponse(i => i.Username == loginViewModel.Username && i.Password == new PasswordEncode().Encoder(loginViewModel.Password));
                if (logedUser == null)
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
                else
                {
                    return RedirectToAction("Index", "Triangle", new { logedUserId = logedUser.Id });
                }

            }
            else
            {
                ModelState.AddModelError("", "Login form data is not valid.");
            }
            return View(loginViewModel);
        }
    }
}
