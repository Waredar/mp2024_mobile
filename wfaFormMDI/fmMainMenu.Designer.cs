namespace wfaFormMDI
{
    partial class FormMDI
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
            btnNote = new Button();
            brnAbout = new Button();
            SuspendLayout();
            // 
            // btnNote
            // 
            btnNote.Location = new Point(12, 12);
            btnNote.Name = "btnNote";
            btnNote.Size = new Size(94, 29);
            btnNote.TabIndex = 0;
            btnNote.Text = "NewNote";
            btnNote.UseVisualStyleBackColor = true;
            // 
            // brnAbout
            // 
            brnAbout.Location = new Point(643, 12);
            brnAbout.Name = "brnAbout";
            brnAbout.Size = new Size(145, 29);
            brnAbout.TabIndex = 1;
            brnAbout.Text = "О Программе";
            brnAbout.UseVisualStyleBackColor = true;
            brnAbout.Click += button1_Click;
            // 
            // FormMDI
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(brnAbout);
            Controls.Add(btnNote);
            IsMdiContainer = true;
            Name = "FormMDI";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private Button btnNote;
        private Button brnAbout;
    }
}
