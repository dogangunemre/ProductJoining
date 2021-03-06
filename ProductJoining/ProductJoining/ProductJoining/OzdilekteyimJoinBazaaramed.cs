using ProductJoining.Context;
using ProductJoining.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductJoining
{
    public class OzdilekteyimJoinBazaaramed
    {
        public void Join()
        {
            

            var contextO = new ProductContext();        
            var contextB = new ProductContextBazaaramed();

            List<Product> BazaaramedPro = contextB.Products.Where(s => s.State == true && !string.IsNullOrWhiteSpace(s.Barcode)).ToList();
            foreach (var item in BazaaramedPro)
            {
                Product product = contextO.Products.FirstOrDefault(s => s.Barcode == item.Barcode);
                
                if (product==null)
                {

                    Product product1 = new Product();
                    product1.Name = item.Name;
                    product1.Description = item.Description;
                    product1.Code = item.Code;
                    product1.Barcode = item.Barcode;
                    product1.Address = item.Address;
                    product1.Price = item.Price;
                    product1.UnitID = item.UnitID;
                    product1.SKU = item.SKU;
                    int? catID= contextB.Categories.FirstOrDefault(s => s.ID == item.CategoryID).FakeID;
                    product1.CategoryID = contextO.Categories.FirstOrDefault(s => s.ID == catID).ID;

                    Brand brandBazaaramed=contextB.Brands.FirstOrDefault(b => b.ID == item.BrandID);

                    product1.BrandID= contextO.Brands.FirstOrDefault(s => s.Name.ToLower().Replace(" ","").Replace("ö","o").Replace("ü","u").Replace("ı","i").Replace("ç","c").Replace("ğ","g").Replace("ş","s").Replace("&","").Replace("-","").Replace(".", "") == brandBazaaramed.Name.ToLower().Replace(" ", "").Replace("ö", "o").Replace("ü", "u").Replace("ı", "i").Replace("ç", "c").Replace("ğ", "g").Replace("ş", "s").Replace("&", "").Replace("-", "").Replace(".", "")).ID;
                 
                    contextO.Products.AddRange(product1);
                    contextO.SaveChanges();

                    File file = contextB.Files.FirstOrDefault(s => s.ProductID == item.ID);
                    File Ofile = new File() {
                        Path = file.Path,
                        RelativePath=file.RelativePath,
                        ProductID=product1.ID,
                        State=true,
                        Source=3,
                    };
                    contextO.Files.AddRange(Ofile);
                    contextO.SaveChanges();
                }
                item.State = false;
                contextB.SaveChanges();
            }



        }
    }
}
