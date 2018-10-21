namespace NextionFontEditor {
    partial class FormFontGenerator {
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
            this.lstFonts = new System.Windows.Forms.ListBox();
            this.cmbSize = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panelPreview = new System.Windows.Forms.FlowLayoutPanel();
            this.offsetX = new System.Windows.Forms.NumericUpDown();
            this.offsetY = new System.Windows.Forms.NumericUpDown();
            this.chkUseGlobalMaxSize = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.offsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetY)).BeginInit();
            this.SuspendLayout();
            // 
            // lstFonts
            // 
            this.lstFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstFonts.FormattingEnabled = true;
            this.lstFonts.Location = new System.Drawing.Point(521, 12);
            this.lstFonts.Name = "lstFonts";
            this.lstFonts.Size = new System.Drawing.Size(331, 316);
            this.lstFonts.TabIndex = 2;
            this.lstFonts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstFonts_DrawItem);
            this.lstFonts.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.lstFonts_MeasureItem);
            // 
            // cmbSize
            // 
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new System.Drawing.Point(13, 13);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new System.Drawing.Size(121, 21);
            this.cmbSize.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 282);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 46);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panelPreview
            // 
            this.panelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreview.AutoScroll = true;
            this.panelPreview.Location = new System.Drawing.Point(12, 370);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(840, 176);
            this.panelPreview.TabIndex = 5;
            // 
            // offsetX
            // 
            this.offsetX.Location = new System.Drawing.Point(171, 120);
            this.offsetX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.offsetX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.offsetX.Name = "offsetX";
            this.offsetX.Size = new System.Drawing.Size(60, 20);
            this.offsetX.TabIndex = 6;
            // 
            // offsetY
            // 
            this.offsetY.Location = new System.Drawing.Point(171, 146);
            this.offsetY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.offsetY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.offsetY.Name = "offsetY";
            this.offsetY.Size = new System.Drawing.Size(60, 20);
            this.offsetY.TabIndex = 7;
            // 
            // chkUseGlobalMaxSize
            // 
            this.chkUseGlobalMaxSize.AutoSize = true;
            this.chkUseGlobalMaxSize.Location = new System.Drawing.Point(171, 190);
            this.chkUseGlobalMaxSize.Name = "chkUseGlobalMaxSize";
            this.chkUseGlobalMaxSize.Size = new System.Drawing.Size(119, 17);
            this.chkUseGlobalMaxSize.TabIndex = 8;
            this.chkUseGlobalMaxSize.Text = "Use global max size";
            this.chkUseGlobalMaxSize.UseVisualStyleBackColor = true;
            // 
            // FormFontGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 558);
            this.Controls.Add(this.chkUseGlobalMaxSize);
            this.Controls.Add(this.offsetY);
            this.Controls.Add(this.offsetX);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cmbSize);
            this.Controls.Add(this.lstFonts);
            this.Name = "FormFontGenerator";
            this.Text = "FormFontGenerator";
            this.Load += new System.EventHandler(this.FormFontGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.offsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.offsetY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lstFonts;
        private System.Windows.Forms.ComboBox cmbSize;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel panelPreview;
        private System.Windows.Forms.NumericUpDown offsetX;
        private System.Windows.Forms.NumericUpDown offsetY;
        private System.Windows.Forms.CheckBox chkUseGlobalMaxSize;
    }
}