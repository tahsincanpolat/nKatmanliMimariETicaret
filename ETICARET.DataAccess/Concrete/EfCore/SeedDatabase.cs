using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class SeedDatabase
    {
        public static void Seed()
        {
            var context = new DataContext();

            if(context.Database.GetPendingMigrations().Count() == 0)
            {
                if(context.Categories.Count() == 0)
                {
                    context.AddRange(Categories);
                }

                if(context.Products.Count() == 0)
                {
                    context.AddRange(Products);
                    context.AddRange(ProductCategories);
                }

                context.SaveChanges();
            }
        }

        private static Category[] Categories =
        {
            new Category(){ Name = "Telefon"},
            new Category(){ Name = "Bilgisayar"},
            new Category(){ Name = "Elektronik"},
            new Category(){ Name = "Ev Gereçleri"},
        };

        private static Product[] Products =
        {
            new Product(){ Name = "Samsung Note 6" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 7" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 8" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 9" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 10" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
             new Product(){ Name = "Samsung Note 11" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
              new Product(){ Name = "Samsung Note 12" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },

        };

        private static ProductCategory[] ProductCategories =
       {
            new ProductCategory(){ Product = Products[0],Category=Categories[0]},
            new ProductCategory(){ Product = Products[0],Category=Categories[1]},
            new ProductCategory(){ Product = Products[1],Category=Categories[0]},
            new ProductCategory(){ Product = Products[1],Category=Categories[2]},
            new ProductCategory(){ Product = Products[3],Category=Categories[1]},
            new ProductCategory(){ Product = Products[4],Category=Categories[2]},
            new ProductCategory(){ Product = Products[5],Category=Categories[1]},
            new ProductCategory(){ Product = Products[6],Category=Categories[0]},
            new ProductCategory(){ Product = Products[6],Category=Categories[2]},
            new ProductCategory(){ Product = Products[6],Category=Categories[3]},
        };
    }
}
