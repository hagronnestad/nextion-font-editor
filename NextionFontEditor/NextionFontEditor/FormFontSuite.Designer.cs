namespace NextionFontEditor {
    partial class FormFontSuite {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFontSuite));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.lblStatusStatic = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewFontGenerator = new System.Windows.Forms.ToolStripButton();
            this.btnNewFontEditor = new System.Windows.Forms.ToolStripButton();
            this.btnNewFontPreview = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAboutCredit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAboutIcons = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatusStatic});
            this.statusStrip.Location = new System.Drawing.Point(0, 527);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(961, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // lblStatusStatic
            // 
            this.lblStatusStatic.Name = "lblStatusStatic";
            this.lblStatusStatic.Size = new System.Drawing.Size(207, 17);
            this.lblStatusStatic.Text = "Nextion Font Suite by @hagronnestad";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewFontGenerator,
            this.btnNewFontEditor,
            this.btnNewFontPreview});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(961, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnNewFontGenerator
            // 
            this.btnNewFontGenerator.Image = ((System.Drawing.Image)(resources.GetObject("btnNewFontGenerator.Image")));
            this.btnNewFontGenerator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewFontGenerator.Name = "btnNewFontGenerator";
            this.btnNewFontGenerator.Size = new System.Drawing.Size(106, 22);
            this.btnNewFontGenerator.Text = "Font Generator";
            this.btnNewFontGenerator.Click += new System.EventHandler(this.btnNewFontGenerator_Click);
            // 
            // btnNewFontEditor
            // 
            this.btnNewFontEditor.Image = ((System.Drawing.Image)(resources.GetObject("btnNewFontEditor.Image")));
            this.btnNewFontEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewFontEditor.Name = "btnNewFontEditor";
            this.btnNewFontEditor.Size = new System.Drawing.Size(85, 22);
            this.btnNewFontEditor.Text = "Font Editor";
            this.btnNewFontEditor.Click += new System.EventHandler(this.btnNewFontEditor_Click);
            // 
            // btnNewFontPreview
            // 
            this.btnNewFontPreview.Image = ((System.Drawing.Image)(resources.GetObject("btnNewFontPreview.Image")));
            this.btnNewFontPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNewFontPreview.Name = "btnNewFontPreview";
            this.btnNewFontPreview.Size = new System.Drawing.Size(95, 22);
            this.btnNewFontPreview.Text = "Font Preview";
            this.btnNewFontPreview.Click += new System.EventHandler(this.btnNewFontPreview_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuAbout});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(961, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(92, 22);
            this.mnuFileExit.Text = "Exit";
            this.mnuFileExit.Click += new System.EventHandler(this.mnuFileExit_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAboutCredit,
            this.mnuAboutIcons});
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(52, 20);
            this.mnuAbout.Text = "About";
            // 
            // mnuAboutCredit
            // 
            this.mnuAboutCredit.Enabled = false;
            this.mnuAboutCredit.Name = "mnuAboutCredit";
            this.mnuAboutCredit.Size = new System.Drawing.Size(274, 22);
            this.mnuAboutCredit.Text = "Nextion Font Suite by @hagronnestad";
            // 
            // mnuAboutIcons
            // 
            this.mnuAboutIcons.Enabled = false;
            this.mnuAboutIcons.Name = "mnuAboutIcons";
            this.mnuAboutIcons.Size = new System.Drawing.Size(274, 22);
            this.mnuAboutIcons.Text = "Icons by Icons8.com";
            // 
            // FormFontSuite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 549);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormFontSuite";
            this.Text = "Nextion Font Suite";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuFileExit;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuAboutCredit;
        private System.Windows.Forms.ToolStripButton btnNewFontGenerator;
        private System.Windows.Forms.ToolStripButton btnNewFontEditor;
        private System.Windows.Forms.ToolStripButton btnNewFontPreview;
        private System.Windows.Forms.ToolStripStatusLabel lblStatusStatic;
        private System.Windows.Forms.ToolStripMenuItem mnuAboutIcons;
    }
}