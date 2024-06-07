using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QrCodeMenuTest.Models
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}