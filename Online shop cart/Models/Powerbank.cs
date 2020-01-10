using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_shop_cart.Models
{
    public class Powerbank
    {
        public Powerbank()
        {
            this.Purchases = new HashSet<Purchase>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public Nullable<decimal> Price { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }

    }
}