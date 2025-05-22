using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;

namespace Library_Management_System___Belaro
{
    
    public partial class Specific_Book : Form
    {
        private int student_id;
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";
        private int current_book_id;
        public Specific_Book(int bookId, int studentId)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.current_book_id = bookId;
            InitializeComponent();
            loadData(bookId);

            if (HasUnreturnedBooks(studentId))
            {
                borrow.Enabled = false;
                borrow.BackColor = Color.LightGray;
            }

            this.student_id = studentId;

        }

        public bool HasUnreturnedBooks(int studentId)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("SELECT has_unreturned_books(@studentId)", connection))
                {
                    command.Parameters.AddWithValue("@studentId", studentId);
                    return Convert.ToBoolean(command.ExecuteScalar());
                }
            }
        }
        private void loadData(int bookId)
        {

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Create command for stored procedure
                    using (MySqlCommand command = new MySqlCommand("get_book_details", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameter
                        command.Parameters.AddWithValue("book_id_param", bookId);

                        // Execute and read results
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update labels with retrieved data
                                book_id.Text = reader["book_id"].ToString();
                                title.Text = reader["title"].ToString();
                                year.Text = reader["publication_year"].ToString();
                                categ.Text = reader["category_name"].ToString();
                                author.Text = reader["author_name"].ToString();
                                copy_num.Text = reader["total_copies"].ToString();
                                avail_stat.Text = reader["available_copies"].ToString();

                                // You can also store additional data if needed
                                int authorId = Convert.ToInt32(reader["author_id"]);
                                int categoryId = Convert.ToInt32(reader["category_id"]);
                            }
                            else
                            {
                                MessageBox.Show("Book not found!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book details: " + ex.Message);
            }
        }
        private void title_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void copy_num_Click(object sender, EventArgs e)
        {

        }

        private void add_to_fav_Click(object sender, EventArgs e)
        {

        }

        private void author_Click(object sender, EventArgs e)
        {

        }
        private DateTime? ShowDateInputDialog()
        {
            // Create the form
            Form dateInputForm = new Form()
            {
                Width = 300,
                Height = 200,
                Text = "Select Due Date",
                StartPosition = FormStartPosition.CenterParent,
                BackColor = Color.Gainsboro,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Create font objects
            Font headerFont = new Font("Poppins", 10, FontStyle.Bold);
            Font labelFont = new Font("Poppins", 8);
            Font buttonFont = new Font("Poppins", 8);

            // Create controls
            Label headerLabel = new Label()
            {
                Text = "Select Due Date for Borrowing",
                Font = headerFont,
                AutoSize = true,
                Top = 20,
                Left = (dateInputForm.ClientSize.Width - 200) / 2
            };

            Label dateLabel = new Label()
            {
                Text = "Due Date:",
                Font = labelFont,
                Top = 60,
                Left = 20,
                AutoSize = true
            };

            DateTimePicker datePicker = new DateTimePicker()
            {
                Font = labelFont,
                Top = 55,
                Left = 100,
                Width = 150,
                MinDate = DateTime.Today.AddDays(1),
                MaxDate = DateTime.Today.AddMonths(6),
                Value = DateTime.Today.AddDays(14) // Default to 2 weeks from today
            };

            Button confirmButton = new Button()
            {
                Text = "Confirm",
                Font = buttonFont,
                Top = 110,
                Left = 150,
                Width = 80
            };

            Button cancelButton = new Button()
            {
                Text = "Cancel",
                Font = buttonFont,
                Top = 110,
                Left = 50,
                Width = 80
            };

            // Add controls to form
            dateInputForm.Controls.Add(headerLabel);
            dateInputForm.Controls.Add(dateLabel);
            dateInputForm.Controls.Add(datePicker);
            dateInputForm.Controls.Add(confirmButton);
            dateInputForm.Controls.Add(cancelButton);

            // Create a variable to store the selected date
            DateTime? selectedDate = null;

            // Handle confirm button click - THIS TRIGGERS THE BORROWING PROCESS
            confirmButton.Click += (s, args) =>
            {
                selectedDate = datePicker.Value;
                dateInputForm.DialogResult = DialogResult.OK;
                dateInputForm.Close();

                // This will now continue execution in borrow_click with the selected date
            };

            // Handle cancel button click
            cancelButton.Click += (s, args) =>
            {
                dateInputForm.DialogResult = DialogResult.Cancel;
                dateInputForm.Close();
            };

            // Show dialog
            dateInputForm.ShowDialog();

            // Return selected date (null if canceled)
            return selectedDate;
        }

        private void borrow_click(object sender, EventArgs e)
        {
            // Get due date from user
            DateTime? dueDate = ShowDateInputDialog();

            if (!dueDate.HasValue) // User clicked cancel
            {
                return;
            }

            try
            {
                // 1. Get available copy for this book
                int copy_id = GetAvailableCopy(this.current_book_id);

                // 2. Call borrow_book procedure
                int studentID = student_id; // Default student (replace later)

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("borrow_book", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("studentID", studentID);
                        command.Parameters.AddWithValue("copyID", copy_id);
                        command.Parameters.AddWithValue("staffID", DBNull.Value);
                        command.Parameters.AddWithValue("dueDate", dueDate.Value);

                        command.ExecuteNonQuery();
                    }
                }

                // 3. Show success message
                MessageBox.Show("Your borrowing request is waiting for staff approval.\n\n" +
                               $"Book Copy: {copy_id}\n" +
                               $"Due Date: {dueDate.Value.ToShortDateString()}",
                               "Request Submitted",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing borrowing: {ex.Message}",
                               "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
            }
        }

        private int GetAvailableCopy(int bookId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT get_available_copy(@bookId)", connection))
                    {
                        command.Parameters.AddWithValue("@bookId", bookId);
                        object result = command.ExecuteScalar();
                        return result != null ? Convert.ToInt32(result) : 0;
                    }
                }
            }
            catch
            {
                return 0; // Return 0 if any error occurs
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}