using BeHeard.Application;
using BeHeard.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BeHeard.Services;
using BeHeard.Application.Models;

namespace BeHeard.Controllers
{
    public class AdminController : Controller
    {
        private readonly BeHeardContextManager _beHeardContextManager;
        public AdminController(BeHeardContext context)
        {
            _beHeardContextManager = new BeHeardContextManager(context);
        }
        [HttpGet]
        public IActionResult Index()
        {
            int count = _beHeardContextManager.UserRepository.Count();
            return View(count);
        }
        public ActionResult Delete(Guid Id)
        {
            var user = _beHeardContextManager.UserRepository.Get(Id);
            _beHeardContextManager.UserRepository.Remove(user);
            _beHeardContextManager.SaveChanges();

            string redirectLocation = "Index";

            return Redirect(redirectLocation);
        }


        public ActionResult Show(Guid Id)
        {
            var user = _beHeardContextManager.UserRepository.Get(Id);
            var userProfile = _beHeardContextManager.UserProfileRepository.GetUserProfileByUser(user);
            var model = new ProfileViewModel { UserProfile = userProfile };

            return PartialView("_ModalView", model);
        }
        public ActionResult Edit(Guid Id)
        {
            var load = _beHeardContextManager.UserRepository.Get(Id);
            var user = _beHeardContextManager.UserRepository.GetUserByUsername(load.Username);
            var model = new EditViewModel
            {
                Address = user.Address,
                Settings = user.Settings,
                Gender = Enum.GetName(typeof(Gender), user.Gender),
            };
            return PartialView("_UserEditView", model);
        }
        public ActionResult UserTable(string searchField)
        {
            var model = _beHeardContextManager.UserRepository.GetAll();


            if (!String.IsNullOrEmpty(searchField))
            {
                model = model.Where(u => u.LastName.Contains(searchField)
                               || u.FirstName.Contains(searchField) || u.Username.Contains(searchField));
                model = model.ToList();
            }

            string redirectLocation = "_UserTableView";

            return PartialView(redirectLocation, model);
        }
        [HttpPost]
        public ActionResult SaveEdit(EditViewModel model)
        {
            var user = _beHeardContextManager.UserRepository.GetUserByUsername(model.Settings.User.Username);
            
            //  Set User Changes.
            user.FirstName = model.Settings.User.FirstName;
            user.LastName = model.Settings.User.LastName;
            user.PhoneNumber = model.Settings.User.PhoneNumber;
            user.Email = model.Settings.User.Email;
            user.Gender = model.Settings.User.Gender;
            user.Password = model.Settings.User.Password;
            user.Username = model.Settings.User.Username;

            //  Set Address Changes.
            user.Address.Street = model.Settings.User.Address.Street;
            user.Address.City = model.Settings.User.Address.City;
            user.Address.PostalCode = model.Settings.User.Address.PostalCode;
            user.Address.Country = model.Settings.User.Address.Country;
            user.Address.State = model.Settings.User.Address.State;

            //user.Address = model.Address;
            //user.Profile = _beHeardContextManager.UserProfileRepository.Get(user.Id);
            _beHeardContextManager.UserRepository.Update(user);
            _beHeardContextManager.SaveChanges();
            //var model = _beHeardContextManager.UserRepository.GetAll();

            string redirectLocation = "Index?saveUpdate=" + user.Id.ToString();

            return Redirect(redirectLocation);
        }

    }
}
