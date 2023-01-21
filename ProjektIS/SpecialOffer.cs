using DomainLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjektIS
{
    public partial class SpecialOffer : Form
    {
        public SpecialOffer()
        {
            InitializeComponent();
            RefreshData();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //DataView dv = dt.DefaultView;
            //dv.RowFilter = "FirstName LIKE '" + textBox2.Text + "%'";
            //dataGridView1.DataSource = dv;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        protected void RefreshData()
        {
            Collection<DataLayer.Product> product = ProductCase.getProducts();
            BindingList<DataLayer.Product> bindingList = new BindingList<DataLayer.Product>(product);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = bindingList;
            dataGridView1.AllowUserToAddRows = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cart form = new Cart();
            form.ShowDialog();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void SpecialOffer_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int kusy = (int)numericUpDown1.Value;
            int ID_P = (int)dataGridView1.SelectedCells[1].Value;
            string message = "Želáte si pridať do košíka počet kusov: " + kusy;
            string caption = "Pridať tovar do košíka"; 
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes)
            {
                CartCase.insertCartProducts(Login.currentID, ID_P, kusy);  //id produktu zvolenej cell
            }
            else
            {
                numericUpDown1.Value = 1;
            }
        }
    }
}
