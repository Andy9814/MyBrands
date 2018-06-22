using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBrands.Models
{
    public class BrandModel
    {

        // get the database from db context
        public AppDbContext _db;
        public BrandModel(AppDbContext ctx)
        {
            _db= ctx;
        }

        // List of all brands
        public List<Brands> GetAll()
        {
            return _db.Brands.ToList<Brands>();
        }

        // 
        public string GetName(int id)
        {
            Brands brand = _db.Brands.First(c => c.Id == id);
            return brand.Name;
        }
    }
}
