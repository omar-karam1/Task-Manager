using Microsoft.VisualBasic.ApplicationServices;
using projactC_2.manage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace projactC_2
{
    public partial class Change : Form
    {
        private bool isDragging;
        private Point lastLocation;
        int id;
        public Change(int id)
        {
            
            InitializeComponent();
            this.id = id;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            Users u = new Users();
            string password = PasswBox2.Text;
            string password1 = PasswBox.Text;
            if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(password1))
            {
                if (password == password1)
                {

                    Users u1 = db.User.Where(i => i.UserId == id).FirstOrDefault();
                    if (u != null)
                    {
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password.ToString());
                        u1.Password = hashedPassword;
                        db.User.Update(u1);
                        db.SaveChanges();

                        Home home = new Home();
                        home.Show();
                        this.Close();
                    }
                    else
                        error.Text = "!not found acount";
                }

                else
                    error.Text = "!password does not match";

            }
            else
                error.Text = "!Password is Empty";



        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Change_Load(object sender, EventArgs e)
        {

        }

        private void Change_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void Change_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Change_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }



        public bool IsStrongPassword(string password)
        {

            

            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");

            if (regex.IsMatch(password))

                return true;


            return false;

        }

        private void PasswBox2_Validating(object sender, CancelEventArgs e)
        {


            if (!IsStrongPassword(PasswBox2.Text))
            {
                errorProvider1.SetError(PasswBox2, "The password is too weak.\nThe password must contain uppercase and lowercase letters,\n numbers, and signs, such as “@ # $.”");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(PasswBox2, null);
            }
        }

       
    }
}
