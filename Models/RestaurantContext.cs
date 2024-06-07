using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace QrCodeMenuTest.Models
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext() : base("name=RestaurantContext")
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}