using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Library_Management_System___Belaro
{
    
    public partial class Management : Form
    {
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";
        public int manager_id;
        public string manager_name;
        public int myID;

        public Management(int staff_id, int userId)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();

            myID = userId;
            this.manager_id = staff_id;
            dash_click();
            
        }
        private void dash_click()
        {
            dash_panel.Visible = true;
            books_panel.Visible = false;
            author_panel.Visible = false;
            staff_panel.Visible = false;
            report_panel.Visible = false;

            panel4.BackColor = Color.FromArgb(64, 64, 64);
            panel5.BackColor = Color.Transparent;
            panel6.BackColor = Color.Transparent;
            staff_button.BackColor = Color.Transparent;
            report_button.BackColor = Color.Transparent;


            getDashboardDisplay();
        }

        private void report_click()
        {
            dash_panel.Visible = false;
            books_panel.Visible = false;
            author_panel.Visible = false;
            staff_panel.Visible = false;
            report_panel.Visible = true;

            panel4.BackColor = Color.Transparent;
            panel5.BackColor = Color.Transparent;
            panel6.BackColor = Color.Transparent;
            staff_button.BackColor = Color.Transparent;
            report_button.BackColor = Color.FromArgb(64, 64, 64);


            loadStat("All Time");
            loadHistory();
        }

        private void loadHistory()
        {
            try
            {
                // Clear existing data
                history_grid.DataSource = null;
                history_grid.Columns.Clear();

                // Configure grid properties
                history_grid.Font = new Font("Poppins", 8);
                history_grid.BackgroundColor = Color.Gainsboro;
                history_grid.BorderStyle = BorderStyle.None;
                history_grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                history_grid.AllowUserToAddRows = false;
                history_grid.AllowUserToDeleteRows = false;
                history_grid.ReadOnly = true;
                history_grid.RowHeadersVisible = false;
                history_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                history_grid.RowTemplate.Height = 40;
                history_grid.DefaultCellStyle.BackColor = Color.Gainsboro;
                history_grid.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                history_grid.DefaultCellStyle.SelectionForeColor = Color.Black;
                history_grid.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                history_grid.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                // Load data from view
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM borrowing_detailed_view ORDER BY borrow_date DESC", connection))
                    {
                        DataTable dt = new DataTable();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            history_grid.DataSource = dt;
                        }
                    }
                }

                // Configure columns with presentable headers and fixed widths
                if (history_grid.Columns.Contains("borrowing_id"))
                {
                    history_grid.Columns["borrowing_id"].HeaderText = "ID";
                    history_grid.Columns["borrowing_id"].Width = 100;
                }

                if (history_grid.Columns.Contains("copy_id"))
                {
                    history_grid.Columns["copy_id"].HeaderText = "Copy ID";
                    history_grid.Columns["copy_id"].Width = 100;
                }

                if (history_grid.Columns.Contains("book_title"))
                {
                    history_grid.Columns["book_title"].HeaderText = "Book Title";
                    history_grid.Columns["book_title"].Width = 200;
                    history_grid.Columns["book_title"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                if (history_grid.Columns.Contains("student_full_name"))
                {
                    history_grid.Columns["student_full_name"].HeaderText = "Student Name";
                    history_grid.Columns["student_full_name"].Width = 180;
                    history_grid.Columns["student_full_name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                if (history_grid.Columns.Contains("borrow_date"))
                {
                    history_grid.Columns["borrow_date"].HeaderText = "Borrow Date";
                    history_grid.Columns["borrow_date"].Width = 120;
                    history_grid.Columns["borrow_date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                if (history_grid.Columns.Contains("due_date"))
                {
                    history_grid.Columns["due_date"].HeaderText = "Due Date";
                    history_grid.Columns["due_date"].Width = 120;
                    history_grid.Columns["due_date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                if (history_grid.Columns.Contains("approved_by_full_name"))
                {
                    history_grid.Columns["approved_by_full_name"].HeaderText = "Approved By";
                    history_grid.Columns["approved_by_full_name"].Width = 150;
                    history_grid.Columns["approved_by_full_name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                if (history_grid.Columns.Contains("return_date"))
                {
                    history_grid.Columns["return_date"].HeaderText = "Return Date";
                    history_grid.Columns["return_date"].Width = 120;
                    history_grid.Columns["return_date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                if (history_grid.Columns.Contains("return_type"))
                {
                    history_grid.Columns["return_type"].HeaderText = "Return Status";
                    history_grid.Columns["return_type"].Width = 120;
                }

                if (history_grid.Columns.Contains("borrow_duration"))
                {
                    history_grid.Columns["borrow_duration"].HeaderText = "Duration (Days)";
                    history_grid.Columns["borrow_duration"].Width = 130;
                }

                if (history_grid.Columns.Contains("returned_by_full_name"))
                {
                    history_grid.Columns["returned_by_full_name"].HeaderText = "Returned By";
                    history_grid.Columns["returned_by_full_name"].Width = 150;
                    history_grid.Columns["returned_by_full_name"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }

                // Style headers
                history_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);
                history_grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                history_grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                history_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                history_grid.ColumnHeadersHeight = 45;
                history_grid.EnableHeadersVisualStyles = false;
                history_grid.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                history_grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                // Set alternating row colors (keeping uniform as requested)
                history_grid.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowing history: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Management_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 loginform = new Form1();
            loginform.Show();
        }
        private void getDashboardDisplay()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("get_library_stats", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@librarian_id", this.manager_id);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Convert to string before assigning to Text properties
                                total_books.Text = reader["active_books"]?.ToString() ?? "0";
                                total_copies.Text = reader["active_copies"]?.ToString() ?? "0";
                                total_student.Text = reader["active_students"]?.ToString() ?? "0";
                                total_staff.Text = reader["active_staff"]?.ToString() ?? "0";
                                this.manager_name = reader["librarian_name"]?.ToString() ?? "Unknown";
                            }
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard data: " + ex.Message);
            }
            label9.Text = "Hello, Welcome " + this.manager_name + "!";
            acct_dropdown.Items.Clear();
            acct_dropdown.DropDownStyle = ComboBoxStyle.DropDownList;
            acct_dropdown.Items.Add(this.manager_name); // Default display
            acct_dropdown.Items.Add("My Account");
            acct_dropdown.Items.Add("Logout");
            acct_dropdown.SelectedIndex = 0;
            this.ActiveControl = null;
            loadDashGrid();
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
        private void label15_Click(object sender, EventArgs e)
        {

        }
        private void topborrwow_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void loadDashGrid()
        {
            // Clear existing data
            top_borrow_grid.DataSource = null;
            top_borrow_grid.Columns.Clear();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    // Get data from the view
                    string query = "SELECT * FROM get_most_borrowed_detailed";
                    using (MySqlCommand cmd = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            // Bind data to grid
                            top_borrow_grid.DataSource = dt;
                            // Configure grid appearance
                            ConfigureTopBorrowedGrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading most borrowed books: " + ex.Message);
            }
        }
        private void ConfigureTopBorrowedGrid()
        {
            // Set basic grid properties
            top_borrow_grid.ReadOnly = true;
            top_borrow_grid.AllowUserToAddRows = false;
            top_borrow_grid.AllowUserToDeleteRows = false;
            top_borrow_grid.AllowUserToOrderColumns = false;
            top_borrow_grid.AllowUserToResizeRows = false;
            top_borrow_grid.RowHeadersVisible = false;
            top_borrow_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Remove all borders
            top_borrow_grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            top_borrow_grid.BorderStyle = BorderStyle.None;

            // Set uniform background color for all rows
            top_borrow_grid.DefaultCellStyle.BackColor = Color.Gainsboro;
            top_borrow_grid.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            top_borrow_grid.BackgroundColor = Color.Gainsboro;

            // Set font and row height
            top_borrow_grid.DefaultCellStyle.Font = new Font("Poppins", 8);
            top_borrow_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);
            top_borrow_grid.RowTemplate.Height = 25;

            // Customize column headers and widths
            if (top_borrow_grid.Columns.Count > 0)
            {
                // Set column header text (make presentable)
                top_borrow_grid.Columns["book_id"].HeaderText = "ID";
                top_borrow_grid.Columns["title"].HeaderText = "Book Title";
                top_borrow_grid.Columns["author"].HeaderText = "Author";
                top_borrow_grid.Columns["category"].HeaderText = "Category";
                top_borrow_grid.Columns["publication_year"].HeaderText = "Year";
                top_borrow_grid.Columns["borrow_count"].HeaderText = "Times Borrowed";
                top_borrow_grid.Columns["available_copies"].HeaderText = "Available";
                top_borrow_grid.Columns["total_copies"].HeaderText = "Total Copies";

                // Set column widths - make title and author wider
                top_borrow_grid.Columns["title"].FillWeight = 200; // Wider for titles
                top_borrow_grid.Columns["author"].FillWeight = 150; // Wider for authors
                top_borrow_grid.Columns["book_id"].FillWeight = 40;
                top_borrow_grid.Columns["category"].FillWeight = 80;
                top_borrow_grid.Columns["publication_year"].FillWeight = 50;
                top_borrow_grid.Columns["borrow_count"].FillWeight = 70;
                top_borrow_grid.Columns["available_copies"].FillWeight = 60;
                top_borrow_grid.Columns["total_copies"].FillWeight = 60;

                // Center-align numeric columns
                top_borrow_grid.Columns["book_id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                top_borrow_grid.Columns["publication_year"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                top_borrow_grid.Columns["borrow_count"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                top_borrow_grid.Columns["available_copies"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                top_borrow_grid.Columns["total_copies"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        public void loadBooks()
        {
            try
            {
                // Clear existing columns if reloading
                books_grid.Columns.Clear();

                // Configure the books_grid appearance
                books_grid.Font = new Font("Poppins", 8.9f);
                books_grid.BorderStyle = BorderStyle.None;
                books_grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                books_grid.BackgroundColor = Color.Gainsboro;
                books_grid.RowsDefaultCellStyle.BackColor = Color.Gainsboro;
                books_grid.AllowUserToAddRows = false;
                books_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                books_grid.RowHeadersVisible = false;
                books_grid.ReadOnly = true;
                books_grid.CellClick += new DataGridViewCellEventHandler(books_grid_CellClick);

                // Load data directly from the view
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    string query = "SELECT * FROM enhanced_book_borrowing_stats";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    books_grid.DataSource = dataTable;

                    // Rename columns
                    RenameColumn("book_id", "ID");
                    RenameColumn("title", "Title");
                    RenameColumn("author_name", "Author");
                    RenameColumn("author_nationality", "Author Nationality");
                    RenameColumn("publication_year", "Year");
                    RenameColumn("category_name", "Category");
                    RenameColumn("borrow_count", "Times Borrowed");
                    RenameColumn("total_copies", "Total Copies");
                    RenameColumn("available_copies", "Available");
                    RenameColumn("borrowed_copies", "Borrowed");

                    // Format headers and columns
                    books_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9f, FontStyle.Bold);
                    books_grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    books_grid.ColumnHeadersHeight = 35;
                    CenterAlignColumns("ID", "Year", "Times Borrowed", "Total Copies", "Available", "Borrowed");
                }

                // Add View button column last
                DataGridViewButtonColumn viewButtonColumn = new DataGridViewButtonColumn();
                viewButtonColumn.HeaderText = "Action";
                viewButtonColumn.Name = "View";
                viewButtonColumn.Text = "View";
                viewButtonColumn.UseColumnTextForButtonValue = true;
                viewButtonColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                books_grid.Columns.Add(viewButtonColumn);
                viewButtonColumn.DisplayIndex = books_grid.Columns.Count - 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void books_grid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in the View column and not header
            if (e.RowIndex >= 0 && books_grid.Columns[e.ColumnIndex].Name == "View")
            {
                // Get the book ID and title from the selected row
                string bookId = books_grid.Rows[e.RowIndex].Cells["book_id"].Value.ToString();
                string title = books_grid.Rows[e.RowIndex].Cells["title"].Value.ToString();

                // Show book details
                getSpecific_Book(bookId, title);
            }
        }

        private void getSpecific_Book(string bookId, string title)
        {
            // Convert the bookId string to an integer (assuming it's a valid integer)
            if (int.TryParse(bookId, out int id))
            {
                // Create and show the SpecificBook dialog
                SpecificBook specificBook = new SpecificBook(id, title, this);
                specificBook.ShowDialog();
            }
            else
            {
                // Handle the case where bookId is not a valid integer
                MessageBox.Show("Invalid Book ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Helper method to rename columns
        private void RenameColumn(string originalName, string newName)
        {
            if (books_grid.Columns.Contains(originalName))
            {
                books_grid.Columns[originalName].HeaderText = newName;
            }
        }

        // Helper method to center-align columns
        private void CenterAlignColumns(params string[] columnNames)
        {
            foreach (var columnName in columnNames)
            {
                if (books_grid.Columns.Contains(columnName))
                {
                    books_grid.Columns[columnName].DefaultCellStyle.Alignment =
                        DataGridViewContentAlignment.MiddleCenter;
                }
            }
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void books_click(object sender, EventArgs e)
        {
            loadBooks();
            dash_panel.Visible = false;
            author_panel.Visible = false;
            books_panel.Visible = true;
            report_panel.Visible = false;

            panel5.BackColor = Color.FromArgb(64, 64, 64);
            panel4.BackColor = Color.Transparent;
            panel6.BackColor = Color.Transparent;
            staff_button.BackColor = Color.Transparent;
            report_button.BackColor = Color.Transparent;
        }

        private void dashClick(object sender, EventArgs e)
        {
            dash_click();
        }

        private void author_click(object sender, EventArgs e)
        {
            panel6.BackColor = Color.FromArgb(64, 64, 64);
            panel4.BackColor = Color.Transparent;
            panel5.BackColor = Color.Transparent;
            staff_button.BackColor = Color.Transparent;
            report_button.BackColor = Color.Transparent;

            dash_panel.Visible = false;
            books_panel.Visible = false;
            author_panel.Visible = true;
            staff_panel.Visible = false;
            report_panel.Visible = false;
            loadAuthors();
        }

        private void staff_click(object sender, EventArgs e)
        {
            panel6.BackColor = Color.Transparent;
            panel4.BackColor = Color.Transparent;
            panel5.BackColor = Color.Transparent;
            staff_button.BackColor = Color.FromArgb(64, 64, 64);
            report_button.BackColor = Color.Transparent;

            dash_panel.Visible = false;
            books_panel.Visible = false;
            author_panel.Visible = false;
            staff_panel.Visible = true;
            report_panel.Visible = false;


            loadStaff();

        }
        private void report_button_click(object sender, EventArgs e)
        {
            report_click();
        }

        private void addBookButton_Click(object sender, EventArgs e)
        {
            // Create a new form for the dialog
            Form addBookDialog = new Form()
            {
                Width = 400,
                Height = 350,
                Text = "Add New Book",
                StartPosition = FormStartPosition.CenterParent
            };

            // Create controls
            Label titleLabel = new Label() { Text = "Title:", Left = 20, Top = 20, Width = 80 };
            TextBox titleTextBox = new TextBox() { Left = 110, Top = 20, Width = 250 };

            Label authorLabel = new Label() { Text = "Author:", Left = 20, Top = 60, Width = 80 };
            ComboBox authorComboBox = new ComboBox() { Left = 110, Top = 60, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            Label categoryLabel = new Label() { Text = "Category:", Left = 20, Top = 100, Width = 80 };
            ComboBox categoryComboBox = new ComboBox() { Left = 110, Top = 100, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            Label yearLabel = new Label() { Text = "Publication Year:", Left = 20, Top = 140, Width = 80 };
            NumericUpDown yearNumeric = new NumericUpDown() { Left = 110, Top = 140, Width = 100, Minimum = 1000, Maximum = DateTime.Now.Year };
            yearNumeric.Value = DateTime.Now.Year; // Set default to current year

            Label statusLabel = new Label() { Text = "Status:", Left = 20, Top = 180, Width = 80 };
            ComboBox statusComboBox = new ComboBox() { Left = 110, Top = 180, Width = 150, DropDownStyle = ComboBoxStyle.DropDownList };
            statusComboBox.Items.AddRange(new string[] { "active", "inactive", "archived" });
            statusComboBox.SelectedIndex = 0;

            Button okButton = new Button() { Text = "OK", Left = 110, Top = 230, Width = 80 };
            Button cancelButton = new Button() { Text = "Cancel", Left = 200, Top = 230, Width = 80 };

            // Lists to store author and category data
            List<KeyValuePair<object, string>> authors = new List<KeyValuePair<object, string>>();
            List<KeyValuePair<object, string>> categories = new List<KeyValuePair<object, string>>();

            // Populate authors and categories dropdowns
            try
            {
                // Modified SQL query to use the view
                string query = "SELECT DISTINCT author_id, author_name, category_id, category_name FROM prepate_for_book_insertion";

                // Get data from the view
                var viewData = GetDataFromDatabase(query);

                // Process the data for authors and categories
                foreach (var row in viewData)
                {
                    // Add unique authors
                    var authorId = row["author_id"];
                    var authorName = row["author_name"].ToString();
                    if (!authors.Any(a => a.Key.Equals(authorId)))
                    {
                        authors.Add(new KeyValuePair<object, string>(authorId, authorName));
                    }

                    // Add unique categories
                    var categoryId = row["category_id"];
                    var categoryName = row["category_name"].ToString();
                    if (!categories.Any(c => c.Key.Equals(categoryId)))
                    {
                        categories.Add(new KeyValuePair<object, string>(categoryId, categoryName));
                    }
                }

                // Set up author combobox
                authorComboBox.DataSource = new BindingSource(authors, null);
                authorComboBox.DisplayMember = "Value";
                authorComboBox.ValueMember = "Key";

                // Set up category combobox
                categoryComboBox.DataSource = new BindingSource(categories, null);
                categoryComboBox.DisplayMember = "Value";
                categoryComboBox.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load authors and categories: " + ex.Message);
            }

            // Button events
            okButton.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(titleTextBox.Text))
                {
                    MessageBox.Show("Please enter a book title");
                    return;
                }

                if (authorComboBox.SelectedValue == null || categoryComboBox.SelectedValue == null)
                {
                    MessageBox.Show("Please select both author and category");
                    return;
                }

                try
                {
                    // Get next book ID
                    string insertProcedure = "CALL insert_book(p_title, p_author_id, p_category_id, p_publication_year, p_status)";

                    var parameters = new Dictionary<string, object>
                    {
                        { "p_title", titleTextBox.Text.Trim() },
                        { "p_author_id", authorComboBox.SelectedValue },
                        { "p_category_id", categoryComboBox.SelectedValue },
                        { "p_publication_year", (int)yearNumeric.Value },
                        { "p_status", statusComboBox.SelectedItem.ToString() }
                    };



                    ExecuteDatabaseQuery(insertProcedure, parameters);

                    MessageBox.Show("Book added successfully!");
                    addBookDialog.DialogResult = DialogResult.OK;
                    addBookDialog.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding book: " + ex.Message);
                }
            };

            cancelButton.Click += (s, ev) =>
            {
                addBookDialog.DialogResult = DialogResult.Cancel;
                addBookDialog.Close();
            };

            // Add controls to the form
            addBookDialog.Controls.Add(titleLabel);
            addBookDialog.Controls.Add(titleTextBox);
            addBookDialog.Controls.Add(authorLabel);
            addBookDialog.Controls.Add(authorComboBox);
            addBookDialog.Controls.Add(categoryLabel);
            addBookDialog.Controls.Add(categoryComboBox);
            addBookDialog.Controls.Add(yearLabel);
            addBookDialog.Controls.Add(yearNumeric);
            addBookDialog.Controls.Add(statusLabel);
            addBookDialog.Controls.Add(statusComboBox);
            addBookDialog.Controls.Add(okButton);
            addBookDialog.Controls.Add(cancelButton);

            // Show the dialog
            if (addBookDialog.ShowDialog() == DialogResult.OK)
            {
                // Refresh your book list or grid here
                RefreshBookList(); // You'll need to implement this method
            }
        }

        // Helper methods with proper MySQL implementations
        private List<Dictionary<string, object>> GetDataFromDatabase(string query)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetName(i), reader.GetValue(i));
                                }
                                results.Add(row);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database error: " + ex.Message);
            }

            return results;
        }

        private void ExecuteDatabaseQuery(string query, Dictionary<string, object> parameters)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Set command type for stored procedures
                        if (query.StartsWith("CALL ", StringComparison.OrdinalIgnoreCase))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            // Extract the procedure name without the CALL keyword
                            string procName = query.Substring(5);
                            // Remove any parameter names from the procedure string
                            if (procName.Contains("("))
                            {
                                procName = procName.Substring(0, procName.IndexOf("("));
                            }
                            command.CommandText = procName;
                        }

                        // Add all parameters to the command
                        foreach (var param in parameters)
                        {
                            command.Parameters.AddWithValue(param.Key, param.Value);
                        }

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Database execution error: " + ex.Message);
            }
        }
        // Add this method to refresh the book list after adding a new book
        private void RefreshBookList()
        {
            // Implement this method to refresh your book data display
            // For example:
            loadBooks();
        }

        private void books_grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void loadAuthors()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Clear existing data and configure grid
                    authors_grid.DataSource = null;
                    authors_grid.Columns.Clear();

                    // Configure grid properties - clean minimalist style
                    authors_grid.Font = new Font("Poppins", 9);
                    authors_grid.BackgroundColor = Color.Gainsboro;
                    authors_grid.BorderStyle = BorderStyle.None;
                    authors_grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    authors_grid.AllowUserToAddRows = false;
                    authors_grid.RowHeadersVisible = false;
                    authors_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Changed from Fill to None
                    authors_grid.ReadOnly = true;
                    authors_grid.DefaultCellStyle.BackColor = Color.Gainsboro;
                    authors_grid.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                    authors_grid.DefaultCellStyle.SelectionForeColor = Color.Black;

                    // Get data from view
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM active_authors_with_book_counts", connection))
                    {
                        DataTable dt = new DataTable();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            authors_grid.DataSource = dt;
                        }
                    }

                    // Format headers
                    authors_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);
                    authors_grid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    authors_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    authors_grid.ColumnHeadersHeight = 35;
                    authors_grid.EnableHeadersVisualStyles = false;
                    authors_grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                    authors_grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

                    // Configure column widths
                    if (authors_grid.Columns.Contains("author_name"))
                    {
                        authors_grid.Columns["author_name"].HeaderText = "Author Name";
                        authors_grid.Columns["author_name"].Width = 300; // Set fixed width here
                    }

                    if (authors_grid.Columns.Contains("date_of_birth"))
                    {
                        authors_grid.Columns["date_of_birth"].HeaderText = "Birth Date";
                        authors_grid.Columns["date_of_birth"].Width = 100;
                        authors_grid.Columns["date_of_birth"].DefaultCellStyle.Format = "yyyy-MM-dd";
                        authors_grid.Columns["date_of_birth"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }

                    if (authors_grid.Columns.Contains("nationality"))
                    {
                        authors_grid.Columns["nationality"].HeaderText = "Nationality";
                        authors_grid.Columns["nationality"].Width = 130;
                    }

                    if (authors_grid.Columns.Contains("active_books_count"))
                    {
                        authors_grid.Columns["active_books_count"].HeaderText = "Books";
                        authors_grid.Columns["active_books_count"].Width = 60;
                        authors_grid.Columns["active_books_count"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }

                    // Add action buttons
                    var editColumn = new DataGridViewButtonColumn()
                    {
                        Name = "Edit",
                        Text = "Edit",
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Flat,
                        HeaderText = "Actions",
                        Width = 70,
                        DefaultCellStyle = new DataGridViewCellStyle()
                        {
                            BackColor = Color.Gainsboro,
                            SelectionBackColor = Color.LightGray
                        }
                    };

                    var deleteColumn = new DataGridViewButtonColumn()
                    {
                        Name = "Delete",
                        Text = "Delete",
                        UseColumnTextForButtonValue = true,
                        FlatStyle = FlatStyle.Flat,
                        Width = 70,
                        DefaultCellStyle = new DataGridViewCellStyle()
                        {
                            BackColor = Color.Gainsboro,
                            SelectionBackColor = Color.LightGray
                        }
                    };

                    authors_grid.Columns.Add(editColumn);
                    authors_grid.Columns.Add(deleteColumn);

                    // Manage event handlers
                    authors_grid.CellClick -= AuthorsGrid_CellClick;
                    authors_grid.CellClick += AuthorsGrid_CellClick;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading authors: " + ex.Message, "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AuthorsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var grid = (DataGridView)sender;

            if (grid.Columns[e.ColumnIndex].Name == "Edit")
            {
                var authorId = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["author_id"].Value);
                EditAuthor(authorId);
            }
            else if (grid.Columns[e.ColumnIndex].Name == "Delete")
            {
                var authorId = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["author_id"].Value);
                DeleteAuthor(authorId);
            }
        }

        private void EditAuthor(int authorId)
        {
            // Fetch existing author details first
            Author author = GetAuthorById(authorId);

            if (author == null)
            {
                MessageBox.Show("Author not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var editForm = new Form())
            {
                editForm.Text = "Edit Author";
                editForm.Size = new Size(400, 250);
                editForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                editForm.StartPosition = FormStartPosition.CenterParent;
                editForm.MaximizeBox = false;
                editForm.MinimizeBox = false;

                // First Name
                var lblFirstName = new Label { Text = "First Name:", Left = 20, Top = 20, Width = 100 };
                var txtFirstName = new TextBox { Left = 120, Top = 20, Width = 250, Text = author.FirstName };

                // Last Name
                var lblLastName = new Label { Text = "Last Name:", Left = 20, Top = 50, Width = 100 };
                var txtLastName = new TextBox { Left = 120, Top = 50, Width = 250, Text = author.LastName };

                // Nationality
                var lblNationality = new Label { Text = "Nationality:", Left = 20, Top = 80, Width = 100 };
                var txtNationality = new TextBox { Left = 120, Top = 80, Width = 250, Text = author.Nationality };

                // Birth Date
                var lblBirthDate = new Label { Text = "Birth Date:", Left = 20, Top = 110, Width = 100 };
                var dtpBirthDate = new DateTimePicker
                {
                    Left = 120,
                    Top = 110,
                    Width = 250,
                    Format = DateTimePickerFormat.Short,
                    Value = author.DateOfBirth
                };

                // Buttons
                var btnOK = new Button { Text = "Save", Left = 120, Top = 150, Width = 100, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancel", Left = 230, Top = 150, Width = 100, DialogResult = DialogResult.Cancel };

                btnOK.Click += (s, ev) => { editForm.DialogResult = DialogResult.OK; };

                editForm.Controls.AddRange(new Control[] {
            lblFirstName, txtFirstName,
            lblLastName, txtLastName,
            lblNationality, txtNationality,
            lblBirthDate, dtpBirthDate,
            btnOK, btnCancel
        });

                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            using (MySqlCommand cmd = new MySqlCommand("update_author", connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_author_id", authorId);
                                cmd.Parameters.AddWithValue("p_first_name", txtFirstName.Text);
                                cmd.Parameters.AddWithValue("p_last_name", txtLastName.Text);
                                cmd.Parameters.AddWithValue("p_date_of_birth", dtpBirthDate.Value.Date);
                                cmd.Parameters.AddWithValue("p_nationality", txtNationality.Text);

                                cmd.ExecuteNonQuery();
                                MessageBox.Show("Author updated successfully!");

                                // Refresh your authors grid if needed
                                loadAuthors();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error updating author: {ex.Message}", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private Author GetAuthorById(int authorId)
        {
            Author author = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("get_author_by_id", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("p_author_id", authorId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                author = new Author
                                {
                                    Id = reader.GetInt32("author_id"),
                                    FirstName = reader.GetString("first_name"),
                                    LastName = reader.GetString("last_name"),
                                    DateOfBirth = reader.GetDateTime("date_of_birth"),
                                    Nationality = reader.GetString("nationality")
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching author details: {ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return author;
        }

        public class Author
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Nationality { get; set; }
        }

        private void DeleteAuthor(int authorId)
        {
            var result = MessageBox.Show("Deactivate this author and all their books?", "Confirm",
                                       MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand("deactivate_author", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("p_author_id", authorId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    loadAuthors(); // Refresh the grid
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Deactivation failed: {ex.Message}", "Error",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var addForm = new Form())
            {
                addForm.Text = "Add New Author";
                addForm.Size = new Size(400, 250);
                addForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                addForm.StartPosition = FormStartPosition.CenterParent;
                addForm.MaximizeBox = false;
                addForm.MinimizeBox = false;

                // First Name
                var lblFirstName = new Label { Text = "First Name:", Left = 20, Top = 20, Width = 100 };
                var txtFirstName = new TextBox { Left = 120, Top = 20, Width = 250 };

                // Last Name
                var lblLastName = new Label { Text = "Last Name:", Left = 20, Top = 50, Width = 100 };
                var txtLastName = new TextBox { Left = 120, Top = 50, Width = 250 };

                // Nationality
                var lblNationality = new Label { Text = "Nationality:", Left = 20, Top = 80, Width = 100 };
                var txtNationality = new TextBox { Left = 120, Top = 80, Width = 250 };

                // Birth Date
                var lblBirthDate = new Label { Text = "Birth Date:", Left = 20, Top = 110, Width = 100 };
                var dtpBirthDate = new DateTimePicker { Left = 120, Top = 110, Width = 250, Format = DateTimePickerFormat.Short };

                // Buttons
                var btnOK = new Button { Text = "Save", Left = 120, Top = 150, Width = 100, DialogResult = DialogResult.OK };
                var btnCancel = new Button { Text = "Cancel", Left = 230, Top = 150, Width = 100, DialogResult = DialogResult.Cancel };

                btnOK.Click += (s, ev) => { addForm.DialogResult = DialogResult.OK; };

                addForm.Controls.AddRange(new Control[] {
            lblFirstName, txtFirstName,
            lblLastName, txtLastName,
            lblNationality, txtNationality,
            lblBirthDate, dtpBirthDate,
            btnOK, btnCancel
        });

                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (MySqlConnection connection = new MySqlConnection(connectionString))
                        {
                            connection.Open();
                            using (MySqlCommand cmd = new MySqlCommand("insert_author", connection))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("p_first_name", txtFirstName.Text);
                                cmd.Parameters.AddWithValue("p_last_name", txtLastName.Text);
                                cmd.Parameters.AddWithValue("p_date_of_birth", dtpBirthDate.Value.Date);
                                cmd.Parameters.AddWithValue("p_nationality", txtNationality.Text);

                                // Execute and get the new author ID
                                int newAuthorId = Convert.ToInt32(cmd.ExecuteScalar());
                                MessageBox.Show($"Author added successfully with ID: {newAuthorId}");

                                // Refresh your authors grid if needed
                                loadAuthors();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error adding author: {ex.Message}", "Error",
                                      MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Create the main form
            Form insertForm = new Form();
            insertForm.Text = "Add New Staff Member";
            insertForm.Width = 400;
            insertForm.Height = 400;
            insertForm.StartPosition = FormStartPosition.CenterParent;
            insertForm.Font = new Font("Poppins", 9);

            // Create a panel to hold all controls
            Panel mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.AutoScroll = true;
            insertForm.Controls.Add(mainPanel);

            int currentY = 20;

            // Header
            Label staffHeader = new Label
            {
                Text = "Staff Information",
                Font = new Font("Poppins", 10, FontStyle.Bold),
                Location = new Point(20, currentY),
                AutoSize = true
            };
            mainPanel.Controls.Add(staffHeader);
            currentY += 30;

            // First Name
            Label lblFirstName = new Label { Text = "First Name:", Location = new Point(20, currentY), AutoSize = true };
            mainPanel.Controls.Add(lblFirstName);

            TextBox txtFirstName = new TextBox { Location = new Point(150, currentY), Width = 200 };
            mainPanel.Controls.Add(txtFirstName);
            currentY += 30;

            // Last Name
            Label lblLastName = new Label { Text = "Last Name:", Location = new Point(20, currentY), AutoSize = true };
            mainPanel.Controls.Add(lblLastName);

            TextBox txtLastName = new TextBox { Location = new Point(150, currentY), Width = 200 };
            mainPanel.Controls.Add(txtLastName);
            currentY += 30;

            // Hire Date
            Label lblHireDate = new Label { Text = "Hire Date:", Location = new Point(20, currentY), AutoSize = true };
            mainPanel.Controls.Add(lblHireDate);

            DateTimePicker dtpHireDate = new DateTimePicker { Location = new Point(150, currentY), Width = 200 };
            mainPanel.Controls.Add(dtpHireDate);
            currentY += 30;

            // Role
            Label lblRole = new Label { Text = "Role:", Location = new Point(20, currentY), AutoSize = true };
            mainPanel.Controls.Add(lblRole);

            ComboBox cmbRole = new ComboBox
            {
                Location = new Point(150, currentY),
                Width = 200,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            mainPanel.Controls.Add(cmbRole);
            currentY += 40;

            // Load roles from view
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT role_id, role FROM staff_roles_view", connection))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cmbRole.Items.Add(new KeyValuePair<int, string>(
                                Convert.ToInt32(reader["role_id"]),
                                reader["role"].ToString()));
                        }
                    }
                }

                cmbRole.DisplayMember = "Value";
                cmbRole.ValueMember = "Key";
                if (cmbRole.Items.Count > 0)
                    cmbRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}");
            }

            // Submit Button
            Button btnSubmit = new Button
            {
                Text = "Submit",
                Font = new Font("Poppins", 9, FontStyle.Bold),
                Location = new Point(150, currentY),
                Width = 100,
                BackColor = Color.FromArgb(0, 123, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSubmit.FlatAppearance.BorderSize = 0;
            mainPanel.Controls.Add(btnSubmit);

            // Click Event
            btnSubmit.Click += (s, ev) =>
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(txtFirstName.Text) ||
                        string.IsNullOrWhiteSpace(txtLastName.Text) ||
                        cmbRole.SelectedItem == null)
                    {
                        MessageBox.Show("Please fill in all required fields.", "Validation Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int roleId = ((KeyValuePair<int, string>)cmbRole.SelectedItem).Key;

                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand("InsertStaff", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@p_first_name", txtFirstName.Text);
                            cmd.Parameters.AddWithValue("@p_last_name", txtLastName.Text);
                            cmd.Parameters.AddWithValue("@p_hire_date", dtpHireDate.Value.Date);
                            cmd.Parameters.AddWithValue("@p_role_id", roleId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Staff member added successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    insertForm.DialogResult = DialogResult.OK;
                    loadStaff(); // Refresh grid
                    insertForm.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error adding staff member: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            // Show the form
            insertForm.ShowDialog();
        }


        public void loadStaff()
        {
            try
            {
                // Clear existing data
                staff_grid.DataSource = null;
                staff_grid.Columns.Clear();

                // Configure grid properties
                staff_grid.Font = new Font("Poppins", 9);
                staff_grid.BackgroundColor = Color.Gainsboro;
                staff_grid.BorderStyle = BorderStyle.None;
                staff_grid.CellBorderStyle = DataGridViewCellBorderStyle.None;
                staff_grid.AllowUserToAddRows = false;
                staff_grid.RowHeadersVisible = false;
                staff_grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                staff_grid.RowTemplate.Height = 30;
                staff_grid.DefaultCellStyle.BackColor = Color.Gainsboro;
                staff_grid.DefaultCellStyle.SelectionBackColor = Color.LightGray;
                staff_grid.DefaultCellStyle.SelectionForeColor = Color.Black;

                // Load data from view
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM active_staff_view", connection))
                    {
                        DataTable dt = new DataTable();
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            dt.Load(reader);
                            staff_grid.DataSource = dt;
                        }
                    }
                }

                // Configure columns
                if (staff_grid.Columns.Contains("full_name"))
                {
                    staff_grid.Columns["full_name"].HeaderText = "Full Name";
                    staff_grid.Columns["full_name"].Width = 300;
                }

                if (staff_grid.Columns.Contains("hire_date"))
                {
                    staff_grid.Columns["hire_date"].HeaderText = "Hire Date";
                    staff_grid.Columns["hire_date"].Width = 170;
                    staff_grid.Columns["hire_date"].DefaultCellStyle.Format = "yyyy-MM-dd";
                }

                if (staff_grid.Columns.Contains("role_id"))
                {
                    staff_grid.Columns["role_id"].HeaderText = "Role";
                    staff_grid.Columns["role_id"].Width = 200;
                }

                // Add action buttons
                var editColumn = new DataGridViewButtonColumn()
                {
                    Name = "Edit",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat,
                    Width = 70,
                    DefaultCellStyle = new DataGridViewCellStyle()
                    {
                        BackColor = Color.Gainsboro,
                        SelectionBackColor = Color.LightGray
                    }
                };

                var deleteColumn = new DataGridViewButtonColumn()
                {
                    Name = "Delete",
                    Text = "Remove",
                    UseColumnTextForButtonValue = true,
                    FlatStyle = FlatStyle.Flat,
                    Width = 70,
                    DefaultCellStyle = new DataGridViewCellStyle()
                    {
                        BackColor = Color.Gainsboro,
                        SelectionBackColor = Color.LightGray
                    }
                };

                staff_grid.Columns.Add(editColumn);
                staff_grid.Columns.Add(deleteColumn);

                // Style headers
                staff_grid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9, FontStyle.Bold);
                staff_grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
                staff_grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
                staff_grid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                staff_grid.ColumnHeadersHeight = 40;
                staff_grid.EnableHeadersVisualStyles = false;

                // Add button click event handler
                staff_grid.CellClick += (sender, e) =>
                {
                    if (e.RowIndex < 0) return; // Header row clicked

                    var grid = (DataGridView)sender;
                    int staffId = Convert.ToInt32(grid.Rows[e.RowIndex].Cells["staff_id"].Value);

                    if (grid.Columns[e.ColumnIndex].Name == "Edit")
                    {
                        EditStaff(staffId);
                    }
                    else if (grid.Columns[e.ColumnIndex].Name == "Delete")
                    {
                        RemoveStaff(staffId);
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading staff: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditStaff(int staffId)
        {
            try
            {
                using (var editForm = new Form())
                {
                    editForm.Text = "Edit Staff Member";
                    editForm.Size = new Size(400, 300);
                    editForm.StartPosition = FormStartPosition.CenterParent;

                    // Get staff details using stored procedure
                    DataTable staffData = new DataTable();
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand("CALL get_staff_details(@id)", connection))
                        {
                            cmd.Parameters.AddWithValue("@id", staffId);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                staffData.Load(reader);
                            }
                        }
                    }

                    if (staffData.Rows.Count == 0)
                    {
                        MessageBox.Show("Staff member not found");
                        return;
                    }

                    DataRow staff = staffData.Rows[0];

                    // Create form controls
                    var lblFirstName = new Label { Text = "First Name:", Left = 20, Top = 20, Width = 100 };
                    var txtFirstName = new TextBox { Left = 130, Top = 20, Width = 250, Text = staff["first_name"].ToString() };

                    var lblLastName = new Label { Text = "Last Name:", Left = 20, Top = 60, Width = 100 };
                    var txtLastName = new TextBox { Left = 130, Top = 60, Width = 250, Text = staff["last_name"].ToString() };

                    var lblHireDate = new Label { Text = "Hire Date:", Left = 20, Top = 100, Width = 100 };
                    var dtpHireDate = new DateTimePicker
                    {
                        Left = 130,
                        Top = 100,
                        Width = 250,
                        Value = Convert.ToDateTime(staff["hire_date"])
                    };

                    var btnSave = new Button { Text = "Save", Left = 130, Top = 180, Width = 100 };
                    var btnCancel = new Button { Text = "Cancel", Left = 250, Top = 180, Width = 100 };

                    btnSave.Click += (s, e) =>
                    {
                        try
                        {
                            using (MySqlConnection connection = new MySqlConnection(connectionString))
                            {
                                connection.Open();
                                using (MySqlCommand cmd = new MySqlCommand("CALL update_staff_member(@staffId, @firstName, @lastName, @hireDate)", connection))
                                {
                                    cmd.Parameters.AddWithValue("@staffId", staffId);
                                    cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text);
                                    cmd.Parameters.AddWithValue("@lastName", txtLastName.Text);
                                    cmd.Parameters.AddWithValue("@hireDate", dtpHireDate.Value);
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            editForm.DialogResult = DialogResult.OK;
                            editForm.Close();
                            loadStaff(); // Refresh grid
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Error updating staff: {ex.Message}");
                        }
                    };

                    btnCancel.Click += (s, e) =>
                    {
                        editForm.DialogResult = DialogResult.Cancel;
                        editForm.Close();
                    };

                    editForm.Controls.AddRange(new Control[]
                    {
                lblFirstName, txtFirstName,
                lblLastName, txtLastName,
                lblHireDate, dtpHireDate,
                btnSave, btnCancel
                    });

                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        MessageBox.Show("Staff member updated successfully");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing staff: {ex.Message}");
            }
        }

        private void RemoveStaff(int staffId)
        {
            var result = MessageBox.Show("Are you sure you want to remove this staff member?",
                                       "Confirm Removal",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand("deactivate_staff", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@staffId", staffId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    loadStaff(); // Refresh grid
                    MessageBox.Show("Staff member removed successfully");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error removing staff: {ex.Message}");
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected time period
            string selectedTimePeriod = comboBox1.SelectedItem.ToString();

            // Call loadStat with the selected time period
            loadStat(selectedTimePeriod);
        }
        private void loadStat(string timePeriod)
        {
            try
            {
                // Default values in case of error
                int pendingOverdueCount = 0;
                int returnedOverdueCount = 0;
                int returnedOntimeCount = 0;
                int totalBorrowedCount = 0;

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Call the stored procedure with the selected time period
                    using (MySqlCommand cmd = new MySqlCommand("GetBorrowingStatistics", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("time_period", timePeriod);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Read values from the result set
                                pendingOverdueCount = reader.IsDBNull(reader.GetOrdinal("pending_overdue")) ?
                                    0 : reader.GetInt32("pending_overdue");

                                returnedOverdueCount = reader.IsDBNull(reader.GetOrdinal("returned_overdue")) ?
                                    0 : reader.GetInt32("returned_overdue");

                                returnedOntimeCount = reader.IsDBNull(reader.GetOrdinal("returned_ontime")) ?
                                    0 : reader.GetInt32("returned_ontime");

                                totalBorrowedCount = reader.IsDBNull(reader.GetOrdinal("total_borrowed")) ?
                                    0 : reader.GetInt32("total_borrowed");
                            }
                        }
                    }
                }

                // Update the labels with the statistics
                pending_overdue.Text = pendingOverdueCount.ToString();
                returned_overdue.Text = returnedOverdueCount.ToString();
                returned_ontime.Text = returnedOntimeCount.ToString();
                total_borrowed.Text = totalBorrowedCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading borrowing statistics: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void report_button_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Create the dialog form
            Form dateRangeDialog = new Form()
            {
                Text = "Select Date Range",
                Width = 400,
                Height = 250,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // Create controls
            Label lblPeriod = new Label() { Text = "Period:", Left = 20, Top = 20, Width = 100 };
            ComboBox cmbPeriod = new ComboBox() { Left = 120, Top = 20, Width = 200 };
            cmbPeriod.Items.AddRange(new object[] { "All Time", "Last Month", "Last Week", "Custom" });
            cmbPeriod.SelectedIndex = 0;

            Label lblStartDate = new Label() { Text = "Start Date:", Left = 20, Top = 60, Width = 100 };
            DateTimePicker dtpStartDate = new DateTimePicker() { Left = 120, Top = 60, Width = 200, Enabled = false };

            Label lblEndDate = new Label() { Text = "End Date:", Left = 20, Top = 100, Width = 100 };
            DateTimePicker dtpEndDate = new DateTimePicker() { Left = 120, Top = 100, Width = 200, Enabled = false };

            Button btnOK = new Button() { Text = "OK", Left = 120, Top = 150, Width = 80 };
            Button btnCancel = new Button() { Text = "Cancel", Left = 220, Top = 150, Width = 80 };

            // Add controls to form
            dateRangeDialog.Controls.Add(lblPeriod);
            dateRangeDialog.Controls.Add(cmbPeriod);
            dateRangeDialog.Controls.Add(lblStartDate);
            dateRangeDialog.Controls.Add(dtpStartDate);
            dateRangeDialog.Controls.Add(lblEndDate);
            dateRangeDialog.Controls.Add(dtpEndDate);
            dateRangeDialog.Controls.Add(btnOK);
            dateRangeDialog.Controls.Add(btnCancel);

            // Event handlers
            cmbPeriod.SelectedIndexChanged += (s, ev) =>
            {
                bool isCustom = cmbPeriod.SelectedItem.ToString() == "Custom";
                dtpStartDate.Enabled = isCustom;
                dtpEndDate.Enabled = isCustom;

                // Set default dates for non-custom options
                if (!isCustom)
                {
                    DateTime now = DateTime.Now;
                    switch (cmbPeriod.SelectedItem.ToString())
                    {
                        case "Last Week":
                            dtpStartDate.Value = now.AddDays(-7);
                            dtpEndDate.Value = now;
                            break;
                        case "Last Month":
                            dtpStartDate.Value = now.AddMonths(-1);
                            dtpEndDate.Value = now;
                            break;
                        case "All Time":
                            // Use the minimum and maximum allowed by DateTimePicker
                            dtpStartDate.Value = dtpStartDate.MinDate; // Typically 1/1/1753
                            dtpEndDate.Value = dtpEndDate.MaxDate;     // Typically 12/31/9998
                            break;
                    }
                }
            };

            btnOK.Click += (s, ev) =>
            {
                dateRangeDialog.DialogResult = DialogResult.OK;
                dateRangeDialog.Close();
            };

            btnCancel.Click += (s, ev) =>
            {
                dateRangeDialog.DialogResult = DialogResult.Cancel;
                dateRangeDialog.Close();
            };

            // Show dialog and process result
            if (dateRangeDialog.ShowDialog() == DialogResult.OK)
            {
                DateTime startDate = dtpStartDate.Value;
                DateTime endDate = dtpEndDate.Value;

                // Fetch data using stored procedure
                DataTable borrowingData = GetBorrowingData(startDate, endDate);

                // Export to Excel
                ExportToExcel(borrowingData);
            }
        }

        private DataTable GetBorrowingData(DateTime startDate, DateTime endDate)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                using (MySqlCommand command = new MySqlCommand("GetBorrowingDataByDateRange", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Debug: Show the dates being sent
                    Console.WriteLine($"Start: {startDate}, End: {endDate}");

                    // Ensure proper MySQL datetime format
                    command.Parameters.AddWithValue("@p_start_date", startDate.ToString("yyyy-MM-dd HH:mm:ss"));
                    command.Parameters.AddWithValue("@p_end_date", endDate.ToString("yyyy-MM-dd HH:mm:ss"));

                    connection.Open();
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            // Write DataTable contents to console
            Console.WriteLine("\nDataTable Contents:");
            Console.WriteLine($"Rows returned: {dataTable.Rows.Count}");

            if (dataTable.Rows.Count > 0)
            {
                // Print column headers
                foreach (DataColumn column in dataTable.Columns)
                {
                    Console.Write($"{column.ColumnName,-25}");
                }
                Console.WriteLine("\n" + new string('-', 25 * dataTable.Columns.Count));

                // Print first 5 rows (or all if less than 5)
                int rowsToPrint = Math.Min(5, dataTable.Rows.Count);
                for (int i = 0; i < rowsToPrint; i++)
                {
                    DataRow row = dataTable.Rows[i];
                    foreach (var item in row.ItemArray)
                    {
                        Console.Write($"{item?.ToString()?.Trim(),-25}");
                    }
                    Console.WriteLine();
                }

                if (dataTable.Rows.Count > 5)
                {
                    Console.WriteLine($"... and {dataTable.Rows.Count - 5} more rows");
                }
            }
            else
            {
                Console.WriteLine("No data returned from the database");
            }

            return dataTable;
        }

        private void ExportToExcel(DataTable data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Excel Files|*.xlsx",
                Title = "Save Excel File",
                FileName = "Borrowing_Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xlsx"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var workbook = new ClosedXML.Excel.XLWorkbook())
                    {
                        var worksheet = workbook.Worksheets.Add("Borrowing Report");

                        // Add headers
                        for (int i = 0; i < data.Columns.Count; i++)
                        {
                            worksheet.Cell(1, i + 1).Value = data.Columns[i].ColumnName;
                        }

                        // Add data
                        for (int row = 0; row < data.Rows.Count; row++)
                        {
                            for (int col = 0; col < data.Columns.Count; col++)
                            {
                                worksheet.Cell(row + 2, col + 1).Value = data.Rows[row][col].ToString();
                            }
                        }

                        // Auto-fit columns
                        worksheet.Columns().AdjustToContents();

                        workbook.SaveAs(saveFileDialog.FileName);
                        MessageBox.Show("Data exported successfully to " + saveFileDialog.FileName, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error exporting to Excel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void report_panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel14_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Create an OpenFileDialog to select Excel files
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter options and filter index
            openFileDialog.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog.FilterIndex = 1;

            // Restore directory after closing
            openFileDialog.RestoreDirectory = true;

            // Show the dialog and check if user clicked OK
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the selected file path
                string excelFilePath = openFileDialog.FileName;

                // Create an instance of the Requests form and pass the file path
                Requests requestsForm = new Requests(excelFilePath);

                // Show the Requests form
                requestsForm.Show();
            }
        }
    }
}
