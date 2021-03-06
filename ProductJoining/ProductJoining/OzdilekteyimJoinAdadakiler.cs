using ProductJoining.Context;
using ProductJoining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductJoining
{
    public class OzdilekteyimJoinAdadakiler
    {
        public void Join()
        {


            var contextO = new ProductContext();
            var contextA = new ProductContextAdadakiler();

            List<Product> BazaaramedPro = contextA.Products.Where(s => s.State == true && !string.IsNullOrWhiteSpace(s.Barcode)).ToList();
            foreach (var item in BazaaramedPro)
            {
                Product product = contextO.Products.FirstOrDefault(s => s.Barcode == item.Barcode);

                if (product == null)
                {

                    Product product1 = new Product();
                    product1.Name = item.Name;
                    product1.Description = item.Description;
                    product1.Code = item.Code;
                    product1.Barcode = item.Barcode;
                    product1.Address = item.Address;
                    product1.Price = item.Price;
                    product1.UnitID = 1;
                    product1.SKU = item.SKU;
                    product1.Source = 1;
                    int? catID = contextA.Categories.FirstOrDefault(s => s.ID == item.CategoryID).FakeID;
                    product1.CategoryID = contextO.Categories.FirstOrDefault(s => s.ID == catID).ID;

                    Brand brandBazaaramed = contextA.Brands.FirstOrDefault(b => b.ID == item.BrandID);

                    product1.BrandID = contextO.Brands.FirstOrDefault(s => s.Name.ToLower().Replace(" ", "").Replace("ö", "o").Replace("ü", "u").Replace("ı", "i").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("&", "").Replace("-", "").Replace(".", "").Replace("'", "") == brandBazaaramed.Name.ToLower().Replace(" ", "").Replace("ö", "o").Replace("ü", "u").Replace("ı", "i").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("&", "").Replace("-", "").Replace(".", "").Replace("'", "")).ID;

                    contextO.Products.AddRange(product1);
                    contextO.SaveChanges();

                    File file = contextA.Files.FirstOrDefault(s => s.ProductID == item.ID);
                    if (file == null)
                    {
                        file = contextO.Files.FirstOrDefault(s => s.ID == 18821);
                    }
                    File Ofile = new File()
                    {
                        Path = file.Path,
                        RelativePath = file.RelativePath,
                        ProductID = product1.ID,
                        State = true,
                        Source = 1,
                    };
                    contextO.Files.AddRange(Ofile);
                    contextO.SaveChanges();
                }
                item.State = false;
                contextA.SaveChanges();
                Console.WriteLine("Product->" + item.Name);
            }



        }
    }
}
