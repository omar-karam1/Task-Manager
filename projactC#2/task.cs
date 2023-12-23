using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using projactC_2.manage;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
using Task = projactC_2.manage.Task;

namespace projactC_2
{

    public partial class task : Form
    {
        //نتفكيشن
        private Timer timer;
        private NotifyIcon notifyIcon;

        //تحريك الشاشة او الفورم
        private bool isDragging;
        private Point lastLocation;

        //يستخدم للبحث عن اليوزر
        int usid;

        public task(int usid)
        {
            //فنكشن النتفكيشن
            InitializeNotifyIcon();
            InitializeComponent();

            this.usid = usid;

        }

        private void InitializeTimer()
        {
            
           DateTime time = TimeStart.Value;
           TimeSpan timeDifference = time - DateTime.Now;

            timer = new Timer();
            timer.Interval = (int)timeDifference.TotalMilliseconds;
            timer.Tick += timer_Tick;
            
            timer.Start();

            int hours = timeDifference.Hours;
            int minutes = timeDifference.Minutes;
            int seconds = timeDifference.Seconds;
            MessageBox.Show("Be ready for the task after : " + hours.ToString() + " hh : "+ minutes.ToString() + " mm : " + seconds.ToString() + " ss ");

            
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Information;
            notifyIcon.Visible = true;
        }





        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.Show();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            show show = new show(usid);
            show.Show();
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void save_Click(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            manage.Task task = new manage.Task()
            {
                Title = titleBox.Text,
                Discription = richTextBox1.Text,
                TimeStart = TimeStart.Value,
                TimeEnd = Dend.Value,
                Priority = (priority)pirBox.SelectedIndex,
                TaskType = (TaskType)typBox.SelectedIndex,
                check = (Checked)checkBox1.CheckState,
                UserID = usid


            };

            DateTime time = TimeStart.Value;

            if (string.IsNullOrEmpty(titleBox.Text))
                MessageBox.Show("Title is empty");

            else if (string.IsNullOrEmpty(typBox.Text))
                MessageBox.Show("Task Type is empty");

            else if (string.IsNullOrEmpty(pirBox.Text))
                MessageBox.Show("priority is empty");

            else if(DateTime.Now > time || TimeStart.Value > Dend.Value)
                MessageBox.Show("Please enter a valid time");

           
            else
            {

                 db.Add(task);
                 db.SaveChanges();
                 InitializeTimer();

                idSearch.Items.Clear();
                ListBox();


            }



        }

        private void timer_Tick(object sender, EventArgs e)
        {

            if (Dend.Value > TimeStart.Value)
            {
                notifyIcon.ShowBalloonTip(4000, "It's Task time", "لا تؤجل عمل اليوم إلى الغد ", ToolTipIcon.Info);
                timer.Stop();
            }
            else
                timer.Stop();
        }
    

    
        private void pictureBox5_Click(object sender, EventArgs e)
        {


            notes notes = new notes(usid);  
            notes.Show();
            this.Close();

        }

        private void edit_Click(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            Task task = new Task();
            if (idSearch.SelectedItem != null)
            {

                Task Selected = db.Tasks.Where(t => t.UserID == usid && t.TaskId == (int)idSearch.SelectedItem).FirstOrDefault();

                if (Selected != null)
                {
                    Selected.Title = titleBox.Text;
                    Selected.Discription = richTextBox1.Text;
                    Selected.TimeStart = TimeStart.Value;
                    Selected.TimeEnd = Dend.Value;
                    Selected.Priority = (priority)pirBox.SelectedIndex;
                    Selected.TaskType = (TaskType)typBox.SelectedIndex;
                    Selected.check = (Checked)checkBox1.CheckState;

                    db.Tasks.Update(Selected);
                    db.SaveChanges();
                    MessageBox.Show("Updated successfully");
                    this.Refresh();





                }
            }
            else
                MessageBox.Show("listBos is Empty");


        }

        private void delete_Click(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            manage.Task task = new manage.Task();

            if (idSearch.SelectedItem != null)
            {
                Task Selected = db.Tasks.Where(t => t.UserID == usid && t.TaskId == (int)idSearch.SelectedItem).FirstOrDefault();

           

                if (Selected != null)
                {
                    db.Tasks.Remove(Selected);
                    db.SaveChanges();

                    MessageBox.Show("Delete successfully");
                    this.Invalidate();
                    this.Refresh();
                    idSearch.Items.Clear();
                    ListBox();




                }
            }
            else
                MessageBox.Show("listBos is Empty");




        }


        private void Show_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void task_Load(object sender, EventArgs e)
        {






            ListBox();

            TimeStart.Format = DateTimePickerFormat.Custom;
            TimeStart.ShowUpDown = true;
            TimeStart.CustomFormat = "MM/dd/yyyy hh:mm tt"; 
            


            Dend.Format = DateTimePickerFormat.Custom;
            Dend.ShowUpDown = true;
            Dend.CustomFormat = "MM/dd/yyyy hh:mm tt";
           


        }
         
        public void ListBox ()
        {
            Appcontext db = new Appcontext();
            manage.Task task = new manage.Task();
            var selectedtask = db.Tasks.Where(u => u.UserID == usid).Select(u => new { u.TaskId }).ToList();

            foreach (var ts in selectedtask)
            {
                int td = ts.TaskId;
                idSearch.Items.Add(td);


            }
        }

        public void Showdata()
        {

            Appcontext db = new Appcontext();
            Task task = new Task();


            var select = db.Tasks.Where(u => u.UserID == usid && (int)idSearch.SelectedItem == u.TaskId)
         .Select(u => new { u.TaskId, u.Title, u.Discription, u.TimeStart, u.TimeEnd, u.Priority, u.TaskType, u.check, u.Notes })
                       .ToList();

            ShowData.DataSource = select.ToList();

        }

        public void showBox()
        {

            Appcontext db = new Appcontext();
            Task task = new Task();
            Task Selected = db.Tasks.Where(t => t.UserID == usid && t.TaskId == (int)idSearch.SelectedItem).FirstOrDefault();

            if (Selected != null)
            {
                titleBox.Text = Selected.Title;
                richTextBox1.Text = Selected.Discription;
                TimeStart.Value = (DateTime)Selected.TimeStart;
                Dend.Value = Selected.TimeEnd;
                pirBox.SelectedIndex = (int)Selected.Priority;
                typBox.SelectedIndex = (int)Selected.TaskType;



            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            Showdata();
            showBox();

      
            


        }

       

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void task_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
            lastLocation = e.Location;
        }

        private void task_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void task_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

       

        

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
          
        }

        private void textBoxTime_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
