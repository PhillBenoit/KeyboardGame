namespace KeyboardGameV2
{
    partial class frmDictTools
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
            menuStrip1 = new MenuStrip();
            backToGameToolStripMenuItem = new ToolStripMenuItem();
            btnLoad = new Button();
            lblLoad = new Label();
            boxLoad = new GroupBox();
            boxExclude = new GroupBox();
            chkExclude = new CheckBox();
            btnExclude = new Button();
            lblExclude = new Label();
            boxOutput = new GroupBox();
            txtOutput = new TextBox();
            btnOutput = new Button();
            menuStrip1.SuspendLayout();
            boxLoad.SuspendLayout();
            boxExclude.SuspendLayout();
            boxOutput.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(48, 48);
            menuStrip1.Items.AddRange(new ToolStripItem[] { backToGameToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(1257, 56);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // backToGameToolStripMenuItem
            // 
            backToGameToolStripMenuItem.Name = "backToGameToolStripMenuItem";
            backToGameToolStripMenuItem.Size = new Size(268, 52);
            backToGameToolStripMenuItem.Text = "Back to Game";
            backToGameToolStripMenuItem.Click += Click_mnuBackToGame;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(1036, 42);
            btnLoad.Name = "btnLoad";
            btnLoad.Size = new Size(191, 66);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Load";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += Click_btnLoad;
            // 
            // lblLoad
            // 
            lblLoad.BackColor = SystemColors.HighlightText;
            lblLoad.Location = new Point(6, 51);
            lblLoad.Name = "lblLoad";
            lblLoad.Size = new Size(1024, 57);
            lblLoad.TabIndex = 3;
            lblLoad.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // boxLoad
            // 
            boxLoad.Controls.Add(btnLoad);
            boxLoad.Controls.Add(lblLoad);
            boxLoad.Location = new Point(12, 116);
            boxLoad.Name = "boxLoad";
            boxLoad.Size = new Size(1233, 127);
            boxLoad.TabIndex = 4;
            boxLoad.TabStop = false;
            boxLoad.Text = "Load Dictionary";
            // 
            // boxExclude
            // 
            boxExclude.Controls.Add(chkExclude);
            boxExclude.Controls.Add(btnExclude);
            boxExclude.Controls.Add(lblExclude);
            boxExclude.Location = new Point(12, 249);
            boxExclude.Name = "boxExclude";
            boxExclude.Size = new Size(1233, 127);
            boxExclude.TabIndex = 5;
            boxExclude.TabStop = false;
            boxExclude.Text = "Exclude";
            // 
            // chkExclude
            // 
            chkExclude.AutoSize = true;
            chkExclude.Location = new Point(6, 60);
            chkExclude.Name = "chkExclude";
            chkExclude.Size = new Size(42, 41);
            chkExclude.TabIndex = 6;
            chkExclude.UseVisualStyleBackColor = true;
            chkExclude.CheckedChanged += CheckedChanged_chkExclude;
            // 
            // btnExclude
            // 
            btnExclude.Enabled = false;
            btnExclude.Location = new Point(1036, 42);
            btnExclude.Name = "btnExclude";
            btnExclude.Size = new Size(191, 66);
            btnExclude.TabIndex = 1;
            btnExclude.Text = "Load";
            btnExclude.UseVisualStyleBackColor = true;
            btnExclude.Click += Click_btnExclude;
            // 
            // lblExclude
            // 
            lblExclude.BackColor = SystemColors.HighlightText;
            lblExclude.Location = new Point(54, 51);
            lblExclude.Name = "lblExclude";
            lblExclude.Size = new Size(976, 57);
            lblExclude.TabIndex = 3;
            lblExclude.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // boxOutput
            // 
            boxOutput.Controls.Add(txtOutput);
            boxOutput.Controls.Add(btnOutput);
            boxOutput.Location = new Point(18, 382);
            boxOutput.Name = "boxOutput";
            boxOutput.Size = new Size(1233, 127);
            boxOutput.TabIndex = 5;
            boxOutput.TabStop = false;
            boxOutput.Text = "Output File Name";
            // 
            // txtOutput
            // 
            txtOutput.Location = new Point(6, 48);
            txtOutput.Name = "txtOutput";
            txtOutput.Size = new Size(1018, 55);
            txtOutput.TabIndex = 6;
            // 
            // btnOutput
            // 
            btnOutput.Enabled = false;
            btnOutput.Location = new Point(1036, 42);
            btnOutput.Name = "btnOutput";
            btnOutput.Size = new Size(191, 66);
            btnOutput.TabIndex = 1;
            btnOutput.Text = "Save";
            btnOutput.UseVisualStyleBackColor = true;
            btnOutput.Click += Click_btnOutput;
            // 
            // frmDictTools
            // 
            AutoScaleDimensions = new SizeF(20F, 48F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1257, 568);
            Controls.Add(boxOutput);
            Controls.Add(boxExclude);
            Controls.Add(boxLoad);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "frmDictTools";
            Text = "frmDictTools";
            FormClosed += FormClosed_frmDictTools;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            boxLoad.ResumeLayout(false);
            boxExclude.ResumeLayout(false);
            boxExclude.PerformLayout();
            boxOutput.ResumeLayout(false);
            boxOutput.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem backToGameToolStripMenuItem;
        private Button btnLoad;
        private Label lblLoad;
        private GroupBox boxLoad;
        private GroupBox boxExclude;
        private CheckBox chkExclude;
        private Button btnExclude;
        private Label lblExclude;
        private GroupBox boxOutput;
        private Button btnOutput;
        private TextBox txtOutput;
    }
}