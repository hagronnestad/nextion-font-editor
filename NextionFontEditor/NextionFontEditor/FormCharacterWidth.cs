using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextionFontEditor {
    public partial class FormCharacterWidth : Form {
        public FormCharacterWidth() {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e) {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void txtWidth_TextChanged(object sender, EventArgs e) {

        }

        private void numWidth_ValueChanged(object sender, EventArgs e) {
            numKerningL.Maximum = numWidth.Value - numKerningR.Value - 1;
            if (numKerningL.Value > (numWidth.Value - numKerningR.Value - 1))
                numKerningL.Value = numWidth.Value - numKerningR.Value - 1;

            numKerningR.Maximum = numWidth.Value - numKerningL.Value - 1;
            if (numKerningR.Value > (numWidth.Value - numKerningL.Value - 1))
                numKerningR.Value = numWidth.Value - numKerningL.Value - 1;
        }

        private void numKerningL_ValueChanged(object sender, EventArgs e) {
            numWidth.Minimum = numKerningL.Value + numKerningR.Value + 1;
            numKerningR.Maximum = numWidth.Value - numKerningL.Value - 1;
            if (numWidth.Value < (numKerningL.Value + numKerningR.Value + 1))
                numWidth.Value = numKerningL.Value + numKerningR.Value + 1;
        }

        private void numKerningR_ValueChanged(object sender, EventArgs e) {
            numWidth.Minimum = numKerningL.Value + numKerningR.Value + 1;
            numKerningL.Maximum = numWidth.Value - numKerningR.Value - 1;
            if (numWidth.Value < (numKerningL.Value + numKerningR.Value + 1))
                numWidth.Value = numKerningL.Value + numKerningR.Value + 1;
        }

        private void lblKerningL_Click(object sender, EventArgs e) {

        }
    }
}
