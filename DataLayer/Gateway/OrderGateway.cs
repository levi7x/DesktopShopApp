using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer.Gateway
{
    public class OrderGateway
    {
        public static String SQL_INSERT = "INSERT INTO objednavka VALUES (@id_u, @canceled, @datum, @notifikacia)";
        public static String SQL_SELECT_ID = "SELECT id_o, uzivatel_id_u, datum_objednavky, canceled, notifikacia FROM objednavka WHERE uzivatel_id_u = @id_u";
        public static String SQL_SELECT_ID_TIME = "SELECT id_o, uzivatel_id_u, datum_objednavky, canceled, notifikacia FROM objednavka WHERE uzivatel_id_u = @id_u AND datum_objednavky = @time";
        public static String SQL_UPDATE = "UPDATE objednavka SET notifikacia = @notifikacia, canceled = 1 where id_o = @id_o";



        public static Order SelectIDandTime(int id_u, DateTime time, Database pDb = null)
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
            command.Parameters.AddWithValue("@id_u", id_u);
            command.Parameters.AddWithValue("@time", time);
            SqlDataReader reader = db.Select(command);

            Order order = new Order();


            while (reader.Read())
            {
                order.Id_o = Convert.ToInt32(reader["id_o"]);
                order.Id_u = Convert.ToInt32(reader["uzivatel_id_u"]);
                order.Time = Convert.ToDateTime(reader["datum_objednavky"]);
                order.Canceled = Convert.ToBoolean(reader["canceled"]);
                order.Notification = reader["notifikacia"].ToString();
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return order;
        }


        public static Collection<Order> SelectID(int id_u, Database pDb = null)
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
            command.Parameters.AddWithValue("@id_u", id_u);
            SqlDataReader reader = db.Select(command);
            Collection<Order> orders = new Collection<Order>();

            while (reader.Read())
            {

                Order order = new Order();
                order.Id_o = Convert.ToInt32(reader["id_o"]);
                order.Id_u = Convert.ToInt32(reader["uzivatel_id_u"]);
                order.Time = Convert.ToDateTime(reader["datum_objednavky"]);
                order.Canceled = Convert.ToBoolean(reader["canceled"]);
                order.Notification = reader["notifikacia"].ToString();
                orders.Add(order);
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return orders;
        }


        public static int Update(int id_o, string notifikacia, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_UPDATE);
            command.Parameters.AddWithValue("@id_o", id_o);
            command.Parameters.AddWithValue("@notifikacia", notifikacia);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }
            return ret;
        }

        public static int Insert(Order order, Database pDb = null)
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
            //command.Parameters.AddWithValue("@kosik", order.Id_c);
            command.Parameters.AddWithValue("@id_u", order.Id_u);
            command.Parameters.AddWithValue("@canceled", order.Canceled);
            command.Parameters.AddWithValue("@datum", order.Time);
            command.Parameters.AddWithValue("@notifikacia", order.Notification);
            //command.Parameters.AddWithValue("@id_o", order.Id_o);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }
    }
}
