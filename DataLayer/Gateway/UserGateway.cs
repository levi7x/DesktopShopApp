using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataLayer.Gateway
{
    public class UserGateway
    {

        public static String SQL_SELECT_ID = "SELECT id_u, meno, priezvisko, email, telefonne_cislo, heslo FROM uzivatel WHERE id_u = @id_u";
        public static String SQL_SELECT_EMAIL = "SELECT id_u, meno, priezvisko, email, telefonne_cislo, heslo from uzivatel WHERE email = @email";
        public static String SQL_UPDATE_ID = "UPDATE uzivatel SET meno = @name, priezvisko = @surname, email = @email, telefonne_cislo = @phoneNum, heslo = @password WHERE id_u = @id_u";
        public static String SQL_INSERT = "INSERT INTO uzivatel VALUES(@id_u, @name, @surname, @email, @phoneNum, @password)";


        public static int Insert(User user, Database pDb = null)
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
            command.Parameters.AddWithValue("@id_u", user.Id_u);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@surname", user.Surname);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@phoneNum", user.PhoneNum);
            command.Parameters.AddWithValue("@password", user.Password);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }

        public static int UpdateUser(User user, Database pDb = null)
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
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@surname", user.Surname);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@phoneNum", user.PhoneNum);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@id_u", user.Id_u);

            int ret = db.ExecuteNonQuery(command);

            if (pDb == null)
            {
                db.Close();
            }

            return ret;
        }


        public static User SelectID(int id_u, Database pDb = null)
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
            User user = new User();
            while (reader.Read())
            {
                user.Id_u = Convert.ToInt32(reader["id_u"]);
                user.Name = reader["Meno"].ToString();
                user.Surname = reader["Priezvisko"].ToString();
                user.Email = reader["Email"].ToString();
                user.PhoneNum = reader["Telefonne_cislo"].ToString();
                user.Password = reader["heslo"].ToString();
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return user;
        }


        public static User SelectEMAIL(string email, Database pDb = null)
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

            SqlCommand command = db.CreateCommand(SQL_SELECT_EMAIL);
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader reader = db.Select(command);
            User user = new User();
            while (reader.Read())
            {
                user.Id_u = Convert.ToInt32(reader["id_u"]);
                user.Name = reader["Meno"].ToString();
                user.Surname = reader["Priezvisko"].ToString();
                user.Email = reader["Email"].ToString();
                user.PhoneNum = reader["Telefonne_cislo"].ToString();
                user.Password = reader["heslo"].ToString();
            }

            reader.Close();

            if (pDb == null)
            {
                db.Close();
            }

            return user;
        }
    }
}


