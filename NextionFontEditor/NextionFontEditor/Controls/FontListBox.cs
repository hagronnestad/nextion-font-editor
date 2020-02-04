using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;

namespace NextionFontEditor.Controls {
    public class FontListBox : ListBox {
        private int _padding = 3;

        private int _fontPreviewSize = 20;
        public int FontPreviewSize {
            get => _fontPreviewSize;
            set {
                _fontPreviewSize = value;
                Invalidate();
                Update();
            }
        }


        public FontListBox() {
            DrawMode = DrawMode.OwnerDrawVariable;
            LoadFontList();
        }

        private void LoadFontList() {
            using (InstalledFontCollection col = new InstalledFontCollection()) {
                Items.AddRange(col.Families
                        .OrderBy(x => x.Name)
                        .Select(x => string.IsNullOrWhiteSpace(x.Name) ? "[No name]" : x.Name)
                        .ToArray()
                    );
            }
        }


        protected override void OnMeasureItem(MeasureItemEventArgs e) {
            var fontName = Items[e.Index].ToString();
            var font = new Font(fontName, _fontPreviewSize, GraphicsUnit.Pixel);
            var size = e.Graphics.MeasureString(fontName, font);

            e.ItemWidth = (int)Math.Ceiling(size.Width);
            e.ItemHeight = (int)Math.Ceiling(size.Height) + _padding * 2;
        }

        protected override void OnDrawItem(DrawItemEventArgs e) {
            e.DrawBackground();

            var fontName = Items[e.Index].ToString();
            var font = new Font(fontName, _fontPreviewSize, GraphicsUnit.Pixel);

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected) {
                e.Graphics.DrawString(fontName, font, SystemBrushes.HighlightText, e.Bounds.Left, e.Bounds.Top + _padding);
            } else {
                using (SolidBrush br = new SolidBrush(e.ForeColor)) {
                    e.Graphics.DrawString(fontName, font, br, e.Bounds.Left, e.Bounds.Top + _padding);
                }
            }

            e.DrawFocusRectangle();
        }
    }
}
