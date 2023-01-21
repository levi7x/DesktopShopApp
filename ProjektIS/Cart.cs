using DomainLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjektIS
{
    public partial class Cart : Form
    {
        private double totalPrice = 0;
        private double totalWeight = 0;
        private List<DataLayer.Cart> content = new List<DataLayer.Cart>();
        public Cart()
        {
            InitializeComponent();
            
        }

        private void Cart_Load(object sender, EventArgs e)
        {
             this.content = CartCase.getCartContent(Login.currentID);
            foreach (var cart in this.content)
            {
                DataLayer.Product product = new DataLayer.Product();
                product = ProductCase.getProductByID(cart.Id_p);
                double price = product.Price * cart.Pieces;
                int weight = product.Weight * cart.Pieces;
                this.totalPrice += price;
                this.totalWeight += weight;
                this.totalPrice = Math.Round(this.totalPrice, 2);
                string listboxOutput = cart.Id_p.ToString() + "<-ID |" + product.ProductName + ": Počet kusov: " + cart.Pieces.ToString() + " Váha: " + weight.ToString() + "g  Cena: " + price.ToString() + "$"; 
                listBox1.Items.Add(listboxOutput);
            }

            label4.Text = Math.Round(this.totalPrice, 2).ToString();
            label6.Text = this.totalWeight.ToString();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                string text = listBox1.GetItemText(listBox1.SelectedItem);
                string digits = new String(text.TakeWhile(Char.IsDigit).ToArray());
                int id_p = int.Parse(digits);
                CartCase.deleteCartProducts(id_p);
                Cart form = new Cart();
                this.Dispose();
                this.Hide();
                form.Show();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CartCase.clearCart(Login.currentID);
            Cart form = new Cart();
            this.Dispose();
            form.Show();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            if (!CartCase.checkCart(Login.currentID))
            {
                // Initializes the variables to pass to the MessageBox.Show method.
                string message = CartCase.getCartInfo(Login.currentID);
                string caption = "Nedostatok tovaru na sklade";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    CartCase.getCartInfo(Login.currentID, true);
                    Cart form = new Cart();
                    this.Dispose();
                    this.Hide();
                    form.Show();
                }
            }
            else
            {
                if (Convert.ToInt32(label6.Text) < 1000)
                {
                    MessageBox.Show("Objednávka musí mať minimálne 1Kg!");
                }
                else
                {
                    Payment form = new Payment(this.content, this.totalPrice, this.totalWeight);
                    form.Show();
                    this.Hide();
                    this.Dispose();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
