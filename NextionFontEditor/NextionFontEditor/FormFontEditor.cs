using System;
using System.Linq;
using System.Windows.Forms;
using ZiLib.FileVersion.Common;

namespace NextionFontEditor {

    public partial class FormFontEditor : Form {

        public FormFontEditor() {
            InitializeComponent();
            UpdateCharacter();
        }

        private ZiLib.IZiFont ziFont;

        private void FormFontEditor_Load(object sender, EventArgs e) {
            cmbZoom.Items.AddRange(Enumerable.Range(1, 30).Select(x => $"{x}x").ToArray());
            cmbZoom.SelectedIndex = 9;
        }

        private void UpdateCharacter() {
            btnAddCharacters.Enabled = ziFont != null;
            btnClear.Enabled = ziFont != null;
            btnCopy.Enabled = ziFont != null;
            btnPaste.Enabled = ziFont != null;
            btnDeleteCharacter.Enabled = ziFont != null;
            btnMoveDown.Enabled = ziFont != null;
            btnMoveLeft.Enabled = ziFont != null;
            btnMoveRight.Enabled = ziFont != null;
            btnMoveUp.Enabled = ziFont != null;

            if (ziFont == null) {
                numChar.Value = 0;
                btnRevertCharacter.Enabled = false;
                return;
            }

            if (numChar.Value >= ziFont.Characters.Count) {
                numChar.Value = 0;
            }

            if (numChar.Value < 0) {
                numChar.Value = ziFont.Characters.Count - 1;
            }

            btnRevertCharacter.Enabled = (ziFont.Characters.Count > 0) && ziFont.Characters[(int)numChar.Value].CanRevert();

            var bmp = ziFont.Characters.Skip((int)numChar.Value).First().ToBitmap();
            if (bmp != null && bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
            {
                charEditor1.Character = ziFont.Characters[(int)numChar.Value];
                pPreview.Image = bmp;
            } else {
            }
            txtEncoding.Text = ziFont.CodePage.CodePageIdentifier.GetDescription();
            txtCodePoint.Text = ziFont.Characters[(int)numChar.Value].CodePoint.ToString();
        }

        private void numChar_ValueChanged(object sender, EventArgs e) {
            UpdateCharacter();
        }

        private void btnOpenFont_Click(object sender, EventArgs e)
        {
            var res = ofd.ShowDialog();

            if (res == DialogResult.OK)
            {
                var ziFont2 = ZiLib.FileVersion.Common.ZiFont.FromFile(ofd.FileName);

                if (ziFont2 != null)
                {
                    ziFont?.Characters.Clear();

                    ziFont = ziFont2;
                    numChar.Maximum = ziFont.CodePage.CharacterCount; // - 1;
                    numChar.Minimum = -1;
                    numChar.Value = 1;
                }
                else {
                    MessageBox.Show("Unsopported file format.","Error",MessageBoxButtons.OK);
                }
                UpdateCharacter();
            }
        }

        private void panel1_Resize(object sender, EventArgs e) {
            charEditor1.Left = (panel1.Width / 2) - (charEditor1.Width / 2);
            charEditor1.Top = (panel1.Height / 2) - (charEditor1.Height / 2);
        }

        private void FormFontEditor_Shown(object sender, EventArgs e) {
            panel1_Resize(sender, e);
        }

        private void cmbZoom_TextChanged(object sender, EventArgs e) {
        }

        private void cmbZoom_SelectedIndexChanged(object sender, EventArgs e) {
            charEditor1.Zoom = int.Parse(cmbZoom.Text.Replace("x", ""));
            panel1_Resize(sender, e);
        }

        private void btnShowGrid_Click(object sender, EventArgs e) {
            charEditor1.ShowGrid = btnShowGrid.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e) {
            var res = sfd.ShowDialog();

            if (res == DialogResult.OK) {

                ziFont.Save(sfd.FileName, ziFont.CodePage);
            }
        }

        private void btnClear_Click(object sender, EventArgs e) {
            charEditor1.Clear();
        }

        private void charEditor1_Paint(object sender, PaintEventArgs e) {
            var CharImage = charEditor1.Character?.ToBitmap();
            if (CharImage?.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined)
            {
                pPreview.Image = CharImage;
            }
            else
            {
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e) {
            charEditor1.MoveCharacterX(-1);
        }

        private void btnMoveRight_Click(object sender, EventArgs e) {
            charEditor1.MoveCharacterX(1);
        }

        private void btnMoveUp_Click(object sender, EventArgs e) {
            charEditor1.MoveCharacterY(-1);
        }

        private void btnMoveDown_Click(object sender, EventArgs e) {
            charEditor1.MoveCharacterY(1);
        }

        private void CharEditor1_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            ZiClipboard.CopyToClipboard(ziFont.Characters[(int)numChar.Value]);
        }

        private void btnDeleteCharacter_Click(object sender, EventArgs e)
        {
            ziFont.RemoveCharacter((int)numChar.Value);
            UpdateCharacter();
        }

        private void btnRevertCharacter_Click(object sender, EventArgs e)
        {
            ziFont.Characters[(int)numChar.Value].RevertBitmap();
            UpdateCharacter();
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {

        }

        private void Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void btnAddCharacters_Click(object sender, EventArgs e)
        {
            using (var form = new FormAddCharacters())
            {
                var result = form.ShowDialog();
                if (result == DialogResult.OK)
                {

                    foreach (var item in ziFont.Characters) {
                        //if (ziFont.CodePage.CodePoints.Contains(ch)) {
                            //var item = ziFont.Characters.Find(character => { return character.CodePoint == ch; });
                            if (item != null)
                            {
                                if (true) {
                                var font = new System.Drawing.Font("Arial", 24);
                                var emheight = font.FontFamily.GetEmHeight(System.Drawing.FontStyle.Regular);
                                var linespacing = font.FontFamily.GetLineSpacing(System.Drawing.FontStyle.Regular);
                                var fontsize = (float)(ziFont.CharacterHeight * emheight / linespacing * 0.975);
                                font = new System.Drawing.Font(font.FontFamily, fontsize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

                                // replace existing char
                                if ((item.CodePoint < 0x00d800) | (item.CodePoint > 0x00dfff)) {
                                        item.SetString(font, new System.Drawing.PointF(0, 0), item.GetString());
                                    } else {
                                        item.SetString(font, new System.Drawing.PointF(0, 0), " ");
                                    }
                                }
                            } else {
                                var txt = Char.ConvertFromUtf32((int)item.CodePoint);
                               // var bmp = ZiLib.Extensions.BitmapExtensions.DrawString(txt, "", (byte)ziFont.CharacterHeight);
                               // var bytes = ZiLib.FileVersion.V5.BinaryTools.BitmapTo3BppData(bmp);
                                var character = ZiCharacter.FromString(ziFont, item.CodePoint, new System.Drawing.Font("Arial",22), new System.Drawing.PointF(0,0), txt);
                                ziFont.AddCharacter(item.CodePoint,character);
                            }
                        //}
                    }

                }
            }
        }
    }
}