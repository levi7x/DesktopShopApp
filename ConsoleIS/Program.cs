using System;
using System.Collections.ObjectModel;
using DataLayer;
using DomainLayer;
namespace ConsoleIS
{
    class Program
    {

        public static string stars = "****************************************************************************************************************";
        public static bool loggedIn = false;
        public static int currentID;

        public static void Quit()
        {
            Console.WriteLine("Applikacia konci...");
            Environment.Exit(0);
        }

        public static void ShowMenu()
        {
            Console.WriteLine(stars);
            Console.WriteLine("Stlacte 1 pre prihlasenie");
            Console.WriteLine("Stlacte 2 pre registraciu");
            Console.WriteLine("Stlacte 3 pre ukoncenie");
            Console.WriteLine(stars);

            string choice = Console.ReadLine();

            if(choice == "1")
            {
                ConsolegreenText("Logining in...");
                LogIn();
            }
            else if(choice == "2")
            {
                RegForm();
            }
            else if(choice == "3")
            {
                Quit();
            }
            else if (choice == "4")
            {
                currentID = 1;
                ShowLoggedInMenu();
            }
            else
            {
                ConsoleredText("Zly vstup");
                ShowMenu();
            }


        }



        public static void RegForm()
        {
            Console.WriteLine(stars);
            Console.WriteLine("REGISTRACIA - vyplnte nasledujuce udaje");
            Console.WriteLine("Meno __________________");
            Console.WriteLine("Priezvisko __________________");
            Console.WriteLine("Email __________________");
            Console.WriteLine("Telefonne cislo __________________");
            Console.WriteLine("Heslo __________________");
            Console.WriteLine("Overenie hesla __________________");
            Console.WriteLine(stars);

            Console.WriteLine("Zadajte meno:");
            string name = Console.ReadLine();
            Console.WriteLine("Zadajte priezvisko:");
            string surname = Console.ReadLine();
            Console.WriteLine("Zadajte Email:");
            string email = Console.ReadLine();
            Console.WriteLine("Telefonne cislo:");
            string phoneNum = Console.ReadLine();
            Console.WriteLine("Heslo:");
            string password = Console.ReadLine();
            Console.WriteLine("Zadajte znova heslo:");
            string password2 = Console.ReadLine();

            if(password != password2)
            {
                ConsoleredText("Hesla sa nezhoduju!");
                RegForm();
                return;
            }

            if(DomainLayer.UserCase.getUserByEmail(email).Email != null)
            {
                Console.WriteLine(DomainLayer.UserCase.getUserByEmail(email).Email);
                ConsoleredText("Uzivatel s danym emailom uz existuje!");
                RegForm();
                return;
            }

                ConsolegreenText("Zelate si zalozit ucet s udajmi:  [Y/N] (ANO / NIE)");
                Console.WriteLine("Meno: " + name );
                Console.WriteLine("Priezvisko: " + surname);
                Console.WriteLine("Email: " + email);
                Console.WriteLine("Telefonne cislo: " + phoneNum);
                Console.WriteLine("Heslo: " + password);

            string yesNo = Console.ReadLine();


            if (yesNo == "y" || yesNo == "Y")
            {
                UserCase.Insert(name,surname,email,phoneNum,password);
                ConsolegreenText("Vas ucet bol uspesne zalozeny, poslali sme vam na mail overovaci link....");
                ShowMenu();
            }
            else
            {
                ConsolegreenText("Opustam registraciu....");
                ShowMenu();
            }





        }



        public static void ConsoleredText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }
        public static void ConsolegreenText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void LogIn()
        {
            Console.WriteLine(stars);
            Console.WriteLine("Prosim zadajte vas email");
            string email = Console.ReadLine();

            Console.WriteLine("Prosim zadajte vase heslo");
            string password = Console.ReadLine();
            if (password == "admin" && email == "admin")
            {
                ShowAdminMenu();
                return;
            }
            try
            {
                User user = new User();
                user = UserCase.getUserByEmail(email);
                if (password == user.Password && email == user.Email) 
                {
                    loggedIn = true; currentID = user.Id_u;
                    ConsolegreenText("Vase prihlasenie bolo upsesne");
                    ShowLoggedInMenu();
                }
                else 
                {
                    ConsoleredText("Nespravne zadane vstupne udaje");
                    Console.WriteLine(stars);
                    Console.WriteLine("Stlacte 1 pre opakovane prihlasenie");
                    Console.WriteLine("Stlacte lubobolnu klavesu pre ukoncenie");


                    string choice = Console.ReadLine();
                    if (choice == "1")
                    {
                        ConsolegreenText("Logining in...");
                        LogIn();
                    }
                    else { ShowMenu(); }
                }
            }

            catch
            {
              
            }


            Console.WriteLine(stars);
        }

        private static void ShowAdminMenu()
        {
            Console.WriteLine(stars);
            Console.WriteLine("ADMIN MENU");
            Console.WriteLine("Vase moznosti su:");
            Console.WriteLine("Stlacte 1 pre zobrazenie skladu");
            Console.WriteLine("Stlacte 2 pre zobrazenie objednavok");
            Console.WriteLine("Stlacte 3 pre vyhlasenie dovolenky");
            Console.WriteLine("Stlacte 4 pre ukoncenie");
            Console.WriteLine(stars);
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                ConsolegreenText("Logining in...");
                ShowStorage();
            }
            else if (choice == "2")
            {
                ShowOrders();
            }
            else if (choice == "3")
            {
                //holiday
            }
            else if (choice == "4")
            {
                Quit();
            }
            else
            {
                ConsoleredText("Zly vstup");
                ShowAdminMenu();
            }
            Console.WriteLine(stars);
        }

        private static void ShowStorage()
        {
            Collection<Product> collection = new Collection<Product>();
            collection = ProductCase.getProducts();
            Console.WriteLine(stars);
            Console.WriteLine("SKLAD: ");
            for(int i = 0; i < collection.Count; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("<" + (i+1) + "> " + collection[i].ProductName + " " + collection[i].Weight + "g " + collection[i].Price + "$ " + collection[i].NumOfPieces + " kusov");
                Console.ResetColor();
            }


            Console.WriteLine(stars);
            Console.WriteLine("Vase moznosti su:");
            Console.WriteLine("Stlacte 1 pre pridanie produktu");
            Console.WriteLine("Stlacte 2 pre upravu produktu");
            Console.WriteLine("Stlacte 3 pre opustenie skladu");
            Console.WriteLine(stars);

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                ConsolegreenText("Logining in...");
                AddProduct();
            }
            else if (choice == "2")
            {
                UpdateProduct();
            }
            else if (choice == "3")
            {
                ShowAdminMenu();
                return;
            }
            else
            {
                ConsoleredText("Zly vstup");
                ShowStorage();
            }
            Console.WriteLine(stars);

            

         
        }

        private static void UpdateProduct()
        {
            Console.WriteLine(stars);
            Console.WriteLine("UPRAVENIE PRODUKTU");
            Console.WriteLine("Vase moznosti su:");
            Console.WriteLine("Stlacte 1 pre pridanie poctu kusov");
            Console.WriteLine("Stlacte 2 pre upravu ceny");
            Console.WriteLine("Stlacte 3 pre opustenie akcie zmeny produktu");

            string choice = Console.ReadLine();
            if (choice == "1")   // exceptions
            {
                ConsolegreenText("Logining in...");
                Console.WriteLine("Zvolte cislo produktu ktoremu chcete pridat kusy");
                int id_p = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Kolko kusov chcete pridat do databazy");
                int pieces = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Zelate si pridat " + pieces + "?   [Y/N] (ANO / NIE)");


                string yesNo = Console.ReadLine();


                if (yesNo == "y" || yesNo == "Y")
                {
                    ProductCase.updateProductAdd(id_p, pieces);
                    ConsolegreenText("Kusy boli uspesne pridane do databazy");
                    ShowStorage();
                }
                else
                {
                    ConsolegreenText("Canceled product addition - Back to storage..");
                    ShowStorage();
                }


            }
            else if (choice == "2") // exceptions
            {
                ConsolegreenText("Logining in...");
                Console.WriteLine("Zvolte cislo produktu ktoremu chcete upravit kusovu cenu");
                int id_p = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Nastavte cenu ($)");
                double price = Convert.ToDouble(Console.ReadLine());

                Console.WriteLine("Zelate si zmenit cenu na " + price + "$ ?   [Y/N] (ANO / NIE)");


                string yesNo = Console.ReadLine();


                if (yesNo == "y" || yesNo == "Y")
                {
                    ProductCase.updateProductPrice(id_p, price);
                    ConsolegreenText("Cena produktu bola uspesne zmenena");
                    ShowStorage();
                }
                else
                {
                    ConsolegreenText("Canceled product addition - Back to storage..");
                    ShowStorage();
                }
            }
            else if (choice == "3")
            {
                ShowAdminMenu();
                return;
            }
            else
            {
                ConsoleredText("Zly vstup");
                ShowStorage();
            }


        }

        private static void AddProduct()
        {
            Console.WriteLine(stars);
            Console.WriteLine("Zadajte nazov noveho produktu:");
            string productName = Console.ReadLine();
            Console.WriteLine("Zadajte kusovu vahu produktu (g)");
            string weight = Console.ReadLine();
            Console.WriteLine("Zadajte kusovu cenu produktu ($)");
            string price = Console.ReadLine();

            Console.WriteLine("Zelate si pridat novy produkt ,," + productName + ",,?  vaha (" + weight + "g),  cena (" + price + "$)   [Y/N] (ANO / NIE)");


            string yesNo = Console.ReadLine();

            
            if (yesNo == "y" || yesNo == "Y")
            {
                int w = Convert.ToInt32(weight);
                double p = Convert.ToDouble(price);
                ProductCase.insertProduct(productName, w, p);
                ConsolegreenText("Produkt bol uspesne pridany do databazy!");
                ShowStorage();
            }
            else
            {
                ConsolegreenText("Canceled product addition - Back to storage..");
                ShowStorage();
            }            

        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public static void ShowProfile(User user)
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(stars);
            Console.WriteLine("<1> Meno: " + user.Name);
            Console.WriteLine("<2> Priezvisko: " + user.Surname);
            Console.WriteLine("<3> Email: " + user.Email);
            Console.WriteLine("<4> Telefonne cislo: " + user.PhoneNum);
            string star = "";
            for(int i = 0; i < user.Password.Length; i++) { star += "*"; }
            Console.WriteLine("<5> Heslo: " + star);
            Console.WriteLine(stars);
            Console.WriteLine("Pre zmenu zvolte cislo udaju, lubovolnou klavesou opustite stranku profilu");

            string caseSwitch = Console.ReadLine();
            string change;
            string yesNo;
            switch (caseSwitch)
            {
                case "1":

                    Console.WriteLine("Zadajte nove meno:");
                    change = Console.ReadLine();
                    Console.WriteLine("Zelate si zmenit meno z " + user.Name + " na " + change + "? [Y/N] (ANO / NIE)");
                    yesNo = Console.ReadLine();
                    if(yesNo == "y" || yesNo == "Y")
                    {
                        ConsolegreenText("Vase meno bolo uspesne zmenene!");
                        user.Name = change;
                        UserCase.Update(user);
                        ShowProfile(user);
                    }
                    else
                    {
                        ShowProfile(user);
                    }
                    break;
                case "2":
                    Console.WriteLine("Zadajte nove priezvisko:");
                    change = Console.ReadLine();
                    Console.WriteLine("Zelate si zmenit priezvisko z " + user.Surname + " na " + change + "? [Y/N] (ANO / NIE)");
                    yesNo = Console.ReadLine();
                    if (yesNo == "y" || yesNo == "Y")
                    {
                        ConsolegreenText("Vase priezvisko bolo uspesne zmenene!");
                        user.Surname = change;
                        UserCase.Update(user);
                        ShowProfile(user);
                    }
                    else
                    {
                        ShowProfile(user);
                    }
                    break;
                case "3":
                    Console.WriteLine("Zadajte novy email:");
                    change = Console.ReadLine();
                    if (IsValidEmail(change))
                    {
                        Console.WriteLine("Zelate si zmenit email z " + user.Email + " na " + change + "? [Y/N] (ANO / NIE)");
                        yesNo = Console.ReadLine();
                        if (yesNo == "y" || yesNo == "Y")
                        {
                            ConsolegreenText("Vas email bol uspesne zmeneny!");
                            user.Email = change;
                            Console.WriteLine(stars);
                            Console.WriteLine("Na vas email vam bol zaslany overovaci link...");
                            UserCase.Update(user);
                            ShowProfile(user);

                        }
                    }
                    ConsoleredText("Email neexistuje!");
                    ShowProfile(user);
                    break;
                case "4":
                    Console.WriteLine("Zadajte nove telefonne cislo:");
                    change = Console.ReadLine();
                    Console.WriteLine("Zelate si zmenit telefonne cislo z " + user.PhoneNum + " na " + change + "? [Y/N] (ANO / NIE)");
                    yesNo = Console.ReadLine();
                    if (yesNo == "y" || yesNo == "Y")
                    {
                        ConsolegreenText("Vase telefonne cislo bolo uspesne zmenene!");
                        user.PhoneNum = change;
                        UserCase.Update(user);
                        ShowProfile(user);

                    }
                    else
                    {
                        ShowProfile(user);

                    }
                    break;
                case "5":
                    Console.WriteLine("Zadajte vase heslo:");
                    change = Console.ReadLine();

                    if(user.Password == change)
                    {
                        Console.WriteLine("Zadajte vase nove heslo:");
                        change = Console.ReadLine();
                    }
                    else
                    {
                        ConsoleredText("Heslo sa nezhoduje!");
                        ShowProfile(user);
                    }
                    if (change.Length < 5)
                    {
                        ConsoleredText("Heslo musi mat aspon 5 znakov");
                        ShowProfile(user);
                    }
                    else
                    {
                        Console.WriteLine("Zelate si zmenit heslo z " + user.Password + " na " + change + "? [Y/N] (ANO / NIE)");
                        yesNo = Console.ReadLine();
                        if (yesNo == "y" || yesNo == "Y")
                        {
                            ConsolegreenText("Vase heslo bolo uspesne zmenene!");
                            user.Password = change;
                            UserCase.Update(user);
                            ShowProfile(user);

                        }
                        else
                        {
                            ShowProfile(user);
                        }
                    }
                    break;

                default:
                    ConsolegreenText("Opustam profil.....");
                    ShowLoggedInMenu();
                    break;
            }
        }

        public static void ShowOrders()
        {

        }

        public static void ShowLoggedInMenu()
        {
            Console.WriteLine(stars);

            User user = new User();
            user = UserCase.getUserByID(currentID);

            Console.WriteLine("Vitaj " + user.Name + " " + user.Surname + "!");
            Console.WriteLine("Vase moznosti su:");
            Console.WriteLine("Stlacte 1 pre zobrazenie profilu");
            Console.WriteLine("Stlacte 2 pre zobrazenie objednavok");
            Console.WriteLine("Stlacte 3 pre ukoncenie");

            string choice = Console.ReadLine();
            if (choice == "1")
            {
                ConsolegreenText("Logining in...");
                ShowProfile(user);
            }
            else if (choice == "2")
            {
                ShowOrders();
            }
            else if(choice == "3")
            {
                Quit();
            }
            else
            {
                ConsoleredText("Zly vstup");
                ShowLoggedInMenu();
            }
            Console.WriteLine(stars);
        }



        static void Main(string[] args)
        {
            ShowMenu();

        }
    }
}
