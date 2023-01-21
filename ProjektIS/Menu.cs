using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjektIS
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SpecialOffer form = new SpecialOffer();
            form.ShowDialog();
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            Cart form = new Cart();
            form.ShowDialog();
           
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Order form = new Order();
            form.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Dispose();
            Login form = new Login();
            form.ShowDialog();

        }
    }
}
