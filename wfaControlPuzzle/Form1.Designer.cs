namespace wfaControlPuzzle
{
    partial class wfaControlPuzzle
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
            menuStrip1 = new MenuStrip();
            MainToolStripMenuItem = new ToolStripMenuItem();
            DifficulToolStripMenuItem = new ToolStripMenuItem();
            EasyToolStripMenuItem = new ToolStripMenuItem();
            NormalToolStripMenuItem = new ToolStripMenuItem();
            HardToolStripMenuItem = new ToolStripMenuItem();
            HelpToolStripMenuItem = new ToolStripMenuItem();
            statusStrip1 = new StatusStrip();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { MainToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(888, 28);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // MainToolStripMenuItem
            // 
            MainToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { DifficulToolStripMenuItem, HelpToolStripMenuItem });
            MainToolStripMenuItem.Name = "MainToolStripMenuItem";
            MainToolStripMenuItem.Size = new Size(65, 24);
            MainToolStripMenuItem.Text = "Меню";
            // 
            // DifficulToolStripMenuItem
            // 
            DifficulToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { EasyToolStripMenuItem, NormalToolStripMenuItem, HardToolStripMenuItem });
            DifficulToolStripMenuItem.Name = "DifficulToolStripMenuItem";
            DifficulToolStripMenuItem.Size = new Size(224, 26);
            DifficulToolStripMenuItem.Text = "Сложность";
            // 
            // EasyToolStripMenuItem
            // 
            EasyToolStripMenuItem.Name = "EasyToolStripMenuItem";
            EasyToolStripMenuItem.Size = new Size(224, 26);
            EasyToolStripMenuItem.Text = "Легкая";
            // 
            // NormalToolStripMenuItem
            // 
            NormalToolStripMenuItem.Checked = true;
            NormalToolStripMenuItem.CheckState = CheckState.Checked;
            NormalToolStripMenuItem.Name = "NormalToolStripMenuItem";
            NormalToolStripMenuItem.Size = new Size(224, 26);
            NormalToolStripMenuItem.Text = "Средняя";
            // 
            // HardToolStripMenuItem
            // 
            HardToolStripMenuItem.Name = "HardToolStripMenuItem";
            HardToolStripMenuItem.Size = new Size(224, 26);
            HardToolStripMenuItem.Text = "Высокая";
            // 
            // HelpToolStripMenuItem
            // 
            HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
            HelpToolStripMenuItem.Size = new Size(224, 26);
            HelpToolStripMenuItem.Text = "Управление";
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 467);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(888, 22);
            statusStrip1.TabIndex = 1;
            statusStrip1.Text = "statusStrip1";
            // 
            // wfaControlPuzzle
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(888, 489);
            Controls.Add(statusStrip1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "wfaControlPuzzle";
            Text = "wfaControlPuzzle";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem MainToolStripMenuItem;
        private StatusStrip statusStrip1;
        private ToolStripMenuItem DifficulToolStripMenuItem;
        private ToolStripMenuItem EasyToolStripMenuItem;
        private ToolStripMenuItem NormalToolStripMenuItem;
        private ToolStripMenuItem HardToolStripMenuItem;
        private ToolStripMenuItem HelpToolStripMenuItem;
    }
}
