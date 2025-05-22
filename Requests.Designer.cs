namespace Library_Management_System___Belaro
{
    partial class Requests
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
            this.rq = new System.Windows.Forms.Label();
            this.requestedBooks = new System.Windows.Forms.DataGridView();
            this.search = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.filterByCateg = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.requestedBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // rq
            // 
            this.rq.AutoSize = true;
            this.rq.Font = new System.Drawing.Font("Poppins SemiBold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rq.Location = new System.Drawing.Point(242, 39);
            this.rq.Name = "rq";
            this.rq.Size = new System.Drawing.Size(266, 50);
            this.rq.TabIndex = 0;
            this.rq.Text = "Requested Books";
            // 
            // requestedBooks
            // 
            this.requestedBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.requestedBooks.Location = new System.Drawing.Point(27, 196);
            this.requestedBooks.Name = "requestedBooks";
            this.requestedBooks.RowHeadersWidth = 51;
            this.requestedBooks.RowTemplate.Height = 24;
            this.requestedBooks.Size = new System.Drawing.Size(709, 372);
            this.requestedBooks.TabIndex = 1;
            this.requestedBooks.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.requestedBooks_CellContentClick);
            // 
            // search
            // 
            this.search.Font = new System.Drawing.Font("Poppins", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.Location = new System.Drawing.Point(27, 141);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(279, 33);
            this.search.TabIndex = 2;
            this.search.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Requested Books";
            // 
            // filterByCateg
            // 
            this.filterByCateg.Font = new System.Drawing.Font("Poppins", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterByCateg.FormattingEnabled = true;
            this.filterByCateg.Location = new System.Drawing.Point(344, 140);
            this.filterByCateg.Name = "filterByCateg";
            this.filterByCateg.Size = new System.Drawing.Size(217, 34);
            this.filterByCateg.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Poppins", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(340, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Filter by Category";
            // 
            // Requests
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(766, 591);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.filterByCateg);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.search);
            this.Controls.Add(this.requestedBooks);
            this.Controls.Add(this.rq);
            this.Name = "Requests";
            this.Text = "Requests";
            this.Load += new System.EventHandler(this.Requests_Load);
            ((System.ComponentModel.ISupportInitialize)(this.requestedBooks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label rq;
        private System.Windows.Forms.DataGridView requestedBooks;
        private System.Windows.Forms.TextBox search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox filterByCateg;
        private System.Windows.Forms.Label label2;
    }
}