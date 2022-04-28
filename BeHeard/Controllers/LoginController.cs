using Microsoft.AspNetCore.Mvc;
using System;
using BeHeard.Core;
using System.Security.Claims;
using System.Security.Cryptography;
using BeHeard.Application;
using BeHeard.Application.Helpers;
using BeHeard.Application.Models;
using BeHeard.Models;
using BeHeard.Services;

namespace BeHeard.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthentication _authentication;
        private readonly BeHeardContextManager _beHeardContextManager;

        public LoginController(IUserService userService, IAuthentication authentication, BeHeardContext context)
        {
            _beHeardContextManager = new BeHeardContextManager(context);

            _userService = userService;
            _authentication = authentication;
        }

        public IActionResult Index()
        {
            TempData["Error"] = "";
            // This is initial login view
            // no action aside from view
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user, bool isFirstLogin = false)
        {
            //TempData["Error"] = "Sorry, that 'Username' and 'Password' combination do not match any record.";
            // NOTE: Add redirects for failed attempts and nonexistent accounts
            var password = isFirstLogin ? user.Password : PasswordService.hashPassword(user.Password);
            if (!_userService.IsValidUserCredentials(user.Username, password))
                return View("~/Views/Login/Index.cshtml");

            var role = _userService.GetUserRole(user.Username);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var authenticatedUser = _beHeardContextManager.UserRepository.GetUserByUsername(user.Username);
            var service = new SessionService(HttpContext);

            service.Create(authenticatedUser).Save();


            // NOTE: implement refresh tokens
            var authenticationResult = _authentication.GenerateTokens(user.Username, claims, DateTime.Now);

            Response.Cookies.Append("token", authenticationResult.AccessToken);
            var home = isFirstLogin ? $"{this.Request.Scheme}://{this.Request.Host}/?newuser=1" :
                                      $"{this.Request.Scheme}://{this.Request.Host}";
            Response.Redirect(home);

            return new EmptyResult();
        }

        public IActionResult Registration()
        {
            TempData["Error"] = "";
            var model = new RegistrationViewModel
            {
                States = Localization.Los,
                AlexImageUrl = "/images/image1.png"
            };
            return View(model);
        }
        public IActionResult TermsConditions()
        {        
           return View();
        }

        [HttpPost]
        public IActionResult RegisterAccount(User user, int termCheck)
        {
            string pass = "";
            if (user.Password != null)
            {
                pass = PasswordService.hashPassword(user.Password);
                user.Password = pass;
            }

            // var random = new Random();
            user.icon = $"face{RandomNumberGenerator.GetInt32(1,4)}.png";
            
            var subscription = new Subscription
            {
                Type = SubscriptionType.Free,
            };

            var preferences = new Preferences
            {
                ColorBlindMode = false,
                DarkMode = false,
                TextToSpeech = false,
            };


            var settings = new Settings
            {
                MasterVolume = 100,
                Preferences = preferences,
                Subscription = subscription,
                User = user,
            };
            var profile = new UserProfile
            {
                Settings = settings,
                User = user,
            };

            user.Settings = settings;
            user.Profile = profile;
            user.Date = DateTime.Now;

            //if (_userService.IsAnExistingUser(user.Username))
            //{
            //    TempData["Error"] = "Sorry, that 'Username' and 'Password' combination do not match any record.";
            //    return View("~/Views/Login/Registration.cshtml");
            //}
            try
            {
                _beHeardContextManager.UserRepository.Add(user);
                _beHeardContextManager.SaveChanges();
                return Login(user, true);
            }
            catch
            {
                return BadRequest();
            }

            //Response.Redirect(home);
            //    return new EmptyResult();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
