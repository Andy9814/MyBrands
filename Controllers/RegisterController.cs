﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBrands.Models;
using MyBrands.Utils;


namespace MyBrands.Controllers
{
    public class RegisterController : Controller
    {
        UserManager<ApplicationUser> _usrMgr;
        SignInManager<ApplicationUser> _signInMgr;
        public RegisterController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
        {
            _usrMgr = userManager;
            _signInMgr = signInManager;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        //
        // POST:/Register/Register
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = model.Email,
                    Email = model.Email,
                    Age = model.Age,
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Address1 = model.Address1,
                    City = model.City,
                    Mailcode = model.Mailcode,
                    Country = model.Country,
                    CreditcardType = model.CreditcardType,
                    Region = model.Region


                };
                var result = await _usrMgr.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInMgr.SignInAsync(user, isPersistent: false);
                    HttpContext.Session.SetString(SessionVars.LoginStatus, "logged on as " + model.Email);
                    HttpContext.Session.SetString(SessionVars.Message, "Registered, logged on as " + model.Email);
                }
                else
                {
                    ViewBag.message = "registration failed - " + result.Errors.First().Description;
                    return View("Index");
                }
            }
            return Redirect("/Home");
        }
    }
}