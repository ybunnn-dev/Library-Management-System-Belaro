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
using MySql.Data.MySqlClient;
using System.Net;
using System.Runtime.Remoting.Messaging;


namespace Library_Management_System___Belaro
{


    public partial class Student_Dashboard : Form
    {
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";
        private int student_id;

        // New class-level variables to store user data
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public string BorrowEligibility { get; private set; }
        public float Balance { get; private set; }
        public Student_Dashboard(int student_id)
        {
            this.StartPosition = FormStartPosition.CenterScreen;

            InitializeComponent();
            this.Text = "Student Dashboard";
            this.FormClosed += new FormClosedEventHandler(Student_Dashboard_FormClosed);
            dataGridViewBooks.RowHeadersVisible = false;
            authors_grid.RowHeadersVisible = false;
            borrow_book_panel.Visible = false;
            current_book_panel.Visible = false;
            historyPanel.Visible = false;
            panel4.BackColor = SystemColors.WindowFrame;
            loadCurrentBalance(student_id);
            loadMostBorrowed();
            load_top_authors();
            loadRecentTransact(student_id);
            this.student_id = student_id;
            GetStudentData();
            
        }
        private void Student_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
        }

        private void GetStudentData()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    using (MySqlCommand cmd = new MySqlCommand("get_student_user_data", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@p_student_id", this.student_id);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Store user data in class variables
                                UserId = Convert.ToInt32(reader["user_id"]);
                                Username = reader["username"].ToString();
                                FirstName = reader["first_name"].ToString();
                                LastName = reader["last_name"].ToString();
                                Email = reader["email"].ToString();
                                EnrollmentDate = Convert.ToDateTime(reader["enrollment_date"]);
                                BorrowEligibility = reader["borrow_eligibility"].ToString();
                                Balance = Convert.ToSingle(reader["balance"]);

                                // Update account dropdown with actual user name
                                acct_dropdown.Items.Clear();
                                acct_dropdown.Items.Add($"{FirstName} {LastName}");
                                acct_dropdown.Items.Add("My Account");
                                acct_dropdown.Items.Add("Logout");
                                acct_dropdown.SelectedIndex = 0;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading student data: " + ex.Message);
                // Fallback to default values
                acct_dropdown.Items.Clear();
                acct_dropdown.Items.Add("Student");
                acct_dropdown.Items.Add("My Account");
                acct_dropdown.Items.Add("Logout");
                acct_dropdown.SelectedIndex = 0;
            }
        }
        private void Student_Dashboard_Load(object sender, EventArgs e)
        {
            acct_dropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            label9.Text = "Welcome, " + FirstName + " " + LastName + "!";
            // Clear first, just in case
            acct_dropdown.Items.Clear();

            // Add options
            acct_dropdown.Items.Add(FirstName+" "+LastName); // Default display
            acct_dropdown.Items.Add("My Account");
            acct_dropdown.Items.Add("Logout");

            // Set default selected item to user's name
            acct_dropdown.SelectedIndex = 0;

            // Remove focus from dropdown so highlight disappears
            this.ActiveControl = null;
        }



        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            this.authors_panel.BackColor = Color.FromArgb(120, 0, 0, 0);
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
                    MyAcc myAcc = new MyAcc(UserId);
                    myAcc.Show();
                    break;
                case "Logout":
                    this.Close();
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

        private void borrow_click(object sender, EventArgs e)
        {
            dashboard_panel.Visible = false;
            borrow_book_panel.Visible = true;
            current_book_panel.Visible = false;
            historyPanel.Visible = false;

            panel4.BackColor = Color.Transparent;
            panel5.BackColor = SystemColors.WindowFrame;
            panel6.BackColor = Color.Transparent;
            history_button.BackColor = Color.Transparent;

            LoadBookAvailability();
        }

        private void dashboard_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadBookAvailability(string searchTerm = "")
        {
            string query = "SELECT * FROM book_availability";

            // Add search filter if term is provided
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query += " WHERE title LIKE @searchTerm OR book_id LIKE @searchTerm";
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // Add parameter if searching
                    if (!string.IsNullOrWhiteSpace(searchTerm))
                    {
                        cmd.Parameters.AddWithValue("@searchTerm", $"%{searchTerm}%");
                    }

                    // Keep all your original grid setup code exactly as is
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Clear existing data and columns
                    dataGridViewBooks.Rows.Clear();
                    dataGridViewBooks.Columns.Clear();

                    // Disable auto-resizing
                    dataGridViewBooks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    // Prevent blank row at the bottom
                    dataGridViewBooks.AllowUserToAddRows = false;

                    // Add columns with exact widths (preserving your original dimensions)
                    dataGridViewBooks.Columns.Add("book_id", "Book ID");
                    dataGridViewBooks.Columns["book_id"].Width = 110;

                    dataGridViewBooks.Columns.Add("title", "Title");
                    dataGridViewBooks.Columns["title"].Width = 350;

                    dataGridViewBooks.Columns.Add("available_copies", "Available Copies");
                    dataGridViewBooks.Columns["available_copies"].Width = 110;

                    dataGridViewBooks.Columns.Add("stat", "Status");
                    dataGridViewBooks.Columns["stat"].Width = 130;

                    // Add button column (preserving your original button config)
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                    buttonColumn.Name = "actions";
                    buttonColumn.HeaderText = "Actions";
                    buttonColumn.Text = "View";
                    buttonColumn.UseColumnTextForButtonValue = true;
                    buttonColumn.Width = 110;
                    dataGridViewBooks.Columns.Add(buttonColumn);

                    // Preserve all your original styling
                    dataGridViewBooks.DefaultCellStyle.Font = new Font("Poppins", 10, FontStyle.Regular);
                    dataGridViewBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 11, FontStyle.Bold);
                    dataGridViewBooks.RowTemplate.Height = 40;
                    dataGridViewBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
                    dataGridViewBooks.RowsDefaultCellStyle.BackColor = Color.Gainsboro;

                    // Fill data (preserving your original data binding)
                    foreach (DataRow row in dt.Rows)
                    {
                        int rowIndex = dataGridViewBooks.Rows.Add();
                        dataGridViewBooks.Rows[rowIndex].Cells["book_id"].Value = row["book_id"];
                        dataGridViewBooks.Rows[rowIndex].Cells["title"].Value = row["title"];
                        dataGridViewBooks.Rows[rowIndex].Cells["available_copies"].Value = row["available_copies"];
                        dataGridViewBooks.Rows[rowIndex].Cells["stat"].Value = row["availability_status"];
                    }

                    // Reattach click event
                    dataGridViewBooks.CellContentClick -= DataGridViewBooks_CellClick; // Remove first to avoid duplicates
                    dataGridViewBooks.CellContentClick += DataGridViewBooks_CellClick;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading book availability: " + ex.Message);
                }
            }
        }

        // Search handler
        private void searchBooks(object sender, EventArgs e)
        {
            LoadBookAvailability(search_books.Text.Trim());
        }


        private void DataGridViewBooks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewBooks.Columns["actions"].Index && e.RowIndex >= 0)
            {
                var bookId = Convert.ToInt32(dataGridViewBooks.Rows[e.RowIndex].Cells["book_id"].Value);
                var detailsForm = new Specific_Book(bookId, student_id);
                detailsForm.ShowDialog(); // Use ShowDialog() to block multiple clicks
            }
        }
        private void LoadAuthors(string searchTerm = "")
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Use stored procedure for searching
                    MySqlCommand cmd = new MySqlCommand("search_authors", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_search_term", searchTerm);

                    DataTable dt = new DataTable();
                    new MySqlDataAdapter(cmd).Fill(dt);

                    // Clear existing data and columns (preserves your exact grid setup)
                    authors_grid.Rows.Clear();
                    authors_grid.Columns.Clear();

                    // Disable auto-resizing
                    authors_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

                    // Prevent blank row at the bottom
                    authors_grid.AllowUserToAddRows = false;
                    authors_grid.RowHeadersVisible = false;

                    // Add columns with exact widths (same as original)
                    authors_grid.Columns.Add("author_id", "Author ID");
                    authors_grid.Columns.Add("author_name", "Author Name");
                    authors_grid.Columns.Add("birthdate", "Birth Date");
                    authors_grid.Columns.Add("nationality", "Nationality");
                    authors_grid.Columns.Add("books", "Book Count");

                    // Add button column (same as original)
                    DataGridViewButtonColumn buttonColumn = new DataGridViewButtonColumn();
                    buttonColumn.Name = "actions";
                    buttonColumn.HeaderText = "Actions";
                    buttonColumn.Text = "View";
                    buttonColumn.UseColumnTextForButtonValue = true;
                    authors_grid.Columns.Add(buttonColumn);

                    // Set fonts (with fallback - same as original)
                    try
                    {
                        authors_grid.DefaultCellStyle.Font = new Font("Poppins", 10, FontStyle.Regular);
                        authors_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 11, FontStyle.Bold);
                    }
                    catch
                    {
                        authors_grid.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
                        authors_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    }

                    // Visual improvements (same as original)
                    authors_grid.RowTemplate.Height = 40;
                    authors_grid.RowsDefaultCellStyle.BackColor = Color.Gainsboro;

                    // Set column widths (same as original)
                    authors_grid.Columns["author_id"].Width = 90;
                    authors_grid.Columns["author_name"].Width = 300;
                    authors_grid.Columns["birthdate"].Width = 120;
                    authors_grid.Columns["nationality"].Width = 120;
                    authors_grid.Columns["books"].Width = 90;
                    authors_grid.Columns["actions"].Width = 90;

                    // Fill data from the results (adapted for stored procedure)
                    foreach (DataRow row in dt.Rows)
                    {
                        int rowIndex = authors_grid.Rows.Add();
                        string authorName = row["first_name"] + " " + row["last_name"];
                        authors_grid.Rows[rowIndex].Cells["author_id"].Value = row["author_id"];
                        authors_grid.Rows[rowIndex].Cells["author_name"].Value = authorName;
                        authors_grid.Rows[rowIndex].Cells["birthdate"].Value =
                            Convert.ToDateTime(row["date_of_birth"]).ToString("MMM. d, yyyy");
                        authors_grid.Rows[rowIndex].Cells["nationality"].Value = row["nationality"];
                        authors_grid.Rows[rowIndex].Cells["books"].Value = row["number_of_books"];
                    }

                    // Reattach click event (same as original)
                    authors_grid.CellContentClick -= AuthorsGrid_CellContentClick;
                    authors_grid.CellContentClick += AuthorsGrid_CellContentClick;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading authors: " + ex.Message);
                }
            }
        }

        private void searchBox_author_TextChanged(object sender, EventArgs e)
        {
            LoadAuthors(searchBox_author.Text.Trim());
        }
        // Example click handler
        private void AuthorsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == authors_grid.Columns["actions"].Index && e.RowIndex >= 0)
            {
                int authorId = Convert.ToInt32(authors_grid.Rows[e.RowIndex].Cells["author_id"].Value);
                // Handle view action for this author


                var authorForm = new Specific_Author(authorId);
                authorForm.ShowDialog(); // Use ShowDialog() to block multiple clicks
            }
        }
        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewBooks_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void current_click(object sender, EventArgs e)
        {
            dashboard_panel.Visible = false;
            borrow_book_panel.Visible = false;
            current_book_panel.Visible = true;
            historyPanel.Visible = false;


            panel4.BackColor = Color.Transparent;
            panel6.BackColor = SystemColors.WindowFrame;
            panel5.BackColor = Color.Transparent;
            history_button.BackColor = Color.Transparent;

            LoadAuthors();

        }

        private void dashboard_click(object sender, EventArgs e)
        {
            dashboard_panel.Visible = true;
            borrow_book_panel.Visible = false;
            current_book_panel.Visible = false;
            historyPanel.Visible= false;

            panel5.BackColor = Color.Transparent;
            panel4.BackColor = SystemColors.WindowFrame;
            panel6.BackColor = Color.Transparent;
            history_button.BackColor = Color.Transparent;


            loadCurrentBalance(this.student_id);
            loadMostBorrowed();
            load_top_authors();
            loadRecentTransact(this.student_id);
        }
        private void load_top_authors()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query the view
                    string query = "SELECT * FROM author_borrowing_stats ORDER BY borrow_counts DESC";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Clear existing data and columns
                    top_authors.Rows.Clear();
                    top_authors.Columns.Clear();

                    // Configure grid properties
                    top_authors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    top_authors.AllowUserToAddRows = false;
                    top_authors.RowHeadersVisible = false;
                    top_authors.ReadOnly = true;

                    // Add columns
                    top_authors.Columns.Add("author_id", "Author ID");
                    top_authors.Columns.Add("author_name", "Author Name");
                    top_authors.Columns.Add("number_of_books", "Book Count");
                    top_authors.Columns.Add("borrow_counts", "Borrow Count");

                    // Format columns
                    top_authors.Columns["author_id"].Width = 80;
                    top_authors.Columns["number_of_books"].Width = 90;
                    top_authors.Columns["borrow_counts"].Width = 90;
                    top_authors.Columns["number_of_books"].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;
                    top_authors.Columns["borrow_counts"].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;

                    // Apply styling (same as your other grid)
                    try
                    {
                        top_authors.DefaultCellStyle.Font = new Font("Poppins", 8, FontStyle.Regular);
                        top_authors.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);
                    }
                    catch
                    {
                        top_authors.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                        top_authors.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    }

                    // Visual improvements
                    top_authors.RowTemplate.Height = 30;
                    top_authors.RowsDefaultCellStyle.BackColor = Color.Gainsboro;

                    // Populate data
                    foreach (DataRow row in dt.Rows)
                    {
                        int rowIndex = top_authors.Rows.Add();
                        top_authors.Rows[rowIndex].Cells["author_id"].Value = row["author_id"];
                        top_authors.Rows[rowIndex].Cells["author_name"].Value = row["author_name"];
                        top_authors.Rows[rowIndex].Cells["number_of_books"].Value = row["number_of_books"];
                        top_authors.Rows[rowIndex].Cells["borrow_counts"].Value = row["borrow_counts"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading top authors: " + ex.Message);
            }
        }
        private void loadRecentTransact(int student_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("get_student_recent_borrowing", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_student_id", student_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Update labels with book information
                            current_title.Text = reader["book_title"].ToString();
                            current_author.Text = reader["author_name"].ToString();
                            current_categ.Text = reader["category_name"].ToString();
                            current_published_year.Text = reader["publication_year"].ToString();

                            // Update borrowing dates
                            current_borrow_date.Text = Convert.ToDateTime(reader["borrow_date"]).ToString("MMM. d, yyyy");
                            current_due_date.Text = Convert.ToDateTime(reader["due_date"]).ToString("MMM. d, yyyy");

                            

                            // Determine and set status
                            string status = reader["status"].ToString();
                            current_status.Text = status == "Active" ? "Borrowed" : status;

                            // Set status color
                            switch (status)
                            {
                                case "Overdue":
                                    current_status.ForeColor = Color.Red;
                                    break;
                                case "Active":
                                    current_status.ForeColor = Color.Green;
                                    break;
                                case "Returned":
                                    current_status.ForeColor = Color.Blue;
                                    break;
                            }
                        }
                        else
                        {
                            // No recent borrowing found
                            ClearCurrentBookLabels();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading recent transaction: " + ex.Message);
                ClearCurrentBookLabels();
            }
        }

        private void ClearCurrentBookLabels()
        {
            current_title.Text = "N/A";
            current_author.Text = "N/A";
            current_categ.Text = "N/A";
            current_published_year.Text = "N/A";
            current_borrow_date.Text = "N/A";
            current_due_date.Text = "N/A";
            current_status.Text = "No recent borrowing";
            current_status.ForeColor = Color.Black;
        }
        private void loadMostBorrowed()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Query the view
                    string query = "SELECT * FROM get_most_borrowed";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Clear existing data and columns
                    mostGrid.Rows.Clear();
                    mostGrid.Columns.Clear();

                    // Configure grid properties
                    mostGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    mostGrid.AllowUserToAddRows = false;
                    mostGrid.RowHeadersVisible = false;
                    mostGrid.ReadOnly = true;

                    // Add columns
                    mostGrid.Columns.Add("book_id", "Book ID");
                    mostGrid.Columns.Add("title", "Title");
                    mostGrid.Columns.Add("borrow_count", "Borrow Count");

                    // Format columns
                    mostGrid.Columns["book_id"].Width = 80;
                    mostGrid.Columns["borrow_count"].Width = 90;
                    mostGrid.Columns["borrow_count"].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleRight;

                    // Apply styling (similar to your other grids)
                    try
                    {
                        mostGrid.DefaultCellStyle.Font = new Font("Poppins", 8, FontStyle.Regular);
                        mostGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);
                    }
                    catch
                    {
                        mostGrid.DefaultCellStyle.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                        mostGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
                    }

                    // Visual improvements
                    mostGrid.RowTemplate.Height = 30;
                    mostGrid.RowsDefaultCellStyle.BackColor = Color.Gainsboro;

                    // Populate data
                    foreach (DataRow row in dt.Rows)
                    {
                        int rowIndex = mostGrid.Rows.Add();
                        mostGrid.Rows[rowIndex].Cells["book_id"].Value = row["book_id"];
                        mostGrid.Rows[rowIndex].Cells["title"].Value = row["title"];
                        mostGrid.Rows[rowIndex].Cells["borrow_count"].Value = row["borrow_count"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading most borrowed books: " + ex.Message);
            }
        }

        private void loadCurrentBalance(int student_id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("SELECT get_student_balance(@studentId)", connection))
                    {
                        command.Parameters.AddWithValue("@studentId", student_id);

                        object result = command.ExecuteScalar();

                        decimal balance = result != DBNull.Value ? Convert.ToDecimal(result) : 0;
                        curbal.Text = $"P {balance:N2}";
                    }
                }
            }
            catch (Exception ex)
            {
                curbal.Text = "P 0.00";
                MessageBox.Show("Error loading balance: " + ex.Message);
            }
        }
        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

       
        private void authors_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void LoadBorrowingHistory(int studentId, DataGridView historyGrid)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("get_student_borrowings", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("p_student_id", studentId);

                        DataTable dt = new DataTable();
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            adapter.Fill(dt);
                        }

                        // Configure grid appearance
                        historyGrid.DataSource = dt;
                        historyGrid.RowHeadersVisible = false;
                        historyGrid.AllowUserToAddRows = false;
                        historyGrid.BackgroundColor = Color.Gainsboro;
                        historyGrid.BorderStyle = BorderStyle.None;
                        historyGrid.DefaultCellStyle.BackColor = Color.Gainsboro;
                        historyGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;

                        // Set column headers and formatting
                        Dictionary<string, string> columnHeaders = new Dictionary<string, string>
                        {
                            {"book_id", "Book ID"},
                            {"book_title", "Book Title"},
                            {"borrow_date", "Borrow Date"},
                            {"due_date", "Due Date"},
                            {"return_date", "Return Date"},
                            {"return_type", "Return Type"},
                            {"borrow_duration", "Duration (Days)"}
                        };

                        foreach (DataGridViewColumn column in historyGrid.Columns)
                        {
                            // Apply header text if it exists in our dictionary
                            if (columnHeaders.ContainsKey(column.DataPropertyName))
                            {
                                column.HeaderText = columnHeaders[column.DataPropertyName];
                            }
                            else
                            {
                                column.Visible = false; // Hide any unexpected columns
                            }

                            // Format date columns
                            if (column.DataPropertyName.Contains("date"))
                            {
                                column.DefaultCellStyle.Format = "yyyy-MM-dd";
                                column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            }

                            // Enable text wrapping and set column width
                            column.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                            column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                        }

                        // Set font (Poppins with Segoe UI fallback)
                        try
                        {
                            historyGrid.Font = new Font("Poppins", 9f);
                            historyGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9f, FontStyle.Bold);
                        }
                        catch
                        {
                            historyGrid.Font = new Font("Segoe UI", 9f);
                            historyGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                        }

                        // Auto-resize rows to fit content
                        historyGrid.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowing history: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void history_click(object sender, EventArgs e)
        {
            dashboard_panel.Visible = false;
            borrow_book_panel.Visible = false;
            current_book_panel.Visible = false;
            historyPanel.Visible =true;

            panel5.BackColor = Color.Transparent;
            history_button.BackColor = SystemColors.WindowFrame;
            panel6.BackColor = Color.Transparent;
            panel4.BackColor = Color.Transparent;

            LoadBorrowingHistory(student_id, history_table);
        }

        private void current_title_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void top_nav_panel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
