using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjektIS
{
    public partial class Payment : Form
    {
        private List<DataLayer.Cart> content = new List<DataLayer.Cart>();
        private double totalPrice;
        private double totalWeight;
        public Payment(List<DataLayer.Cart> list, double totalPrice, double totalWeight)
        {
            InitializeComponent();
            this.content = list;
            this.totalPrice = totalPrice;
            this.totalWeight = totalWeight;
            label6.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string message = "Naozaj chcete zaplatiť?";
            string caption = "Platba";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);


            string cardNum = textBox1.Text;
            string cvc = textBox6.Text;
            string expD = textBox3.Text + '/' + textBox5.Text;

            if (DomainLayer.PaymentCase.IsCreditCardInfoValid(cardNum, expD, cvc))
            {

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    
                    expD = textBox3.Text + textBox5.Text;
                    /*
                    DateTime time = DateTime.Now; // cas vzniku objednavky
                    DomainLayer.OrderCase.insertOrder(Login.currentID, Login.currentID, false, time); // prida sa objednavka do DB

                    foreach (var cart in this.content)
                    {
                        DomainLayer.ProductCase.updateProductPop(cart.Id_p, cart.Pieces);
                    }


                    DomainLayer.OrderCase.generateFile(this.content, time, Login.currentID, this.totalPrice, this.totalWeight); // vytvori sa CSV s info o objednavke
                    int id_o = DomainLayer.OrderCase.getOrderID(Login.currentID, time);
                    DomainLayer.CartCase.clearCart(Login.currentID);    // vymazeme itemy z kosika
                    expD = textBox3.Text + textBox5.Text;
                    DomainLayer.PaymentCase.insertCard(id_o, cardNum, cvc, expD); // prida kartu do DB

                    */

                    DomainLayer.PaymentCase.MakePayment(Login.currentID,this.content, expD, cvc, cardNum, this.totalPrice, this.totalWeight);

                    MessageBoxButtons okButton = MessageBoxButtons.OK;
                    string msg = "Vaša platba prebehla úspešne";
                    MessageBox.Show(msg, caption, okButton);

                    this.Dispose();
                    this.Hide();
                }
            }
            else
            {
                label6.Visible = true;
            }
        }


        private void Payment_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 19;
            textBox3.MaxLength = 2;
            textBox6.MaxLength = 3;
            textBox5.MaxLength = 4;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 4 || textBox1.Text.Length == 9 || textBox1.Text.Length == 14)
            {
                textBox1.Text = textBox1.Text + "*";
                textBox1.SelectionStart = textBox1.Text.Length;
            }
        }
    }
}
