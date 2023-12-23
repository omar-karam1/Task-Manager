using projactC_2.manage;
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

namespace projactC_2
{
    public partial class control : Form
    {
        public control()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            var allItems = db.User.ToList(); 
            db.User.RemoveRange(allItems); 
            db.SaveChanges();
            MessageBox.Show("Delete successfully");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Appcontext db = new Appcontext();
            var allItems = db.Tasks.ToList();
            db.Tasks.RemoveRange(allItems);
            db.SaveChanges();
            MessageBox.Show("Delete successfully");
        }
    }
}
