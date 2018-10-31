using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NextionFontEditor.Controls {

    public class CharEditor : Control {
        private Bitmap _charImage;
        private int _zoom = 10;
        private bool _showGrid = true;

        private Bitmap bBuffer;
        private Graphics gBuffer;

        protected override bool DoubleBuffered => true;

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            SetSize();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent) {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (_charImage != null) {
                gBuffer.DrawImage(_charImage, 0, 0, _charImage.Width * Zoom, CharImage.Height * Zoom);
            }

            if (ShowGrid) DrawGrid();

            e.Graphics.DrawImage(bBuffer, 0, 0, bBuffer.Width, bBuffer.Height);
        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);

            var x = e.X / Zoom;
            var y = e.Y / Zoom;

            if (x < 0 || x > _charImage.Width - 1 || y < 0 || y > _charImage.Height - 1) return;

            TogglePixel(_charImage, x, y);

            Invalidate();
            Refresh();
        }

        private int lastX = 0;
        private int lastY = 0;

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            var x = e.X / Zoom;
            var y = e.Y / Zoom;

            if (x < 0 || x > _charImage.Width - 1 || y < 0 || y > _charImage.Height - 1) return;

            lastX = x;
            lastY = y;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            var x = e.X / Zoom;
            var y = e.Y / Zoom;

            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (y > _charImage.Height - 1) y = _charImage.Height - 1;
            if (x > _charImage.Width - 1) x = _charImage.Width - 1;

            if (e.Button == MouseButtons.Left) {
                if (x != lastX || y != lastY) {
                    TogglePixel(_charImage, lastX, lastY);

                    Invalidate();
                    Refresh();
                }

                lastX = x;
                lastY = y;
            }
        }

        private void CreateBuffer() {
            if (_charImage == null) return;

            bBuffer = new Bitmap(_charImage.Width * Zoom, _charImage.Height * Zoom);
            gBuffer = Graphics.FromImage(bBuffer);
            gBuffer.Clear(Color.White);

            gBuffer.PixelOffsetMode = PixelOffsetMode.Half;
            gBuffer.InterpolationMode = InterpolationMode.NearestNeighbor;
            gBuffer.SmoothingMode = SmoothingMode.None;
        }

        private void TogglePixel(Bitmap b, int x, int y) {
            var p = b.GetPixel(x, y);
            b.SetPixel(x, y, p.A != 255 || p.R != 0 || p.G != 0 || p.B != 0 ? Color.Black : Color.White);
        }

        private void SetSize() {
            if (_charImage == null) return;

            Width = _charImage.Width * Zoom;
            Height = _charImage.Height * Zoom;
        }

        private void DrawGrid() {
            if (_charImage == null) return;
            if (gBuffer == null) return;

            for (int x = 0; x < _charImage.Width; x++) {
                gBuffer.DrawLine(Pens.LightGray, x * Zoom, 0, x * Zoom, _charImage.Height * Zoom);
            }

            for (int y = 0; y < _charImage.Height; y++) {
                gBuffer.DrawLine(Pens.LightGray, 0, y * Zoom, _charImage.Width * Zoom, y * Zoom);
            }

            gBuffer.DrawRectangle(Pens.LightGray, 1, 1, _charImage.Width * Zoom - 1, _charImage.Height * Zoom - 1);
        }

        public Bitmap CharImage
        {
            get => _charImage;
            set
            {
                _charImage = value;
                CreateBuffer();
                SetSize();
                Invalidate();
            }

        }

        public int Zoom
        {
            get => _zoom;
            set
            {
                _zoom = value;
                ShowGrid = _zoom > 3;
                CreateBuffer();
                SetSize();
                Invalidate();
            }
        }

        public bool ShowGrid
        {
            get => _showGrid;
            set
            {
                _showGrid = value;
                Invalidate();
            }
        }
    }
}