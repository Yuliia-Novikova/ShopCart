using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_shop_cart.Models
{
    public class Order
    {
        public Order()
        {
            this.Purchases = new HashSet<Purchase>();
        }
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public virtual ICollection<Purchase> Purchases { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public virtual Discount Discount { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}