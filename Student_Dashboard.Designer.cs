namespace Library_Management_System___Belaro
{
    partial class Student_Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Student_Dashboard));
            this.top_nav_panel = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.acct_dropdown = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.directorySearcher1 = new System.DirectoryServices.DirectorySearcher();
            this.recent_panel = new System.Windows.Forms.Panel();
            this.current_due_date = new System.Windows.Forms.Label();
            this.current_borrow_date = new System.Windows.Forms.Label();
            this.current_published_year = new System.Windows.Forms.Label();
            this.current_categ = new System.Windows.Forms.Label();
            this.current_author = new System.Windows.Forms.Label();
            this.current_title = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.current_status = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.top_books = new System.Windows.Forms.Panel();
            this.history_panel = new System.Windows.Forms.Panel();
            this.dashboard_panel = new System.Windows.Forms.Panel();
            this.label21 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.borrow_book_panel = new System.Windows.Forms.Panel();
            this.dataGridViewBooks = new System.Windows.Forms.DataGridView();
            this.book_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Available_Copies = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.actions = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.search_books = new System.Windows.Forms.TextBox();
            this.current_book_panel = new System.Windows.Forms.Panel();
            this.label22 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.authors_grid = new System.Windows.Forms.DataGridView();
            this.author_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.author_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.birthdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nationality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.books = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label23 = new System.Windows.Forms.Label();
            this.searchBox_author = new System.Windows.Forms.TextBox();
            this.authors_panel = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.history_button = new System.Windows.Forms.Panel();
            this.history_label = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.historyPanel = new System.Windows.Forms.Panel();
            this.history_table = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.curbal = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label24 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.mostGrid = new System.Windows.Forms.DataGridView();
            this.top_authors = new System.Windows.Forms.DataGridView();
            this.top_nav_panel.SuspendLayout();
            this.recent_panel.SuspendLayout();
            this.top_books.SuspendLayout();
            this.history_panel.SuspendLayout();
            this.dashboard_panel.SuspendLayout();
            this.borrow_book_panel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).BeginInit();
            this.current_book_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authors_grid)).BeginInit();
            this.authors_panel.SuspendLayout();
            this.history_button.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.historyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.history_table)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mostGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.top_authors)).BeginInit();
            this.SuspendLayout();
            // 
            // top_nav_panel
            // 
            this.top_nav_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.top_nav_panel.Controls.Add(this.label9);
            this.top_nav_panel.Controls.Add(this.acct_dropdown);
            this.top_nav_panel.Controls.Add(this.label8);
            this.top_nav_panel.Location = new System.Drawing.Point(0, 0);
            this.top_nav_panel.Name = "top_nav_panel";
            this.top_nav_panel.Size = new System.Drawing.Size(1178, 94);
            this.top_nav_panel.TabIndex = 1;
            this.top_nav_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.top_nav_panel_Paint);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Poppins", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(25, 16);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(466, 65);
            this.label9.TabIndex = 3;
            this.label9.Text = "Hello, Welcome Phaye!";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // acct_dropdown
            // 
            this.acct_dropdown.BackColor = System.Drawing.Color.Gainsboro;
            this.acct_dropdown.Font = new System.Drawing.Font("Poppins Medium", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.acct_dropdown.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.acct_dropdown.FormattingEnabled = true;
            this.acct_dropdown.Location = new System.Drawing.Point(894, 31);
            this.acct_dropdown.Name = "acct_dropdown";
            this.acct_dropdown.Size = new System.Drawing.Size(246, 44);
            this.acct_dropdown.TabIndex = 1;
            this.acct_dropdown.Text = "Phaye Wibster";
            this.acct_dropdown.SelectedIndexChanged += new System.EventHandler(this.acct_dropdown_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Poppins Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(750, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 31);
            this.label8.TabIndex = 0;
            // 
            // directorySearcher1
            // 
            this.directorySearcher1.ClientTimeout = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerPageTimeLimit = System.TimeSpan.Parse("-00:00:01");
            this.directorySearcher1.ServerTimeLimit = System.TimeSpan.Parse("-00:00:01");
            // 
            // recent_panel
            // 
            this.recent_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.recent_panel.Controls.Add(this.current_due_date);
            this.recent_panel.Controls.Add(this.current_borrow_date);
            this.recent_panel.Controls.Add(this.current_published_year);
            this.recent_panel.Controls.Add(this.current_categ);
            this.recent_panel.Controls.Add(this.current_author);
            this.recent_panel.Controls.Add(this.current_title);
            this.recent_panel.Controls.Add(this.label16);
            this.recent_panel.Controls.Add(this.label15);
            this.recent_panel.Controls.Add(this.label14);
            this.recent_panel.Controls.Add(this.label13);
            this.recent_panel.Controls.Add(this.label12);
            this.recent_panel.Controls.Add(this.label11);
            this.recent_panel.Controls.Add(this.current_status);
            this.recent_panel.Controls.Add(this.label10);
            this.recent_panel.Location = new System.Drawing.Point(29, 103);
            this.recent_panel.Name = "recent_panel";
            this.recent_panel.Size = new System.Drawing.Size(560, 410);
            this.recent_panel.TabIndex = 2;
            this.recent_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel9_Paint);
            // 
            // current_due_date
            // 
            this.current_due_date.AutoSize = true;
            this.current_due_date.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_due_date.Location = new System.Drawing.Point(157, 275);
            this.current_due_date.Name = "current_due_date";
            this.current_due_date.Size = new System.Drawing.Size(119, 26);
            this.current_due_date.TabIndex = 22;
            this.current_due_date.Text = "Apr. 18, 2025";
            this.current_due_date.Click += new System.EventHandler(this.label20_Click);
            // 
            // current_borrow_date
            // 
            this.current_borrow_date.AutoSize = true;
            this.current_borrow_date.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_borrow_date.Location = new System.Drawing.Point(157, 235);
            this.current_borrow_date.Name = "current_borrow_date";
            this.current_borrow_date.Size = new System.Drawing.Size(119, 26);
            this.current_borrow_date.TabIndex = 21;
            this.current_borrow_date.Text = "Apr. 10, 2025";
            // 
            // current_published_year
            // 
            this.current_published_year.AutoSize = true;
            this.current_published_year.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_published_year.Location = new System.Drawing.Point(157, 195);
            this.current_published_year.Name = "current_published_year";
            this.current_published_year.Size = new System.Drawing.Size(46, 26);
            this.current_published_year.TabIndex = 20;
            this.current_published_year.Text = "1915";
            // 
            // current_categ
            // 
            this.current_categ.AutoSize = true;
            this.current_categ.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_categ.Location = new System.Drawing.Point(157, 150);
            this.current_categ.Name = "current_categ";
            this.current_categ.Size = new System.Drawing.Size(83, 26);
            this.current_categ.TabIndex = 19;
            this.current_categ.Text = "Classics";
            this.current_categ.Click += new System.EventHandler(this.label17_Click);
            // 
            // current_author
            // 
            this.current_author.AutoSize = true;
            this.current_author.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_author.Location = new System.Drawing.Point(157, 113);
            this.current_author.Name = "current_author";
            this.current_author.Size = new System.Drawing.Size(110, 26);
            this.current_author.TabIndex = 18;
            this.current_author.Text = "Franz Kafka";
            this.current_author.Click += new System.EventHandler(this.current_author_Click);
            // 
            // current_title
            // 
            this.current_title.AutoSize = true;
            this.current_title.Font = new System.Drawing.Font("Poppins Medium", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_title.Location = new System.Drawing.Point(157, 73);
            this.current_title.Name = "current_title";
            this.current_title.Size = new System.Drawing.Size(180, 26);
            this.current_title.TabIndex = 17;
            this.current_title.Text = "The Metamorphosis";
            this.current_title.Click += new System.EventHandler(this.current_title_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(27, 275);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(84, 26);
            this.label16.TabIndex = 13;
            this.label16.Text = "Due Date:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(26, 235);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(107, 26);
            this.label15.TabIndex = 12;
            this.label15.Text = "Borrow Date:";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(26, 195);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 26);
            this.label14.TabIndex = 11;
            this.label14.Text = "Year:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(26, 150);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(85, 26);
            this.label13.TabIndex = 10;
            this.label13.Text = "Category:";
            this.label13.Click += new System.EventHandler(this.label13_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(26, 113);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(70, 26);
            this.label12.TabIndex = 9;
            this.label12.Text = "Author: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(26, 75);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(90, 26);
            this.label11.TabIndex = 8;
            this.label11.Text = "Book Title: ";
            // 
            // current_status
            // 
            this.current_status.AutoSize = true;
            this.current_status.BackColor = System.Drawing.Color.Transparent;
            this.current_status.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_status.ForeColor = System.Drawing.SystemColors.Highlight;
            this.current_status.Location = new System.Drawing.Point(192, 324);
            this.current_status.Name = "current_status";
            this.current_status.Size = new System.Drawing.Size(158, 50);
            this.current_status.TabIndex = 1;
            this.current_status.Text = "Borrowed";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Poppins", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label10.Location = new System.Drawing.Point(22, 21);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(302, 50);
            this.label10.TabIndex = 6;
            this.label10.Text = "Recent Transaction";
            // 
            // top_books
            // 
            this.top_books.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.top_books.Controls.Add(this.top_authors);
            this.top_books.Controls.Add(this.label24);
            this.top_books.Location = new System.Drawing.Point(595, 495);
            this.top_books.Name = "top_books";
            this.top_books.Size = new System.Drawing.Size(583, 319);
            this.top_books.TabIndex = 3;
            // 
            // history_panel
            // 
            this.history_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.history_panel.Controls.Add(this.label25);
            this.history_panel.Controls.Add(this.curbal);
            this.history_panel.Controls.Add(this.label5);
            this.history_panel.Location = new System.Drawing.Point(28, 524);
            this.history_panel.Name = "history_panel";
            this.history_panel.Size = new System.Drawing.Size(561, 290);
            this.history_panel.TabIndex = 3;
            // 
            // dashboard_panel
            // 
            this.dashboard_panel.Controls.Add(this.panel2);
            this.dashboard_panel.Controls.Add(this.top_nav_panel);
            this.dashboard_panel.Controls.Add(this.recent_panel);
            this.dashboard_panel.Controls.Add(this.top_books);
            this.dashboard_panel.Controls.Add(this.history_panel);
            this.dashboard_panel.Location = new System.Drawing.Point(347, 0);
            this.dashboard_panel.Name = "dashboard_panel";
            this.dashboard_panel.Size = new System.Drawing.Size(1257, 826);
            this.dashboard_panel.TabIndex = 4;
            this.dashboard_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.dashboard_panel_Paint);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.Color.Transparent;
            this.label21.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label21.Location = new System.Drawing.Point(21, 33);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(199, 50);
            this.label21.TabIndex = 1;
            this.label21.Text = "Borrow Book";
            // 
            // label27
            // 
            this.label27.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.Location = new System.Drawing.Point(35, 113);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(58, 23);
            this.label27.TabIndex = 3;
            this.label27.Text = "Search";
            // 
            // borrow_book_panel
            // 
            this.borrow_book_panel.Controls.Add(this.dataGridViewBooks);
            this.borrow_book_panel.Controls.Add(this.search_books);
            this.borrow_book_panel.Controls.Add(this.label27);
            this.borrow_book_panel.Controls.Add(this.label21);
            this.borrow_book_panel.Location = new System.Drawing.Point(347, 0);
            this.borrow_book_panel.Name = "borrow_book_panel";
            this.borrow_book_panel.Size = new System.Drawing.Size(1263, 826);
            this.borrow_book_panel.TabIndex = 5;
            // 
            // dataGridViewBooks
            // 
            this.dataGridViewBooks.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dataGridViewBooks.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewBooks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewBooks.ColumnHeadersHeight = 29;
            this.dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridViewBooks.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.book_id,
            this.title,
            this.Available_Copies,
            this.stat,
            this.actions});
            this.dataGridViewBooks.GridColor = System.Drawing.Color.Gainsboro;
            this.dataGridViewBooks.Location = new System.Drawing.Point(29, 199);
            this.dataGridViewBooks.Name = "dataGridViewBooks";
            this.dataGridViewBooks.RowHeadersWidth = 51;
            this.dataGridViewBooks.RowTemplate.Height = 24;
            this.dataGridViewBooks.Size = new System.Drawing.Size(1193, 604);
            this.dataGridViewBooks.TabIndex = 7;
            this.dataGridViewBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewBooks_CellContentClick);
            // 
            // book_id
            // 
            this.book_id.HeaderText = "Book ID";
            this.book_id.MinimumWidth = 90;
            this.book_id.Name = "book_id";
            this.book_id.Width = 90;
            // 
            // title
            // 
            this.title.HeaderText = "Book Title";
            this.title.MinimumWidth = 300;
            this.title.Name = "title";
            this.title.Width = 300;
            // 
            // Available_Copies
            // 
            this.Available_Copies.HeaderText = "Available Copies";
            this.Available_Copies.MinimumWidth = 100;
            this.Available_Copies.Name = "Available_Copies";
            this.Available_Copies.Width = 125;
            // 
            // stat
            // 
            this.stat.HeaderText = "Status";
            this.stat.MinimumWidth = 90;
            this.stat.Name = "stat";
            this.stat.Width = 90;
            // 
            // actions
            // 
            this.actions.HeaderText = "Action";
            this.actions.MinimumWidth = 90;
            this.actions.Name = "actions";
            this.actions.Width = 90;
            // 
            // search_books
            // 
            this.search_books.BackColor = System.Drawing.SystemColors.Menu;
            this.search_books.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.search_books.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search_books.Location = new System.Drawing.Point(32, 141);
            this.search_books.Name = "search_books";
            this.search_books.Size = new System.Drawing.Size(303, 33);
            this.search_books.TabIndex = 4;
            this.search_books.TextChanged += new System.EventHandler(this.searchBooks);
            // 
            // current_book_panel
            // 
            this.current_book_panel.Controls.Add(this.label22);
            this.current_book_panel.Controls.Add(this.panel1);
            this.current_book_panel.Location = new System.Drawing.Point(347, 0);
            this.current_book_panel.Name = "current_book_panel";
            this.current_book_panel.Size = new System.Drawing.Size(1263, 826);
            this.current_book_panel.TabIndex = 8;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.Color.Transparent;
            this.label22.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label22.Location = new System.Drawing.Point(21, 33);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(135, 50);
            this.label22.TabIndex = 1;
            this.label22.Text = "Authors";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.authors_grid);
            this.panel1.Controls.Add(this.label23);
            this.panel1.Controls.Add(this.searchBox_author);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(9, 82);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1221, 694);
            this.panel1.TabIndex = 0;
            // 
            // authors_grid
            // 
            this.authors_grid.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.authors_grid.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.authors_grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.authors_grid.ColumnHeadersHeight = 29;
            this.authors_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.authors_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.author_id,
            this.author_name,
            this.birthdate,
            this.nationality,
            this.books});
            this.authors_grid.GridColor = System.Drawing.Color.Gainsboro;
            this.authors_grid.Location = new System.Drawing.Point(22, 109);
            this.authors_grid.Name = "authors_grid";
            this.authors_grid.RowHeadersWidth = 51;
            this.authors_grid.RowTemplate.Height = 24;
            this.authors_grid.Size = new System.Drawing.Size(1183, 582);
            this.authors_grid.TabIndex = 8;
            this.authors_grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.authors_grid_CellContentClick);
            // 
            // author_id
            // 
            this.author_id.HeaderText = "Author ID";
            this.author_id.MinimumWidth = 90;
            this.author_id.Name = "author_id";
            this.author_id.Width = 90;
            // 
            // author_name
            // 
            this.author_name.HeaderText = "Author Name";
            this.author_name.MinimumWidth = 300;
            this.author_name.Name = "author_name";
            this.author_name.Width = 300;
            // 
            // birthdate
            // 
            this.birthdate.HeaderText = "Birthdate";
            this.birthdate.MinimumWidth = 100;
            this.birthdate.Name = "birthdate";
            this.birthdate.Width = 125;
            // 
            // nationality
            // 
            this.nationality.HeaderText = "Nationality";
            this.nationality.MinimumWidth = 90;
            this.nationality.Name = "nationality";
            this.nationality.Width = 90;
            // 
            // books
            // 
            this.books.HeaderText = "Books";
            this.books.MinimumWidth = 90;
            this.books.Name = "books";
            this.books.Width = 90;
            // 
            // label23
            // 
            this.label23.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(24, 30);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(58, 23);
            this.label23.TabIndex = 6;
            this.label23.Text = "Search";
            // 
            // searchBox_author
            // 
            this.searchBox_author.BackColor = System.Drawing.SystemColors.Menu;
            this.searchBox_author.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox_author.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBox_author.Location = new System.Drawing.Point(24, 53);
            this.searchBox_author.Name = "searchBox_author";
            this.searchBox_author.Size = new System.Drawing.Size(303, 33);
            this.searchBox_author.TabIndex = 5;
            this.searchBox_author.TextChanged += new System.EventHandler(this.searchBox_author_TextChanged);
            // 
            // authors_panel
            // 
            this.authors_panel.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.authors_panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.authors_panel.Controls.Add(this.label7);
            this.authors_panel.Controls.Add(this.label6);
            this.authors_panel.Controls.Add(this.history_button);
            this.authors_panel.Controls.Add(this.panel6);
            this.authors_panel.Controls.Add(this.panel5);
            this.authors_panel.Controls.Add(this.panel4);
            this.authors_panel.Location = new System.Drawing.Point(-7, 3);
            this.authors_panel.Name = "authors_panel";
            this.authors_panel.Size = new System.Drawing.Size(357, 840);
            this.authors_panel.TabIndex = 0;
            this.authors_panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label7.Location = new System.Drawing.Point(69, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(196, 23);
            this.label7.TabIndex = 5;
            this.label7.Text = "Library Management System";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Poppins", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label6.Location = new System.Drawing.Point(34, 37);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(273, 58);
            this.label6.TabIndex = 1;
            this.label6.Text = "Librong James";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // history_button
            // 
            this.history_button.BackColor = System.Drawing.Color.Transparent;
            this.history_button.Controls.Add(this.history_label);
            this.history_button.Location = new System.Drawing.Point(32, 440);
            this.history_button.Name = "history_button";
            this.history_button.Size = new System.Drawing.Size(286, 69);
            this.history_button.TabIndex = 4;
            this.history_button.Click += new System.EventHandler(this.history_click);
            // 
            // history_label
            // 
            this.history_label.AutoSize = true;
            this.history_label.BackColor = System.Drawing.Color.Transparent;
            this.history_label.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.history_label.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.history_label.Location = new System.Drawing.Point(3, 10);
            this.history_label.Name = "history_label";
            this.history_label.Size = new System.Drawing.Size(122, 50);
            this.history_label.TabIndex = 0;
            this.history_label.Text = "History";
            this.history_label.Click += new System.EventHandler(this.history_click);
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.label3);
            this.panel6.Location = new System.Drawing.Point(32, 351);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(286, 69);
            this.panel6.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label3.Location = new System.Drawing.Point(3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 50);
            this.label3.TabIndex = 0;
            this.label3.Text = "Authors";
            this.label3.Click += new System.EventHandler(this.current_click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.Transparent;
            this.panel5.Controls.Add(this.label2);
            this.panel5.Location = new System.Drawing.Point(32, 261);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(286, 69);
            this.panel5.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label2.Location = new System.Drawing.Point(3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 50);
            this.label2.TabIndex = 0;
            this.label2.Text = "Borrow Book";
            this.label2.Click += new System.EventHandler(this.borrow_click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.label1);
            this.panel4.Location = new System.Drawing.Point(32, 170);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(286, 69);
            this.panel4.TabIndex = 0;
            this.panel4.Click += new System.EventHandler(this.dashboard_click);
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.InactiveBorder;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dashboard";
            this.label1.Click += new System.EventHandler(this.dashboard_click);
            // 
            // historyPanel
            // 
            this.historyPanel.Controls.Add(this.history_table);
            this.historyPanel.Controls.Add(this.label4);
            this.historyPanel.Location = new System.Drawing.Point(347, 0);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(1260, 824);
            this.historyPanel.TabIndex = 2;
            // 
            // history_table
            // 
            this.history_table.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.history_table.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.history_table.Location = new System.Drawing.Point(31, 125);
            this.history_table.Name = "history_table";
            this.history_table.RowHeadersWidth = 51;
            this.history_table.RowTemplate.Height = 24;
            this.history_table.Size = new System.Drawing.Size(1093, 660);
            this.history_table.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label4.Location = new System.Drawing.Point(33, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 50);
            this.label4.TabIndex = 1;
            this.label4.Text = "History";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(170, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(202, 30);
            this.label5.TabIndex = 23;
            this.label5.Text = "Your Current Balance: ";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // curbal
            // 
            this.curbal.AutoSize = true;
            this.curbal.BackColor = System.Drawing.Color.Transparent;
            this.curbal.Font = new System.Drawing.Font("Poppins", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.curbal.ForeColor = System.Drawing.SystemColors.Desktop;
            this.curbal.Location = new System.Drawing.Point(170, 113);
            this.curbal.Name = "curbal";
            this.curbal.Size = new System.Drawing.Size(202, 60);
            this.curbal.TabIndex = 23;
            this.curbal.Text = "P 2,000.00";
            this.curbal.Click += new System.EventHandler(this.label24_Click);
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label25.Location = new System.Drawing.Point(72, 161);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(404, 23);
            this.label25.TabIndex = 24;
            this.label25.Text = "Please pay the current amount at the library to borrow books.";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.mostGrid);
            this.panel2.Controls.Add(this.label26);
            this.panel2.Location = new System.Drawing.Point(595, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(583, 386);
            this.panel2.TabIndex = 4;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.Color.Transparent;
            this.label24.Font = new System.Drawing.Font("Poppins", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label24.Location = new System.Drawing.Point(23, 13);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(198, 50);
            this.label24.TabIndex = 23;
            this.label24.Text = "Top Authors";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Font = new System.Drawing.Font("Poppins", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label26.Location = new System.Drawing.Point(12, 17);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(330, 50);
            this.label26.TabIndex = 24;
            this.label26.Text = "Most Borrowed Books";
            // 
            // mostGrid
            // 
            this.mostGrid.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.mostGrid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.mostGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mostGrid.GridColor = System.Drawing.Color.Gainsboro;
            this.mostGrid.Location = new System.Drawing.Point(21, 70);
            this.mostGrid.Name = "mostGrid";
            this.mostGrid.RowHeadersWidth = 51;
            this.mostGrid.RowTemplate.Height = 24;
            this.mostGrid.Size = new System.Drawing.Size(544, 304);
            this.mostGrid.TabIndex = 25;
            // 
            // top_authors
            // 
            this.top_authors.BackgroundColor = System.Drawing.Color.Gainsboro;
            this.top_authors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.top_authors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.top_authors.GridColor = System.Drawing.Color.Gainsboro;
            this.top_authors.Location = new System.Drawing.Point(15, 61);
            this.top_authors.Name = "top_authors";
            this.top_authors.RowHeadersWidth = 51;
            this.top_authors.RowTemplate.Height = 24;
            this.top_authors.Size = new System.Drawing.Size(550, 240);
            this.top_authors.TabIndex = 26;
            // 
            // Student_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(1609, 826);
            this.Controls.Add(this.dashboard_panel);
            this.Controls.Add(this.historyPanel);
            this.Controls.Add(this.authors_panel);
            this.Controls.Add(this.current_book_panel);
            this.Controls.Add(this.borrow_book_panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Student_Dashboard";
            this.Text = "Student";
            this.Load += new System.EventHandler(this.Student_Dashboard_Load);
            this.top_nav_panel.ResumeLayout(false);
            this.top_nav_panel.PerformLayout();
            this.recent_panel.ResumeLayout(false);
            this.recent_panel.PerformLayout();
            this.top_books.ResumeLayout(false);
            this.top_books.PerformLayout();
            this.history_panel.ResumeLayout(false);
            this.history_panel.PerformLayout();
            this.dashboard_panel.ResumeLayout(false);
            this.borrow_book_panel.ResumeLayout(false);
            this.borrow_book_panel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).EndInit();
            this.current_book_panel.ResumeLayout(false);
            this.current_book_panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.authors_grid)).EndInit();
            this.authors_panel.ResumeLayout(false);
            this.authors_panel.PerformLayout();
            this.history_button.ResumeLayout(false);
            this.history_button.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.historyPanel.ResumeLayout(false);
            this.historyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.history_table)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mostGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.top_authors)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel top_nav_panel;
        private System.Windows.Forms.ComboBox acct_dropdown;
        private System.Windows.Forms.Label label8;
        private System.DirectoryServices.DirectorySearcher directorySearcher1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel recent_panel;
        private System.Windows.Forms.Panel top_books;
        private System.Windows.Forms.Panel history_panel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label current_status;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label current_author;
        private System.Windows.Forms.Label current_title;
        private System.Windows.Forms.Label current_categ;
        private System.Windows.Forms.Label current_borrow_date;
        private System.Windows.Forms.Label current_published_year;
        private System.Windows.Forms.Label current_due_date;
        private System.Windows.Forms.Panel dashboard_panel;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Panel borrow_book_panel;
        private System.Windows.Forms.TextBox search_books;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.DataGridViewTextBoxColumn book_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Available_Copies;
        private System.Windows.Forms.DataGridViewTextBoxColumn stat;
        private System.Windows.Forms.DataGridViewTextBoxColumn actions;
        private System.Windows.Forms.Panel current_book_panel;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox searchBox_author;
        private System.Windows.Forms.Panel authors_panel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel history_button;
        private System.Windows.Forms.Label history_label;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView authors_grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn author_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn author_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn birthdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn nationality;
        private System.Windows.Forms.DataGridViewTextBoxColumn books;
        private System.Windows.Forms.Panel historyPanel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView history_table;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label curbal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.DataGridView mostGrid;
        private System.Windows.Forms.DataGridView top_authors;
    }
}