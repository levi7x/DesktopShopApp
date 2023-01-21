using DataLayer;
using DataLayer.Gateway;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DomainLayer
{
    public class CartCase
    {
        public static void insertCartProducts(int cis, int id_p, int kusy)
        {
            Cart cart = new Cart();
            cart.Id_p = id_p;
            cart.Pieces = kusy;
            cart.CartNumber = cis;

            CartGateway.Insert(cart);
        }

        public static List<Cart> getCartContent(int cis)
        {
            return CartGateway.SelectID(cis);
        }

        public static void deleteCartProducts(int id_p)
        {
            CartGateway.Delete(id_p);
        }

        public static void clearCart(int currentID)
        {
            CartGateway.DeleteID(currentID);
        }

        public static bool checkCart(int currentID)
        {
            List<Cart> carts = CartGateway.SelectID(currentID);
            Product product = new Product();
            foreach(var cart in carts)
            {
               product = ProductGateway.SelectID(cart.Id_p);
               if(cart.Pieces > product.NumOfPieces) { return false; }
            }
            return true;
        }

        public static string getCartInfo(int currentID, bool flag = false)
        {
            List<Cart> carts = CartGateway.SelectID(currentID);
            Product product = new Product();
            string sb = "Nedostatok nasledovneho tovaru: ";
            foreach (var cart in carts)
            {
                product = ProductGateway.SelectID(cart.Id_p);
                if (cart.Pieces > product.NumOfPieces)
                {
                    int inNeed = cart.Pieces - product.NumOfPieces;
                    sb += product.ProductName + " ";
                    sb += inNeed + " kusov, ";

                    if (flag)
                    {
                        UpdateCart(currentID, cart.Id_p, product.NumOfPieces);
                    }

                }
            }
            sb += "\nŽeláte si odstrániť tento počet kusov z vašho košíka ?";
            return sb;
        }

        public static void UpdateCart(int currentID, int id_p, int pieces)
        {
            CartGateway.Update(currentID, id_p, pieces);
        }

    }
}
