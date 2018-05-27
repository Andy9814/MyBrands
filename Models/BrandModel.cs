using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBrands.Models
{
    public class BrandModel
    {
        public AppDbContext _db;
        public BrandModel(AppDbContext ctx)
        {
            _db= ctx;
        }
        public List<Brands> GetAll()
        {
            return _db.Brands.ToList<Brands>();
        }
        public string GetName(int id)
        {
            Brands brand = _db.Brands.First(c => c.Id == id);
            return brand.Name;
        }
    }
}
