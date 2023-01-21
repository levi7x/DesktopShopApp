using DataLayer;
using DomainLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektIS
{
    public partial class Login : Form
    {
        public static int currentID;
        public Login()
        {
            InitializeComponent();
            label3.Visible = false;
            textBox2.PasswordChar = '*';
            textBox2.MaxLength = 15;
            textBox1.Text = "jj123@gmail.com";
            textBox2.Text = "pw123";
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*
            Menu form = new Menu();
            form.ShowDialog();
            */
            bool flag = false;
            string tx1 = textBox1.Text;
            string tx2 = textBox2.Text;

            try
            {
                if(UserCase.UserExists(tx1, tx2))
                {
                    currentID = UserCase.getUserByEmail(tx1).Id_u;
                    flag = true;
                }
                else { label3.Text = "Nespravne vstupne udaje"; label3.Visible = true; }
            }

            catch
            {
                label3.Text = "Zle zadane vstupne udaje";
                label3.Visible = true;
            }
            if (flag) {
                this.Hide();
                Menu form = new Menu();
                form.ShowDialog();
                this.Dispose();
                
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
