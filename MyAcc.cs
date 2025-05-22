using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BCrypt.Net;

namespace Library_Management_System___Belaro
{
    public partial class MyAcc : Form
    {
        private readonly int _userId;
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";

        public MyAcc(int user_id)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            _userId = user_id;
            LoadUserData();
            save.Enabled = false;

            // Set up event handlers
            save.Click += Save_Click;
            button1.Click += (s, e) => this.Close();

            // Change password field to use password characters
            pass.UseSystemPasswordChar = true;
        }
        private void Pass_TextChanged(object sender, EventArgs e)
        {
            // Enable Save button only when password has at least 8 characters
            save.Enabled = pass.Text.Length >= 8;
        }
        private void LoadUserData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("get_user_by_id", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_user_id", _userId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                username.Text = reader["username"].ToString();
                                // We don't load password for security reasons
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(username.Text))
            {
                MessageBox.Show("Username cannot be empty", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(pass.Text))
            {
                MessageBox.Show("Password cannot be empty", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (pass.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters", "Validation Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Hash the password with BCrypt
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(pass.Text);

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("update_user_credentials", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_user_id", _userId);
                        cmd.Parameters.AddWithValue("p_username", username.Text);
                        cmd.Parameters.AddWithValue("p_password_hash", hashedPassword);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Account updated successfully!", "Success",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("No changes were made to your account", "Information",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (MySqlException ex) when (ex.Number == 1062) // Duplicate username
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating account: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            // Your existing implementation
        }

        private void MyAcc_Load(object sender, EventArgs e)
        {

        }
    }
}