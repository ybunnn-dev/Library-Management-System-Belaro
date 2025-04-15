using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Library_Management_System___Belaro
{
    public partial class Student_Dashboard : Form
    {
        public Student_Dashboard()
        {
            InitializeComponent();
            this.Text = "Student Dashboard";
            this.FormClosed += new FormClosedEventHandler(Student_Dashboard_FormClosed);
        }
        private void Student_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


        private void Student_Dashboard_Load(object sender, EventArgs e)
        {
            acct_dropdown.DropDownStyle = ComboBoxStyle.DropDownList;

            // Clear first, just in case
            acct_dropdown.Items.Clear();

            // Add options
            acct_dropdown.Items.Add("Phaye Wibster"); // Default display
            acct_dropdown.Items.Add("My Account");
            acct_dropdown.Items.Add("Logout");

            // Set default selected item to user's name
            acct_dropdown.SelectedIndex = 0;

            // Remove focus from dropdown so highlight disappears
            this.ActiveControl = null;
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.panel2.BackColor = Color.FromArgb(120, 0, 0, 0);
        }

        private void dash_button1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void acct_dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Move focus to something else (like the form itself)
            this.ActiveControl = null;

            // Then proceed with logic
            if (acct_dropdown.SelectedIndex == 0) return;

            string selected = acct_dropdown.SelectedItem.ToString();
            switch (selected)
            {
                case "My Account":
                    // Open account page
                    break;
                case "Settings":
                    break;
                case "Logout":
                    Application.Exit();
                    break;
            }
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void current_author_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }
    }
}
