﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBrands.Models
{
    public class ProductViewModel
    {
        public string BrandName { get; set; }
        public int BrandId { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public int Id { get; set; }
        
        public string Description { get; set;}
        public string GraphicName { get; set; }
        public int QtyOnHand { get; set; }
        public decimal MSRP { get; set; }
        public string ProductName { get; set; }


        public string PRODUCT { get; set; }
        public string JsonData { get; set; }
        public decimal CostPrice { get; set; }

    }
}
