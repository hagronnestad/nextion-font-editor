using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace NextionFontEditor {

    public partial class FormFontGenerator : Form {

        public FormFontGenerator() {
            InitializeComponent();
        }

        private void lstFonts_MeasureItem(object sender, MeasureItemEventArgs e) {
            e.ItemWidth = lstFonts.Width;
            e.ItemHeight = 30;
        }

        private void lstFonts_DrawItem(object sender, DrawItemEventArgs e) {
            e.DrawBackground();

            var fontName = lstFonts.Items[e.Index].ToString();

            var font = new Font(fontName, 20, GraphicsUnit.Pixel);

            // See if the item is selected.
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) {

                e.Graphics.DrawString(fontName, font, SystemBrushes.HighlightText, e.Bounds.Left, e.Bounds.Top);

            } else {

                using (SolidBrush br = new SolidBrush(e.ForeColor)) {
                    e.Graphics.DrawString(fontName, font, br, e.Bounds.Left, e.Bounds.Top);
                }

            }

            e.DrawFocusRectangle();
        }

        private void FormFontGenerator_Load(object sender, EventArgs e) {

            InitializeFontList();
            InitializeSizesList();

        }

        private void InitializeFontList() {
            lstFonts.Items.Clear();

            using (InstalledFontCollection col = new InstalledFontCollection()) {
                lstFonts.Items.AddRange(col.Families
                        .OrderBy(x => x.Name)
                        .Select(x => x.Name)
                        .ToArray()
                    );
            }
        }

        private void InitializeSizesList() {
            for (int i = 8; i <= 800; i += 8) {
                cmbSize.Items.Add(i);
            }

            cmbSize.Text = "80";
        }

        private void CreatePreview() {
            var previewChars = Enumerable.Range(32, 225).Select(x => ((char) x).ToString()).ToArray();

            panelPreview.Controls.Clear();

            var fontName = lstFonts.SelectedItem?.ToString() ?? "";

            var size = int.Parse(cmbSize.Text);
            var width = size / 2;
            var height = size;

            var ms = size;
            foreach (var c in previewChars) {
                var mst = GetMaxFontSizeForRect(c, fontName, size, new SizeF(width, height));
                if (mst < ms) {
                    ms = mst;
                }
            }

            foreach (var c in previewChars) {
                var b = new Bitmap(width, height);

                using (var g = Graphics.FromImage(b)) {

                    g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
                    g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

                    g.FillRectangle(new SolidBrush(Color.White), 0, 0, width, height);

                    if (chkUseGlobalMaxSize.Checked) {
                        ms = GetMaxFontSizeForRect(c, fontName, size, new SizeF(width, height));
                    }

                    var font = new Font(fontName, ms, GraphicsUnit.Pixel);

                    g.DrawString(
                        c,
                        font,
                        new SolidBrush(Color.Black),
                        new RectangleF((float) offsetX.Value, (float) offsetY.Value, width, height),
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

                    var p = new PictureBox() {
                        Width = width,
                        Height = height,
                        Image = b,
                        Margin = new Padding(0)
                    };

                    panelPreview.Controls.Add(p);
                }
            }

            var data = BitmapTo1BppData(new Bitmap(((PictureBox) panelPreview.Controls[1]).Image));
        }

        private void button1_Click(object sender, EventArgs e) {
            CreatePreview();
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

        private byte[] BitmapTo1BppData(Bitmap b) {
            var pixels = new bool[b.Width * b.Height];

            for (int y = 0; y < b.Height; y++) {

                for (int x = 0; x < b.Width; x++) {

                    var pixel = b.GetPixel(x, y);
                    pixels[(y * b.Width) + x] = (pixel.R == 0 && pixel.G == 0 && pixel.B == 0);
                }

            }

            var data = new byte[b.Width * b.Height / 8];

            for (int i = 0; i < data.Length; i++) {

                for (int j = 0; j < 8; j++) {

                    var bit = pixels[(i * 8) + j] ? 1 : 0;
                    data[i] |= (byte) (bit << (7 - j));

                }

            }

            return data;
        }
    }
}