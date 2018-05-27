using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyBrands.Models
{
    
        public class Product
        {
            [Key]

            [StringLength(50)]
            public string Id { get; set; }

            [ForeignKey("BrandId")]
            [Required]
            public Brands Brand { get; set; }
            [Required]
            public int BrandId { get; set; }

            [Required]
            [Column(TypeName = "timestamp")]
            [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
            [MaxLength(8)]
            public byte[] Time { get; set; }

            [Required]
            [StringLength(50)]
            public string ProductName { get; set; }

            [Required]
            [StringLength(50)]
            public string GraphicName { get; set; }

            [Required]
            [Column(TypeName = "money")]
            public decimal CostPrice { get; set; }

            [Required]
            [Column(TypeName = "money")]
            public decimal MSRP { get; set; }

            [Required]
            public int QtyOnHand { get; set; }

            [Required]

            public int QtyOnBackOrder { get; set; }

            [Required]
            [StringLength(2000)]
            public string Description { get; set; }




        }
    
}
