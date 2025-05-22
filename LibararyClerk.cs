using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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
    public partial class LibararyClerk : Form
    {
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";
        private int staff_id;
        private string staffFullName; 
        public int myID;
        public LibararyClerk(int staff_id, int userID)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosed += new FormClosedEventHandler(Clerk_Dashboard_FormClosed);
            
            InitializeComponent();
            panel4.BackColor = Color.FromArgb(64, 64, 64);
            ongoingPanel.Visible = false;
            bor_req_panel.Visible = false;
            studentPanel.Visible = false;
            UpdateInventory();
            loadPending();
            loadOngoingBorrowings();
            getcurrentStaff(staff_id);
            this.staff_id = staff_id;
            myID = userID;

            stud_grid.CellContentClick += new DataGridViewCellEventHandler(stud_grid_CellContentClick);
            stud_grid.CellFormatting += new DataGridViewCellFormattingEventHandler(stud_grid_CellFormatting);
        }
        private void clickDash(object sender, EventArgs e)
        {
            dash_panel.Visible = true;
            bor_req_panel.Visible = false;
            ongoingPanel.Visible = false;
            studentPanel.Visible = false;

            panel4.BackColor = Color.FromArgb(64, 64, 64);
            panel5.BackColor = Color.Transparent;
            panel6.BackColor = Color.Transparent;
            student_button.BackColor = Color.Transparent;

            UpdateInventory();
            loadPending();
            loadOngoingBorrowings();
            getcurrentStaff(staff_id);
        }
        private void LoadNav()
        {
            acct_dropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            label9.Text = "Welcome, " + staffFullName + "!";
            // Clear first, just in case
            acct_dropdown.Items.Clear();

            // Add options
            acct_dropdown.Items.Add(staffFullName); // Default display
            acct_dropdown.Items.Add("My Account");
            acct_dropdown.Items.Add("Logout");

            // Set default selected item to user's name
            acct_dropdown.SelectedIndex = 0;

            // Remove focus from dropdown so highlight disappears
            this.ActiveControl = null;
        }
        private void acct_dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Skip if nothing is selected or if it's the first item (name display)
            if (acct_dropdown.SelectedIndex <= 0) return;

            string selected = acct_dropdown.SelectedItem.ToString();
            switch (selected)
            {
                case "My Account":
                    MyAcc myAcc = new MyAcc(myID);
                    myAcc.Show();
                    break;
                case "Logout":
                    this.Close();
                    break;
            }

            // Reset selection to show name again
            acct_dropdown.SelectedIndex = 0;
        }
        private void getcurrentStaff(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("GetStaffInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@p_staff_id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                this.staffFullName = reader["full_name"].ToString();
                                LoadNav();
                            }
                            else
                            {
                                MessageBox.Show("Staff member not found!");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving staff information: {ex.Message}");
            }
        }
        private void updateNav()
        {

        }
        private void Clerk_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 loginPage = new Form1();
            loginPage.Show();
        }
        private void label12_Click(object sender, EventArgs e)
        {

        }
        private void UpdateInventory()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Directly query the view we created
                    string query = "SELECT * FROM book_inventory_status";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update the labels with the view data
                                total_copies.Text = (reader["total_copies"].GetType() == typeof(DBNull))
                                    ? "0"
                                    : reader["total_copies"].ToString();

                                avail_copies.Text = (reader["available_copies"].GetType() == typeof(DBNull))
                                    ? "0"
                                    : reader["available_copies"].ToString();

                                ongoing_borrowing.Text = (reader["ongoing_borrowings"].GetType() == typeof(DBNull))
                                    ? "0"
                                    : reader["ongoing_borrowings"].ToString();

                                overdue.Text = (reader["overdue_borrowings"].GetType() == typeof(DBNull))
                                    ? "0"
                                    : reader["overdue_borrowings"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating inventory: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadPending()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM pending_borrow_requests_simple";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Configure grid view
                    pendingGrid.DataSource = dt;

                    // Apply all your styling
                    pendingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    pendingGrid.BackgroundColor = Color.Gainsboro;
                    pendingGrid.BorderStyle = BorderStyle.None;
                    pendingGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    pendingGrid.RowHeadersVisible = false;
                    pendingGrid.RowsDefaultCellStyle.BackColor = Color.Gainsboro;
                    pendingGrid.AllowUserToAddRows = false;

                    // Set fonts
                    pendingGrid.DefaultCellStyle.Font = new Font("Poppins", 8);
                    pendingGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);

                    // Set row height
                    pendingGrid.RowTemplate.Height = 20;

                    // Format column headers
                    pendingGrid.Columns["student_name"].HeaderText = "Student Name";
                    pendingGrid.Columns["book_title"].HeaderText = "Book Title";
                    pendingGrid.Columns["borrow_date"].HeaderText = "Borrow Date";

                    // Format date column
                    pendingGrid.Columns["borrow_date"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    pendingGrid.Columns["borrow_date"].DefaultCellStyle.NullValue = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading pending requests: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void loadCompletedPending()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM unapproved_borrowings";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Configure grid view
                    completedPendingGrid.DataSource = dt;

                    // Apply styling (same as before)
                    completedPendingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    completedPendingGrid.BackgroundColor = Color.Gainsboro;
                    completedPendingGrid.BorderStyle = BorderStyle.None;
                    completedPendingGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    completedPendingGrid.RowHeadersVisible = false;
                    completedPendingGrid.RowsDefaultCellStyle.BackColor = Color.Gainsboro;
                    completedPendingGrid.AllowUserToAddRows = false;

                    // Set fonts
                    completedPendingGrid.DefaultCellStyle.Font = new Font("Poppins", 8);
                    completedPendingGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);

                    // Set row height
                    completedPendingGrid.RowTemplate.Height = 20;

                    // Format column headers
                    completedPendingGrid.Columns["borrowing_id"].Visible = false; 

                    completedPendingGrid.Columns["student_name"].HeaderText = "Student Name";
                    completedPendingGrid.Columns["book_title"].HeaderText = "Book Title";
                    completedPendingGrid.Columns["borrow_date"].HeaderText = "Borrow Date";
                    completedPendingGrid.Columns["due_date"].HeaderText = "Due Date";
                  

                    // Format date columns
                    completedPendingGrid.Columns["borrow_date"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    completedPendingGrid.Columns["borrow_date"].DefaultCellStyle.NullValue = "";
                    completedPendingGrid.Columns["due_date"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    completedPendingGrid.Columns["due_date"].DefaultCellStyle.NullValue = "";

                    // Add action buttons
                    AddActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading completed pending requests: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddActionButtons()
        {
            // Add Approve button column if it doesn't exist
            if (completedPendingGrid.Columns["Approve"] == null)
            {
                DataGridViewButtonColumn approveButton = new DataGridViewButtonColumn();
                approveButton.Name = "Approve";
                approveButton.HeaderText = "Action";
                approveButton.Text = "Approve";
                approveButton.UseColumnTextForButtonValue = true;
                approveButton.DefaultCellStyle.BackColor = Color.LimeGreen;
                approveButton.DefaultCellStyle.ForeColor = Color.Black;
                approveButton.FlatStyle = FlatStyle.Flat;
                completedPendingGrid.Columns.Add(approveButton);
            }

            // Add Cancel button column if it doesn't exist
            if (completedPendingGrid.Columns["Cancel"] == null)
            {
                DataGridViewButtonColumn cancelButton = new DataGridViewButtonColumn();
                cancelButton.Name = "Cancel";
                cancelButton.HeaderText = "Action";
                cancelButton.Text = "Cancel";
                cancelButton.UseColumnTextForButtonValue = true;
                cancelButton.DefaultCellStyle.BackColor = Color.IndianRed;
                cancelButton.DefaultCellStyle.ForeColor = Color.Black;
                cancelButton.FlatStyle = FlatStyle.Flat;
                completedPendingGrid.Columns.Add(cancelButton);
            }
        }

        // Handle button clicks
        private void completedPendingGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ensure we're handling a button click and not header/empty space
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var grid = (DataGridView)sender;

            // Get the borrowing_id from the hidden column (assuming it's the first column)
            int borrowingId = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["borrowing_id"].Value);

            if (grid.Columns[e.ColumnIndex].Name == "Approve")
            {
                // Approve the borrowing
                ApproveBorrowing(borrowingId);
            }
            else if (grid.Columns[e.ColumnIndex].Name == "Cancel")
            {
                // Cancel the borrowing
                CancelBorrowing(borrowingId);
            }
        }

        private void ApproveBorrowing(int borrowingId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand command = new MySqlCommand("approve_borrowing", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@p_borrowing_id", borrowingId);
                        command.Parameters.AddWithValue("@p_staff_id", this.staff_id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string result = reader["result"].ToString();
                                MessageBox.Show(result);
                            }
                        }

                        loadCompletedPending(); // Refresh the grid
                        UpdateInventory(); // Update inventory counts
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error approving borrowing: " + ex.Message);
            }
        }


        private void CancelBorrowing(int borrowingId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM borrowing WHERE borrowing_id = @borrowingId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@borrowingId", borrowingId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Borrowing cancelled successfully!");
                            loadCompletedPending(); 
                            UpdateInventory(); 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cancelling borrowing: " + ex.Message);
            }
        }
        private void loadOngoingBorrowings()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM ongoing_borrowings";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    // Configure grid view
                    ongoingGrid.DataSource = dt;
                    // Apply all styling
                    ongoingGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    ongoingGrid.BackgroundColor = Color.Gainsboro;
                    ongoingGrid.BorderStyle = BorderStyle.None;
                    ongoingGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    ongoingGrid.RowHeadersVisible = false;
                    ongoingGrid.RowsDefaultCellStyle.BackColor = Color.Gainsboro;
                    ongoingGrid.AllowUserToAddRows = false;
                    // Set fonts
                    ongoingGrid.DefaultCellStyle.Font = new Font("Poppins", 8);
                    ongoingGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);
                    // Set row height
                    ongoingGrid.RowTemplate.Height = 20;
                    // Format column headers
                    ongoingGrid.Columns["student_name"].HeaderText = "Student Name";
                    ongoingGrid.Columns["book_title"].HeaderText = "Book Title";

                    ongoingGrid.Columns["borrow_date"].HeaderText = "Borrow Date";
                    ongoingGrid.Columns["due_date"].HeaderText = "Due Date";
                    // Format date columns
                    ongoingGrid.Columns["borrow_date"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    ongoingGrid.Columns["borrow_date"].DefaultCellStyle.NullValue = "";
                    ongoingGrid.Columns["due_date"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    ongoingGrid.Columns["due_date"].DefaultCellStyle.NullValue = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ongoing borrowings: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void pendingGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gotoPendingBurrow(object sender, EventArgs e)
        {
            bor_req_panel.Visible = true;
            loadCompletedPending();
            dash_panel.Visible = false;
            studentPanel.Visible = false;

            panel5.BackColor = Color.FromArgb(64, 64, 64);
            panel4.BackColor = Color.Transparent;
            panel6.BackColor = Color.Transparent;
            student_button.BackColor = Color.Transparent; 
        }

        private void loadOngoingTransact()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Using the updated view
                    string query = "SELECT * FROM ongoing_borrowings";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Configure grid view
                    ongoingTable.DataSource = dt;

                    // Apply consistent styling
                    ongoingTable.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    ongoingTable.BackgroundColor = Color.Gainsboro;
                    ongoingTable.BorderStyle = BorderStyle.None;
                    ongoingTable.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    ongoingTable.RowHeadersVisible = false;
                    ongoingTable.RowsDefaultCellStyle.BackColor = Color.Gainsboro;
                    ongoingTable.AllowUserToAddRows = false;

                    // Set fonts
                    ongoingTable.DefaultCellStyle.Font = new Font("Poppins", 8);
                    ongoingTable.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);

                    // Set row height
                    ongoingTable.RowTemplate.Height = 30;

                    // Format column headers
                    ongoingTable.Columns["borrowing_id"].Visible = false; // Hide ID column
                    ongoingTable.Columns["student_name"].HeaderText = "Student Name";
                    ongoingTable.Columns["book_title"].HeaderText = "Book Title";
                    ongoingTable.Columns["borrow_date"].HeaderText = "Borrow Date";
                    ongoingTable.Columns["due_date"].HeaderText = "Due Date";

                    // Format date columns
                    ongoingTable.Columns["borrow_date"].DefaultCellStyle.Format = "MMM dd, yyyy";
                    ongoingTable.Columns["due_date"].DefaultCellStyle.Format = "MMM dd, yyyy";

                    // Add Return button column
                    AddReturnButtonColumn();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading ongoing transactions: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddReturnButtonColumn()
        {
            // Remove existing button column to avoid duplicates
            if (ongoingTable.Columns["Return"] != null)
                ongoingTable.Columns.Remove("Return");

            // Add new Return button column
            DataGridViewButtonColumn returnBtn = new DataGridViewButtonColumn
            {
                Name = "Return",
                HeaderText = "Action",
                Text = "Return",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.SteelBlue,
                    ForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            ongoingTable.Columns.Add(returnBtn);
        }

        private void ongoingTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex].Name == "Return")
            {
                int borrowingId = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["borrowing_id"].Value);

                // Confirm return action
                DialogResult result = MessageBox.Show("Are you sure you want to mark this book as returned?",
                                                   "Confirm Return",
                                                   MessageBoxButtons.YesNo,
                                                   MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    ReturnBook(borrowingId);
                }
            }
        }

        private void ReturnBook(int borrowingId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("return_book", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@borrowID", borrowingId);

                        command.ExecuteNonQuery();

                        // Show success message
                        MessageBox.Show("Book successfully returned!",
                                       "Success",
                                       MessageBoxButtons.OK,
                                       MessageBoxIcon.Information);

                        // Refresh data
                        loadOngoingTransact();
                        UpdateInventory();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error returning book: {ex.Message}\n\n{ex.StackTrace}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            dash_panel.Visible = false;
            bor_req_panel.Visible = false;
            ongoingPanel.Visible = true;
            studentPanel.Visible = false;

            panel6.BackColor = Color.FromArgb(64, 64, 64);
            panel5.BackColor = Color.Transparent;
            panel4.BackColor = Color.Transparent;
            student_button.BackColor = Color.Transparent;

            loadOngoingTransact();
        }

       
        private void loadStudents()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string query = "SELECT * FROM active_students_view";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Configure grid view
                    stud_grid.DataSource = dt;

                    // Apply styling
                    stud_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    stud_grid.BackgroundColor = Color.Gainsboro;
                    stud_grid.BorderStyle = BorderStyle.None;
                    stud_grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    stud_grid.RowHeadersVisible = false;
                    stud_grid.RowsDefaultCellStyle.BackColor = Color.Gainsboro;
                    stud_grid.AllowUserToAddRows = false;

                    // Set fonts
                    stud_grid.DefaultCellStyle.Font = new Font("Poppins", 8);
                    stud_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);

                    // Set row height
                    stud_grid.RowTemplate.Height = 25;

                    // Hide student_id column
                    stud_grid.Columns["student_id"].Visible = false;

                    // Format column headers
                    stud_grid.Columns["full_name"].HeaderText = "Student Name";
                    stud_grid.Columns["email"].HeaderText = "Email";
                    stud_grid.Columns["enrollment_date"].HeaderText = "Enrollment Date";
                    stud_grid.Columns["borrow_eligibility"].HeaderText = "Borrow Status";
                    stud_grid.Columns["balance"].HeaderText = "Balance";

                    // Format date column
                    stud_grid.Columns["enrollment_date"].DefaultCellStyle.Format = "MMM dd, yyyy";

                    // Format balance column
                    stud_grid.Columns["balance"].DefaultCellStyle.Format = "C2"; // Currency format

                    // Add action buttons
                    AddStudentActionButtons();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading students: " + ex.Message,
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddStudentActionButtons()
        {
            // Remove existing buttons to avoid duplicates
            if (stud_grid.Columns["PayBalance"] != null)
                stud_grid.Columns.Remove("PayBalance");
            if (stud_grid.Columns["RemoveStudent"] != null)
                stud_grid.Columns.Remove("RemoveStudent");

            // Add Pay Balance button
            DataGridViewButtonColumn payButton = new DataGridViewButtonColumn
            {
                Name = "PayBalance",
                HeaderText = "Actions",
                Text = "Pay Balance",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.LimeGreen,
                    ForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            stud_grid.Columns.Add(payButton);

            // Add Remove Student button
            DataGridViewButtonColumn removeButton = new DataGridViewButtonColumn
            {
                Name = "RemoveStudent",
                HeaderText = "Actions",
                Text = "Remove",
                UseColumnTextForButtonValue = true,
                FlatStyle = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.IndianRed,
                    ForeColor = Color.Black,
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                }
            };
            stud_grid.Columns.Add(removeButton);
        }

        private void stud_grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Disable Pay Balance button if balance is zero
            if (stud_grid.Columns[e.ColumnIndex].Name == "PayBalance")
            {
                if (e.RowIndex >= 0)
                {
                    decimal balance = Convert.ToDecimal(stud_grid.Rows[e.RowIndex].Cells["balance"].Value);
                    if (balance == 0)
                    {
                        stud_grid.Rows[e.RowIndex].Cells["PayBalance"].ReadOnly = true;
                        stud_grid.Rows[e.RowIndex].Cells["PayBalance"].Style.BackColor = Color.Gray;
                        stud_grid.Rows[e.RowIndex].Cells["PayBalance"].Style.ForeColor = Color.LightGray;
                    }
                }
            }
        }

        private void stud_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var grid = (DataGridView)sender;
            int studentId = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["student_id"].Value);
            string studentName = grid.Rows[e.RowIndex].Cells["full_name"].Value.ToString();

            if (grid.Columns[e.ColumnIndex].Name == "PayBalance")
            {
                // Handle payment
                decimal balance = Convert.ToDecimal(grid.Rows[e.RowIndex].Cells["balance"].Value);
                if (balance > 0)
                {
                    PayStudentBalance(studentId, studentName, balance);
                }
            }
            else if (grid.Columns[e.ColumnIndex].Name == "RemoveStudent")
            {
                // Handle student removal
                RemoveStudent(studentId, studentName);
            }
        }

        private void PayStudentBalance(int studentId, string studentName, decimal balance)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    $"Process payment of {balance:C2} for {studentName}?",
                    "Confirm Payment",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();

                        // Execute the stored procedure
                        using (MySqlCommand command = new MySqlCommand("process_student_payment", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@p_student_id", studentId);
                            command.ExecuteNonQuery();

                            // If no exception is thrown, consider the payment successful
                            MessageBox.Show("Payment processed successfully!");
                            loadStudents(); // Refresh the grid
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error processing payment: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
        private void RemoveStudent(int studentId, string studentName)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    $"Are you sure you want to deactivate {studentName}?",
                    "Confirm Deactivation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                
                        // First execute the stored procedure
                        using (MySqlCommand command = new MySqlCommand("deactivate_student", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@p_student_id", studentId);
                            command.ExecuteNonQuery(); // Execute but ignore the SELECT result
                        }
                
                        // Then check if the student was actually updated
                        using (MySqlCommand checkCommand = new MySqlCommand(
                            "SELECT COUNT(*) FROM students WHERE student_id = @id AND status = 'inactive'", 
                            connection))
                        {
                            checkCommand.Parameters.AddWithValue("@id", studentId);
                            int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    
                            if (count > 0)
                            {
                                MessageBox.Show("Student deactivated successfully!");
                                loadStudents(); // Refresh the grid
                            }
                            else
                            {
                                MessageBox.Show("No student found with that ID.",
                                              "Deactivation Failed",
                                              MessageBoxButtons.OK,
                                              MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deactivating student: {ex.Message}",
                              "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
            }
        }
        private void clickStudents(object sender, EventArgs e)
        {
            dash_panel.Visible = false;
            bor_req_panel.Visible = false;
            ongoingPanel.Visible = false;
            studentPanel.Visible = true;

            student_button.BackColor = Color.FromArgb(64, 64, 64);
            panel5.BackColor = Color.Transparent;
            panel4.BackColor = Color.Transparent;
            panel6.BackColor = Color.Transparent;

            loadStudents();
        }

       

        private void stud_grid_CellContentClick(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void insertStudent(object sender, EventArgs e)
        {
            // Create the main form
            Form insertForm = new Form();
            insertForm.Text = "Add New Student";
            insertForm.Width = 400;
            insertForm.Height = 500; // Reduced height since we removed balance
            insertForm.StartPosition = FormStartPosition.CenterParent;
            insertForm.Font = new Font("Poppins", 9);

            // Create a panel to hold all controls
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.AutoScroll = true;
            insertForm.Controls.Add(mainPanel);

            // Current Y position for control placement
            int currentY = 20;

            // STUDENT INFORMATION SECTION
            Label studentHeader = new Label();
            studentHeader.Text = "Student Information";
            studentHeader.Font = new Font("Poppins", 10, FontStyle.Bold);
            studentHeader.Location = new Point(20, currentY);
            studentHeader.AutoSize = true;
            mainPanel.Controls.Add(studentHeader);
            currentY += 30;

            // First Name
            Label lblFirstName = new Label();
            lblFirstName.Text = "First Name:";
            lblFirstName.Location = new Point(20, currentY);
            lblFirstName.AutoSize = true;
            mainPanel.Controls.Add(lblFirstName);

            TextBox txtFirstName = new TextBox();
            txtFirstName.Location = new Point(150, currentY);
            txtFirstName.Width = 200;
            mainPanel.Controls.Add(txtFirstName);
            currentY += 30;

            // Last Name
            Label lblLastName = new Label();
            lblLastName.Text = "Last Name:";
            lblLastName.Location = new Point(20, currentY);
            lblLastName.AutoSize = true;
            mainPanel.Controls.Add(lblLastName);

            TextBox txtLastName = new TextBox();
            txtLastName.Location = new Point(150, currentY);
            txtLastName.Width = 200;
            mainPanel.Controls.Add(txtLastName);
            currentY += 30;

            // Email
            Label lblEmail = new Label();
            lblEmail.Text = "Email:";
            lblEmail.Location = new Point(20, currentY);
            lblEmail.AutoSize = true;
            mainPanel.Controls.Add(lblEmail);

            TextBox txtEmail = new TextBox();
            txtEmail.Location = new Point(150, currentY);
            txtEmail.Width = 200;
            mainPanel.Controls.Add(txtEmail);
            currentY += 30;

            // Enrollment Date
            Label lblEnrollmentDate = new Label();
            lblEnrollmentDate.Text = "Enrollment Date:";
            lblEnrollmentDate.Location = new Point(20, currentY);
            lblEnrollmentDate.AutoSize = true;
            mainPanel.Controls.Add(lblEnrollmentDate);

            DateTimePicker dtpEnrollmentDate = new DateTimePicker();
            dtpEnrollmentDate.Location = new Point(150, currentY);
            dtpEnrollmentDate.Width = 200;
            mainPanel.Controls.Add(dtpEnrollmentDate);
            currentY += 40;

            // USER ACCOUNT SECTION
            Label userHeader = new Label();
            userHeader.Text = "User Account Information";
            userHeader.Font = new Font("Poppins", 10, FontStyle.Bold);
            userHeader.Location = new Point(20, currentY);
            userHeader.AutoSize = true;
            mainPanel.Controls.Add(userHeader);
            currentY += 30;

            // Username
            Label lblUsername = new Label();
            lblUsername.Text = "Username:";
            lblUsername.Location = new Point(20, currentY);
            lblUsername.AutoSize = true;
            mainPanel.Controls.Add(lblUsername);

            TextBox txtUsername = new TextBox();
            txtUsername.Location = new Point(150, currentY);
            txtUsername.Width = 200;
            mainPanel.Controls.Add(txtUsername);
            currentY += 40;

            // Birthdate
            Label lblBirthdate = new Label();
            lblBirthdate.Text = "Birthdate:";
            lblBirthdate.Location = new Point(20, currentY);
            lblBirthdate.AutoSize = true;
            mainPanel.Controls.Add(lblBirthdate);

            DateTimePicker dtpBirthdate = new DateTimePicker();
            dtpBirthdate.Location = new Point(150, currentY);
            dtpBirthdate.Width = 200;
            mainPanel.Controls.Add(dtpBirthdate);
            currentY += 40;

            // Add Submit Button
            Button btnSubmit = new Button();
            btnSubmit.Text = "Submit";
            btnSubmit.Font = new Font("Poppins", 9, FontStyle.Bold);
            btnSubmit.Location = new Point(150, currentY);
            btnSubmit.Width = 100;
            btnSubmit.BackColor = Color.White;
            btnSubmit.ForeColor = Color.Black;
            btnSubmit.FlatStyle = FlatStyle.Flat;
            btnSubmit.FlatAppearance.BorderSize = 0;
            mainPanel.Controls.Add(btnSubmit);

            // Submit button click handler
            btnSubmit.Click += async (s, ev) => 
            {
                try
                {
                    // Validate inputs
                    if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                        string.IsNullOrWhiteSpace(txtLastName.Text) ||
                        string.IsNullOrWhiteSpace(txtEmail.Text) ||
                        string.IsNullOrWhiteSpace(txtUsername.Text))
                    {
                        MessageBox.Show("Please fill in all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Generate password based on firstname-birthyear
                    string firstname = txtFirstName.Text.Replace(" ", ""); // Remove spaces from first name
                    string birthYear = dtpBirthdate.Value.Year.ToString();
                    string generatedPassword = $"{firstname.ToLower()}-{birthYear}";
            
                    // Hash the password with bcrypt
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(generatedPassword);

                    // Create connection and command
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        await connection.OpenAsync();

                        // Create command for stored procedure
                        using (MySqlCommand command = new MySqlCommand("create_student_and_user", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Add parameters
                            command.Parameters.AddWithValue("@p_first_name", txtFirstName.Text);
                            command.Parameters.AddWithValue("@p_last_name", txtLastName.Text);
                            command.Parameters.AddWithValue("@p_email", txtEmail.Text);
                            command.Parameters.AddWithValue("@p_enrollment_date", dtpEnrollmentDate.Value);
                            command.Parameters.AddWithValue("@p_balance", 0); // Set default balance to 0
                            command.Parameters.AddWithValue("@p_username", txtUsername.Text);
                            command.Parameters.AddWithValue("@p_birthdate", dtpBirthdate.Value);
                            command.Parameters.AddWithValue("@p_hashed_password", hashedPassword);

                            // Execute the stored procedure
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                if (reader.Read())
                                {
                            

                                    // Show success message with generated password
                                    MessageBox.Show($"Student created successfully!\n\nGenerated Password: {generatedPassword}", 
                                        "Success", 
                                        MessageBoxButtons.OK, 
                                        MessageBoxIcon.Information);
                            
                                    insertForm.DialogResult = DialogResult.OK;
                                    loadStudents();
                                    insertForm.Close();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error creating student: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Show the form
            insertForm.ShowDialog();
        }

        private void studentPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void stud_grid_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }
    }

}
