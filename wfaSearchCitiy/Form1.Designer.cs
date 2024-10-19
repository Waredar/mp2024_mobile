namespace wfaSearchCitiy
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
            edResult = new TextBox();
            edSearch = new TextBox();
            SuspendLayout();
            // 
            // edResult
            // 
            edResult.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edResult.Location = new Point(12, 61);
            edResult.Multiline = true;
            edResult.Name = "edResult";
            edResult.ReadOnly = true;
            edResult.ScrollBars = ScrollBars.Vertical;
            edResult.Size = new Size(873, 335);
            edResult.TabIndex = 0;
            // 
            // edSearch
            // 
            edSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            edSearch.Location = new Point(12, 12);
            edSearch.Name = "edSearch";
            edSearch.Size = new Size(873, 27);
            edSearch.TabIndex = 1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(897, 408);
            Controls.Add(edSearch);
            Controls.Add(edResult);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox edResult;
        private TextBox edSearch;
    }
}
