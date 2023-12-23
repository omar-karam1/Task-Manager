using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using projactC_2.manage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace projactC_2
{
    public partial class Register : Form
    {
        private bool isDragging;
        private Point lastLocation;
        public Register()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.Image = new Bitmap(dialog.FileName);

            }
           
                
                
        }

       

        private void Singup_Click(object sender, EventArgs e)
        {

            
            Appcontext db = new Appcontext();


            if (pictureBox2.Image == null)
            {

                MessageBox.Show("Please enter a photo");
            }
            else
            {
                MemoryStream memoryStream = new MemoryStream();
                pictureBox2.Image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                var picture = memoryStream.ToArray();
               
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(PasswBox5.Text.ToString());


                Users user1 = new Users()
                {

                    Fname = FnameBox.Text,
                    Lname = LnameBox2.Text,
                    UserName = UserBox4.Text,
                    Email = EmailBox.Text,
                    Password = hashedPassword,
                    BirthDate = DateBox.Value,
                    ImageData = picture,


                };

                

               


                bool isFirstNameValid = string.IsNullOrWhiteSpace(FnameBox.Text);
                bool isLastNameValid = string.IsNullOrWhiteSpace(LnameBox2.Text);
                bool isEmailValid = string.IsNullOrWhiteSpace(UserBox4.Text);
                bool isPasswordValid = string.IsNullOrWhiteSpace(EmailBox.Text);
                bool isUsernameValid = string.IsNullOrWhiteSpace(PasswBox5.Text);

                if (isFirstNameValid || isLastNameValid || isEmailValid || isPasswordValid || isUsernameValid)
                {
                    MessageBox.Show("Please enter all required data");
                   
                }

               else
                {

                    db.Add(user1);
                    db.SaveChanges();
                    MessageBox.Show("Account successfully created");
                    Home home = new Home();
                    home.Show();
                    this.Close();
                }
            }
        }

        private void EmailBox_Validating(object sender, CancelEventArgs e)
        {



                       string userEmail = EmailBox.Text;

                    if (!(userEmail.EndsWith("@gmail.com") || userEmail.EndsWith("@hotmail.com")))
                    {
                       
                        errorProvider1.SetError(EmailBox, "Please enter a valid email.");
                           e.Cancel = true;
                    }
                    else
                    errorProvider1.SetError(EmailBox, null);

        }

           



        private void FnameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            { 
                e.Handled = true;
            
            }
        }

        private void LnameBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;

            }
            
        }



        private void UserBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            
                if (UserBox4.TextLength == 0)
                {
                   
                    if (!char.IsLetter(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                }

            
        }

        private void UserBox4_Validating(object sender, CancelEventArgs e)
        {
            Appcontext db = new Appcontext();
            Users users = new Users();
            string username = UserBox4.Text;

            var test = db.User
       .Where(us => us.UserName == username)
       .Select(us => new { us.UserName })
                            .ToList();


            bool testuser = false;
           
            foreach (var ts in test)
            {
                
                if (ts.UserName == username)
                    testuser = true;


            }
            if (testuser)
            {
                e.Cancel = true;
            errorProvider1.SetError(UserBox4, "Username is invalid.");
            }
            else
            {
            e.Cancel = false;
            errorProvider1.SetError(UserBox4, null);
            }
           
        }



        private void FnameBox_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(FnameBox.Text))
                errorProvider1.SetError(FnameBox, "Please enter yout First Name.");
            else 
                errorProvider1.SetError(FnameBox, null);



        }

        private void LnameBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LnameBox2.Text))
                errorProvider1.SetError(LnameBox2, "Please enter yout Last Name.");
            else
                errorProvider1.SetError(LnameBox2, null);



        }



        private void PasswBox5_Validating(object sender, CancelEventArgs e)
        {

            

            if (!IsStrongPassword(PasswBox5.Text))
            {
                errorProvider1.SetError(PasswBox5, "The password is too weak.\nThe password must contain uppercase and lowercase letters,\n numbers, and signs, such as “@ # $.”");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(PasswBox5, null);
            }
        }

        public bool IsStrongPassword(string password)
        {

            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");

            if (regex.IsMatch(password))
                return true;

                return false;

        }   
        private void Register_Load(object sender, EventArgs e)
        {
            FnameBox.Focus();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Register_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void Register_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Register_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void EmailBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (EmailBox.TextLength == 0)
            {

                if (!char.IsLetter(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
    }
    }

