using MagicVilla_Utility;
using MagicVilla_Web.Models;
using MagicVilla_Web.Models.Dto;
using MagicVilla_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace MagicVilla_Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO obj = new ();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Login(LoginRequestDTO obj)
        {
            APIResponse response= await _authService.LoginAsync<APIResponse>(obj);
            if(response!=null && response.IsSuccess)
            {
                LoginResponseDTO model=JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));

              //  var identity = new ClaimsIdentity(cookieAuthen)
                HttpContext.Session.SetString(SD.SessionToken, model.Token);
                return RedirectToAction("index","home");

            }
            else
            {
                ModelState.AddModelError("CustomError",response.ErrorMessages.FirstOrDefault());
                return View(obj);
            }
        }


        [HttpGet]
        public IActionResult Register()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterationRequestDTO obj)
        {
            APIResponse result= await _authService.RegisterAsync<APIResponse>(obj);
            if(result!=null && result.IsSuccess)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("index","home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        } public IActionResult LogSucc()
        {
            return View();
        }
    }
}
