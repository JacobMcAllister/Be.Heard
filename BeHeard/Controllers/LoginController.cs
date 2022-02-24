﻿using Microsoft.AspNetCore.Mvc;
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
using BeHeard.Application.Models;

namespace BeHeard.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAuthentication _authentication;
        private readonly BeHeardContext _context;
        private readonly BeHeardContextManager _beHeardContextManager;

        public LoginController(IUserService userService, IAuthentication authentication, BeHeardContext context)
        {
            _context = context;
            _beHeardContextManager = new BeHeardContextManager(_context);

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
        public IActionResult Login(User user)
        {
            // NOTE: Add redirects for failed attempts and nonexistent accounts

            if (!_userService.IsValidUserCredentials(user.Username, user.Password))
                return Unauthorized();

            var role = _userService.GetUserRole(user.Username);
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var thisUser = _beHeardContextManager.UserRepository.GetUserByUsername(user.Username);
            var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(thisUser);

            HttpContext.Session.SetString("FirstName", userProfile.User.FirstName);
            HttpContext.Session.SetString("LastName", userProfile.User.LastName);
            HttpContext.Session.SetString("Email", userProfile.User.Email);
            HttpContext.Session.SetString("Username", userProfile.User.Username);

            SessionModel thisSession = new SessionModel()
            {
                FirstName = HttpContext.Session.GetString("FirstName"),
                LastName = HttpContext.Session.GetString("LastName"),
                FullName = HttpContext.Session.GetString("FirstName") + HttpContext.Session.GetString("LastName"),
                Email = HttpContext.Session.GetString("Email"),
                Username = HttpContext.Session.GetString("Username"),
                Phone = " ",
                City = " ",
                Street = " ",
                ZipCode = " ",
                Age = " ",
                Gender = 1
            };

   
            HttpContext.Session.SetObjectAsJson("thisSession", thisSession);



            // NOTE: implement refresh tokens
            var authenticationResult = _authentication.GenerateTokens(user.Username, claims, DateTime.Now);

            Response.Cookies.Append("token", authenticationResult.AccessToken);
            var home = $"{this.Request.Scheme}://{this.Request.Host}";
            Response.Redirect(home);

            return new EmptyResult();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterAccount(User user)
        {

            var home = $"{this.Request.Scheme}://{this.Request.Host}";
            if (_userService.IsAnExistingUser(user.Username, user.Email)) {
                return new EmptyResult();
            }

            var newAccount = new User
            {
                Username = user.Username,
                Password = user.Password,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
            };
            var userSettings = new Settings
            {
                User = newAccount
            };

            var userProfile = new UserProfile
            {
                User =newAccount,
                Settings = userSettings,
                ActivityResults = new List<ActivityResult>()
                
            };

            _beHeardContextManager.UserProfileRepository.Add(userProfile);
            _beHeardContextManager.SaveChanges();

            Response.Redirect(home);
                return new EmptyResult();
            }
        public IActionResult ForgotPassword()
        {
            return View();
        }
    }
}
