using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Online_shop_cart.Models
{
    public class DbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            context.Powerbanks.Add(new Powerbank() { Name = "Xiaomi Redmi 10000 mAh White", Price = 389, Producer = "Xiaomi" });
            context.Powerbanks.Add(new Powerbank() { Name = "Xiaomi Mi Power Bank 2s 10000 mAh", Price = 499, Producer = "Xiaomi" });
            context.Powerbanks.Add(new Powerbank() { Name = "Gelius Pro Soft GP-PB5-G2 5000 mAh", Price = 149, Producer = "Gelius" });
            context.Powerbanks.Add(new Powerbank() { Name = "ERGO 10000 mAh Type-C Black", Price = 333, Producer = "Ergo" });
            context.Powerbanks.Add(new Powerbank() { Name = "Remax Jane Power Bank 10000 mAh Black", Price = 309, Producer = "Remax" });
            context.Powerbanks.Add(new Powerbank() { Name = "Promate Bolt-10 10000 mAh", Price = 299, Producer = "Promate" });
            context.Powerbanks.Add(new Powerbank() { Name = "Esperanza Photon 17400 mAh", Price = 339, Producer = "Esperanza" });
            context.Powerbanks.Add(new Powerbank() { Name = "Acme PB101 10000 mAh Black", Price = 339, Producer = "Acme" });
            context.Powerbanks.Add(new Powerbank() { Name = "Defender ExtraLife Fast 5000 mAh", Price = 425, Producer = "Defender" });

            context.Discounts.Add(new Discount() { PersonalDiscount = 5 });
            context.Discounts.Add(new Discount() { PersonalDiscount = 10 });
            context.Discounts.Add(new Discount() { PersonalDiscount = 15 });
            context.Discounts.Add(new Discount() { PersonalDiscount = 20 });
            context.Discounts.Add(new Discount() { PersonalDiscount = 25 });
            context.Discounts.Add(new Discount() { PersonalDiscount = 30 });

            base.Seed(context);
        }
    }
}