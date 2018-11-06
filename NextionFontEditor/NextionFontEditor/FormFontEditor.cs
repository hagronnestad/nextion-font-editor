using System;
using System.Linq;
using System.Windows.Forms;
using ZiLib;

namespace NextionFontEditor {

    public partial class FormFontEditor : Form {

        public FormFontEditor() {
            InitializeComponent();
        }

        private ZiFont ziFont;

        private void FormFontEditor_Load(object sender, EventArgs e) {
            ziFont = ZiFont.FromFile(@"Test Files\Arial_16_ascii.zi");

            numChar.Maximum = ziFont.CodePage.CharacterCount - 1;

            charEditor1.CharImage = ziFont.CharBitmaps.Skip(1).First();
            numChar.Value = 1;
        }

        private void charEditor1_Click(object sender, EventArgs e) {
            pictureBox1.Image = charEditor1.CharImage;
        }

        private void numChar_ValueChanged(object sender, EventArgs e) {
            charEditor1.CharImage = ziFont.CharBitmaps.Skip((int) numChar.Value).First();
            pictureBox1.Image = charEditor1.CharImage;
        }
    }
}