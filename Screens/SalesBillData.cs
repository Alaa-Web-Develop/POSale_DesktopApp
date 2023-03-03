using POS_Demo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo.Screens
{
    public partial class SalesBillData : Form
    {
        POSTutEntities db = new POSTutEntities();
        public SalesBillData()
        {
            InitializeComponent();
            dataGridView2.DataSource = db.salesBill.Select(x => new { x.id, x.Customers.Name, x.Users.UserName, x.date }).ToList();
            
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            //click bill in grid to show its details
            int billId = int.Parse(dataGridView2.CurrentRow.Cells[0].Value.ToString());
            var selectedBill = db.salesBill.FirstOrDefault(x => x.id == billId);
            txtbillid.Text = selectedBill.id.ToString();
            dateTimePicker1.Value = (DateTime)selectedBill.date;
            lbltotal.Text = selectedBill.total.ToString();
            txtdiscount.Text = selectedBill.discount.ToString();
            lbltotalafterdiscount.Text = selectedBill.totalafterdiscount.ToString();

            //customer Name :
            comboBox1.SelectedValue = selectedBill.Customers.id;

            //add products details in grid2 :
            dataGridView1.Rows.Clear();
            foreach (var item in selectedBill.salesBillDetails)
            {
                dataGridView1.Rows.Add(item.Products.id, item.Products.Name, item.price, item.quantity, item.totalPrice, item.Products.Image == null ? new Bitmap(20, 20) : Image.FromFile(item.Products.Image));
            }

        }

        private void SalesBillData_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet8.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter1.Fill(this.pOSTutDataSet8.Customers);

        }
    }
}
