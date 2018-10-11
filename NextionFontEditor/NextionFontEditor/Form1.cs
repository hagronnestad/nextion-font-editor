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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var file16 = @"C:\Users\hag\Desktop\fontello_16.zi";
            var file32 = @"C:\Users\hag\Desktop\fontello_32.zi";

            var bytes16 = File.ReadAllBytes(file16);
            var bytes32 = File.ReadAllBytes(file32);

            textBox1.Text = Encoding.ASCII.GetString(bytes16.Skip(0x1C).Take(bytes16[0x14]).ToArray());
            textBox2.Text = Encoding.ASCII.GetString(bytes32.Skip(0x1C).Take(bytes32[0x14]).ToArray());

            //DrawFont(bytes16, p);
            DrawFont(bytes32, p2);
        }

        private void DrawFont(byte[] bytes, PictureBox p)
        {
            var cWidth = bytes[6];
            var cHeight = bytes[7];
            var spacing = 12;
            var g = p.CreateGraphics();

            var bb = new SolidBrush(Color.Black);
            var pr = new Pen(Color.Red);

            var columns = 15;
            var rows = 1;

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    var cell = (row * columns) + column;
                    var bytesPerLine = cWidth / 8;
                    var charData = bytes.Skip(0x24 + (cell * cHeight * bytesPerLine)).Take(cHeight * bytesPerLine).ToArray();

                    for (int charLine = 0; charLine < cHeight; charLine++)
                    {
                        g.DrawRectangle(pr, (column * cWidth) + (column * spacing), row * (cHeight + spacing), cWidth, cHeight);

                        var lineBytes = charData.Skip(bytesPerLine * charLine).Take(bytesPerLine).ToArray();

                        for (int lineByte = 0; lineByte < lineBytes.Length; lineByte++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                if ((lineBytes[lineByte] & (1 << k)) != 0)
                                {
                                    var x = (column * (cWidth + spacing)) + (bytesPerLine * lineByte) + k;
                                    var y = (row * (cHeight + spacing)) + charLine;

                                    g.FillRectangle(bb, x, y, 1, 1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
