using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            new Product(){ Name = "Samsung Note 6" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "3.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 7" , Price = 15000, Images = { new Image() {ImageUrl = "4.jpg"},  new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "5.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 8" , Price = 15000, Images = { new Image() {ImageUrl = "3.jpg"},  new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "5.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 9" , Price = 15000, Images = { new Image() {ImageUrl = "4.jpg"},  new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 10" , Price = 15000, Images = { new Image() {ImageUrl = "5jpg"},  new Image() {ImageUrl = "5.jpg"}, new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "5.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 11" , Price = 15000, Images = { new Image() {ImageUrl = "6.jpg"},  new Image() {ImageUrl = "6.jpg"}, new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "10.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Monster notebook" , Price = 20799, Images = { new Image() {ImageUrl = "11.jpg"},  new Image() {ImageUrl = "11.jpg"}, new Image() {ImageUrl = "12.jpg"}, new Image() {ImageUrl = "13.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Lenovo ThinkPad P16" , Price = 20799, Images = { new Image() {ImageUrl = "14.jpg"},  new Image() {ImageUrl = "14.jpg"}, new Image() {ImageUrl = "15.jpg"}, new Image() {ImageUrl = "16.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "HP Pavilion" , Price = 19750, Images = { new Image() {ImageUrl = "17.jpg"},  new Image() {ImageUrl = "17.jpg"}, new Image() {ImageUrl = "18.jpg"}, new Image() {ImageUrl = "19.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Asus x515ja" , Price = 22000, Images = { new Image() {ImageUrl = "20.jpg"},  new Image() {ImageUrl = "21.jpg"}, new Image() {ImageUrl = "22.jpg"}, new Image() {ImageUrl = "23.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Notebook 9" , Price = 18500, Images = { new Image() {ImageUrl = "23.jpg"},  new Image() {ImageUrl = "24.jpg"}, new Image() {ImageUrl = "25.jpg"}, new Image() {ImageUrl = "26.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Huawei MateBook X Pro" , Price = 18500, Images = { new Image() {ImageUrl = "27.jpg"},  new Image() {ImageUrl = "28.jpg"}, new Image() {ImageUrl = "29.jpg"}, new Image() {ImageUrl = "30.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Apple Macbook M1 Pro " , Price = 39997, Images = { new Image() {ImageUrl = "31.jpg"},  new Image() {ImageUrl = "32.jpg"}, new Image() {ImageUrl = "33.jpg"}, new Image() {ImageUrl = "34.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Apple Macbook M1 Pro " , Price = 39997, Images = { new Image() {ImageUrl = "31.jpg"},  new Image() {ImageUrl = "32.jpg"}, new Image() {ImageUrl = "33.jpg"}, new Image() {ImageUrl = "34.jpg"}},Description ="<p>Güzel telefon</p>" },

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
            new ProductCategory(){ Product = Products[6],Category=Categories[1]},
            new ProductCategory(){ Product = Products[7],Category=Categories[1]},
            new ProductCategory(){ Product = Products[8],Category=Categories[1]},
            new ProductCategory(){ Product = Products[9],Category=Categories[1]},
            new ProductCategory(){ Product = Products[10],Category=Categories[1]},
            new ProductCategory(){ Product = Products[11],Category=Categories[1]},
            //new ProductCategory(){ Product = Products[9],Category=Categories[1]},
           

        };
    }
}
