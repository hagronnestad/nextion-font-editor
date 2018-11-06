using System;
using System.Linq;
using System.Windows.Forms;
using ZiLib;

namespace NextionFontEditor {

    public partial class FormFontPreview : Form {

        public FormFontPreview() {
            InitializeComponent();
        }

        private void FormFontPreview_Load(object sender, EventArgs e) {
        }

        private void CreateCharacterPreview(ZiFont font) {
            flowPanel.Controls.Clear();

            foreach (var b in font.CharBitmaps.Take(300)) {
                var p = new PictureBox() {
                    Width = font.CharacterWidth + 2,
                    Height = font.CharacterHeight + 2,
                    Image = b,
                    BorderStyle = BorderStyle.FixedSingle
                };

                flowPanel.Controls.Add(p);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            var res = ofd.ShowDialog();

            if (res == DialogResult.OK) {
                var zifont = ZiFont.FromFile(ofd.FileName);
                CreateCharacterPreview(zifont);
            }
        }
    }
}