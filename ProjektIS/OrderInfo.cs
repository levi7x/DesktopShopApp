using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjektIS
{
    public partial class OrderInfo : Form
    {
        private int id_o;
        public OrderInfo(int id_o)
        {
            InitializeComponent();
            this.id_o = id_o;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void OrderInfo_Load(object sender, EventArgs e)
        {
            try
            {
                string txt = DomainLayer.OrderCase.readFile(Login.currentID, this.id_o);
                label1.Text = txt;
            }
            catch
            {
                string error = "Subor sa nepodarilo načítať!";
                MessageBox.Show(error);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
