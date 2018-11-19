namespace NextionFontEditor {
    partial class FormFontEditor {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFontEditor));
            this.pPreview = new System.Windows.Forms.PictureBox();
            this.numChar = new System.Windows.Forms.NumericUpDown();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnOpenFont = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tsCharEditor = new System.Windows.Forms.ToolStrip();
            this.btnShowGrid = new System.Windows.Forms.ToolStripButton();
            this.cmbZoom = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.panelWrapper = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.btnClear = new System.Windows.Forms.ToolStripButton();
            this.btnMoveLeft = new System.Windows.Forms.ToolStripButton();
            this.btnMoveRight = new System.Windows.Forms.ToolStripButton();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.charEditor1 = new NextionFontEditor.Controls.CharEditor();
            ((System.ComponentModel.ISupportInitialize)(this.pPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChar)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tsCharEditor.SuspendLayout();
            this.panelWrapper.SuspendLayout();
            this.SuspendLayout();
            // 
            // pPreview
            // 
            this.pPreview.BackColor = System.Drawing.Color.White;
            this.pPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pPreview.Location = new System.Drawing.Point(12, 106);
            this.pPreview.Name = "pPreview";
            this.pPreview.Size = new System.Drawing.Size(100, 100);
            this.pPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pPreview.TabIndex = 1;
            this.pPreview.TabStop = false;
            // 
            // numChar
            // 
            this.numChar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numChar.Location = new System.Drawing.Point(12, 54);
            this.numChar.Name = "numChar";
            this.numChar.Size = new System.Drawing.Size(100, 30);
            this.numChar.TabIndex = 2;
            this.numChar.ValueChanged += new System.EventHandler(this.numChar_ValueChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenFont,
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(553, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnOpenFont
            // 
            this.btnOpenFont.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenFont.Image")));
            this.btnOpenFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenFont.Name = "btnOpenFont";
            this.btnOpenFont.Size = new System.Drawing.Size(56, 22);
            this.btnOpenFont.Text = "Open";
            this.btnOpenFont.Click += new System.EventHandler(this.btnOpenFont_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // ofd
            // 
            this.ofd.Filter = "Nextion Font Files|*.zi";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.AutoScrollMargin = new System.Drawing.Size(20, 20);
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.charEditor1);
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 471);
            this.panel1.TabIndex = 4;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // tsCharEditor
            // 
            this.tsCharEditor.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnShowGrid,
            this.cmbZoom,
            this.toolStripLabel1,
            this.btnClear,
            this.btnMoveLeft,
            this.btnMoveRight,
            this.btnMoveUp,
            this.btnMoveDown});
            this.tsCharEditor.Location = new System.Drawing.Point(0, 0);
            this.tsCharEditor.Name = "tsCharEditor";
            this.tsCharEditor.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.tsCharEditor.Size = new System.Drawing.Size(413, 25);
            this.tsCharEditor.TabIndex = 1;
            this.tsCharEditor.Text = "toolStrip2";
            // 
            // btnShowGrid
            // 
            this.btnShowGrid.Checked = true;
            this.btnShowGrid.CheckOnClick = true;
            this.btnShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowGrid.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowGrid.Image = ((System.Drawing.Image)(resources.GetObject("btnShowGrid.Image")));
            this.btnShowGrid.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowGrid.Name = "btnShowGrid";
            this.btnShowGrid.Size = new System.Drawing.Size(23, 22);
            this.btnShowGrid.Text = "Show Grid";
            this.btnShowGrid.Click += new System.EventHandler(this.btnShowGrid_Click);
            // 
            // cmbZoom
            // 
            this.cmbZoom.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cmbZoom.AutoSize = false;
            this.cmbZoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZoom.Name = "cmbZoom";
            this.cmbZoom.Size = new System.Drawing.Size(50, 23);
            this.cmbZoom.SelectedIndexChanged += new System.EventHandler(this.cmbZoom_SelectedIndexChanged);
            this.cmbZoom.TextChanged += new System.EventHandler(this.cmbZoom_TextChanged);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel1.Text = "Zoom";
            // 
            // panelWrapper
            // 
            this.panelWrapper.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelWrapper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelWrapper.Controls.Add(this.tsCharEditor);
            this.panelWrapper.Controls.Add(this.panel1);
            this.panelWrapper.Location = new System.Drawing.Point(126, 28);
            this.panelWrapper.Name = "panelWrapper";
            this.panelWrapper.Size = new System.Drawing.Size(415, 496);
            this.panelWrapper.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Character";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Preview";
            // 
            // sfd
            // 
            this.sfd.Filter = "Nextion Font Files|*.zi";
            // 
            // btnClear
            // 
            this.btnClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnClear.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.Image")));
            this.btnClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(23, 22);
            this.btnClear.Text = "toolStripButton1";
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnMoveLeft
            // 
            this.btnMoveLeft.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveLeft.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveLeft.Image")));
            this.btnMoveLeft.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveLeft.Name = "btnMoveLeft";
            this.btnMoveLeft.Size = new System.Drawing.Size(23, 22);
            this.btnMoveLeft.Text = "toolStripButton1";
            this.btnMoveLeft.Click += new System.EventHandler(this.btnMoveLeft_Click);
            // 
            // btnMoveRight
            // 
            this.btnMoveRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveRight.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveRight.Image")));
            this.btnMoveRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveRight.Name = "btnMoveRight";
            this.btnMoveRight.Size = new System.Drawing.Size(23, 22);
            this.btnMoveRight.Text = "toolStripButton1";
            this.btnMoveRight.Click += new System.EventHandler(this.btnMoveRight_Click);
            // 
            // btnMoveUp
            // 
            this.btnMoveUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(23, 22);
            this.btnMoveUp.Text = "toolStripButton1";
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(23, 22);
            this.btnMoveDown.Text = "toolStripButton1";
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // charEditor1
            // 
            this.charEditor1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.charEditor1.CharImage = ((System.Drawing.Bitmap)(resources.GetObject("charEditor1.CharImage")));
            this.charEditor1.Location = new System.Drawing.Point(53, 36);
            this.charEditor1.Margin = new System.Windows.Forms.Padding(20);
            this.charEditor1.Name = "charEditor1";
            this.charEditor1.ShowGrid = true;
            this.charEditor1.Size = new System.Drawing.Size(160, 320);
            this.charEditor1.TabIndex = 0;
            this.charEditor1.Text = "charEditor1";
            this.charEditor1.Zoom = 10;
            this.charEditor1.Paint += new System.Windows.Forms.PaintEventHandler(this.charEditor1_Paint);
            // 
            // FormFontEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 536);
            this.Controls.Add(this.flowPanel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panelWrapper);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.numChar);
            this.Controls.Add(this.pPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFontEditor";
            this.Text = "Font Editor";
            this.Load += new System.EventHandler(this.FormFontEditor_Load);
            this.Shown += new System.EventHandler(this.FormFontEditor_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChar)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.tsCharEditor.ResumeLayout(false);
            this.tsCharEditor.PerformLayout();
            this.panelWrapper.ResumeLayout(false);
            this.panelWrapper.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.CharEditor charEditor1;
        private System.Windows.Forms.PictureBox pPreview;
        private System.Windows.Forms.NumericUpDown numChar;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnOpenFont;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip tsCharEditor;
        private System.Windows.Forms.ToolStripComboBox cmbZoom;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton btnShowGrid;
        private System.Windows.Forms.Panel panelWrapper;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStripButton btnClear;
        private System.Windows.Forms.ToolStripButton btnMoveLeft;
        private System.Windows.Forms.ToolStripButton btnMoveRight;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
    }
}