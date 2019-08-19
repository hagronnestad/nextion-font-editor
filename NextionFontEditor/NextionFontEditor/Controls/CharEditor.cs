using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NextionFontEditor.Controls {

    public class CharEditor : Control {
        private Bitmap _charImage;
        private Graphics _charGraphics;
        private int _zoom = 10;
        private bool _showGrid = true;

        private Bitmap _bBuffer;
        private Graphics _gBuffer;

        public CharEditor() : base() {
            BackColor = Color.WhiteSmoke;
        }

        protected override bool DoubleBuffered => true;

        protected override void OnCreateControl() {
            base.OnCreateControl();

            CharImage = new Bitmap(16, 32);
        }

        protected override void OnResize(EventArgs e) {
            base.OnResize(e);
            SetSize();
        }

        protected override void OnPaintBackground(PaintEventArgs pevent) {
            base.OnPaintBackground(pevent);
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (_charImage != null && _bBuffer != null && _gBuffer != null) {
                _gBuffer.Clear(Color.Transparent);

                if (_charImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
                {
                    _gBuffer.DrawImage(_charImage, 0, 0, _charImage.Width * Zoom, CharImage.Height * Zoom);
                }
                else
                {
                }

                if (_showGrid) DrawGrid();

                e.Graphics.DrawImage(_bBuffer, 0, 0, _bBuffer.Width, _bBuffer.Height);
            }

        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left) return;

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
            if (e.Button != MouseButtons.Left) return;

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

        public void Clear() {
            if (_charGraphics == null) return;

            _charGraphics.Clear(Color.Transparent);
            Invalidate();
            Refresh();
        }

        public void MoveCharacterX(int pixels) {
            if (_charGraphics == null) return;

            using(var b = new Bitmap(_charImage)) {
                _charGraphics.Clear(Color.Transparent);
                _charGraphics.DrawImage(b, pixels, 0);
            }

            Invalidate();
            Refresh();
        }

        public void MoveCharacterY(int pixels) {
            if (_charGraphics == null) return;

            using (var b = new Bitmap(_charImage)) {
                _charGraphics.Clear(Color.Transparent);
                _charGraphics.DrawImage(b, 0, pixels);
            }

            Invalidate();
            Refresh();
        }

        private void CreateBuffer() {
            if (_charImage == null) return;

            _bBuffer = new Bitmap(_charImage.Width * Zoom, _charImage.Height * Zoom);
            _gBuffer = Graphics.FromImage(_bBuffer);

            _gBuffer.PixelOffsetMode = PixelOffsetMode.Half;
            _gBuffer.InterpolationMode = InterpolationMode.NearestNeighbor;
            _gBuffer.SmoothingMode = SmoothingMode.None;
        }

        private void TogglePixel(Bitmap b, int x, int y) {
            var p = b.GetPixel(x, y);
            b.SetPixel(x, y, p.A == 255 && p.R == 0 && p.G == 0 && p.B == 0 ? Color.FromArgb(0, 255, 255, 255) : Color.FromArgb(255, 0, 0, 0));
        }

        private void SetSize() {
            Visible = false;
            if (_charImage == null) return;
            if (_charImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined) return;

            Width = _charImage.Width * Zoom;
            Height = _charImage.Height * Zoom;
            Visible = true;
        }

        private void DrawGrid() {
            if (_charImage == null) return;
            if (_charImage.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined) return;
            if (_gBuffer == null) return;

            for (int x = 0; x < _charImage.Width; x++) {
                _gBuffer.DrawLine(Pens.LightGray, x * Zoom, 0, x * Zoom, _charImage.Height * Zoom);
            }

            for (int y = 0; y < _charImage.Height; y++) {
                _gBuffer.DrawLine(Pens.LightGray, 0, y * Zoom, _charImage.Width * Zoom, y * Zoom);
            }


            _gBuffer.DrawRectangle(Pens.LightGray, 1, 1, _charImage.Width * Zoom - 1, _charImage.Height * Zoom - 1);
        }

        public Bitmap CharImage
        {
            get => _charImage;
            set
            {
                _charImage = value;
                if (_charImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
                {
                    _charGraphics = Graphics.FromImage(_charImage);
                    CreateBuffer();
                }
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