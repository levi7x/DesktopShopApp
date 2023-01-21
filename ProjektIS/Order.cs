using Microsoft.VisualBasic;
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
    public partial class Order : Form
    {
        public Order()
        {
            InitializeComponent();
            RefreshData();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void RefreshData()
        {
            Collection<DataLayer.Order> list = DomainLayer.OrderCase.getOrdersbyID(Login.currentID);
            //listBox1.Items.Add("ID | DATUM");
            //listBox1.Items.Add("---------------------");

            foreach (var order in list)
            {
                string str = "";
                str +=  order.Id_o + "<- ID OBJEDNAVKY| " + "DATUM -> " + order.Time.ToString();
                listBox1.Items.Add(str);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                int id_o = getIDofSelectedItem();
                OrderInfo form = new OrderInfo(id_o);
                form.Show();
            }
            catch
            {

            }
        }

        private int getIDofSelectedItem()
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            string digits = new String(text.TakeWhile(Char.IsDigit).ToArray());
            int id_o = int.Parse(digits);
            return id_o;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                try
                {
                    int id_o = getIDofSelectedItem();
                    Notification form = new Notification(id_o);
                   
                    form.Show();
                }
                catch {
                    MessageBox.Show("Presla urcita doba-");
                }
            }
        }

        private void Order_Load(object sender, EventArgs e)
        {

        }
    }
}
