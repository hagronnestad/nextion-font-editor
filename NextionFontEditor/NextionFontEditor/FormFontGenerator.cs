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

            //new FormFontPreview().Show();
        }

        private void InitializeNextionFontSizesList() {
            for (int i = 8; i <= 255; i += 8) {
                cmbNextionFontSize.Items.Add(i);
            }

            cmbNextionFontSize.Text = "16";
            numFontSize.Value = 16;
        }

        public Graphics CreateGraphics(Bitmap bitmap = null) {
            var b = bitmap ?? new Bitmap(1, 1);
            var g = Graphics.FromImage(b);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit;

            return g;
        }

        private void CreatePreview() {
            var codePage = CodePages.GetCodePage(CodePageIdentifier.ISO_8859_1);

            var fontName = lstFonts.SelectedItem?.ToString() ?? "";
            var fontSize = (int) numFontSize.Value;

            var size = int.Parse(cmbNextionFontSize.Text);
            var width = size / 2;
            var height = size;

            var font = new Font(fontName, fontSize, GraphicsUnit.Pixel);

            var pPreviews = new List<Bitmap>();

            foreach (var c in codePage.Characters) {
                var bPreview = new Bitmap(width, height);

                using (var gPreview = CreateGraphics(bPreview)) {

                    var sChar = gPreview.MeasureString(c.ToString(), font, new PointF(0, 0), StringFormat.GenericTypographic).ToSize();
                    if (sChar.Width == 0) sChar.Width = 1;

                    var bChar = new Bitmap(sChar.Width, sChar.Height);

                    using (var gChar = CreateGraphics(bChar)) {
                        var sb = bChar.Width > bPreview.Width ? new SolidBrush(Color.LightCoral) : new SolidBrush(Color.LightGreen);

                        gChar.FillRectangle(sb, 0, 0, sChar.Width, sChar.Height);

                        gChar.DrawString(c.ToString(), font,
                            new SolidBrush(Color.Black),
                            0, 0, StringFormat.GenericTypographic
                        );
                    }

                    gPreview.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

                    if (bChar.Width > bPreview.Width) {
                        gPreview.DrawImage(bChar, 0, 0, width, height);

                    } else {
                        gPreview.DrawImage(bChar, (width / 2) - (bChar.Width / 2), 0, bChar.Width, height);
                    }
                }

                pPreviews.Add(bPreview);
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

            var newZiFont = ZiFont.FromCharacterBitmaps("test", (byte) width, (byte) height, pPreviews);
            newZiFont.Save("test.zi", codePage);
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

        private void numDbs_ValueChanged(object sender, EventArgs e) {
            CreatePreview();
        }
    }
}