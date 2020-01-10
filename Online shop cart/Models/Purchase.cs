using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_shop_cart.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public virtual Order Order { get; set; }
        public virtual Powerbank Powerbank { get; set; }
    }

}