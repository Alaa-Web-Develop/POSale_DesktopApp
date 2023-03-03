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
    public partial class Products : Form
    {
        POSTutEntities db = new POSTutEntities();
        string imagePath = "";

        public Products()
        {
            InitializeComponent();
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            POS_Demo.DB.Products prod = new POS_Demo.DB.Products();
            // sure it's prod class in EF using namespace

            if(txtcode.Text =="" || txtname.Text =="" || txtprice.Text == "" || comboBox1.SelectedValue == null)
            {
                MessageBox.Show("برجاء اكمال البيانات المطلوبة");
            }
            else
            {
                prod.Code = txtcode.Text;
                prod.Name = txtname.Text;
                prod.Notes = txtnotes.Text;

                int price, qty;
                int.TryParse(txtprice.Text, out price);
                int.TryParse(txtqty.Text,out qty);

                prod.Price = price;
                prod.Quantity = qty;

                prod.cateogryId = int.Parse(comboBox1.SelectedValue.ToString());

                db.Products.Add(prod);
                db.SaveChanges();
                MessageBox.Show("تم اضافة منتج جديد بنجاح");

                //save prod Image :

                if (imagePath != "")
                {
                    string newPath = Environment.CurrentDirectory + "\\images\\products" + prod.id + ".jpg";
                    File.Copy(imagePath, newPath);
                    prod.Image = newPath;
                    db.SaveChanges();
                    MessageBox.Show("تم حفظ صورة المنتج");
                }
            }
            //aftr save clear fields :

            txtcode.Text = "";
            txtname.Text = "";
            txtnotes.Text = "";
            txtprice.Text = "";
            txtqty.Text = "";
            pictureBox1.ImageLocation = "";

           
           

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = imagePath;

            }
        }

        private void Products_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet3.Cateogry' table. You can move, or remove it, as needed.
            this.cateogryTableAdapter.Fill(this.pOSTutDataSet3.Cateogry);

        }
    }
}
