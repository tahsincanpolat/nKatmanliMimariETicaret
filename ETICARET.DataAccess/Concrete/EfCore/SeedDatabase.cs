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
            new Product(){ Name = "Samsung Note 6" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "4.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 7" , Price = 15000, Images = { new Image() {ImageUrl = "5.jpg"},  new Image() {ImageUrl = "6.jpg"}, new Image() {ImageUrl = "7.jpg"}, new Image() {ImageUrl = "8.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 8" , Price = 15000, Images = { new Image() {ImageUrl = "9.jpg"},  new Image() {ImageUrl = "10.jpg"}, new Image() {ImageUrl = "1.jpg"}, new Image() {ImageUrl = "4.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 9" , Price = 15000, Images = { new Image() {ImageUrl = "3.jpg"},  new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "5.jpg"}, new Image() {ImageUrl = "7.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung Note 10" , Price = 15000, Images = { new Image() {ImageUrl = "7.jpg"},  new Image() {ImageUrl = "8.jpg"}, new Image() {ImageUrl = "9.jpg"}, new Image() {ImageUrl = "9.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "iPhone 14 128 GB" , Price = 15000, Images = { new Image() {ImageUrl = "2.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "3.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "İphone 13 128 GB" , Price = 15000, Images = { new Image() {ImageUrl = "5.jpg"},  new Image() {ImageUrl = "6.jpg"}, new Image() {ImageUrl = "7.jpg"}, new Image() {ImageUrl = "2.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Monster notebook" , Price = 15000, Images = { new Image() {ImageUrl = "9.jpg"},  new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "10.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Lenovo IdeaPad" , Price = 15000, Images = { new Image() {ImageUrl = "3.jpg"},  new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "5.jpg"}, new Image() {ImageUrl = "9.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "HP 2W6K4EA Pavilion" , Price = 15000, Images = { new Image() {ImageUrl = "7.jpg"},  new Image() {ImageUrl = "8.jpg"}, new Image() {ImageUrl = "9.jpg"}, new Image() {ImageUrl = "1.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Asus X515JF Core i7" , Price = 15000, Images = { new Image() {ImageUrl = "4.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "3.jpg"}, new Image() {ImageUrl = "2.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Xiaomi Mi Vacuum Mop 2in1 Essential Robot Süpürge" , Price = 15000, Images = { new Image() {ImageUrl = "9.jpg"},  new Image() {ImageUrl = "5.jpg"}, new Image() {ImageUrl = "1.jpg"}, new Image() {ImageUrl = "2.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Bosch BGS 41X300 Toz Torbasız Zemin Tipi Elektrikli Süpürge" , Price = 15000, Images = { new Image() {ImageUrl = "3.jpg"},  new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "5.jpg"}, new Image() {ImageUrl = "6.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Bosch WGA244X0TR 1400 Devir 9 KG Çamaşır Makinesi" , Price = 15000, Images = { new Image() {ImageUrl = "7.jpg"},  new Image() {ImageUrl = "8.jpg"}, new Image() {ImageUrl = "9.jpg"}, new Image() {ImageUrl = "6.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Samsung WD12TP34DSH/AH 12 KG Yıkama 8 KG Kurutmalı Çamaşır Makinesi" , Price = 15000, Images = { new Image() {ImageUrl = "1.jpg"},  new Image() {ImageUrl = "2.jpg"}, new Image() {ImageUrl = "6.jpg"}, new Image() {ImageUrl = "4.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Bosch SMS4IKI50T 4 Programlı Bulaşık Makinesi" , Price = 15000, Images = { new Image() {ImageUrl = "5.jpg"},  new Image() {ImageUrl = "6.jpg"}, new Image() {ImageUrl = "7.jpg"}, new Image() {ImageUrl = "8.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Karaca Rosegold Mutfak Robotu Karaca" , Price = 15000, Images = { new Image() {ImageUrl = "9.jpg"},  new Image() {ImageUrl = "7.jpg"}, new Image() {ImageUrl = "1.jpg"}, new Image() {ImageUrl = "2.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Bosch KFN96VPEA 4 Kapılı No-Frost Buzdolabı" , Price = 15000, Images = { new Image() {ImageUrl = "3.jpg"},  new Image() {ImageUrl = "4.jpg"}, new Image() {ImageUrl = "5.jpg"}, new Image() {ImageUrl = "6.jpg"}},Description ="<p>Güzel telefon</p>" },
            new Product(){ Name = "Philips 5400 Serisi EP5447/90 Tam Otomatik Kahve Makinesi" , Price = 15000, Images = { new Image() {ImageUrl = "7.jpg"},  new Image() {ImageUrl = "8.jpg"}, new Image() {ImageUrl = "9.jpg"}, new Image() {ImageUrl = "8.jpg"}},Description ="<p>Güzel telefon</p>" },

        };

        private static ProductCategory[] ProductCategories =
        {
            new ProductCategory(){ Product = Products[0],Category=Categories[0]},
            new ProductCategory(){ Product = Products[0],Category=Categories[2]},
            new ProductCategory(){ Product = Products[1],Category=Categories[0]},
            new ProductCategory(){ Product = Products[1],Category=Categories[2]},
            new ProductCategory(){ Product = Products[2],Category=Categories[0]},
            new ProductCategory(){ Product = Products[2],Category=Categories[2]},
            new ProductCategory(){ Product = Products[3],Category=Categories[0]},
            new ProductCategory(){ Product = Products[3],Category=Categories[2]},
            new ProductCategory(){ Product = Products[4],Category=Categories[0]},
            new ProductCategory(){ Product = Products[5],Category=Categories[0]},
            new ProductCategory(){ Product = Products[5],Category=Categories[2]},
            new ProductCategory(){ Product = Products[6],Category=Categories[0]},
            new ProductCategory(){ Product = Products[6],Category=Categories[2]},
            new ProductCategory(){ Product = Products[7],Category=Categories[1]},
            new ProductCategory(){ Product = Products[7],Category=Categories[2]},
            new ProductCategory(){ Product = Products[8],Category=Categories[1]},
            new ProductCategory(){ Product = Products[8],Category=Categories[2]},
            new ProductCategory(){ Product = Products[9],Category=Categories[1]},
            new ProductCategory(){ Product = Products[9],Category=Categories[2]},
            new ProductCategory(){ Product = Products[10],Category=Categories[1]},
            new ProductCategory(){ Product = Products[10],Category=Categories[2]},
            new ProductCategory(){ Product = Products[11],Category=Categories[3]},
            new ProductCategory(){ Product = Products[11],Category=Categories[2]},
            new ProductCategory(){ Product = Products[12],Category=Categories[3]},
            new ProductCategory(){ Product = Products[12],Category=Categories[2]},
            new ProductCategory(){ Product = Products[13],Category=Categories[3]},
            new ProductCategory(){ Product = Products[13],Category=Categories[2]},
            new ProductCategory(){ Product = Products[14],Category=Categories[3]},
            new ProductCategory(){ Product = Products[14],Category=Categories[2]},
            new ProductCategory(){ Product = Products[15],Category=Categories[3]},
            new ProductCategory(){ Product = Products[15],Category=Categories[2]},
            new ProductCategory(){ Product = Products[16],Category=Categories[3]},
            new ProductCategory(){ Product = Products[16],Category=Categories[2]},
            new ProductCategory(){ Product = Products[17],Category=Categories[3]},
            new ProductCategory(){ Product = Products[17],Category=Categories[2]},
            new ProductCategory(){ Product = Products[18],Category=Categories[3]},
            new ProductCategory(){ Product = Products[18],Category=Categories[2]},





        };
    }
}
