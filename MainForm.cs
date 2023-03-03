using POS_Demo.Screens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void اضافةمنتجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Products prodfrm = new Products();
            prodfrm.Show();
        }

        private void اغلاقToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void اضافةمستخدمجديدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewUser newuser = new NewUser();
            newuser.Show();
        }

        private void عرضالمنتجاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductsList prodlist = new ProductsList();
            prodlist.Show();
        }

        private void تعديلمنتجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProductsList p = new ProductsList();
            p.Show();
        }

        private void العملاءToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CustomersManagement cust = new CustomersManagement();
            cust.Show();
        }

        private void الموردينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SuppliersManagement sup = new SuppliersManagement();
            sup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            p.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CustomersManagement cus = new CustomersManagement();
            cus.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SuppliersManagement sup = new SuppliersManagement();
            sup.Show();
        }

        private void فاتورةمبيعاتToolStripMenuItem_Click(object sender, EventArgs e)
        {
            POS_Demo.Screens.SalesBillsFrm frm = new SalesBillsFrm();
            frm.ShowDialog();
        }
    }
}
