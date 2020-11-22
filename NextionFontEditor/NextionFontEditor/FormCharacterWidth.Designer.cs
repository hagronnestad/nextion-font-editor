namespace NextionFontEditor {
    partial class FormCharacterWidth {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCharacterWidth));
            this.lblWidth = new System.Windows.Forms.Label();
            this.lblKerningL = new System.Windows.Forms.Label();
            this.lblKerningR = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.numWidth = new System.Windows.Forms.NumericUpDown();
            this.numKerningL = new System.Windows.Forms.NumericUpDown();
            this.numKerningR = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKerningL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKerningR)).BeginInit();
            this.SuspendLayout();
            // 
            // lblWidth
            // 
            this.lblWidth.AutoSize = true;
            this.lblWidth.Location = new System.Drawing.Point(47, 14);
            this.lblWidth.Name = "lblWidth";
            this.lblWidth.Size = new System.Drawing.Size(87, 13);
            this.lblWidth.TabIndex = 0;
            this.lblWidth.Text = "Character Width:";
            this.lblWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblKerningL
            // 
            this.lblKerningL.AutoSize = true;
            this.lblKerningL.Location = new System.Drawing.Point(67, 42);
            this.lblKerningL.Name = "lblKerningL";
            this.lblKerningL.Size = new System.Drawing.Size(67, 13);
            this.lblKerningL.TabIndex = 2;
            this.lblKerningL.Text = "Kerning Left:";
            this.lblKerningL.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblKerningL.Click += new System.EventHandler(this.lblKerningL_Click);
            // 
            // lblKerningR
            // 
            this.lblKerningR.AutoSize = true;
            this.lblKerningR.Location = new System.Drawing.Point(60, 70);
            this.lblKerningR.Name = "lblKerningR";
            this.lblKerningR.Size = new System.Drawing.Size(74, 13);
            this.lblKerningR.TabIndex = 4;
            this.lblKerningR.Text = "Kerning Right:";
            this.lblKerningR.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(235, 65);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(235, 31);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // numWidth
            // 
            this.numWidth.Location = new System.Drawing.Point(140, 12);
            this.numWidth.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWidth.Name = "numWidth";
            this.numWidth.Size = new System.Drawing.Size(60, 20);
            this.numWidth.TabIndex = 8;
            this.numWidth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWidth.ValueChanged += new System.EventHandler(this.numWidth_ValueChanged);
            // 
            // numKerningL
            // 
            this.numKerningL.Location = new System.Drawing.Point(140, 40);
            this.numKerningL.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numKerningL.Name = "numKerningL";
            this.numKerningL.Size = new System.Drawing.Size(60, 20);
            this.numKerningL.TabIndex = 9;
            this.numKerningL.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKerningL.ValueChanged += new System.EventHandler(this.numKerningL_ValueChanged);
            // 
            // numKerningR
            // 
            this.numKerningR.Location = new System.Drawing.Point(140, 68);
            this.numKerningR.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
            this.numKerningR.Name = "numKerningR";
            this.numKerningR.Size = new System.Drawing.Size(60, 20);
            this.numKerningR.TabIndex = 10;
            // 
            // FormCharacterWidth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 101);
            this.Controls.Add(this.numKerningR);
            this.Controls.Add(this.numKerningL);
            this.Controls.Add(this.numWidth);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblKerningR);
            this.Controls.Add(this.lblKerningL);
            this.Controls.Add(this.lblWidth);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormCharacterWidth";
            this.Text = "Character Width";
            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKerningL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKerningR)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWidth;
        private System.Windows.Forms.Label lblKerningL;
        private System.Windows.Forms.Label lblKerningR;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.NumericUpDown numWidth;
        public System.Windows.Forms.NumericUpDown numKerningL;
        public System.Windows.Forms.NumericUpDown numKerningR;
    }
}