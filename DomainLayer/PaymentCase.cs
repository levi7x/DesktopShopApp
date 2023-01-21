using DataLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DomainLayer
{
    public class PaymentCase
    {

        public static bool MakePayment(int currentID, List<Cart> content, string expD, string cvc, string cardNum, double totalPrice, double totalWeight)
        {
            DateTime time = DateTime.Now; // cas vzniku objednavky
            DomainLayer.OrderCase.insertOrder(currentID, currentID, false, time); // prida sa objednavka do DB

            foreach (var cart in content)
            {
                DomainLayer.ProductCase.updateProductPop(cart.Id_p, cart.Pieces); // odstrani kusy z DB
            }


            try
            {
                DomainLayer.OrderCase.generateFile(content, time, currentID, totalPrice, totalWeight); // vytvori sa CSV s info o objednavke
            }
            catch
            {
                return false;
            }


            int id_o = DomainLayer.OrderCase.getOrderID(currentID, time);
            DomainLayer.CartCase.clearCart(currentID);    // vymazeme itemy z kosika
            insertCard(id_o, cardNum, cvc, expD); // prida kartu do DB
            return true;
        }



        public static void insertCard(int id_o, string cardNum, string cvc, string MMYYY)
        {
            Payment payment = new Payment();
            payment.Id_p = DataLayer.Gateway.PaymentGateway.GetMaxId() + 1;   // nahradenie autoincrementu pre Payment ID na ktory som zabudol
            payment.Id_o = id_o;
            payment.CVC = Convert.ToInt32(cvc);
            payment.CardNum = cardNum;
            payment.MMYYYY = MMYYY;
            DataLayer.Gateway.PaymentGateway.Insert(payment);       
        }

        public static bool IsCreditCardInfoValid(string cardNo, string expiryDate, string cvv)
        {
            var cardCheck = new Regex(@"^(1298|1267|4512|4567|8901|8933)([\-\s]?[0-9]{4}){3}$");
            var monthCheck = new Regex(@"^(0[1-9]|1[0-2])$");
            var yearCheck = new Regex(@"^20[0-9]{2}$");
            var cvvCheck = new Regex(@"^\d{3}$");

           // if (!cardCheck.IsMatch(cardNo)) // <1>check card number is valid
           //     return false;
            if (!cvvCheck.IsMatch(cvv)) // <2>check cvv is valid as "999"
                return false;

            var dateParts = expiryDate.Split('/'); //expiry date in from MM/yyyy            
            if (!monthCheck.IsMatch(dateParts[0]) || !yearCheck.IsMatch(dateParts[1])) // <3 - 6>
                return false; // ^ check date format is valid as "MM/yyyy"

            var year = int.Parse(dateParts[1]);
            var month = int.Parse(dateParts[0]);
            var lastDateOfExpiryMonth = DateTime.DaysInMonth(year, month); //get actual expiry date
            var cardExpiry = new DateTime(year, month, lastDateOfExpiryMonth, 23, 59, 59);

            //check expiry greater than today & within next 6 years <7, 8>>
            return (cardExpiry > DateTime.Now && cardExpiry < DateTime.Now.AddYears(6));
        }
    }
}
