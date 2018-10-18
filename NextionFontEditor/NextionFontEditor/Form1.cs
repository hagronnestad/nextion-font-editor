using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NextionFontEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file1 = @"Test Files\Arial_40_ascii.zi";
            var file2 = @"Test Files\Arial_40_iso-8859-1.zi";
            var file3 = @"Test Files\Arial_40_gb2312.zi";

            var bytes1 = File.ReadAllBytes(file1);
            var bytes2 = File.ReadAllBytes(file2);
            var bytes3 = File.ReadAllBytes(file3);

            DrawFont(bytes1, p, textBox1);
            DrawFont(bytes2, p2, textBox2);
            DrawFont(bytes3, p3, textBox3);
        }

        private void DrawFont(byte[] bytes, PictureBox p, TextBox t)
        {
            var headerLength = 0x1C; // 27
            var header = bytes.Take(headerLength).ToArray();

            var fontNameLength = header[0x11]; var fontNameLength2 = header[0x12]; // Always the same as 0x11?
            var fontName = Encoding.ASCII.GetString(bytes.Skip(headerLength).Take(fontNameLength).ToArray());

            var cWidth = header[0x6];
            var cHeight = header[0x7];

            var variableDataLength = BitConverter.ToUInt32(header.Skip(0x14).Take(4).ToArray(), 0);
            var charDataLength = variableDataLength - fontNameLength;

            var charactersData = bytes.Skip(headerLength + fontNameLength).ToArray();
            var bytesPerChar = (cWidth * cHeight) / 8;
            var charCount = charDataLength / bytesPerChar;

            t.Text = fontName;

            var spacing = 6;
            var g = p.CreateGraphics();

            var bb = new SolidBrush(Color.Black);
            var pr = new Pen(Color.DarkCyan);

            var columns = 20;
            var row = -1;

            for (int charIndex = 0; charIndex < charCount; charIndex++)
            {
                var charData = charactersData.Skip(charIndex * bytesPerChar).Take(bytesPerChar).ToArray();
                var bits = BytesToBits(charData);

                var column = charIndex % columns;
                if (column == 0) row++;

                var pixel = 0;

                var xPos = 1 + (column * cWidth + (column * spacing));
                var yPos = 1 + (row * cHeight + (row * spacing));

                g.DrawRectangle(pr, xPos - 1, yPos - 1, cWidth + 1, cHeight + 1);

                for (int y = 0; y < cHeight; y++)
                {
                    for (int x = 0; x < cWidth; x++)
                    {
                        if (bits[pixel]) g.FillRectangle(bb, xPos + x, yPos + y, 1, 1);
                        pixel++;
                    }
                }

            }
        }

        public bool[] BytesToBits(byte[] bytes)
        {
            bool[] bits = new bool[bytes.Length * 8];

            for (int i = 0; i < bytes.Length; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    var index = i * 8 + j;
                    bits[index] = (bytes[i] & (1 << (7 - j))) != 0;
                }
            }

            return bits;
        }
    }
}
