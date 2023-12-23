using projactC_2.manage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projactC_2
{
    public partial class Password : Form
    {
        //private bool isDragging = false;
        //private Point lastLocation;
        public Password()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            Users u = new Users();

            string username = UserBox.Text;
            string email = EmailBox.Text;
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(email))

            {

                var user = db.User.SingleOrDefault(us => us.UserName == username && us.Email==email);

                if (user != null)
                {

                   


                    Change change = new Change(user.UserId);
                    change.Show();
                    this.Hide();

                   



                }
                else
                    error.Text = "!Username or Email Is Incorrect";


            }
            else

                error.Text = "!Username Or Password Is Empty";

        }


        private void Password_MouseDown(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    isDragging = true;
            //    lastLocation = e.Location;
            //}

        }


        private void Password_MouseMove(object sender, MouseEventArgs e)
        {

            //if (isDragging)
            //{
            //    this.Location = new Point(
            //        (this.Location.X - lastLocation.X) + e.X,
            //        (this.Location.Y - lastLocation.Y) + e.Y);

            //    this.Update();



            //}
        }

        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button == MouseButtons.Left)
            //{
            //    isDragging = false;
            //}

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit(); 
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home home = new Home(); 
            home.Show();
            this.Close();
        }

       

       

        private void Password_Load(object sender, EventArgs e)
        {

        }
    }
}
