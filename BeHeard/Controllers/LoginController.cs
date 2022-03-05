using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Models;
using Microsoft.AspNetCore.Http;
using BeHeard.Core;
using System.Security.Claims;
using BeHeard.Application;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using BeHeard.Application.Models;
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
            // This is initial login view
            // no action aside from view
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user, bool isFirstLogin = false)
        {
            // NOTE: Add redirects for failed attempts and nonexistent accounts
            TempData["Error"] = "Sorry, that 'Username' and 'Password' combination do not match any record.";

            if (!_userService.IsValidUserCredentials(user.Username, user.Password))
                return View("~/Views/Login/Index.cshtml");

            var role = _userService.GetUserRole(user.Username);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            //var thisUser = _beHeardContextManager.UserRepository.GetUserByUsername(user.Username);
            //var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(thisUser);

            //HttpContext.Session.SetString("FirstName", userProfile.User.FirstName);
            //HttpContext.Session.SetString("LastName", userProfile.User.LastName);
            //HttpContext.Session.SetString("Email", userProfile.User.Email);
            //HttpContext.Session.SetString("Username", userProfile.User.Username);


            //SessionModel thisSession = new SessionModel()
            //{
            //    FirstName = HttpContext.Session.GetString("FirstName"),
            //    LastName = HttpContext.Session.GetString("LastName"),
            //    FullName = HttpContext.Session.GetString("FirstName") + HttpContext.Session.GetString("LastName"),
            //    Email = HttpContext.Session.GetString("Email"),
            //    Username = HttpContext.Session.GetString("Username"),
            //    Phone = " ",
            //    City = " ",
            //    Street = " ",
            //    ZipCode = " ",
            //    Age = " ",
            //    Gender = 1
            //};
   
            //HttpContext.Session.SetObjectAsJson("thisSession", thisSession);

            var _user = _beHeardContextManager.UserRepository.GetUserByUsername(user.Username);
            var service = new SessionService(HttpContext);
            service.Create(_user).Save();

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
            return View();
        }
        public IActionResult TermsConditions()
        {        
           return View();
        }

        [HttpPost]
        public IActionResult RegisterAccount(User user, int termCheck)
        {


            //var home = $"{this.Request.Scheme}://{this.Request.Host}";
            //if (_userService.IsAnExistingUser(user.Username, user.Email)) {
            //    TempData["Error"] = "Sorry, that 'Username' or 'Email' is already used.";
            //    return View("Registration");
            //}
            //if (termCheck == 0)
            //{
            //    Response.Redirect(home);
            //}

            //var newAccount = new User
            //{
            //    Username = user.Username,
            //    Password = user.Password,

            //    FirstName = user.FirstName,
            //    LastName = user.LastName,RedirectToAction("Login", user);
            //    Email = user.Email,
            //    Gender = user.Gender,
            //};
            //var userSettings = new Settings
            //{
            //    User = newAccount
            //};

            //var userProfile = new UserProfile
            //{
            //    User =newAccount,
            //    Settings = userSettings,
            //    ActivityResults = new List<ActivityResult>()
                
            //};

            //_beHeardContextManager.UserProfileRepository.Add(userProfile);
            //_beHeardContextManager.SaveChanges();

            var subscription = new Subscription
            {
                Type = SubscriptionType.Paid,
            };

            var preferences = new Preferences
            {
                ColorBlindMode = false,
                DarkMode = true,
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

            try
            {
                _beHeardContextManager.UserRepository.Add(user);
                _beHeardContextManager.SaveChanges();
                // return Redirect("~/?NewUser=1");
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
