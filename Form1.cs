using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Generators;
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
    
    public partial class Form1 : Form
    {
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
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
            string username = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password");
                return;
            }

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // First, get the user's record including the stored bcrypt hash
                    string query = @"SELECT ur.user_id, ur.role, ur.student_id, ur.staff_id, s.role_id, ur.password_hash
                            FROM user_roles ur 
                            LEFT JOIN staff s ON ur.staff_id = s.staff_id 
                            WHERE username = @username";

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string storedHash = reader["password_hash"].ToString();

                            // Verify password against bcrypt hash
                            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, storedHash);

                            if (isPasswordValid)
                            {
                                string role = reader["role"].ToString();
                                int? studentId = reader["student_id"] as int?;
                                int? staffId = reader["staff_id"] as int?;
                                int? roleId = reader["role_id"] as int?;
                                int? userID = reader["user_id"] as int?;

                                // Redirect based on role (same as before)
                                if (role == "student")
                                {
                                    Student_Dashboard dashboard = new Student_Dashboard(studentId.Value);
                                    dashboard.Show();
                                }
                                else if (role == "staff")
                                {
                                    if (roleId.HasValue)
                                    {
                                        switch (roleId.Value)
                                        {
                                            case 1:
                                                Management managementForm = new Management(staffId.Value, userID.Value);
                                                managementForm.Show();
                                                break;
                                            case 2:
                                                LibararyClerk clerksForm = new LibararyClerk(staffId.Value, userID.Value);
                                                clerksForm.Show();
                                                break;
                                            default:
                                                MessageBox.Show("Unknown staff role");
                                                return;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Staff role not assigned");
                                        return;
                                    }
                                }
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Invalid password");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Username not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during login: " + ex.Message);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Create the verification dialog
            Form verifyDialog = new Form()
            {
                Width = 400,
                Height = 220,
                Text = "Account Verification",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.Gainsboro
            };

            // Add controls with Poppins font
            Font poppinsRegular = null;
            Font poppinsBold = null;

            try
            {
                poppinsRegular = new Font("Poppins", 9f);
                poppinsBold = new Font("Poppins", 9f, FontStyle.Bold);
            }
            catch
            {
                // Fallback to Segoe UI if Poppins not available
                poppinsRegular = new Font("Segoe UI", 9f);
                poppinsBold = new Font("Segoe UI", 9f, FontStyle.Bold);
            }

            Label lblUsername = new Label()
            {
                Text = "Username:",
                Left = 20,
                Top = 20,
                Width = 100,
                Font = poppinsBold
            };

            TextBox txtUsername = new TextBox()
            {
                Left = 130,
                Top = 20,
                Width = 180,
                Font = poppinsRegular
            };

            Label lblBirthdate = new Label()
            {
                Text = "Birthdate:",
                Left = 20,
                Top = 60,
                Width = 100,
                Font = poppinsBold
            };

            DateTimePicker dateBirthdate = new DateTimePicker()
            {
                Left = 130,
                Top = 60,
                Width = 180,
                Font = poppinsRegular,
                Format = DateTimePickerFormat.Short
            };

            Button btnSubmit = new Button()
            {
                Text = "Continue",
                Left = 130,
                Top = 100,
                Width = 80,
                Font = poppinsBold,
                BackColor = SystemColors.Window
            };

            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Left = 230,
                Top = 100,
                Width = 80,
                Font = poppinsBold,
                BackColor = SystemColors.Window
            };

            // Add event handlers
            btnSubmit.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Please enter your username", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = @"SELECT user_id FROM user_roles 
                                WHERE username = @username AND birthdate = @birthdate";

                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@birthdate", dateBirthdate.Value.Date);

                        object result = cmd.ExecuteScalar();

                        if (result != null)
                        {
                            int userId = Convert.ToInt32(result);
                            verifyDialog.Close();
                            ShowPasswordResetDialog(userId);
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or birthdate", "Verification Failed",
                                           MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error verifying credentials: " + ex.Message, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (s, ev) => { verifyDialog.Close(); };

            // Add controls to form
            verifyDialog.Controls.Add(lblUsername);
            verifyDialog.Controls.Add(txtUsername);
            verifyDialog.Controls.Add(lblBirthdate);
            verifyDialog.Controls.Add(dateBirthdate);
            verifyDialog.Controls.Add(btnSubmit);
            verifyDialog.Controls.Add(btnCancel);

            // Show dialog
            verifyDialog.ShowDialog(this);
        }

        private void ShowPasswordResetDialog(int userId)
        {
            // Create password reset dialog
            Form resetDialog = new Form()
            {
                Width = 400,
                Height = 250,
                Text = "Reset Password",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                BackColor = Color.Gainsboro
            };

            // Create fonts
            Font poppinsRegular = null;
            Font poppinsBold = null;

            try
            {
                poppinsRegular = new Font("Poppins", 9f);
                poppinsBold = new Font("Poppins", 9f, FontStyle.Bold);
            }
            catch
            {
                poppinsRegular = new Font("Segoe UI", 9f);
                poppinsBold = new Font("Segoe UI", 9f, FontStyle.Bold);
            }

            // Add controls
            Label lblNewPassword = new Label()
            {
                Text = "New Password:",
                Left = 20,
                Top = 20,
                Width = 120,
                Font = poppinsBold
            };

            TextBox txtNewPassword = new TextBox()
            {
                Left = 150,
                Top = 20,
                Width = 160,
                Font = poppinsRegular,
                UseSystemPasswordChar = true
            };

            Label lblConfirmPassword = new Label()
            {
                Text = "Confirm Password:",
                Left = 20,
                Top = 70,
                Width = 120,
                Font = poppinsBold
            };

            TextBox txtConfirmPassword = new TextBox()
            {
                Left = 150,
                Top = 70,
                Width = 160,
                Font = poppinsRegular,
                UseSystemPasswordChar = true
            };

            Button btnReset = new Button()
            {
                Text = "Reset Password",
                Left = 110,
                Top = 120,
                Width = 120,
                Font = poppinsBold,
                BackColor = SystemColors.Window
            };

            Button btnCancel = new Button()
            {
                Text = "Cancel",
                Left = 240,
                Top = 120,
                Width = 80,
                Font = poppinsBold,
                BackColor = SystemColors.Window
            };

            // Add event handlers
            btnReset.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    MessageBox.Show("Please enter a new password", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (txtNewPassword.Text.Length < 8)
                {
                    MessageBox.Show("Password must be at least 8 characters", "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    // Hash the new password with bcrypt
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(txtNewPassword.Text);

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = @"UPDATE user_roles 
                               SET password_hash = @passwordHash
                               WHERE user_id = @userId";

                        MySqlCommand cmd = new MySqlCommand(query, connection);
                        cmd.Parameters.AddWithValue("@passwordHash", hashedPassword);
                        cmd.Parameters.AddWithValue("@userId", userId);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password updated successfully!", "Success",
                                          MessageBoxButtons.OK, MessageBoxIcon.Information);
                            resetDialog.Close();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update password", "Error",
                                          MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating password: " + ex.Message, "Error",
                                  MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            btnCancel.Click += (s, ev) => { resetDialog.Close(); };

            // Add controls to form
            resetDialog.Controls.Add(lblNewPassword);
            resetDialog.Controls.Add(txtNewPassword);
            resetDialog.Controls.Add(lblConfirmPassword);
            resetDialog.Controls.Add(txtConfirmPassword);
            resetDialog.Controls.Add(btnReset);
            resetDialog.Controls.Add(btnCancel);

            // Show dialog
            resetDialog.ShowDialog(this);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
