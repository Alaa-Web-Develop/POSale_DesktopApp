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
    public partial class ProductsList : Form
    {
        POSTutEntities db = new POSTutEntities();

        int Id = 0;
        POS_Demo.DB.Products selectedRow;
        string imagePath = "";
        public ProductsList()
        {
            InitializeComponent();
            
            //datasource combobox
            comboBox1.DataSource = db.Cateogry.ToList();
            comboBox2.DataSource = db.Cateogry.ToList(); // combo search


            dataGridView1.DataSource = db.Products.ToList();
            //dataGridView1.DataSource = db.Products.OrderBy(x => x.Price).ToList();
            //dataGridView1.DataSource = db.Products.OrderByDescending(x => x.Name).ToList();
            //max price :
            var maxPrice = db.Products.Max(x => x.Price);
            txtmaxprice.Text = maxPrice.ToString();
            txtmaxpricename.Text = db.Products.SingleOrDefault(x => x.Price == maxPrice).Name;
            txttotalprods.Text = db.Products.Count().ToString(); // return int
            txtavgprice.Text = (db.Products.Sum(x => x.Price) / db.Products.Count()).ToString();
            //= db.products.average( => x.price)

            //pagination :  skip >> sortedList orderBy
            //dataGridView1.DataSource = db.Products.OrderBy(x => x.Price).Skip(2).Take(2).ToList();

            //dataGridView1.DataSource = db.Products.Where(x => x.Name.StartsWith("منتج ج")).ToList();
            txtstr.Text = db.Products.All(x => x.Price == 80).ToString(); // return bool

        }



        private void btnreload_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = db.Products.ToList();
        }

        private void ProductsList_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'pOSTutDataSet6.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter3.Fill(this.pOSTutDataSet6.Products);
            // TODO: This line of code loads data into the 'pOSTutDataSet4.Cateogry' table. You can move, or remove it, as needed.
            this.cateogryTableAdapter.Fill(this.pOSTutDataSet4.Cateogry);
            /*
            // TODO: This line of code loads data into the 'pOSTutDataSet2.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter2.Fill(this.pOSTutDataSet2.Products);
            // TODO: This line of code loads data into the 'pOSTutDataSet1.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter1.Fill(this.pOSTutDataSet1.Products);
         
            */
        }

        private void btnsrch_Click(object sender, EventArgs e)
        {
            if (txtprodname.Text == "")
                dataGridView1.DataSource = db.Products.Where(x => x.Code == txtbarcode.Text).ToList();
            else
                dataGridView1.DataSource = db.Products.Where(x => x.Code == txtbarcode.Text || x.Name.Contains(txtprodname.Text)).ToList();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //catch Id First selected , then cond using id , then show details int textbox

            try
            {
                Id = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                selectedRow = db.Products.SingleOrDefault(x => x.id == Id);
                // MessageBox.Show(id.ToString());
                txtcodeedit.Text = selectedRow.Code;
                txtnameedit.Text = selectedRow.Name;
                txtnotes.Text = selectedRow.Notes;
                txtprice.Text = selectedRow.Price.ToString();
                txtqty.Text = selectedRow.Quantity.ToString();
                pictureBox1.ImageLocation = selectedRow.Image;

                comboBox1.SelectedValue = selectedRow.cateogryId;
            }
            catch (Exception) { }
          
               
            
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            //i catch id shared when change select in grid , then select row and edit values
            // selectedRow = db.Products.SingleOrDefault(x => x.id == Id);
            selectedRow.Name = txtnameedit.Text;
            selectedRow.Code = txtcodeedit.Text;
            selectedRow.Notes = txtnotes.Text;
            selectedRow.Price = decimal.Parse(txtprice.Text);
            selectedRow.Quantity = int.Parse(txtqty.Text);

            selectedRow.cateogryId = int.Parse(comboBox1.SelectedValue.ToString());


            if(imagePath != "")
            {
                string newImagePath = Environment.CurrentDirectory + "\\images\\products" + "prod" + Id + ".jpg";
                File.Copy(imagePath, newImagePath,true); //Must write true to overwrite existed image
                selectedRow.Image = newImagePath;
                
            }

            //must save changes ,لا تحتاج للحفظ على مرحلتين لان Id  معاك , u don't need two transaction:
            
            db.SaveChanges();
            MessageBox.Show("تم التعديل بنجاح", "تم التعديل");

            //then refresh grid
            dataGridView1.DataSource = db.Products.ToList();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = dialog.FileName;
                pictureBox1.ImageLocation = imagePath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var confirmDelete = MessageBox.Show("هل تريد الحذف", "تأكيد الحذف", MessageBoxButtons.YesNo);
            if (confirmDelete == DialogResult.Yes)
            {
                // remove(object selected by find(PK without condition return obj)
                var rowselectedForDelete = db.Products.Find(Id);
                db.Products.Remove(rowselectedForDelete);
                db.SaveChanges();
                //dataGridView1.DataBindings;
                MessageBox.Show("تم الحذف بنجاح");

                dataGridView1.DataSource = db.Products.ToList();
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            p.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int catgrId = int.Parse(comboBox2.SelectedValue.ToString());
            dataGridView1.DataSource = db.Products.Where(x => x.cateogryId == catgrId).ToList();
        }
    }
}
