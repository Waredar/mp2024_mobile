namespace WfaPaint
{
    partial class WfaPaint
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
            panel1 = new Panel();
            btnSave = new Button();
            btnClear = new Button();
            btnStar = new Button();
            btnRec = new Button();
            btnEllipse = new Button();
            btnLine = new Button();
            btnPen = new Button();
            trackBar1 = new TrackBar();
            pBColor4 = new PictureBox();
            pBColor3 = new PictureBox();
            pBColor2 = new PictureBox();
            pBColor1 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pBColor4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pBColor3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pBColor2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pBColor1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(btnClear);
            panel1.Controls.Add(btnStar);
            panel1.Controls.Add(btnRec);
            panel1.Controls.Add(btnEllipse);
            panel1.Controls.Add(btnLine);
            panel1.Controls.Add(btnPen);
            panel1.Controls.Add(trackBar1);
            panel1.Controls.Add(pBColor4);
            panel1.Controls.Add(pBColor3);
            panel1.Controls.Add(pBColor2);
            panel1.Controls.Add(pBColor1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(279, 506);
            panel1.TabIndex = 0;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(27, 393);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(218, 37);
            btnSave.TabIndex = 11;
            btnSave.Text = "Сохранить";
            btnSave.UseVisualStyleBackColor = true;
            // 
            // btnClear
            // 
            btnClear.Location = new Point(27, 436);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(218, 37);
            btnClear.TabIndex = 10;
            btnClear.Text = "Очистить";
            btnClear.UseVisualStyleBackColor = true;
            // 
            // btnStar
            // 
            btnStar.Location = new Point(27, 285);
            btnStar.Name = "btnStar";
            btnStar.Size = new Size(218, 29);
            btnStar.TabIndex = 9;
            btnStar.Text = "Звёздочки";
            btnStar.UseVisualStyleBackColor = true;
            // 
            // btnRec
            // 
            btnRec.Location = new Point(27, 249);
            btnRec.Name = "btnRec";
            btnRec.Size = new Size(218, 29);
            btnRec.TabIndex = 8;
            btnRec.Text = "Прямоугольник";
            btnRec.UseVisualStyleBackColor = true;
            // 
            // btnEllipse
            // 
            btnEllipse.Location = new Point(27, 213);
            btnEllipse.Name = "btnEllipse";
            btnEllipse.Size = new Size(218, 29);
            btnEllipse.TabIndex = 7;
            btnEllipse.Text = "Эллипс";
            btnEllipse.UseVisualStyleBackColor = true;
            // 
            // btnLine
            // 
            btnLine.Location = new Point(27, 177);
            btnLine.Name = "btnLine";
            btnLine.Size = new Size(218, 29);
            btnLine.TabIndex = 6;
            btnLine.Text = "Линия";
            btnLine.UseVisualStyleBackColor = true;
            // 
            // btnPen
            // 
            btnPen.Location = new Point(27, 141);
            btnPen.Name = "btnPen";
            btnPen.Size = new Size(218, 29);
            btnPen.TabIndex = 5;
            btnPen.Text = "Карандаш";
            btnPen.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            trackBar1.Location = new Point(27, 79);
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(218, 56);
            trackBar1.TabIndex = 4;
            // 
            // pBColor4
            // 
            pBColor4.BackColor = Color.SkyBlue;
            pBColor4.Location = new Point(195, 12);
            pBColor4.Name = "pBColor4";
            pBColor4.Size = new Size(50, 50);
            pBColor4.TabIndex = 3;
            pBColor4.TabStop = false;
            // 
            // pBColor3
            // 
            pBColor3.BackColor = Color.LawnGreen;
            pBColor3.Location = new Point(139, 12);
            pBColor3.Name = "pBColor3";
            pBColor3.Size = new Size(50, 50);
            pBColor3.TabIndex = 2;
            pBColor3.TabStop = false;
            // 
            // pBColor2
            // 
            pBColor2.BackColor = Color.Coral;
            pBColor2.Location = new Point(83, 12);
            pBColor2.Name = "pBColor2";
            pBColor2.Size = new Size(50, 50);
            pBColor2.TabIndex = 1;
            pBColor2.TabStop = false;
            // 
            // pBColor1
            // 
            pBColor1.BackColor = Color.Goldenrod;
            pBColor1.Location = new Point(27, 12);
            pBColor1.Name = "pBColor1";
            pBColor1.Size = new Size(50, 50);
            pBColor1.TabIndex = 0;
            pBColor1.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Dock = DockStyle.Fill;
            pictureBox1.Location = new Point(279, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(653, 506);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // WfaPaint
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(932, 506);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            MinimumSize = new Size(950, 550);
            Name = "WfaPaint";
            Text = "Form1";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBColor4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBColor3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBColor2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pBColor1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private PictureBox pBColor2;
        private PictureBox pBColor1;
        private PictureBox pictureBox1;
        private PictureBox pBColor3;
        private PictureBox pBColor4;
        private Button btnClear;
        private Button btnStar;
        private Button btnRec;
        private Button btnEllipse;
        private Button btnLine;
        private Button btnPen;
        private TrackBar trackBar1;
        private Button btnSave;
    }
}
