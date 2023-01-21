using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer.Gateway
{
    public class PaymentGateway
    {
        public static String SQL_SELECT_ID = "select id_o, id_k, id_u, canceled, datum_objednavky, notofikacia from product where id_p = @id_p";
        public static String SQL_INSERT = "INSERT INTO platba VALUES (@id_p, @objednavka_id_o, @cislo_karty, @cvc, @mmrrrr)";
        public static String SQL_GETMAX_ID = "select max(id_p) as Max_Id from platba";

        public static int Insert(Payment payment, Database pDb = null)
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
            PrepareCommand(command, payment);
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int GetMaxId(Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_GETMAX_ID);
            SqlDataReader reader = db.Select(command);
            int? maxId = 0;
            while (reader.Read())
            {
                maxId = Convert.ToInt32(reader["Max_Id"]);
                if(maxId == null)
                {
                    maxId = 1;
                }
               
            }
            reader.Close();
            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return (int)maxId;
        }

        private static void PrepareCommand(SqlCommand command, Payment payment)
        {
            command.Parameters.AddWithValue("@id_p", payment.Id_p);
            command.Parameters.AddWithValue("@cvc", payment.CVC);
            command.Parameters.AddWithValue("@cislo_karty", payment.CardNum);
            command.Parameters.AddWithValue("@mmrrrr", payment.MMYYYY);
            command.Parameters.AddWithValue("@objednavka_id_o", payment.Id_o);
        }
    }
}
