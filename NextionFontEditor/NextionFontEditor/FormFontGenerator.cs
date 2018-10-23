using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using ZiLib;

namespace NextionFontEditor {

    public partial class FormFontGenerator : Form {

        public FormFontGenerator() {
            InitializeComponent();
        }

        private void FormFontGenerator_Load(object sender, EventArgs e) {
            InitializeNextionFontSizesList();
        }

        private void InitializeNextionFontSizesList() {
            for (int i = 8; i <= 255; i += 8) {
                cmbNextionFontSize.Items.Add(i);
            }

            cmbNextionFontSize.Text = "16";
            numFontSize.Value = 16;
        }

        private void CreatePreview() {
            var previewChars = Enumerable.Range(32, 224).Select(x => ((char) x).ToString()).ToArray();

            //var previewChars = 

            var fontName = lstFonts.SelectedItem?.ToString() ?? "";
            var fontSize = (int) numFontSize.Value;

            var size = int.Parse(cmbNextionFontSize.Text);
            var width = size / 2;
            var height = size;

            var ms = fontSize;

            if (rbUseAllCharactersMaxSize.Checked) {
                var allCharsMaxSize = size;

                foreach (var c in previewChars) {
                    var mst = GetMaxFontSizeForRect(c, fontName, size, new SizeF(width, height));
                    if (mst < allCharsMaxSize) {
                        allCharsMaxSize = mst;
                    }
                }

                ms = allCharsMaxSize;
            }

            var pPreviews = new List<Bitmap>();

            foreach (var c in previewChars) {
                var b = new Bitmap(width, height);

                using (var g = Graphics.FromImage(b)) {

                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                    g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

                    g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

                    if (rbUseSingleCharacterMaxSize.Checked) {
                        ms = GetMaxFontSizeForRect(c, fontName, size, new SizeF(width, height));
                    }

                    var font = new Font(fontName, ms, GraphicsUnit.Pixel);

                    g.DrawString(
                        c,
                        font,
                        new SolidBrush(Color.Black),
                        new RectangleF((float) numCharOffsetX.Value, (float) numCharOffsetY.Value, width, height),
                            new StringFormat(StringFormat.GenericDefault) {
                                Alignment = StringAlignment.Center,
                                LineAlignment = StringAlignment.Center
                            }
                        );

                    //new StringFormat(StringFormat.GenericDefault) {
                    //    Alignment = StringAlignment.Center
                    //}

                    //var scale = width / ss.Width;
                    //if (scale < 1) {
                    //    g.ScaleTransform(scale, scale);
                    //}

                    pPreviews.Add(b);
                }
            }

            panelPreview.SuspendLayout();
            panelPreview.Controls.Clear();
            panelPreview.Controls.AddRange(
                pPreviews.Select(x => new PictureBox() {
                    Width = width,
                    Height = height,
                    Image = x,
                    Margin = new Padding(3)
                }).ToArray());
            panelPreview.ResumeLayout();

            //var data = BitmapTo1BppData(new Bitmap(((PictureBox) panelPreview.Controls[1]).Image));

            var newZiFont = ZiFont.FromCharacterBitmaps("test", (byte) width, (byte) height, pPreviews);
            newZiFont.Save("test.zi");
        }

        private int GetMaxFontSizeForRect(string text, String fontName, int fontSize, SizeF rect) {

            var g = Graphics.FromImage(new Bitmap(10, 10));

            for (int i = fontSize; i > 0; i--) {

                var font = new Font(fontName, i, GraphicsUnit.Pixel);

                var ss = g.MeasureString(text, font, 0, StringFormat.GenericTypographic);

                if (ss.Width < rect.Width && ss.Height < rect.Height) return i;
            }

            return fontSize;
        }

        private void lstFonts_SelectedIndexChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void numCharOffsetX_ValueChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void numCharOffsetY_ValueChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void cmbFontSize_SelectedIndexChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void cmbNextionFontSize_SelectedIndexChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void numFontSize_ValueChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void rbManualFontSize_CheckedChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void rbUseAllCharactersMaxSize_CheckedChanged(object sender, EventArgs e) {
            CreatePreview();
        }

        private void rbUseSingleCharacterMaxSize_CheckedChanged(object sender, EventArgs e) {
            CreatePreview();
        }
    }
}