using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZiLib;
using ZiLib.FileVersion.Common;

namespace NextionFontEditor {

    public partial class FormFontPreview : Form {

        public FormFontPreview() {
            InitializeComponent();
        }

        private void FormFontPreview_Load(object sender, EventArgs e) {
            var p = new PictureBox() {
                Width = 16,
                Height = 32,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            flowPanel.Controls.Add(p);
        }

        private void CreateCharacterPreview(IZiFont font) {
            flowPanel.Controls.Clear();

            foreach (var b in font.CharBitmaps.Take(300)) {
                if (b.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
                {
                    var p = new PictureBox()
                    {
                        Width = b.Width + 2,
                        Height = font.CharacterHeight + 2,
                        Image = b,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White
                    };

                    flowPanel.Controls.Add(p);
                }
                else
                {
                    var img = new Bitmap(font.CharacterHeight + 2, 2);
                    var p = new PictureBox()
                    {
                        Width = 2,
                        Height = font.CharacterHeight + 2,
                        Image = img,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White
                    };

                    flowPanel.Controls.Add(p);
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e) {
            var res = ofd.ShowDialog();

            if (res == DialogResult.OK) {
                var zifont = ZiFont.FromFile(ofd.FileName);
                CreateCharacterPreview(zifont);

                lblFile.Text = Path.GetFileName(ofd.FileName);
                lblFontName.Text = zifont.Name;
                lblCodePage.Text = zifont.CodePage.CodePageIdentifier.ToString();

                lblWidth.Text = zifont.CharacterWidth.ToString();
                lblHeight.Text = zifont.CharacterHeight.ToString();
                lblCharacters.Text = zifont.CodePage.CharacterCount.ToString();

                lblFileSize.Text = zifont.FileSize.ToString();
                lblFileVersion.Text = zifont.Version.ToString();
                //lblBytesPerChar.Text = zifont.BytesPerChar.ToString();
            }
        }
    }
}