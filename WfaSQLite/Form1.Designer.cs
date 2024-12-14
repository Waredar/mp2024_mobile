namespace WfaSQLite
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lvLogs = new ListView();
            buCityAdd = new Button();
            buCity = new Button();
            edCityName = new TextBox();
            dataGridView1 = new DataGridView();
            tbQuery = new TextBox();
            byShowQuery = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // lvLogs
            // 
            lvLogs.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lvLogs.Location = new Point(12, 12);
            lvLogs.Name = "lvLogs";
            lvLogs.Size = new Size(399, 361);
            lvLogs.TabIndex = 0;
            lvLogs.UseCompatibleStateImageBehavior = false;
            // 
            // buCityAdd
            // 
            buCityAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buCityAdd.Location = new Point(641, 12);
            buCityAdd.Name = "buCityAdd";
            buCityAdd.Size = new Size(185, 29);
            buCityAdd.TabIndex = 1;
            buCityAdd.Text = "Добавить город";
            buCityAdd.UseVisualStyleBackColor = true;
            // 
            // buCity
            // 
            buCity.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buCity.Location = new Point(426, 56);
            buCity.Name = "buCity";
            buCity.Size = new Size(400, 29);
            buCity.TabIndex = 2;
            buCity.Text = "Показать все города в таблице";
            buCity.UseVisualStyleBackColor = true;
            // 
            // edCityName
            // 
            edCityName.Location = new Point(426, 12);
            edCityName.Name = "edCityName";
            edCityName.Size = new Size(209, 27);
            edCityName.TabIndex = 3;
            // 
            // dataGridView1
            // 
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(426, 91);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(400, 180);
            dataGridView1.TabIndex = 4;
            // 
            // tbQuery
            // 
            tbQuery.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tbQuery.Location = new Point(426, 277);
            tbQuery.Multiline = true;
            tbQuery.Name = "tbQuery";
            tbQuery.Size = new Size(295, 98);
            tbQuery.TabIndex = 5;
            tbQuery.Text = "select count(id)\r\nfrom city";
            // 
            // byShowQuery
            // 
            byShowQuery.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            byShowQuery.Location = new Point(733, 277);
            byShowQuery.Name = "byShowQuery";
            byShowQuery.Size = new Size(93, 96);
            byShowQuery.TabIndex = 7;
            byShowQuery.Text = "Запуск";
            byShowQuery.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(838, 385);
            Controls.Add(byShowQuery);
            Controls.Add(tbQuery);
            Controls.Add(dataGridView1);
            Controls.Add(edCityName);
            Controls.Add(buCity);
            Controls.Add(buCityAdd);
            Controls.Add(lvLogs);
            Name = "Form1";
            Text = "WfaSQLite";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvLogs;
        private Button buCityAdd;
        private Button buCity;
        private TextBox edCityName;
        private DataGridView dataGridView1;
        private TextBox tbQuery;
        private Button byShowQuery;
    }
}
