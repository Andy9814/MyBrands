using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBrands.Models
{
    public class BrandViewModel
    {
        public BrandViewModel() { }

        public List<Brands> _Brands;
        public IEnumerable<Product> products { get; set; }

        public string BrandName { get; set; }
        public int QtyOnHand { get; set; }
        public int Id { get; set; }
        public int BrandId { get; set; }
        public List<Brands> BrandList { get; set; }
        public IEnumerable<SelectListItem> GetBrands()
        {
            return _Brands.Select(brand => new SelectListItem
            {
                Text = brand.Name,
                Value = brand.Id.ToString()
            });
        }


      
        public void SetBrands (List<Brands> brand)
        {
            _Brands = brand;
        }

    }
}
