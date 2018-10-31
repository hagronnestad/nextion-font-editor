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
            this.charEditor1 = new NextionFontEditor.Controls.CharEditor();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.numChar = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChar)).BeginInit();
            this.SuspendLayout();
            // 
            // charEditor1
            // 
            this.charEditor1.CharImage = ((System.Drawing.Bitmap)(resources.GetObject("charEditor1.CharImage")));
            this.charEditor1.Location = new System.Drawing.Point(12, 162);
            this.charEditor1.Name = "charEditor1";
            this.charEditor1.ShowGrid = true;
            this.charEditor1.Size = new System.Drawing.Size(240, 480);
            this.charEditor1.TabIndex = 0;
            this.charEditor1.Text = "charEditor1";
            this.charEditor1.Zoom = 30;
            this.charEditor1.Click += new System.EventHandler(this.charEditor1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(240, 86);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // numChar
            // 
            this.numChar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numChar.Location = new System.Drawing.Point(12, 12);
            this.numChar.Name = "numChar";
            this.numChar.Size = new System.Drawing.Size(122, 30);
            this.numChar.TabIndex = 2;
            this.numChar.ValueChanged += new System.EventHandler(this.numChar_ValueChanged);
            // 
            // FormFontEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1141, 765);
            this.Controls.Add(this.numChar);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.charEditor1);
            this.Name = "FormFontEditor";
            this.Text = "Font Editor";
            this.Load += new System.EventHandler(this.FormFontEditor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numChar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.CharEditor charEditor1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown numChar;
    }
}