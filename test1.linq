<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Drawing.Design.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.Mobile.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Web.RegularExpressions.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Drawing</Namespace>
  <Namespace>System.Drawing.Text</Namespace>
</Query>

var f = new Form();
f.Show();

var p1 = new PictureBox()
{
	Location = new Point(0, 0),
	BackColor = Color.Red
};
f.Controls.Add(p1);

var p2 = new PictureBox()
{
	Location = new Point(0, 100),
	BackColor = Color.Red
};
f.Controls.Add(p2);

var font1 = new Font("Arial", 24, GraphicsUnit.Pixel);
var b1 = new Bitmap(24, 24);
var p1g = Graphics.FromImage(b1);
p1g.DrawString(
	"a",
	font1,
	new SolidBrush(Color.Black), 0, 0);
p1.Image = b1;

var font2 = new Font("Arial", 96, GraphicsUnit.Pixel);
var b2 = new Bitmap(96, 96);

var p2g = Graphics.FromImage(b2);
p2g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.None;
p2g.TextRenderingHint = TextRenderingHint.SingleBitPerPixel;

p2g.DrawString(
	"a",
	font2,
	new SolidBrush(Color.Black), 0, 0);
	


	var b3 = new Bitmap(24, 24);
	var p3g = Graphics.FromImage(b3);
	p3g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
	p3g.DrawImage(b2, 0, 0, 24, 24);
	
p2.Image = b3;