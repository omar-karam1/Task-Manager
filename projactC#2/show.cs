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
using Microsoft.EntityFrameworkCore;
using projactC_2.manage;
using Task = projactC_2.manage.Task;

namespace projactC_2
{
    public partial class show : Form
    {
        int usid;
        private bool isDragging;
        private Point lastLocation;
        public show(int usid)
        {
            InitializeComponent();
            this.usid = usid;
        }

        private void show_Load(object sender, EventArgs e)
        {

            showData();
        }
        
        public void showData()
        {
            Appcontext db = new Appcontext();
            Task task1 = new Task();

            var select = db.Tasks
           .Where(u => u.UserID == usid)
           .Select(u => new { u.TaskId, u.Title, u.Discription, u.TimeStart, u.TimeEnd, u.Priority, u.TaskType, u.check, u.Notes })
                                .ToList();


            dataGridView1.DataSource = select.ToList();

        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            notes notes = new notes(usid);
            notes.Show();   
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            task task = new task(usid);
            task.Show();
            this.Close();
        }



        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void show_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void show_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void show_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Appcontext db=new Appcontext();

            
           
           var selectedtask = db.Tasks.Where(u => u.UserID == usid).Select(u => new { u.TaskId }).ToList();
 

            var user = db.User.Include(u => u.Tasks).FirstOrDefault(u => u.UserId ==usid);

          
                if (user != null)
                {
                    db.Tasks.RemoveRange(user.Tasks);
                    db.SaveChanges(); 
                    MessageBox.Show("Delete successfully");
                   showData();
                }
            
            else
                MessageBox.Show("not found task");


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
