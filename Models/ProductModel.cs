using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBrands.Models
{
    public class ProductModel
    {
        AppDbContext _db;
        public ProductModel( AppDbContext ctx)
        {
            _db = ctx;
        }


        public List<Product> GetAll()
        {
            return _db.Products.ToList<Product>();
        }

        public List<Product> GetByBrand(int id)
        {
            return _db.Products.Where(prod => prod.Brand.Id == id).ToList();
        }


    }
}
