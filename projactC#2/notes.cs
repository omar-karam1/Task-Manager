using projactC_2.manage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Task = projactC_2.manage.Task;

namespace projactC_2
{
    public partial class notes : Form
    {
        private bool isDragging;
        private Point lastLocation;

        int usid;
        public notes(int usid)
        {
            InitializeComponent();
            this.usid=usid;
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            task t = new task(usid) ;
            t.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

            show show = new show(usid);
            show.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home home = new Home(); 
            home.Show();
            this.Close();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           Appcontext db = new Appcontext();
            Task t = new Task();
           
        }

        private void edit_Click(object sender, EventArgs e)
        {


            Appcontext db = new Appcontext();
            Task t = new Task();
            if (listEd.SelectedItem != null)
            {
                Task Selected = db.Tasks.Where(tt => tt.UserID == usid && tt.TaskId == (int)listEd.SelectedItem).FirstOrDefault();
            
                if (Selected != null)
                {
                    Selected.Notes = note.Text;

                    db.Tasks.Update(Selected);
                    db.SaveChanges();
                    MessageBox.Show("Notes Added");




                }
            }
            else
            {
                MessageBox.Show("Select id from the list or add a task first");
               

            }
               




        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }



        private void notes_Load(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            manage.Task task = new manage.Task();
            var selectedtask = db.Tasks
         .Where(u => u.UserID == usid)
         .Select(u => new { u.TaskId })
                              .ToList();



            foreach (var ts in selectedtask)
            {
                int td = ts.TaskId;
                listEd.Items.Add(td);


            }
        }

        private void listEd_SelectedIndexChanged(object sender, EventArgs e)
        {
            Appcontext db =new Appcontext();
            Task task = new Task();
            Task Selected = db.Tasks.Where(t => t.UserID == usid && t.TaskId == (int)listEd.SelectedItem).FirstOrDefault();
            if (Selected != null)
            {
                if (Selected.Notes != null)
                    note.Text = Selected.Notes.ToString();
                else
                    note.Text = "بسم الله الرحمن الرحيم";

            }
            else
                MessageBox.Show("id not found");
        }

        private void idTask_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
