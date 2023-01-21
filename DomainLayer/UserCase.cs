using DataLayer;
using DataLayer.Gateway;
using System;

namespace DomainLayer
{
    public class UserCase
    {

        public static User getUserByID(int id)
        {
            User user = new User();
            user = UserGateway.SelectID(id);
            return user;
        }

        public static User getUserByEmail(string email)
        {
            User user = new User();
            user = UserGateway.SelectEMAIL(email);
            return user;
        }

        public static void Insert(string name, string surname, string email, string phoneNum, string password)
        {
            Random random = new Random();
            User user = new User();
            user.Name = name;
            user.Surname = surname;
            user.Email = email;
            user.PhoneNum = phoneNum;
            user.Password = password;
            user.Id_u = random.Next(4, 9999999); // zabudol som v databaze nastavit autoincrement - toto vyriesi par userov :D
            UserGateway.Insert(user);
        }

          
        public static void Update(User user)
        {
            UserGateway.UpdateUser(user);
        }

        public static bool UserExists(string email, string password)
        {
            User user = new User();
            user = UserCase.getUserByEmail(email);
            if (password == user.Password && email == user.Email) { return true; }
            else { return false; }
        }

    }
}
