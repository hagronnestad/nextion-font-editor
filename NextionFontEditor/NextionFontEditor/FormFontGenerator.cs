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

        private void CreatePreview() {
            var codePage = CodePages.GetCodePage(CodePageIdentifier.ISO_8859_1);

            var fontName = lstFonts.SelectedItem?.ToString() ?? "";
            var fontSize = (int) numFontSize.Value;

            var size = int.Parse(cmbNextionFontSize.Text);
            var width = size / 2;
            var height = size;

            var ms = fontSize;

            if (rbUseAllCharactersMaxSize.Checked) {
                var allCharsMaxSize = size;

                foreach (var c in codePage.Characters) {
                    var mst = GetMaxFontSizeForRect(c.ToString(), fontName, size, new SizeF(width, height));
                    if (mst < allCharsMaxSize) {
                        allCharsMaxSize = mst;
                    }
                }

                ms = allCharsMaxSize;
            }

            var pPreviews = new List<Bitmap>();

            foreach (var c in codePage.Characters) {
                var b = new Bitmap(width, height);

                using (var g = Graphics.FromImage(b)) {

                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                    g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

                    g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

                    if (rbUseSingleCharacterMaxSize.Checked) {
                        ms = GetMaxFontSizeForRect(c.ToString(), fontName, size, new SizeF(width, height));
                    }

                    //var font = new Font(fontName, ms, GraphicsUnit.Pixel);
                    //g.DrawString(
                    //    c.ToString(),
                    //    font,
                    //    new SolidBrush(Color.Black),
                    //    new RectangleF((float) numCharOffsetX.Value, (float) numCharOffsetY.Value, width, height),
                    //        new StringFormat(StringFormat.GenericDefault) {
                    //            Alignment = StringAlignment.Center,
                    //            //LineAlignment = StringAlignment.Center
                    //        }
                    //    );

                    var dbs = (int) numDbs.Value;

                    var bb = new Bitmap(width * dbs, height * dbs);
                    var gg = Graphics.FromImage(bb);
                    var font = new Font(fontName, ms * dbs, GraphicsUnit.Pixel);
                    gg.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                    gg.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;
                    gg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                    var ss = gg.MeasureString(c.ToString(), font, new PointF(0, 0), new StringFormat(StringFormat.GenericTypographic) {
                        //Alignment = StringAlignment.Center
                    });

                    if (ss.Width > bb.Width) {
                        var bbb = new Bitmap((int) ss.Width, bb.Height);
                        var ggg = Graphics.FromImage(bbb);
                        ggg.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                        ggg.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                        ggg.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

                        ggg.DrawString(
                            c.ToString(),
                            font,
                            new SolidBrush(Color.Black),
                            new RectangleF((float) numCharOffsetX.Value, (float) numCharOffsetY.Value, width * dbs, height * dbs),
                                new StringFormat(StringFormat.GenericTypographic) {
                                    Alignment = StringAlignment.Center
                                }
                            );

                        gg.DrawImage(bbb, 0, 0, bb.Width, bb.Height);

                    } else {
                        gg.DrawString(
                        c.ToString(),
                        font,
                        new SolidBrush(Color.Black),
                        new RectangleF((float) numCharOffsetX.Value, (float) numCharOffsetY.Value, width * dbs, height * dbs),
                            new StringFormat(StringFormat.GenericTypographic) {
                                Alignment = StringAlignment.Center
                            }
                        );
                    }

                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                    g.DrawImage(bb, 0, 0, width, height);

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