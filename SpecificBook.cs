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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Library_Management_System___Belaro
{
    public partial class SpecificBook : Form
    {
        private string connectionString = "server=localhost;user=root;password=mike;database=demodb;";
        private Management managementForm;
        public int BookId { get; private set; }
        public SpecificBook(int book_id, string title, Management parent)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            this.BookId = book_id;
            this.Text = title;
            this.managementForm = parent;
            SpecificBook_Load();
            this.FormClosed += new FormClosedEventHandler(SpecificBookForm_FormClosed);
        }

        private void SpecificBook_Load()
        {
            LoadBookDetails();
            LoadBookCopies();
            StyleDataGridView();
        }
        private void StyleDataGridView()
        {
            copyGrid.BorderStyle = BorderStyle.None;
            copyGrid.BackgroundColor = Color.Gainsboro;
            copyGrid.DefaultCellStyle.BackColor = Color.Gainsboro;
            copyGrid.DefaultCellStyle.SelectionBackColor = Color.Gainsboro;
            copyGrid.DefaultCellStyle.Font = new Font("Poppins", 8.5f);
            copyGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro;
            copyGrid.CellBorderStyle = DataGridViewCellBorderStyle.None;
            copyGrid.RowHeadersVisible = false;
            copyGrid.EnableHeadersVisualStyles = false;
            copyGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            copyGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9f, FontStyle.Bold);
        }

        private void LoadBookDetails()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetBookDetailsById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("input_book_id", this.BookId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                title.Text = reader["book_title"]?.ToString() ?? "N/A";
                                book_id.Text = this.BookId.ToString();
                                author.Text = reader["author_name"]?.ToString() ?? "N/A";
                                year.Text = reader["publication_year"]?.ToString() ?? "N/A";
                                categ.Text = reader["category_name"]?.ToString() ?? "N/A";
                                copies_num.Text = reader["available_copies"]?.ToString() ?? "0";
                            }
                            else
                            {
                                MessageBox.Show("Book not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                this.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book details: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void LoadBookCopies()
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand cmd = new MySqlCommand("GetBookCopiesByBookId", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("input_book_id", this.BookId);

                        DataTable dt = new DataTable();
                        dt.Load(cmd.ExecuteReader());

                        copyGrid.DataSource = dt;

                        // Configure grid properties first
                        copyGrid.AutoGenerateColumns = true;
                        copyGrid.AllowUserToAddRows = false;
                        copyGrid.ReadOnly = true;

                        // Set width for all data columns
                        foreach (DataGridViewColumn column in copyGrid.Columns)
                        {
                            column.Width = 135; // Set width to 100 pixels

                            // Additional styling for consistency
                            column.DefaultCellStyle.Font = new Font("Poppins", 8.5f);
                            column.HeaderCell.Style.Font = new Font("Poppins", 9f, FontStyle.Bold);

                            // Hide location column if it exists
                            if (column.Name.Equals("location", StringComparison.OrdinalIgnoreCase))
                            {
                                column.Visible = false;
                            }
                        }

                        // Add or configure Remove button column
                        if (!copyGrid.Columns.Contains("Remove"))
                        {
                            DataGridViewButtonColumn removeButton = new DataGridViewButtonColumn();
                            removeButton.Name = "Remove";
                            removeButton.HeaderText = "Action";
                            removeButton.Text = "Remove";
                            removeButton.UseColumnTextForButtonValue = true;
                            removeButton.DefaultCellStyle.ForeColor = Color.Black;
                            removeButton.DefaultCellStyle.Font = new Font("Poppins", 8.5f);
                            removeButton.Width = 100; // Set same width for consistency
                            copyGrid.Columns.Add(removeButton);
                        }
                        else
                        {
                            // Ensure existing Remove button has correct width
                            copyGrid.Columns["Remove"].Width = 100;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading book copies: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void copyGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Handle Remove button click
            if (e.ColumnIndex == copyGrid.Columns["Remove"].Index)
            {
                int copyId = Convert.ToInt32(copyGrid.Rows[e.RowIndex].Cells["copy_id"].Value);
                RemoveCopy(copyId);
            }
        }

        private void RemoveCopy(int copyId)
        {
            try
            {
                var result = MessageBox.Show("Are you sure you want to remove this copy?", "Confirm Removal",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand("MarkBookCopyInactive", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@input_copy_id", copyId);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Copy removed successfully.", "Success",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                                LoadBookDetails();
                                LoadBookCopies();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void close_specific_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void title_Click(object sender, EventArgs e)
        {

        }
        

        private void copies_num_Click(object sender, EventArgs e)
        {

        }

        private void SpecificBook_Load(object sender, EventArgs e)
        {

        }

        private void edit_book_Click(object sender, EventArgs e)
        {
            Form editBookDialog = new Form()
            {
                Width = 400,
                Height = 350,
                Text = "Edit Book Details",
                StartPosition = FormStartPosition.CenterParent
            };

            Label titleLabel = new Label() { Text = "Title:", Left = 20, Top = 20, Width = 80 };
            TextBox titleTextBox = new TextBox() { Left = 110, Top = 20, Width = 250 };

            Label authorLabel = new Label() { Text = "Author:", Left = 20, Top = 60, Width = 80 };
            ComboBox authorComboBox = new ComboBox() { Left = 110, Top = 60, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            Label categoryLabel = new Label() { Text = "Category:", Left = 20, Top = 100, Width = 80 };
            ComboBox categoryComboBox = new ComboBox() { Left = 110, Top = 100, Width = 250, DropDownStyle = ComboBoxStyle.DropDownList };

            Label yearLabel = new Label() { Text = "Publication Year:", Left = 20, Top = 140, Width = 80 };
            NumericUpDown yearNumeric = new NumericUpDown()
            {
                Left = 110,
                Top = 140,
                Width = 100,
                Minimum = 1000,
                Maximum = DateTime.Now.Year
            };

            Button okButton = new Button() { Text = "Save", Left = 110, Top = 230, Width = 80 };
            Button cancelButton = new Button() { Text = "Cancel", Left = 200, Top = 230, Width = 80 };

            int selectedAuthorId = -1;
            int selectedCategoryId = -1;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    // Load book title and publication year
                    using (MySqlCommand cmd = new MySqlCommand("GetBookDetailsById", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("input_book_id", this.BookId);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                titleTextBox.Text = reader["book_title"].ToString();
                                yearNumeric.Value = Convert.ToInt32(reader["publication_year"]);
                            }
                        }
                    }

                    // Get author_id and category_id
                    using (MySqlCommand cmd = new MySqlCommand("SELECT author_id, category_id FROM books WHERE book_id = @book_id", connection))
                    {
                        cmd.Parameters.AddWithValue("@book_id", this.BookId);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                selectedAuthorId = Convert.ToInt32(reader["author_id"]);
                                selectedCategoryId = Convert.ToInt32(reader["category_id"]);
                            }
                        }
                    }

                    // Use your suggested query to load distinct author and category values
                    Dictionary<int, string> authorDict = new Dictionary<int, string>();
                    Dictionary<int, string> categoryDict = new Dictionary<int, string>();

                    using (MySqlCommand cmd = new MySqlCommand("SELECT DISTINCT author_id, author_name, category_id, category_name FROM prepate_for_book_insertion", connection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int authorId = Convert.ToInt32(reader["author_id"]);
                                string authorName = reader["author_name"].ToString();
                                if (!authorDict.ContainsKey(authorId))
                                {
                                    authorDict.Add(authorId, authorName);
                                    authorComboBox.Items.Add(new KeyValuePair<int, string>(authorId, authorName));
                                }

                                int categoryId = Convert.ToInt32(reader["category_id"]);
                                string categoryName = reader["category_name"].ToString();
                                if (!categoryDict.ContainsKey(categoryId))
                                {
                                    categoryDict.Add(categoryId, categoryName);
                                    categoryComboBox.Items.Add(new KeyValuePair<int, string>(categoryId, categoryName));
                                }
                            }
                        }
                    }

                    // Set selected items
                    foreach (KeyValuePair<int, string> item in authorComboBox.Items)
                    {
                        if (item.Key == selectedAuthorId)
                        {
                            authorComboBox.SelectedItem = item;
                            break;
                        }
                    }

                    foreach (KeyValuePair<int, string> item in categoryComboBox.Items)
                    {
                        if (item.Key == selectedCategoryId)
                        {
                            categoryComboBox.SelectedItem = item;
                            break;
                        }
                    }

                    authorComboBox.DisplayMember = "Value";
                    categoryComboBox.DisplayMember = "Value";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load book details: " + ex.Message);
                return;
            }

            okButton.Click += (s, ev) =>
            {
                if (string.IsNullOrWhiteSpace(titleTextBox.Text))
                {
                    MessageBox.Show("Please enter a book title");
                    return;
                }

                if (authorComboBox.SelectedItem == null || categoryComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select both author and category");
                    return;
                }

                try
                {
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand("UpdateBookDetails", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            var selectedAuthor = (KeyValuePair<int, string>)authorComboBox.SelectedItem;
                            var selectedCategory = (KeyValuePair<int, string>)categoryComboBox.SelectedItem;

                            cmd.Parameters.AddWithValue("p_book_id", this.BookId);
                            cmd.Parameters.AddWithValue("p_title", titleTextBox.Text.Trim());
                            cmd.Parameters.AddWithValue("p_author_id", selectedAuthor.Key);
                            cmd.Parameters.AddWithValue("p_category_id", selectedCategory.Key);
                            cmd.Parameters.AddWithValue("p_publication_year", (int)yearNumeric.Value);

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Book updated successfully!");
                                editBookDialog.DialogResult = DialogResult.OK;
                                editBookDialog.Close();
                                this.LoadBookDetails();
                            }
                            else
                            {
                                MessageBox.Show("No changes were made to the book.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating book: " + ex.Message);
                }
            };

            cancelButton.Click += (s, ev) =>
            {
                editBookDialog.DialogResult = DialogResult.Cancel;
                editBookDialog.Close();
            };

            editBookDialog.Controls.Add(titleLabel);
            editBookDialog.Controls.Add(titleTextBox);
            editBookDialog.Controls.Add(authorLabel);
            editBookDialog.Controls.Add(authorComboBox);
            editBookDialog.Controls.Add(categoryLabel);
            editBookDialog.Controls.Add(categoryComboBox);
            editBookDialog.Controls.Add(yearLabel);
            editBookDialog.Controls.Add(yearNumeric);
            editBookDialog.Controls.Add(okButton);
            editBookDialog.Controls.Add(cancelButton);

            editBookDialog.ShowDialog();
        }


        private void insert_copy_Click(object sender, EventArgs e)
        {
            addCopy();
        }
        private void addCopy()
        {
            int book = BookId; // Ensure this is correctly assigned

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand cmd = new MySqlCommand("AddBookCopy", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@input_book_id", book);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Copy added successfully.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBookCopies(); // Refresh the grid/view
                    }
                }
            }
        }

        private void closeSpecific(object sender, EventArgs e)
        {
            this.Close(); // Close the current form (Specific Book)
        }
        private void SpecificBookForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (managementForm != null)
            {
                managementForm.loadBooks();
            }
        }

    }
}
