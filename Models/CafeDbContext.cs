using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace JapnesseCafe.Models
{
    public class CafeDbContext : DbContext
    {
        public CafeDbContext() : base("CafeDbContext")
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Product> Products { get; set; }
    }

    public class CafeDbInitializer : CreateDatabaseIfNotExists<CafeDbContext>
    {
        protected override void Seed(CafeDbContext context)
        {
            var menu = new List<MenuItem>
            {
                NewMenu("Matcha Latte", "Creamy ceremonial matcha with steamed milk and a tiny sakura foam art finish.", 4.75m, "Drinks", "#b7e4c7", "抹茶"),
                NewMenu("Sakura Iced Tea", "Refreshing floral tea with cherry blossom syrup, citrus, and crystal ice.", 4.25m, "Drinks", "#ffc2d1", "桜茶"),
                NewMenu("Taiyaki", "Warm fish-shaped cake filled with sweet red bean custard.", 3.95m, "Desserts", "#ffd6a5", "鯛焼"),
                NewMenu("Mochi Dessert Plate", "Soft mochi trio with strawberry, matcha, and vanilla bean flavors.", 6.50m, "Desserts", "#e7c6ff", "餅"),
                NewMenu("Onigiri Set", "Rice ball trio with salmon, tuna mayo, and umeboshi fillings.", 7.25m, "Snacks", "#d8f3dc", "おにぎり"),
                NewMenu("Chicken Katsu Curry", "Crispy chicken cutlet over rice with mild Japanese curry.", 12.95m, "Main Course Meals", "#ffb703", "カツ"),
                NewMenu("Ramen Bowl", "Comforting shoyu ramen with egg, nori, scallions, and chashu.", 11.95m, "Main Course Meals", "#f4a261", "ラーメン"),
                NewMenu("Anime Bento Box", "Colorful cafe bento with karaage, tamagoyaki, rice, and seasonal sides.", 13.50m, "Main Course Meals", "#a2d2ff", "弁当")
            };

            var products = new List<Product>
            {
                NewProduct("Anime Hero Figure", "Poseable hero figure with display stand and effect parts.", 29.99m, "Figures", "#90dbf4", "英雄"),
                NewProduct("Sakura Magical Girl Figure", "Pastel magical girl figure with sakura wand and ribbon base.", 34.99m, "Figures", "#ffafcc", "魔法"),
                NewProduct("Anime T-Shirt", "Soft cotton tee with cafe mascot artwork.", 19.99m, "T-Shirts", "#cdb4db", "Tシャツ"),
                NewProduct("Shonen Power T-Shirt", "Bold action-inspired graphic tee for tournament arcs and coffee runs.", 21.99m, "T-Shirts", "#bde0fe", "熱血"),
                NewProduct("Keychain Set", "Acrylic charm set featuring drinks, desserts, and mascot faces.", 9.99m, "Anime Accessories", "#fdffb6", "鍵"),
                NewProduct("Anime Pin Pack", "Enamel pin pack with sakura, ramen, cat, and star motifs.", 8.50m, "Anime Accessories", "#ffc8dd", "ピン"),
                NewProduct("Manga Tote Bag", "Canvas tote with manga panel pattern and sturdy handles.", 16.99m, "Anime Accessories", "#d0f4de", "漫画"),
                NewProduct("Cat Ear Headband", "Cute cafe cosplay headband with soft plush ears.", 12.99m, "Anime Accessories", "#f1c0e8", "猫耳")
            };

            context.MenuItems.AddRange(menu);
            context.Products.AddRange(products);
            context.SaveChanges();
        }

        private static MenuItem NewMenu(string name, string description, decimal price, string category, string color, string text)
        {
            return new MenuItem
            {
                Name = name,
                Description = description,
                Price = price,
                Category = category,
                IsAvailable = true,
                CreatedAt = DateTime.Now,
                ImageMimeType = "image/png",
                ImageData = MakePng(color, text)
            };
        }

        private static Product NewProduct(string name, string description, decimal price, string category, string color, string text)
        {
            return new Product
            {
                Name = name,
                Description = description,
                Price = price,
                Category = category,
                IsAvailable = true,
                CreatedAt = DateTime.Now,
                ImageMimeType = "image/png",
                ImageData = MakePng(color, text)
            };
        }

        private static byte[] MakePng(string htmlColor, string text)
        {
            using (var bitmap = new Bitmap(900, 560))
            using (var g = Graphics.FromImage(bitmap))
            using (var stream = new MemoryStream())
            {
                g.Clear(ColorTranslator.FromHtml(htmlColor));
                using (var brush = new SolidBrush(Color.FromArgb(245, 255, 255, 255)))
                using (var pen = new Pen(Color.FromArgb(210, 255, 255, 255), 8))
                using (var font = new Font("Yu Gothic", 72, FontStyle.Bold, GraphicsUnit.Pixel))
                using (var small = new Font("Segoe UI", 30, FontStyle.Bold, GraphicsUnit.Pixel))
                {
                    g.FillEllipse(brush, 95, 80, 700, 370);
                    g.DrawEllipse(pen, 95, 80, 700, 370);
                    var format = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(text, font, Brushes.DeepPink, new RectangleF(0, 130, 900, 160), format);
                    g.DrawString("Sakura Cafe", small, Brushes.HotPink, new RectangleF(0, 285, 900, 80), format);
                }
                bitmap.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
