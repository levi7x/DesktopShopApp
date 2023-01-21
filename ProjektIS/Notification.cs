using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjektIS
{
    public partial class Notification : Form
    {
        private int id_o;
        public Notification(int id_o)
        {
            InitializeComponent();
            this.id_o = id_o;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Notification_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string notification = "";
            notification = richTextBox1.Text;
            if (notification.Length > 10)
            {
                DomainLayer.OrderCase.Update(id_o, notification);
            }
            else
            {
                string txt = "Notifikacia musi mat aspon 10 znakov";
                MessageBox.Show(txt);
            }
        }
    }
}
