using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ZiLib;

namespace NextionFontEditor.Controls {

    public class CharEditor : Control {
        private IZiCharacter _character;
        private Graphics _charGraphics;
        private int _zoom = 10;
        private bool _showGrid = true;
        private bool _showKerning = true;

        private Bitmap _bBuffer;
        private Graphics _gBuffer;

        private byte _kerningL;
        private byte _kerningR;

        private const int GUIDE_SIZE = 5;

        public CharEditor() : base() {
            BackColor = Color.WhiteSmoke;
        }

        protected override bool DoubleBuffered => true;

        protected override void OnCreateControl() {
            base.OnCreateControl();

            //CharImage = new Bitmap(16, 32);
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
            var _charImage = _character?.ToBitmap();

            if (_charImage != null && _bBuffer != null && _gBuffer != null) {
                _gBuffer.Clear(Color.Transparent);
                _gBuffer.DrawImage(_charImage, 0, 0, _charImage.Width * Zoom, _charImage.Height * Zoom);

                if (_showGrid) DrawGrid();

                /* Kerning updates */
                if (_showKerning) DrawKerning();

                SolidBrush bgBrush = new SolidBrush(SystemColors.ControlDark);
                e.Graphics.FillRectangle(bgBrush, 0, 0, _charImage.Width * Zoom, GUIDE_SIZE);
                e.Graphics.FillRectangle(bgBrush, 0, _charImage.Height * Zoom + GUIDE_SIZE, _charImage.Width * Zoom, GUIDE_SIZE);

                SolidBrush fgBrush = new SolidBrush(Color.Red);
                Rectangle rect = new Rectangle(2 * Zoom - GUIDE_SIZE, -GUIDE_SIZE, 2* GUIDE_SIZE, 2* GUIDE_SIZE);
                e.Graphics.FillPie(fgBrush, rect, 90, 90);
                fgBrush = new SolidBrush(Color.Red);
                rect = new Rectangle(2 * Zoom - GUIDE_SIZE, -GUIDE_SIZE, 2* GUIDE_SIZE, 2* GUIDE_SIZE);
                rect = new Rectangle(2 * Zoom - GUIDE_SIZE, _charImage.Height * Zoom - GUIDE_SIZE, 2* GUIDE_SIZE, 2* GUIDE_SIZE);
                e.Graphics.FillPie(fgBrush, rect, 90, -90);

                fgBrush = new SolidBrush(Color.Red);
                rect = new Rectangle(2 * Zoom - GUIDE_SIZE, 0, 2* GUIDE_SIZE, GUIDE_SIZE);
                e.Graphics.FillPie(fgBrush, rect, 270, 90);
                fgBrush = new SolidBrush(Color.Red);
                rect = new Rectangle(2 * Zoom - GUIDE_SIZE, _charImage.Height * Zoom + GUIDE_SIZE, 2* GUIDE_SIZE, GUIDE_SIZE);
                e.Graphics.FillPie(fgBrush, rect, 270, 90);

                e.Graphics.DrawImage(_bBuffer, 0, GUIDE_SIZE, _bBuffer.Width, _bBuffer.Height);
            }

        }

        protected override void OnMouseClick(MouseEventArgs e) {
            base.OnMouseClick(e);
            if (e.Button != MouseButtons.Left) return;
            var _charImage = _character.ToBitmap();

            var x = e.X / Zoom;
            var y = (e.Y- GUIDE_SIZE) / Zoom;

            if (x < 0 || x > _character.Width - 1 || y < 0 || y > _character.Parent.CharacterHeight - 1) return;

            TogglePixel(_charImage, x, y);

            Invalidate();
            Refresh();
        }

        private int lastX = -1;
        private int lastY = -1;

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
            if (e.Button != MouseButtons.Left) return;
            var _charImage = _character.ToBitmap();

            var x = e.X / Zoom;
            var y = (e.Y - GUIDE_SIZE) / Zoom;

            if (x < 0 || x > _charImage.Width - 1 || y < 0 || y > _charImage.Height - 1) return;

            lastX = x;
            lastY = y;
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
            var _charImage = _character.ToBitmap();

            var x = e.X / Zoom;
            var y = (e.Y - GUIDE_SIZE) / Zoom;

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
            var _charImage = _character.ToBitmap();

            _charGraphics.Clear(Color.Transparent);

            _charImage.Tag = true;   // Bitmap is dirty or has changed
            var ts = (ToolStrip)this.Parent.Parent.Controls["tsCharEditor"];
            ts.Items["btnRevertCharacter"].Enabled = true;

            Invalidate();
            Refresh();
        }

        public void MoveCharacterX(int pixels) {
            if (_charGraphics == null) return;
            var _charImage = _character.ToBitmap();

            using (var b = new Bitmap(_charImage)) {
                _charGraphics.Clear(Color.Transparent);
                _charGraphics.DrawImage(b, pixels, 0);
            }
            _charImage.Tag = true;   // Bitmap is dirty or has changed
            var ts = (ToolStrip)this.Parent.Parent.Controls["tsCharEditor"];
            ts.Items["btnRevertCharacter"].Enabled = true;

            Invalidate();
            Refresh();
        }

        public void MoveCharacterY(int pixels) {
            if (_charGraphics == null) return;
            var _charImage = _character.ToBitmap();

            using (var b = new Bitmap(_charImage)) {
                _charGraphics.Clear(Color.Transparent);
                _charGraphics.DrawImage(b, 0, pixels);
            }
            _charImage.Tag = true;   // Bitmap is dirty or has changed
            var ts = (ToolStrip)this.Parent.Parent.Controls["tsCharEditor"];
            ts.Items["btnRevertCharacter"].Enabled = true;

            Invalidate();
            Refresh();
        }

        private void CreateBuffer() {
            if (_character == null) return;
            var _charImage = _character.ToBitmap();

            _bBuffer = new Bitmap(_charImage.Width * Zoom, _charImage.Height * Zoom);
            _gBuffer = Graphics.FromImage(_bBuffer);

            _gBuffer.PixelOffsetMode = PixelOffsetMode.Half;
            _gBuffer.InterpolationMode = InterpolationMode.NearestNeighbor;
            _gBuffer.SmoothingMode = SmoothingMode.None;
        }

        private void TogglePixel(Bitmap b, int x, int y) {
            if (x < 0 || y < 0) { return; }
            b = _character.ToBitmap();

            var p = b.GetPixel(x, y);
            b.SetPixel(x, y, p.A == 255 && p.R == 0 && p.G == 0 && p.B == 0 ? Color.FromArgb(0, 255, 255, 255) : Color.FromArgb(255, 0, 0, 0));

            //_charImage.Tag = true;   // Bitmap is dirty or has changed
            var ts = (ToolStrip)this.Parent.Parent.Controls["tsCharEditor"];
            ts.Items["btnRevertCharacter"].Enabled = true;
        }

        private void SetSize() {
            if (_character == null) return;

            Width = _character.Width * Zoom;
            Height = _character.Parent.CharacterHeight * Zoom + 10;
        }

        private void DrawGrid() {
            if (_character == null) return;
            if (_gBuffer == null) return;
            var _charImage = _character.ToBitmap();

            for (int x = 0; x < _charImage.Width; x++) {
                _gBuffer.DrawLine(Pens.LightGray, x * Zoom, 0, x * Zoom, _charImage.Height * Zoom);
            }

            for (int y = 0; y < _charImage.Height; y++) {
                _gBuffer.DrawLine(Pens.LightGray, 0, y * Zoom, _charImage.Width * Zoom, y * Zoom);
            }


            _gBuffer.DrawRectangle(Pens.LightGray, 1, 1, _charImage.Width * Zoom - 1, _charImage.Height * Zoom - 1);
        }

        private void DrawKerning()
        {
            if (_character == null) return;
            if (_gBuffer == null) return;
            var _charImage = _character.ToBitmap();

            if (_kerningL > 0) {
                _gBuffer.DrawLine(Pens.Red, _kerningL * Zoom, 0, _kerningL * Zoom, _charImage.Height * Zoom);
            }

            if (_kerningR > 0) {
                _gBuffer.DrawLine(Pens.Red, (_charImage.Width - _kerningR) * Zoom, 0, (_charImage.Width - _kerningR) * Zoom, _charImage.Height * Zoom);
            }

        }

        public IZiCharacter Character
        {
            get => _character;
            set
            {
                _character = value;
                _charGraphics = Graphics.FromImage(_character.ToBitmap());

                CreateBuffer();
                SetSize();
                Invalidate();
            }

        }

        public byte KerningL
        {
            get => _kerningL;
            set
            {
                _kerningL = value;
                Invalidate();
            }

        }
        public byte KerningR
        {
            get => _kerningR;
            set
            {
                _kerningR = value;
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