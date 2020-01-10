using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Online_shop_cart.Models;

namespace Online_shop_cart.Controllers
{
    public class HomeController : Controller
    {
        static ApplicationDbContext ApplicationDbContext = new ApplicationDbContext();
        List<Purchase> ShopCart = ApplicationDbContext.ShopCart;
        static IQueryable<Discount> PersonalDiscounts = from temp in ApplicationDbContext.Discounts select temp;
        static List<Discount> DiscountList = PersonalDiscounts.ToList();
        Discount pd;
        int totalQuantity;

        public ActionResult Index()
        {
            IEnumerable<Powerbank> powerbanks = ApplicationDbContext.Powerbanks;
            ViewBag.Powerbanks = powerbanks;

            if (ShopCart.Count != 0)
            {
                foreach (var sc in ShopCart)
                    totalQuantity += sc.Quantity;
            }
            else
                totalQuantity = 0;
            ViewBag.TotalQuantity = totalQuantity;

            return View();
        }

        [HttpGet]
        public ActionResult Buy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Buy(Order order)
        {
            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext()
                    .GetUserManager<ApplicationUserManager>()
                    .FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            order.ApplicationUser = ApplicationDbContext.Users.Find(user.Id); ;
            decimal? total = 0;
            foreach (var sc in ShopCart)
            {
                order.Purchases.Add(sc);
                total += sc.Amount;
            }
            order.DateTime = DateTime.Now;
            order.Discount = countDiscount(total);
            order.TotalAmount = total * (100 - order.Discount.PersonalDiscount) / 100;
            ApplicationDbContext.Orders.Add(order); 
            ApplicationDbContext.SaveChanges();
            ViewData["FullName"] = order.ApplicationUser.FirstName + " " + order.ApplicationUser.LastName;

            return RedirectToAction("Confirmation");
        }

        [HttpGet]
        public ActionResult Cart()
        {
            if (User.Identity.IsAuthenticated)
            {
                IEnumerable<Purchase> purchases = ShopCart;
                ViewBag.Purchases = purchases;
                decimal? total = countTotalAmount();
                ViewData["TotalAmount"] = total;
                pd = countDiscount(total);
                ViewData["Discount"] = total * pd.PersonalDiscount / 100;
                ViewData["TotalAmountWithDiscount"] = total * (100 - pd.PersonalDiscount) / 100;

                return View();
            }
            else
                return RedirectToAction("../Account/Login");

        }

        [HttpGet]
        public ActionResult Confirmation()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddToOder(int id)
        {
            IEnumerable<Powerbank> powerbanks = ApplicationDbContext.Powerbanks;
            Powerbank powerbank = powerbanks.Single(el => el.Id == id);

            ViewBag.Powerbanks = powerbanks;

            Purchase purchase = new Purchase();
            purchase.Powerbank = powerbank;
            var sc = ShopCart.Find(p => p.Powerbank.Id == powerbank.Id);
            if (ShopCart.Count == 0 || sc == null)
            {
                purchase.Quantity = 1;
                purchase.Amount = powerbank.Price;
                ShopCart.Add(purchase);
            }
            else
            {
                sc.Quantity++;
                sc.Amount += powerbank.Price;
            }

            if (ShopCart.Count != 0)
            {
                foreach (var item in ShopCart)
                    totalQuantity += item.Quantity;
            }
            else
                totalQuantity = 0;
            ViewBag.TotalQuantity = totalQuantity;

            return View("/Views/Home/Index.cshtml");
        }

        [HttpGet]
        public ActionResult RemoveFromOder(int id)
        {
            Purchase purchase = ShopCart.Where(el => el.Powerbank.Id == id).First();
            if (purchase.Quantity == 1)
                ShopCart.Remove(purchase);
            else
            {
                purchase.Quantity -= 1;
                purchase.Amount = purchase.Quantity * purchase.Powerbank.Price;
            }

            ViewBag.Purchases = ShopCart;

            decimal? total = countTotalAmount();
            ViewData["TotalAmount"] = total;
            pd = countDiscount(total);
            ViewData["Discount"] = total * pd.PersonalDiscount / 100;
            ViewData["TotalAmountWithDiscount"] = total * (100 - pd.PersonalDiscount) / 100;

            foreach (var item in ShopCart)
                totalQuantity -= item.Quantity;
            ViewBag.TotalQuantity = totalQuantity;

            return View("/Views/Home/Cart.cshtml");
        }

        private Discount countDiscount(decimal? total)
        {
            Discount disc = null;
            for (int i = 1; i < 7; i++)
            {
                if (total < 500 * i && i != 6)
                {
                    disc = DiscountList.Find(d => d.Id == 0 + i);
                    return disc;
                }
                else
                    disc = DiscountList.Find(d => d.Id == 0 + (i - 1));
            }
            return disc;
        }

        private decimal? countTotalAmount()
        {
            decimal? total = 0;
            foreach (var sc in ShopCart)
                total += sc.Amount;
            return total;
        }
    }
}