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
    public partial class SalesBillsFrm : Form
    {
        POSTutEntities db = new POSTutEntities();
        List<POS_Demo.DB.Products> products;
        public SalesBillsFrm()
        {
            InitializeComponent();
            comboBox1.DataSource = db.Customers.Where(x => x.IsActive == true).ToList();
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "id";

            products = db.Products.ToList();
            //imageList1.ImageSize = new Size(70, 70);

            lbluser.Text = POS_Demo.users.Name;
        }

        private void SalesBillsForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet7.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.pOSTutDataSet7.Customers);

            //1 Loop products to add images in imagelist - 2 add items in listview :
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Image != null)
                {
                    imageList1.Images.Add(Image.FromFile(products[i].Image));
                }
                else
                {
                    //put def image
                    Bitmap defimg = new Bitmap(70, 70);
                    imageList1.Images.Add(defimg);
                }
                ListViewItem item = new ListViewItem();
                item.Text = products[i].Name;
                item.ImageIndex = i;
                item.Tag = products[i];

                listView1.Items.Add(item);
            }

        }

        //calc Method :
        void calcTotal()
        {
            //loop grid row
            try
            {
                decimal total = 0;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    total += decimal.Parse(dataGridView1.Rows[i].Cells["totalPrice"].Value.ToString());
                }
                lbltotal.Text = total.ToString();
                decimal discount = decimal.Parse(txtdiscount.Text);
                lbltotalafterdiscount.Text = (total - discount).ToString();
            }
            catch (Exception ex) { }
           
            
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
         {
            //catch item in listview
            if (listView1.SelectedItems.Count > 0 ) //sure that you select item
            {
                var selectedProd = (POS_Demo.DB.Products)listView1.SelectedItems[0].Tag; // selected prod
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    if (dataGridView1.Rows[i].Cells["id"].Value.ToString() == selectedProd.id.ToString())
                    {
                        dataGridView1.Rows[i].Cells["quantity"].Value = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) + 1;
                        dataGridView1.Rows[i].Cells["totalPrice"].Value = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()) * decimal.Parse(dataGridView1.Rows[i].Cells["price"].Value.ToString());

                        calcTotal();
                        return; // stop excute the following
                    }
                }
                dataGridView1.Rows.Add(selectedProd.id,selectedProd.Name,selectedProd.Price,1,selectedProd.Price * 1,selectedProd.Image == null?new Bitmap(40,40) : Image.FromFile(selectedProd.Image));
                calcTotal();
            }
        }

        private void txtdiscount_TextChanged(object sender, EventArgs e)
        {
            calcTotal();
        }

        private void txtdiscount_KeyPress(object sender, KeyPressEventArgs e)
        {
            //prevent user to enter characters , enter only numbers to handle null error in discount text box 

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                 (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //add bill data then details date , add in db
            POS_Demo.DB.salesBill bill = new salesBill
            {
                cust_id = int.Parse(comboBox1.SelectedValue.ToString()),
                date = (DateTime)dateTimePicker1.Value.Date,
                discount = decimal.Parse(txtdiscount.Text),
                total = decimal.Parse(lbltotal.Text),
                totalafterdiscount = decimal.Parse(lbltotalafterdiscount.Text),
                //userId = POS_Demo.users.Id
            };

            List<POS_Demo.DB.salesBillDetails> billdetails = new List<salesBillDetails>();
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                billdetails.Add(new salesBillDetails
                {
                    prod_id = int.Parse(dataGridView1.Rows[i].Cells["id"].Value.ToString()),
                    price = decimal.Parse(dataGridView1.Rows[i].Cells["Price"].Value.ToString()),
                    quantity = int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString()),
                    totalPrice = decimal.Parse(dataGridView1.Rows[i].Cells["Price"].Value.ToString()) * int.Parse(dataGridView1.Rows[i].Cells["quantity"].Value.ToString())

                }) ;
            }
            bill.salesBillDetails = billdetails;
            db.salesBill.Add(bill);
            db.SaveChanges();
            MessageBox.Show("  تم حفظ الفاتورة رقم" + bill.id);

        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            calcTotal();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            POS_Demo.Screens.SalesBillData frm = new SalesBillData();
            frm.Show();
        }
    }
}
