using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models.ProductModule
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; } 
        public string PictureUrl { get; set; } = null!;

        public ProductBrand ProductBrand { get; set; } 
        public int BrandId { get; set; } //FK
        public ProductType ProductType { get; set; }
        public int TypeId { get; set; } //FK
    }
}
