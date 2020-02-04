using System;
using System.Drawing;
using System.IO;
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
            this.SuspendLayout();

            for (int i = 0; i < font.Characters.Count; i++) {
                var b = font.Characters[i].ToBitmap();
                if (b != null && b.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                    var p = new PictureBox() {
                        Width = b.Width + 2,
                        Height = font.CharacterHeight + 2,
                        Image = b,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White
                    };
                    flowPanel.Controls.Add(p);
                } else {
                    var img = new Bitmap(font.CharacterHeight + 2, 2);
                    var p = new PictureBox() {
                        Width = 2,
                        Height = font.CharacterHeight + 2,
                        Image = img,
                        BorderStyle = BorderStyle.FixedSingle,
                        BackColor = Color.White
                    };

                    flowPanel.Controls.Add(p);
                }
                if (i >= 1024) { break; }
            }
            this.ResumeLayout();
        }

        private void CreateCharacterPreview2(IZiFont font) {
            flowPanel.Controls.Clear();
            this.SuspendLayout();

            var preview = new Bitmap(flowPanel.Width - 15, flowPanel.Height - 15);
            var x = 0;
            var y = 0;

            using (var graphics = Graphics.FromImage(preview)) {
                graphics.FillRectangle(Brushes.Transparent, 0, 0, preview.Width, preview.Height);

                foreach (var ch in font.Characters) {
                    var b = ch.ToBitmap();

                    if ((x + b.Width) > preview.Width) {
                        x = 0;
                        y += b.Height;
                    }

                    if (y >= preview.Height) { break; }

                    graphics.DrawImage(b, x, y);
                    x += b.Width;
                }

            }

            var p = new PictureBox() {
                Width = preview.Width,
                Height = preview.Height,
                Image = preview,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.Black
            };

            flowPanel.Controls.Add(p);

            this.ResumeLayout();
        }


        private void btnOpen_Click(object sender, EventArgs e) {
            var res = ofd.ShowDialog();

            if (res == DialogResult.OK) {
                var zifont = ZiFont.FromFile(ofd.FileName);
                CreateCharacterPreview2(zifont);

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