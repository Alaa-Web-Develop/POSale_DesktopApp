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
    public partial class NewUser : Form
    {
        //obj from EF Container
        POSTutEntities db = new POSTutEntities();
        string imagePath = "";
        public NewUser()
        {
            InitializeComponent();
           // MessageBox.Show(Environment.CurrentDirectory);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
            Users obj = new Users();
            obj.UserName = txtname.Text;
            obj.Password = txtpass.Text;
            

            db.Users.Add(obj); // save in memory only
            db.SaveChanges(); // save in DB;
            

            //Save Image : 1 - copy in solution , 2 - save in DB

            string newImagePath = Environment.CurrentDirectory + "\\images\\users\\" + obj.id + ".jpg"; //obj.id : appear after save row in db
            File.Copy(imagePath,newImagePath); // source , dist
            obj.Image = newImagePath; // save in db the filename in solution not in local
            db.SaveChanges();

            MessageBox.Show("تم حفظ بيانات المستخدم الجديد");

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
    }
}
