using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using ZiLib.FileVersion.Common;

namespace NextionFontEditor {

    public partial class FormFontEditor : Form {

        private FormCharacterWidth frmCharWidth;

        public FormFontEditor() {
            InitializeComponent();
            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = false;
            UpdateCharacter();
        }

        private ZiLib.IZiFont ziFont;

        private void FormFontEditor_Load(object sender, EventArgs e) {
            cmbZoom.Items.AddRange(Enumerable.Range(1, 30).Select(x => $"{x}x").ToArray());
            cmbZoom.SelectedIndex = 9;

            frmCharWidth = new FormCharacterWidth();
        }

        private void UpdateCharacter() {
            btnAddCharacters.Enabled = ziFont != null;
            btnClear.Enabled = ziFont != null;
            btnCopy.Enabled = ziFont != null;
            btnPaste.Enabled = ziFont != null;
            btnDeleteCharacter.Enabled = ziFont != null;
            btnCharacterWidth.Enabled = ziFont != null;
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
            if (bmp != null && bmp.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
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

        private void btnOpenFont_Click(object sender, EventArgs e) {
            var res = ofd.ShowDialog();

            if (res == DialogResult.OK) {
                var ziFont2 = ZiLib.FileVersion.Common.ZiFont.FromFile(ofd.FileName);

                if (ziFont2 != null) {
                    ziFont?.Characters.Clear();

                    ziFont = ziFont2;
                    numChar.Maximum = ziFont.CodePage.CharacterCount; // - 1;
                    numChar.Minimum = -1;
                    numChar.Value = 1;
                } else {
                    MessageBox.Show("Unsupported file format.", "Error", MessageBoxButtons.OK);
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


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            System.Diagnostics.Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.RealTime;
            BackgroundWorker worker = sender as BackgroundWorker;
            //Debugger.Break();
            ZiLib.IZiFont myFont = e.Argument as ZiLib.IZiFont;
            int totalbytecount = 0;
            int count = myFont.CharacterCount;

            for (int i = 0; i < count; i++) {
                totalbytecount += myFont.Characters[i].GetCharacterData().Length;
                if (i % 100 == 0) {
                    worker.ReportProgress(i * 100 / count);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            var res = sfd.ShowDialog();

            if (res == DialogResult.OK) {
                var count = ziFont.CharacterCount;
                var step = (int)(count / 100);

                /* Encode all characters first */
                /*                Cursor.Current = Cursors.WaitCursor;
                                for (var i = 0; i < count; i+=step) {
                                    for (var j = 0; j < step; j++) {
                                        if ((i + j) >= count) {
                                            break;
                                        }
                                        var tmp = ziFont.Characters[i + j].GetCharacterData().Length;
                                    }
                                    Debug.WriteLine( i.ToString() + " / " + count.ToString() );
                                }
                */
                if (!backgroundWorker1.IsBusy && count > 256) {
                    btnSave.Enabled = false;
                    progressBar1.Value = 0;
                    progressBar1.Visible = true;
                    backgroundWorker1.RunWorkerAsync(ziFont);
                } else {
                    ziFont.Save(sfd.FileName, ziFont.CodePage);
                    Cursor.Current = Cursors.Default;
                }

            }
        }
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            /* Quick call to save method */
            ziFont.Save(sfd.FileName, ziFont.CodePage);
            Cursor.Current = Cursors.Default;
            btnSave.Enabled = true;
            progressBar1.Visible = false;
        }

        private void btnClear_Click(object sender, EventArgs e) {
            charEditor1.Clear();
        }

        private void charEditor1_Paint(object sender, PaintEventArgs e) {
            var CharImage = charEditor1.Character?.ToBitmap();
            if (CharImage?.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined) {
                pPreview.Image = CharImage;
            } else {
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

        private void CharEditor1_Click(object sender, EventArgs e) {

        }

        private void Panel1_Paint(object sender, PaintEventArgs e) {

        }

        private void btnCopy_Click(object sender, EventArgs e) {
            ZiClipboard.CopyToClipboard(ziFont.Characters[(int)numChar.Value]);
        }

        private void btnDeleteCharacter_Click(object sender, EventArgs e) {
            ziFont.RemoveCharacter((int)numChar.Value);
            UpdateCharacter();
        }

        private void btnRevertCharacter_Click(object sender, EventArgs e) {
            ziFont.Characters[(int)numChar.Value].RevertBitmap();
            UpdateCharacter();
        }

        private void BtnUndo_Click(object sender, EventArgs e) {

        }

        private void Panel1_Paint_1(object sender, PaintEventArgs e) {

        }

        /* Speed up debugger
        https://devblogs.microsoft.com/devops/performance-improvement-when-debugging-net-code-with-visual-studio-2015/
        */
        [DebuggerNonUserCode]
        private void btnAddCharacters_Click(object sender, EventArgs e) {
            using (var form = new FormAddCharacters()) {
                var result = form.ShowDialog();
                if (result == DialogResult.OK) {

                    var font = new System.Drawing.Font("Arial Unicode MS", 24);
                    var emheight = font.FontFamily.GetEmHeight(System.Drawing.FontStyle.Regular);
                    var linespacing = font.FontFamily.GetLineSpacing(System.Drawing.FontStyle.Regular);
                    var fontsize = (float)(ziFont.CharacterHeight * emheight / linespacing * 0.985);
                    font = new System.Drawing.Font(font.FontFamily, fontsize, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);

                    /* System.Windows.Media */
                    var fontface = new Typeface("Arial Unicode MS");
                    var ret = false;
                    GlyphTypeface glyphFace;
                    ret = fontface.TryGetGlyphTypeface(out glyphFace);

                    //GlyphRun glyphRun = null;
                    //glyphRun = new GlyphRun(glyphFace, 0, false, 24, new ushort[] { 0 }, new Point(0, 0), new double[] { 0 },
                    //    null, null, null, null, null, null);

                    foreach (var item in ziFont.Characters) {
                        //if (ziFont.CodePage.CodePoints.Contains(ch)) {
                        //var item = ziFont.Characters.Find(character => { return character.CodePoint == ch; });
                        if (item != null) {
                            if (true) {
                                var leftKern = 0d;
                                var rightKern = 0d;
                                try {
                                    ushort glyphIndex = glyphFace.CharacterToGlyphMap[(int)item.CodePoint];

                                    //glyphRun = new GlyphRun(glyphFace, 0, false, 24, new ushort[] { (ushort)item.CodePoint }, new Point(0, 0), new double[] { glyphFace.AdvanceWidths[glyphIndex] },
                                    //        null, null, null, null, null, null);
                                    //glyphRun.GlyphTypeface = glyphFace;
                                    //glyphRun.FontRenderingEmSize = emheight;


                                    string txt = item.GetString();
                                    leftKern = glyphFace.LeftSideBearings[glyphIndex] * fontsize;
                                    rightKern = glyphFace.RightSideBearings[glyphIndex] * fontsize;
                                    double charWidth = glyphFace.AdvanceWidths[glyphIndex] * fontsize;
                                    if (leftKern < -0.5 || rightKern < -0.5) {
                                        var a = 0;
                                    }
                                }
                                catch (Exception err) {
                                }

                                // replace existing char
                                item.SetString(font, new System.Drawing.PointF(0, 0), item.GetString());
                                if (leftKern < -0) {
                                    item.KerningLeft = (byte)Math.Ceiling(-leftKern + 1);
                                } else {
                                    item.KerningLeft = 0;
                                }
                                if (rightKern < -0) {
                                    item.KerningRight = (byte)Math.Ceiling(-rightKern + 1);
                                } else {
                                    item.KerningRight = 0;
                                }

                            }
                        } else {
                            var txt = Char.ConvertFromUtf32((int)item.CodePoint);
                            // var bmp = ZiLib.Extensions.BitmapExtensions.DrawString(txt, "", (byte)ziFont.CharacterHeight);
                            // var bytes = ZiLib.FileVersion.V5.BinaryTools.BitmapTo3BppData(bmp);
                            var character = ZiCharacter.FromString(ziFont, item.CodePoint, new System.Drawing.Font("Arial", 22), new System.Drawing.PointF(0, 0), txt);
                            ziFont.AddCharacter(item.CodePoint, character);
                        }
                        //}
                    }

                }
            }
        }

        private void BackgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e) {

        }

        private void BtnPaste_Click(object sender, EventArgs e) {
            ZiClipboard.PasteFromClipboard(ziFont.Characters[(int)numChar.Value]);
            UpdateCharacter();
        }

        private void btnCharacterWidth_Click(object sender, EventArgs e) {

            frmCharWidth.numKerningL.Minimum = 0;
            frmCharWidth.numKerningL.Maximum = 254;

            frmCharWidth.numKerningR.Minimum = 0;
            frmCharWidth.numKerningR.Maximum = 254;

            frmCharWidth.numWidth.Minimum = 1;
            frmCharWidth.numWidth.Maximum = 255;

            int Width = ziFont.Characters[(int)numChar.Value].Width;
            if (Width > frmCharWidth.numWidth.Maximum) {
                frmCharWidth.numWidth.Value = frmCharWidth.numWidth.Maximum;
            } else if (Width < frmCharWidth.numWidth.Minimum) {
                frmCharWidth.numWidth.Value = frmCharWidth.numWidth.Minimum;
            } else {
                frmCharWidth.numWidth.Value = Width;
            }

            frmCharWidth.numKerningL.Value = ziFont.Characters[(int)numChar.Value].KerningLeft;
            frmCharWidth.numKerningL.Maximum = frmCharWidth.numWidth.Value - ziFont.Characters[(int)numChar.Value].KerningRight - 1;

            int KernL = ziFont.Characters[(int)numChar.Value].KerningLeft;
            if (KernL > frmCharWidth.numKerningL.Maximum) {
                frmCharWidth.numKerningL.Value = frmCharWidth.numKerningL.Maximum;
            } else if (KernL < frmCharWidth.numKerningL.Minimum) {
                frmCharWidth.numKerningL.Value = frmCharWidth.numKerningL.Minimum;
            } else {
                frmCharWidth.numKerningL.Value = KernL;
            }

            frmCharWidth.numKerningR.Value = ziFont.Characters[(int)numChar.Value].KerningRight;
            frmCharWidth.numKerningR.Maximum = frmCharWidth.numWidth.Value - ziFont.Characters[(int)numChar.Value].KerningLeft - 1;

            int KernR = ziFont.Characters[(int)numChar.Value].KerningRight;
            if (KernR > frmCharWidth.numKerningR.Maximum) {
                frmCharWidth.numKerningR.Value = frmCharWidth.numKerningR.Maximum;
            } else if (KernR < frmCharWidth.numKerningR.Minimum) {
                frmCharWidth.numKerningR.Value = frmCharWidth.numKerningR.Minimum;
            } else {
                frmCharWidth.numKerningR.Value = KernR;
            }

            var result = frmCharWidth.ShowDialog();
            if (result == DialogResult.OK) {
                ziFont.Characters[(int)numChar.Value].Width = (byte)frmCharWidth.numWidth.Value;
                ziFont.Characters[(int)numChar.Value].KerningLeft = (byte)frmCharWidth.numKerningL.Value;
                ziFont.Characters[(int)numChar.Value].KerningRight = (byte)frmCharWidth.numKerningR.Value;

                ziFont.Characters[(int)numChar.Value].SetBitmap(ziFont.Characters[(int)numChar.Value].ToBitmap());
                UpdateCharacter();
            }

        }
    }
}