using System;
using System.Windows.Forms;
using ZiLib;

namespace NextionFontEditor {

    public partial class FormFontPreview : Form {

        public FormFontPreview() {
            InitializeComponent();
        }

        private void FormFontPreview_Load(object sender, EventArgs e) {
            var zifont = ZiFont.FromFile(@"Test Files\Arial_40_iso-8859-1.zi");
            zifont.CreateBitmaps();

            CreateCharacterPreview(zifont);
        }

        private void CreateCharacterPreview(ZiFont font) {
            flowPanel.Controls.Clear();

            foreach (var b in font.CharBitmaps) {
                var p = new PictureBox() {
                    Width = font.CharacterWidth + 2,
                    Height = font.CharacterHeight + 2,
                    Image = b,
                    BorderStyle = BorderStyle.FixedSingle
                };

                flowPanel.Controls.Add(p);
            }
        }
    }
}