﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using MyBrands.Utils;
using MyBrands.Models;
using System.Collections.Generic;

namespace MyBrands.Controllers
{
    public class TrayController : Controller
    {
        AppDbContext _db;
        // TrayController
        public TrayController(AppDbContext context)
        {
            _db = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        // ClearTray
        public ActionResult ClearTray() // clear out current tray
        {
            HttpContext.Session.Remove(SessionVars.Tray);
            HttpContext.Session.Set<String>(SessionVars.Message, "Cart Cleared");
            return Redirect("/Home");
        }


        // AddOrder 
        public ActionResult AddOrder()
        {
            OrderModel model = new OrderModel(_db);
            int retVal = -1;
            string retMessage = "";
           
            try
            {
                Dictionary<string, object> trayItems = HttpContext.Session.Get<Dictionary<string, object>>(SessionVars.Tray);
                retVal = model.AddOrder(trayItems, HttpContext.Session.GetString(SessionVars.User));
                if (retVal > 0) // Tray Added
                {
                    retMessage = "Cart " + retVal + " Created!";
                }
                else // problem
                {
                    retMessage = "Cart not added, try again later";
                }
            }
            catch (Exception ex) // big problem
            {
                retMessage = "Cart was not created, try again later! - " + ex.Message;
            }
            HttpContext.Session.Remove(SessionVars.Tray); // clear out current tray once persisted
            HttpContext.Session.SetString(SessionVars.Message, retMessage);
            return Redirect("/Home");
        }


        // List
        public IActionResult List()
        {
            // they can't list Trays if they're not logged on
            if (HttpContext.Session.GetString(SessionVars.User) == null)
            {
                return Redirect("/Login");
            }
            return View("List");
        }

        // Get order for the list
        [Route("[action]")]
        public IActionResult GetOrders()
        {
            OrderModel model = new OrderModel(_db);
            return Ok(model.GetByUserId(HttpContext.Session.GetString(SessionVars.User)));
        }


        // get the orderDetails
  
        [Route("[action]/{tid:int}")]
        public IActionResult GetOrderDetails(int tid)
        {
            OrderModel model = new OrderModel(_db);
            return Ok(model.GetOrderDetails(tid, HttpContext.Session.GetString(SessionVars.User)));
        }
    }
}


