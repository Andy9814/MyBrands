using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBrands.Models;

namespace MyBrands.Controllers
{
    public class ProductController : Controller
    {
        AppDbContext _db;

        public ProductController(AppDbContext ctx)
        {
            _db = ctx;
        }
        public IActionResult Index(BrandViewModel BrandVm)
        {
            ProductModel prodModel = new ProductModel(_db);
            ProductViewModel ProdviewModel = new ProductViewModel();
           
            ProdviewModel.BrandName = BrandVm.BrandName;
            ProdviewModel.Products = prodModel.GetByBrand(BrandVm.Id);
            return View(ProdviewModel);

        }
    }
}