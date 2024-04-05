using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

internal class CouponService
{
    /*
    private List<Coupon> CouponList = new List<Coupon>
    {
        new Coupon { CouponId = 0, Code = "swD23", AmountOfDiscount = 15, TypeOfDiscount = ETypeOfDiscount.Percentage, MaxNumberOfUses = 100 },
        new Coupon { CouponId = 1, Code = "dsf4wsdf", AmountOfDiscount = 150, TypeOfDiscount = ETypeOfDiscount.Total, MaxNumberOfUses = 750 },
        new Coupon { CouponId = 2, Code = "sdfsdgfgh5fh", AmountOfDiscount = 25, TypeOfDiscount = ETypeOfDiscount.Percentage, MaxNumberOfUses = 500 }
    };
    */

    /*
     * private List<Product> ProductList = new List<Product>
        {
            new Product { ProductId = 0, Name="Persil", Producer="Henkel", Category= EProductCategorys.Cleaning, Picture="Persil123.jpg"},
            new Product { ProductId = 1, Name="Weingummi", Producer="Haribo", Category= EProductCategorys.Sweets, Picture="Haribo123.jpg"},
            new Product { ProductId = 2, Name="Pizza Salami", Producer="Dr. Oetker", Category= EProductCategorys.Food, Picture="Pizza123.jpg"}
        };

        public List<Product> GetProductList() { return ProductList; }

        public Product? GetProduct(int productId)
        {
            return ProductList.Where(o => o.ProductId == productId).FirstOrDefault();
        }

        public bool Delete(int productId)
        {
            var productToDelete = ProductList.Where(o => o.ProductId == productId).FirstOrDefault();
            return ProductList.Remove(productToDelete);
        }

        public void AddProduct(int productId, string name, string producer, EProductCategorys category, string picture)
        {
            ProductList.Add(new Product { ProductId = productId, Name = name, Producer = producer, Category=category, Picture = picture });
        }
    */
}