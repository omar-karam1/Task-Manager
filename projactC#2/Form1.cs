using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using projactC_2.manage;


namespace projactC_2
{
    public partial class Home : Form
    {
        private bool isDragging;
        private Point lastLocation;
        public Home()
        {
            InitializeComponent();
        }



       

        private void button1_Click_1(object sender, EventArgs e)
        {


            Appcontext db = new Appcontext();
            Users u = new Users();

            string username = UserBox1.Text;
            string pass = PasswBox2.Text;
            if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(pass))

            {

                var user = db.User.SingleOrDefault(us => us.UserName == username);
              
                if (user != null)
                {
                   
                    bool passwordMatches = BCrypt.Net.BCrypt.Verify(PasswBox2.Text.ToString(), user.Password.ToString());
                    

                    if (passwordMatches)
                    {
                        int usid = user.UserId;
                        task tasks = new task(usid);
                        tasks.Show();
                        this.Hide();

                    }

                    else
                        error.Text = "! password Is Incorrect";

                  

                }
                else
                    error.Text = "!Username Is Incorrect";


            }
            else
            
                error.Text = "!Username Or Password Is Empty";
            


           


        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
          
            Application.Exit();
        }




        private void Home_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void Home_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Home_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Password password = new Password();
            password.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.Show();
            this.Hide();
        }
    }
}
