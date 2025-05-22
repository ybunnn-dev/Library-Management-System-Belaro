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

namespace Library_Management_System___Belaro
{
    public partial class Specific_Author : Form
    {
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";
        private int currentAuthorId;
        

        public Specific_Author(int author_id)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.currentAuthorId = author_id;

            // Load author details and books when form loads
            this.Load += (s, e) => {
                GetAuthorDetails(currentAuthorId);
                LoadAuthorBooksToGrid(currentAuthorId, dataGridView1); // Assuming your DataGridView is named dataGridView1
            };
        }

        
        private void label1_Click(object sender, EventArgs e)
        {
            // Empty event handler
        }

        public void GetAuthorDetails(int authorId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("get_specific_author", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_author_id", authorId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Directly update form controls with author data
                                string firstName = reader.GetString("first_name");
                                string lastName = reader.GetString("last_name");
                                string fullName = $"{firstName} {lastName}";

                                // Update author name label
                                author_name.Text = fullName;

                                // Update nationality label (handle null value)
                                if (!reader.IsDBNull(reader.GetOrdinal("nationality")))
                                {
                                    nationality.Text = reader.GetString("nationality");
                                }
                                else
                                {
                                    nationality.Text = "Unknown";
                                }

                                // Update birthdate label with formatted date
                                if (!reader.IsDBNull(reader.GetOrdinal("date_of_birth")))
                                {
                                    DateTime dob = reader.GetDateTime("date_of_birth");
                                    birthdate.Text = dob.ToString("MMM. dd, yyyy"); // New format
                                }
                                else
                                {
                                    birthdate.Text = "Unknown";
                                }

                                // If you need to update age label (assuming you have one)
                                if (!reader.IsDBNull(reader.GetOrdinal("age")) && Controls.Find("age_label", true).Length > 0)
                                {
                                    int age = reader.GetInt32("age");
                                    ((Label)Controls.Find("age_label", true)[0]).Text = age.ToString();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Author not found!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving author details: {ex.Message}");
            }
        }

        public void LoadAuthorBooksToGrid(int authorId, DataGridView grid)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("get_author_books_for_grid", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_author_id", authorId);

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }

                        // Configure grid
                        grid.DataSource = dt;
                        grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Remove row headers
                        grid.RowHeadersVisible = false;

                        // Disable the blank "new row" at the bottom
                        grid.AllowUserToAddRows = false;

                        // Set font to Poppins (fallback to default if not available)
                        try
                        {
                            grid.Font = new Font("Poppins", 9f);
                            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9f, FontStyle.Bold);
                        }
                        catch
                        {
                            // Fallback to default font if Poppins isn't installed
                            grid.Font = new Font("Segoe UI", 9f);
                            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                        }

                        // Hide "Available Copy IDs" column if it exists
                        if (grid.Columns.Contains("Available Copy IDs"))
                        {
                            grid.Columns["Available Copy IDs"].Visible = false;
                        }

                        // Optional: Formatting for Year column if it exists
                        if (grid.Columns.Contains("Year"))
                        {
                            grid.Columns["Year"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Specific_Author_Load(object sender, EventArgs e)
        {

        }
    }
}