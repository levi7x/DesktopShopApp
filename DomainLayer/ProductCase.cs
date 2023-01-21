using DataLayer;
using DataLayer.Gateway;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DomainLayer
{
    public class ProductCase
    {
        public static Collection<Product> getProducts()
        {
            Collection<Product> products = ProductGateway.Select();
            return products;
        }

        public static Product getProductByID(int id_p)
        {
            Product product = new Product();
            product = ProductGateway.SelectID(id_p);
            return product;
        }

        public static void updateProductPop(int id_p, int kusy)
        {
            Product product = new Product();
            product = getProductByID(id_p);
            int pieces = product.NumOfPieces - kusy;
            ProductGateway.UpdateProduct(id_p, pieces);
        }


        public static void updateProductAdd(int id_p, int kusy)
        {
            Product product = new Product();
            product = getProductByID(id_p);
            int pieces = product.NumOfPieces + kusy;
            ProductGateway.UpdateProduct(id_p, pieces);
        }


        public static void insertProduct(string name, int weight, double price)
        {

            int id_p = ProductGateway.Select().Count + 1;
            Product product = new Product();
            product.Id_p = id_p;
            product.NumOfPieces = 0;
            product.Price = price;
            product.ProductName = name;
            product.Weight = weight;

            ProductGateway.Insert(product);

        }

        public static void updateProductPrice(int id_p, double price)
        {
            ProductGateway.UpdateProductPrice(id_p, price);
        }
    }
}
