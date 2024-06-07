using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QrCodeMenuTest.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}