using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Library_Management_System___Belaro
{
    public partial class Requests : Form
    {
        private string _filePath;
        private DataTable _booksData;
        private List<string> _categories = new List<string>
        {
            "Fiction", "Science Fiction", "Mystery", "Fantasy",
            "Biography", "History", "Romance", "Horror",
            "Philosophy", "Poetry"
        };

        public Requests(string file_path)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            _filePath = file_path;

            // Initialize UI components
            InitializeDataGridView();
            InitializeCategoryFilter();
        }

        private void Requests_Load(object sender, EventArgs e)
        {
            LoadExcelData();
        }

        
        private void InitializeCategoryFilter()
        {
            filterByCateg.Items.Add("All Categories");  // Default option
            foreach (var category in _categories)
            {
                filterByCateg.Items.Add(category);
            }
            filterByCateg.SelectedIndex = 0;
            filterByCateg.SelectedIndexChanged += FilterByCateg_SelectedIndexChanged;
        }
        private void InitializeDataGridView()
        {
            // Basic setup
            requestedBooks.AutoGenerateColumns = true;
            requestedBooks.AllowUserToAddRows = false;
            requestedBooks.ReadOnly = true;
            requestedBooks.AllowUserToResizeRows = false;
            requestedBooks.RowHeadersVisible = false;
            requestedBooks.BackgroundColor = Color.Gainsboro;
            requestedBooks.BorderStyle = BorderStyle.None;
            requestedBooks.CellBorderStyle = DataGridViewCellBorderStyle.None;
            requestedBooks.GridColor = Color.Gainsboro;

            // Font settings
            requestedBooks.DefaultCellStyle.Font = new Font("Poppins", 8.5f);
            requestedBooks.ColumnHeadersDefaultCellStyle.Font = new Font("Poppins", 9f, FontStyle.Bold);

            // Cell styling
            requestedBooks.DefaultCellStyle.BackColor = Color.Gainsboro;
            requestedBooks.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            requestedBooks.DefaultCellStyle.SelectionForeColor = Color.Black;
            requestedBooks.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Header styling
            requestedBooks.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            requestedBooks.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            requestedBooks.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            requestedBooks.EnableHeadersVisualStyles = false;

            // Row styling
            requestedBooks.RowsDefaultCellStyle.BackColor = Color.Gainsboro;
            requestedBooks.AlternatingRowsDefaultCellStyle.BackColor = Color.Gainsboro; // Remove alternating colors

            // Disable user editing
            requestedBooks.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void LoadExcelData()
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet worksheet = null;
            Excel.Range range = null;

            try
            {
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Open(_filePath);
                worksheet = (Excel.Worksheet)workbook.Sheets[1];
                range = worksheet.UsedRange;

                _booksData = new DataTable();

                // Add columns (only first 3 columns)
                for (int col = 1; col <= 3; col++)
                {
                    var cellValue = range.Cells[1, col].Value2;
                    _booksData.Columns.Add(cellValue?.ToString() ?? $"Column{col}");
                }

                // Add data rows (skip header row)
                for (int row = 2; row <= range.Rows.Count; row++)
                {
                    // Skip empty rows
                    if (range.Cells[row, 1].Value2 == null)
                        continue;

                    DataRow dataRow = _booksData.NewRow();
                    for (int col = 1; col <= 3; col++)
                    {
                        dataRow[col - 1] = range.Cells[row, col].Value2?.ToString() ?? "";
                    }
                    _booksData.Rows.Add(dataRow);
                }

                // Bind to DataGridView on UI thread
                this.Invoke((MethodInvoker)delegate {
                    requestedBooks.DataSource = _booksData;

                    // Apply formatting
                    requestedBooks.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    requestedBooks.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    requestedBooks.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading Excel file: {ex.Message}", "Error",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Proper COM cleanup
                if (range != null) Marshal.ReleaseComObject(range);
                if (worksheet != null) Marshal.ReleaseComObject(worksheet);

                if (workbook != null)
                {
                    workbook.Close(false);
                    Marshal.ReleaseComObject(workbook);
                }

                if (excelApp != null)
                {
                    excelApp.Quit();
                    Marshal.ReleaseComObject(excelApp);
                }

                // Force garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            FilterBooks();
        }

        private void FilterByCateg_SelectedIndexChanged(object sender, EventArgs e)
        {
            FilterBooks();
        }

        private void FilterBooks()
        {
            if (_booksData == null) return;

            string searchText = search.Text.ToLower();
            string selectedCategory = filterByCateg.SelectedItem?.ToString();

            var filteredRows = _booksData.AsEnumerable().Where(row =>
                (selectedCategory == "All Categories" || row.Field<string>("Category") == selectedCategory) &&
                (string.IsNullOrWhiteSpace(searchText) ||
                 row.Field<string>("Book Title")?.ToLower().Contains(searchText) == true ||
                 row.Field<string>("Author")?.ToLower().Contains(searchText) == true ||
                 row.Field<string>("Category")?.ToLower().Contains(searchText) == true));

            if (filteredRows.Any())
            {
                requestedBooks.DataSource = filteredRows.CopyToDataTable();
            }
            else
            {
                requestedBooks.DataSource = _booksData.Clone(); // Empty table with same structure
            }
        }
    }
}