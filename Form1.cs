using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System___Belaro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            this.panel3.BackColor = Color.FromArgb(120, 0, 0, 0);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Student_Dashboard dashboard = new Student_Dashboard();
            dashboard.Show();
            this.Hide(); // Optional: hide the current form
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
