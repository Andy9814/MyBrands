using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyBrands.Models;
using MyBrands.Utils;

namespace MyBrands.Controllers
{
    public class BrandController : Controller
    {
        AppDbContext _db;
        public BrandController(AppDbContext ctx)
        {
            _db = ctx;

        }
        public IActionResult Index(BrandViewModel vm)
        {
            // only build the catalogue once
            if (HttpContext.Session.Get<List<Brands>>("brandSession") == null)
            {
                // no session information so let's go to the database
                try
                {
                    BrandModel brandModel = new BrandModel(_db);
                    // now load the categories
                    List<Brands> brandList= brandModel.GetAll();
                    HttpContext.Session.Set<List<Brands>>("brandSession", brandList);
                    vm.SetBrands(brandList);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Catalogue Problem - " + ex.Message;
                }
            }
            else
            {
                // no need to go back to the database as information is already in the session
                vm.SetBrands(HttpContext.Session.Get<List<Brands>>("brandSession"));
                ProductModel itemModel = new ProductModel(_db);
                vm.products = itemModel.GetByBrand(vm.Id);
            }
            return View(vm);
        }






        // to get the detial of product 
        public IActionResult SelectBrand(BrandViewModel brandVm)
        {

            BrandModel brandModel = new BrandModel(_db);
            ProductModel prodModel = new ProductModel(_db);
            List<Product> productsListByBrand = prodModel.GetByBrand(brandVm.BrandId);
            List<ProductViewModel> prodVMList = new List<ProductViewModel>();
            if (productsListByBrand.Count > 0)
            {

                // give me the details of each product by foreach loop 
                foreach (Product prod in productsListByBrand)
                {
                    ProductViewModel prodVM = new ProductViewModel();

                    prodVM.QtyOnHand = 0;

                    prodVM.Description = prod.Description;
                    prodVM.CostPrice = prod.CostPrice;
                    prodVM.BrandId = prod.BrandId;

                    prodVM.BrandName = brandModel.GetName(prod.BrandId);

                    prodVM.GraphicName = prod.GraphicName;
                    prodVM.MSRP = prod.MSRP;
                    prodVM.ProductName = prod.ProductName;

                    
                    prodVMList.Add(prodVM);
                }


                ProductViewModel[] myProdArray = prodVMList.ToArray();
                HttpContext.Session.Set<ProductViewModel[]>("productSession", myProdArray);

            }
                brandVm.SetBrands(HttpContext.Session.Get<List<Brands>>("brandSession"));
                return View("Index", brandVm);
                

          

        }

        [HttpPost]
        public ActionResult SelectItem(BrandViewModel brandVm)
        {
            // cs we  change int to string 
            Dictionary<string, object> tray;
            if (HttpContext.Session.Get<Dictionary<string, Object>>("tray") == null)
            {
                tray = new Dictionary<string, object>();
            }
            else
            {
                tray = HttpContext.Session.Get<Dictionary<string, object>>("tray");
            }
            ProductViewModel[] myProdArray  = HttpContext.Session.Get<ProductViewModel[]>("productSession");
            String retMsg = "";
            foreach (ProductViewModel item in myProdArray)
            {
                if (item.Id == brandVm.Id)// change that to .productId
                {
                    if (brandVm.QtyOnHand > 0) // update only selected item
                    {
                        item.QtyOnHand = brandVm.QtyOnHand;
                        retMsg = brandVm.QtyOnHand + " - item(s) Added!";
                        tray[item.Id.ToString()] = item;
                    }
                    else
                    {
                        item.QtyOnHand = 0;
                        tray.Remove(item.Id.ToString());
                        retMsg = "item(s) Removed!";
                    }
                    brandVm.BrandId = item.BrandId;
                    break;
                }
            }
            ViewBag.AddMessage = retMsg;
            HttpContext.Session.Set<Dictionary<string, Object>>("tray", tray);
            brandVm.SetBrands(HttpContext.Session.Get<List<Brands>>("brandSession"));
            return View("Index", brandVm);
        }

    }
}