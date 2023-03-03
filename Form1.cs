using POS_Demo.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Demo
{
    public partial class Form1 : Form
    {
        POSTutEntities db = new POSTutEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var result = db.Users.FirstOrDefault(x => x.UserName == txtuser.Text && x.Password == txtpass.Text);
            //return List 
            if (result !=null)
            {
                this.Close();

                //new thread new task in memory  لن ينفذ الأمرين معا
                Thread th = new Thread(OpenMain);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();

                users.Name = result.UserName;
                users.Id = result.id;
            }
            else
            {
                MessageBox.Show("بيانات المستخدم خاطئة أعد المحاولة");
            }




            
        }
        void OpenMain()
        {
            Application.Run(new MainForm());
        }
    }
    static class users
    {
        static public string Name { get; set; }
        static public int Id { get; set; }
    }
}
