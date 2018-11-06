using System;
using System.Windows.Forms;

namespace NextionFontEditor {

    public partial class FormFontSuite : Form {

        public FormFontSuite() {
            InitializeComponent();
        }

        private void mnuFileExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnNewFontGenerator_Click(object sender, EventArgs e) {
            var form = new FormFontGenerator {
                MdiParent = this,
                //WindowState = FormWindowState.Maximized
            };
            form.Show();
        }

        private void btnNewFontEditor_Click(object sender, EventArgs e) {
            var form = new FormFontEditor {
                MdiParent = this,
                //WindowState = FormWindowState.Maximized
            };
            form.Show();
        }

        private void btnNewFontPreview_Click(object sender, EventArgs e) {
            var form = new FormFontPreview {
                MdiParent = this,
                //WindowState = FormWindowState.Maximized
            };
            form.Show();
        }
    }
}