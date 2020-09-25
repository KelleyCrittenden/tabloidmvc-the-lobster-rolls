using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Security.Claims;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;
using TabloidMVC.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserProfileRepository _userRepo;
        private readonly IUserTypeRepository _userTypeReop;


        public UserController(IUserProfileRepository userRepository, IUserTypeRepository userTypeRepository)
        {
            _userRepo = userRepository;
            _userTypeReop = userTypeRepository;
        }
        // GET: UserController
        public ActionResult Index()
        {
            List<UserProfile> userProfiles = _userRepo.GetAllUserProfiles();
            return View(userProfiles);
        }
        public ActionResult DeactivatedIndex()
        {
            List<UserProfile> userProfiles = _userRepo.GetAllDeactivatedUserProfiles();
            return View(userProfiles);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            var user = _userRepo.GetUserProfileById(id);
            if (user != null)
            {
                return View(user);
                
            }
            else
            {
                return NotFound();
            }
           
        }

        

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            UserProfile profile = _userRepo.GetUserProfileById(id);
            List<UserType> types = _userTypeReop.GetAllUserTypes();

            UserProfileViewModel vm = new UserProfileViewModel
            {
                Profile = profile,
                UserTypes = types
            };
            return View(vm);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfileViewModel vm)
        {
            try
            {

                if (vm.Profile.UserTypeId == 2)
                {


                    List<UserProfile> admins = _userRepo.GetAllAdminUserProfiles();
                    if (admins.Count <= 1)
                    {
                        ModelState.AddModelError("Profile.UserTypeId", "There can not be less than 1 Admin. Must give Admin rights to someone else before this action will work.");
                        UserProfile profile = _userRepo.GetUserProfileById(vm.Profile.Id);
                        List<UserType> types = _userTypeReop.GetAllUserTypes();

                        UserProfileViewModel upvm = new UserProfileViewModel
                        {
                            Profile = profile,
                            UserTypes = types
                        };
                        return View(upvm);
                    }
                    else
                    {
                        _userRepo.UpdateUserType(vm.Profile.Id, vm.Profile.UserTypeId);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    _userRepo.UpdateUserType(vm.Profile.Id, vm.Profile.UserTypeId);
                    return RedirectToAction("Index");
                }
            }
            catch
            {
               
                return View(vm);
            }
        }

        // GET: UserController/Delete/5
        public ActionResult Deactivate(int id)
        {
            UserProfile user = _userRepo.GetUserProfileById(id);
            if (user != null)
            {
                return View(user);

            }
            else
            {
                return NotFound();
            }
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Deactivate(UserProfile profile )
        {
            UserProfile userId = _userRepo.GetUserProfileById(profile.Id);
            try
            {
                if(userId.UserTypeId == 1)
                {
                    List<UserProfile> admins = _userRepo.GetAllAdminUserProfiles();
                    if (admins.Count <= 1)
                    {
                        ModelState.AddModelError("UserType", "There can not be less than 1 Admin. Must give Admin rights to someone else before this action will work.");
                        var user = _userRepo.GetUserProfileById(profile.Id);
                        return View(user);
                    }
                    else
                    {
                        _userRepo.DeactivateProfile(profile.Id);
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    _userRepo.DeactivateProfile(profile.Id);
                    return RedirectToAction("Index");

                }

               
            }
            catch
            {
                return View(profile);
            }
        }

        public ActionResult Reactivate(int id)
        {
            var user = _userRepo.GetUserProfileById(id);
            if (user != null)
            {
                return View(user);

            }
            else
            {
                return NotFound();
            }
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reactivate(UserProfile profile)
        {
            try
            {
                _userRepo.ReactivateProfile(profile.Id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(profile);
            }
        }
    }
}
