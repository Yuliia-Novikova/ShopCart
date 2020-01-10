using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_shop_cart.Models
{
    public class Discount
    {
        public Discount()
        {
            this.Orders = new HashSet<Order>();
        }
        public int Id { get; set; }
        public Nullable<int> PersonalDiscount { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}