using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer.Gateway
{
    public class CartGateway
    {
        public static String SQL_INSERT = "INSERT INTO kosik(product_id_p, kusy, cislo_kosika) VALUES (@id_p, @kusy, @kosik)";
        public static String SQL_SELECT_ID = "SELECT id_k, product_id_p, kusy, cislo_kosika FROM kosik WHERE cislo_kosika = @c_num";
        public static String SQL_DELETE = "DELETE FROM kosik WHERE product_id_p = @id_p";
        public static String SQL_DELETE_ID = "DELETE FROM kosik WHERE cislo_kosika = @kosik";
        public static String SQL_UPDATE_ID = "UPDATE kosik SET kusy = @kusy WHERE product_id_p = @id_p and cislo_kosika = @kosik";


        public static int Insert(Cart cart, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_INSERT);
            PrepareCommand(command, cart);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int DeleteID(int id_u, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_DELETE_ID);
            command.Parameters.AddWithValue("@kosik", id_u);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Update(int id_u, int id_p, int kusy, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_UPDATE_ID);
            command.Parameters.AddWithValue("@kosik", id_u);
            command.Parameters.AddWithValue("@id_p", id_p);
            command.Parameters.AddWithValue("@kusy", kusy);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int Delete(int id_p, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_DELETE);
            command.Parameters.AddWithValue("@id_p", id_p);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        private static void PrepareCommand(SqlCommand command, Cart cart)
        {
            command.Parameters.AddWithValue("@id_p", cart.Id_p);
            command.Parameters.AddWithValue("@kusy", cart.Pieces);
            command.Parameters.AddWithValue("@kosik", cart.CartNumber);

        }

        public static List<Cart> SelectID(int cart_num, Database pDb = null)
        {
            Database db;
            if (pDb == null)
            {
                db = new Database();
                db.Connect();
            }
            else
            {
                db = (Database)pDb;
            }

            SqlCommand command = db.CreateCommand(SQL_SELECT_ID);
            command.Parameters.AddWithValue("@c_num", cart_num);
            SqlDataReader reader = db.Select(command);
            List<Cart> carts = new List<Cart>();
            while (reader.Read())
            {
                Cart cart = new Cart();
                cart.Id_c = Convert.ToInt32(reader["id_k"]);
                cart.Id_p = Convert.ToInt32(reader["product_id_p"]);
                cart.Pieces = Convert.ToInt32(reader["kusy"]);
                cart.CartNumber = cart_num;
                carts.Add(cart);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return carts;
        }
    }
}
