<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Windows.Forms.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Security.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Configuration.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\Accessibility.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Deployment.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Namespace>System.Windows.Forms</Namespace>
  <Namespace>System.Drawing</Namespace>
</Query>

var f = new Form();
f.Width = 500;
f.Show();

var p1 = new PictureBox()
{
	Location = new Point(10, 10),
	//BackColor = Color.Red,
	Size = new Size(200, 200),
	//SizeMode = PictureBoxSizeMode.StretchImage
};
f.Controls.Add(p1);

var c = "w";

var font1 = new Font("Arial Black", 48, GraphicsUnit.Pixel);
var b1 = new Bitmap(200, 200);
var p1g = Graphics.FromImage(b1);

var size = p1g.MeasureString(c, font1, default(PointF), StringFormat.GenericTypographic);
//var size = TextRenderer.MeasureText(c, font1, new Size(1, 1), TextFormatFlags.Default);

p1g.DrawRectangle(new Pen(Color.DarkRed), new Rectangle(0, 0, (int)size.Width + 2, (int)size.Height + 2));

p1g.DrawString(
	c,
	font1,
	new SolidBrush(Color.Black), 1, 1, StringFormat.GenericTypographic);

f.Text = size.ToString();
	
p1.Image = b1;
