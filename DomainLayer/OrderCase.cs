using DataLayer;
using DataLayer.Gateway;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace DomainLayer
{
    public class OrderCase
    {
        public static void insertOrder(int id_u, int id_c, bool canceled, DateTime time,string notifikacia = "")
        {


            DataLayer.Order order = new DataLayer.Order();
            order.Id_u = id_u;
            //order.Id_c = id_c;
            order.Canceled = canceled;
            order.Notification = notifikacia;
            order.Time = time;
            //test
            //order.Id_o = 1;



            DataLayer.Gateway.OrderGateway.Insert(order);
        }


        public static void Update(int id_o, string not)
        {
            OrderGateway.Update(id_o, not);
        }

        public static Collection<Order> getOrdersbyID(int id_u)
        {
            return OrderGateway.SelectID(id_u);
        }




        public static string readFile(int id_u, int id_o)
        {
            string textFileName = "order" + id_u + id_o;
            string fileName = @"C:\Users\Marián\Škola\5. SEMESTER\VIS\ProjektIS-2\DataLayer\OrderCSV\" + textFileName;
            string txt = File.ReadAllText(fileName);
            return txt;
        }

        public static int getOrderID(int id_u,DateTime time)
        {
            Order order = new Order();
            order = OrderGateway.SelectIDandTime(id_u, time);
            return order.Id_o;
        }

        public static void generateFile(List<Cart> carts, DateTime time, int id_u, double totalPrice, double totalWeight)
        {
            Order order = new Order();
            order = OrderGateway.SelectIDandTime(id_u, time);


            string textFileName = "order" + order.Id_u + order.Id_o;
            string fileName = @"C:\Users\Marián\Škola\5. SEMESTER\VIS\ProjektIS-2\DataLayer\OrderCSV\" + textFileName;

            User user = DataLayer.Gateway.UserGateway.SelectID(order.Id_u);
            Product product = new Product();
            try
            {
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                // Create a new file     
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    sw.WriteLine("[" + id_u + order.Id_o + "]" + "<- ID");
                    sw.WriteLine("Malá cukráreňská výroba - potvrdenie o platbe");
                    sw.Write("Objednávka číslo: {0}", order.Id_o);
                    sw.WriteLine(" pre zákazníka " + user.Name + " " + user.Surname);
                    sw.WriteLine("Dátum platby: " + order.Time.ToString());
                    sw.WriteLine("Celková cena: " + totalPrice.ToString() + "€");
                    sw.WriteLine("Celková váha: " + totalWeight.ToString() + "g");
                    sw.WriteLine("Zoznam objednaných produktov:");
                    foreach(var cart in carts)
                    {
                        product = ProductGateway.SelectID(cart.Id_p);
                        sw.WriteLine(product.ProductName + " | " + cart.Pieces + " kusov | Vaha za kusy: " + cart.Pieces * product.Weight + "g");
                    }
                }

              
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }


        }
    }
}
