using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBrands.Utils;
using MyBrands.Models;

namespace MyBrands.Controllers
{
    public class BranchController : Controller
    {
        // Get the database from context
        AppDbContext _db;
        
        // constructor to get the db filled
        public BranchController(AppDbContext context)
        {
            _db = context;
        }

        //Index method
        public ActionResult Index()
        {
            if (HttpContext.Session.GetString(SessionVars.Message) != null)
            {
                ViewBag.Message = HttpContext.Session.GetString(SessionVars.Message);
            }
            return View();
        }

        // Get Branch for Branch.js to get the data to axios
        [Route("[action]/{lat:double}/{lng:double}")]
        public IActionResult GetBranches(float lat, float lng)
        {
            BranchModel model = new BranchModel(_db);
            return Ok(model.GetThreeClosestBranches(lat, lng));
        }
    }
}