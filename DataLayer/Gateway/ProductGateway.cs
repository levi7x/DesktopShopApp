using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer.Gateway
{
    public class ProductGateway
    {
        public static String SQL_SELECT_ID = "select id_p, nazov_produktu, pocet_kusov, cena_za_kus, vaha from product where id_p = @id_p";
        public static String SQL_SELECT = "select id_p, nazov_produktu, pocet_kusov, cena_za_kus, vaha from product";
        public static String SQL_UPDATE_ID = "UPDATE product SET pocet_kusov = @kusy WHERE id_p = @id_p";
        public static String SQL_UPDATE_PRICE = "UPDATE product SET cena_za_kus = @price WHERE id_p = @id_p";
        public static String SQL_INSERT = "insert into product values (@id_p, @name, @pieces, @price, @weight)";




        public static int Insert(Product product, Database pDb = null)
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
            command.Parameters.AddWithValue("@id_p", product.Id_p);
            command.Parameters.AddWithValue("@name", product.ProductName);
            command.Parameters.AddWithValue("@pieces", product.NumOfPieces);
            command.Parameters.AddWithValue("@price", product.Price);
            command.Parameters.AddWithValue("@weight", product.Weight);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int UpdateProductPrice(int id_p, double price, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_UPDATE_PRICE);
            command.Parameters.AddWithValue("@id_p", id_p);
            command.Parameters.AddWithValue("@price", price);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
        public static int UpdateProduct(int id_p, int kusy, Database pDb = null)
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
            command.Parameters.AddWithValue("@id_p", id_p);
            command.Parameters.AddWithValue("@kusy", kusy);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static Product SelectID(int id_p, Database pDb = null)
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
            command.Parameters.AddWithValue("@id_p", id_p);
            SqlDataReader reader = db.Select(command);
            Product product = new Product();
            while (reader.Read())
            {
                product.Id_p = Convert.ToInt32(reader["id_p"]);
                product.ProductName = reader["nazov_produktu"].ToString();
                product.NumOfPieces = Convert.ToInt32(reader["pocet_kusov"]);
                product.Price = Convert.ToDouble(reader["cena_za_kus"]);
                product.Weight = Convert.ToInt32(reader["vaha"]);

            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return product;
        }

        public static Collection<Product> Select(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT);
            SqlDataReader reader = db.Select(command);
            Collection<Product> products = new Collection<Product>();

            while (reader.Read())
            {
                
                Product product = new Product();
                product.Id_p = Convert.ToInt32(reader["id_p"]);
                product.ProductName = reader["nazov_produktu"].ToString();
                product.NumOfPieces = Convert.ToInt32(reader["pocet_kusov"]);
                product.Price = Math.Round(Convert.ToDouble(reader["cena_za_kus"]), 2);
                product.Weight = Convert.ToInt32(reader["vaha"]);
                products.Add(product);
            }
 
            
            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return products;
        }


    }
}
