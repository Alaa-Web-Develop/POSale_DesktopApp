using POS_Demo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo.Screens
{

    public partial class CustomersManagement : Form
    {
        POSTutEntities db = new POSTutEntities();
        string imagePath = "";
        int id = 0;
        POS_Demo.DB.Customers selectedRow;
        public CustomersManagement()
        {
            InitializeComponent();
            checkBox1.Checked = false; // initial value 
            dataGridView1.DataSource = db.Customers.ToList();
        }

        private void btnsrch_Click(object sender, EventArgs e)
        {
            if (txtsrchname.Text == "")
            {
                dataGridView1.DataSource = db.Customers.Where(x => x.CustPhone.Contains(txtsrchphone.Text)).ToList();
            }
            else
            {
                dataGridView1.DataSource = db.Customers.Where(x => x.CustPhone.Contains(txtsrchphone.Text) || x.Name.Contains(txtsrchname.Text)).ToList();
            }
        }

        private void btnreload_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Customers.ToList();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            POS_Demo.DB.Customers cust = new POS_Demo.DB.Customers();
            var resMsgBox = MessageBox.Show("تأكيد الاضافة", "تأكيد الاضافة", MessageBoxButtons.YesNo);
            if(resMsgBox == DialogResult.Yes)
            {
                
                if (txtname.Text == "" || txtphone.Text == "")
                {
                    MessageBox.Show("رجاء اكمال البيانات المطلوبة");
                }
                else if(db.Customers.Where(x => x.CustPhone == txtphone.Text).Count() > 0) { MessageBox.Show("رقم هاتف موجود بالفعل"); }

                else
                {
                    cust.Name = txtname.Text;
                    cust.Address = txtaddress.Text;
                    cust.Notes = txtnotes.Text;
                    cust.Compaby = txtcomname.Text;
                    cust.CustPhone = txtphone.Text;
                    cust.IsActive = checkBox1.Checked;

                    db.Customers.Add(cust);
                    db.SaveChanges();

                    //Adding new image path:

                    if (imagePath != "")
                    {
                        string newimagePath = Environment.CurrentDirectory + "\\images\\customers" + cust.id + ".jpg";
                        File.Copy(imagePath, newimagePath);
                        cust.Image = newimagePath;
                        db.SaveChanges();
                        MessageBox.Show("تم حفظ بيانات العميل الجديد");
                    }
                }

                //clear all fields
                txtaddress.Text = "";
                txtcomname.Text = "";
                txtname.Text = "";
                txtnotes.Text = "";
                txtphone.Text = "";
                checkBox1.Checked = false;
                pictureBox1.ImageLocation = "";

                //update grid
                dataGridView1.DataSource = db.Customers.ToList();

            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog()==DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = imagePath;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                selectedRow = db.Customers.SingleOrDefault(x => x.id == id);

                txtaddress.Text = selectedRow.Address;
                txtcomname.Text = selectedRow.Compaby;
                txtname.Text = selectedRow.Name;
                txtnotes.Text = selectedRow.Notes;
                txtphone.Text = selectedRow.CustPhone;
                if(selectedRow.IsActive == null)selectedRow.IsActive = false;
                checkBox1.Checked = (bool)selectedRow.IsActive;
                pictureBox1.ImageLocation = selectedRow.Image;
            }
            catch (Exception)
            {

                
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            var resDelete = MessageBox.Show("تأكيد الحذف", "تأكيد الحذف", MessageBoxButtons.YesNo);
            if (resDelete == DialogResult.Yes)
            {
                var SelectedDeletedRow = db.Customers.Find(id);
                db.Customers.Remove(SelectedDeletedRow);
                db.SaveChanges();
                MessageBox.Show("تم الحذف بنجاح");
                dataGridView1.DataSource = db.Customers.ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e) // Edit
        {
            var result = MessageBox.Show("تأكيد التعديل", "تأكيد التعديل", MessageBoxButtons.YesNo);
            
                if(db.Customers.Where(x => x.CustPhone == txtphone.Text && x.id != id).Count() > 0)
                {
                    MessageBox.Show("هذا العميل موجود بالفعل");
                return; // stop excute prog
                }
            if (result == DialogResult.Yes)
            {
                selectedRow.Name = txtname.Text;
                selectedRow.Notes = txtnotes.Text;
                selectedRow.Compaby = txtcomname.Text;
                selectedRow.Address = txtaddress.Text;
                selectedRow.CustPhone = txtphone.Text;
                selectedRow.IsActive = checkBox1.Checked;

                if (imagePath != "")
                {
                    string newImagePath = Environment.CurrentDirectory + "\\images\\customers" + id + ".jpg";
                    File.Copy(imagePath, newImagePath, true);

                }

                db.SaveChanges();
                MessageBox.Show("تم تعديل البيانات بنجاح");
                dataGridView1.DataSource = db.Customers.ToList();
            }
            
        }
    }
}
