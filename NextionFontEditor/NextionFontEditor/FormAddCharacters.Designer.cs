
namespace NextionFontEditor
{
    partial class FormAddCharacters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddCharacters));
            this.cmbNextionFontSize = new System.Windows.Forms.ComboBox();
            this.panelPreview = new System.Windows.Forms.FlowLayoutPanel();
            this.numCharOffsetX = new System.Windows.Forms.NumericUpDown();
            this.numCharOffsetY = new System.Windows.Forms.NumericUpDown();
            this.lblPreview = new System.Windows.Forms.Label();
            this.lblNextionFontSize = new System.Windows.Forms.Label();
            this.lblCharOffsetX = new System.Windows.Forms.Label();
            this.lblCharOffsetY = new System.Windows.Forms.Label();
            this.lblCharOffset = new System.Windows.Forms.Label();
            this.lblFontSize = new System.Windows.Forms.Label();
            this.lblFont = new System.Windows.Forms.Label();
            this.numDbs = new System.Windows.Forms.NumericUpDown();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbUseSingleCharacterMaxSize = new System.Windows.Forms.RadioButton();
            this.rbUseAllCharactersMaxSize = new System.Windows.Forms.RadioButton();
            this.rbManualFontSize = new System.Windows.Forms.RadioButton();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.PreviewTest = new System.Windows.Forms.RadioButton();
            this.PreviewBW = new System.Windows.Forms.RadioButton();
            this.PreviewWB = new System.Windows.Forms.RadioButton();
            this.lstFonts = new NextionFontEditor.Controls.FontListBox();
            this.sldContrast = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDbs)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldContrast)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbNextionFontSize
            // 
            this.cmbNextionFontSize.FormattingEnabled = true;
            this.cmbNextionFontSize.Location = new System.Drawing.Point(452, 144);
            this.cmbNextionFontSize.Name = "cmbNextionFontSize";
            this.cmbNextionFontSize.Size = new System.Drawing.Size(121, 21);
            this.cmbNextionFontSize.TabIndex = 3;
            this.cmbNextionFontSize.SelectedIndexChanged += new System.EventHandler(this.cmbNextionFontSize_SelectedIndexChanged);
            // 
            // panelPreview
            // 
            this.panelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreview.AutoScroll = true;
            this.panelPreview.BackColor = System.Drawing.SystemColors.Control;
            this.panelPreview.Location = new System.Drawing.Point(356, 463);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(460, 132);
            this.panelPreview.TabIndex = 5;
            // 
            // numCharOffsetX
            // 
            this.numCharOffsetX.Location = new System.Drawing.Point(472, 284);
            this.numCharOffsetX.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCharOffsetX.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numCharOffsetX.Name = "numCharOffsetX";
            this.numCharOffsetX.Size = new System.Drawing.Size(53, 20);
            this.numCharOffsetX.TabIndex = 6;
            this.numCharOffsetX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCharOffsetX.ValueChanged += new System.EventHandler(this.numCharOffsetX_ValueChanged);
            // 
            // numCharOffsetY
            // 
            this.numCharOffsetY.Location = new System.Drawing.Point(472, 310);
            this.numCharOffsetY.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCharOffsetY.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numCharOffsetY.Name = "numCharOffsetY";
            this.numCharOffsetY.Size = new System.Drawing.Size(53, 20);
            this.numCharOffsetY.TabIndex = 7;
            this.numCharOffsetY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numCharOffsetY.ValueChanged += new System.EventHandler(this.numCharOffsetY_ValueChanged);
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(355, 444);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(48, 13);
            this.lblPreview.TabIndex = 10;
            this.lblPreview.Text = "Preview:";
            // 
            // lblNextionFontSize
            // 
            this.lblNextionFontSize.AutoSize = true;
            this.lblNextionFontSize.Location = new System.Drawing.Point(449, 128);
            this.lblNextionFontSize.Name = "lblNextionFontSize";
            this.lblNextionFontSize.Size = new System.Drawing.Size(90, 13);
            this.lblNextionFontSize.TabIndex = 11;
            this.lblNextionFontSize.Text = "Nextion Font Size";
            // 
            // lblCharOffsetX
            // 
            this.lblCharOffsetX.AutoSize = true;
            this.lblCharOffsetX.Location = new System.Drawing.Point(452, 286);
            this.lblCharOffsetX.Name = "lblCharOffsetX";
            this.lblCharOffsetX.Size = new System.Drawing.Size(14, 13);
            this.lblCharOffsetX.TabIndex = 12;
            this.lblCharOffsetX.Text = "X";
            // 
            // lblCharOffsetY
            // 
            this.lblCharOffsetY.AutoSize = true;
            this.lblCharOffsetY.Location = new System.Drawing.Point(452, 312);
            this.lblCharOffsetY.Name = "lblCharOffsetY";
            this.lblCharOffsetY.Size = new System.Drawing.Size(14, 13);
            this.lblCharOffsetY.TabIndex = 13;
            this.lblCharOffsetY.Text = "Y";
            // 
            // lblCharOffset
            // 
            this.lblCharOffset.AutoSize = true;
            this.lblCharOffset.Location = new System.Drawing.Point(452, 263);
            this.lblCharOffset.Name = "lblCharOffset";
            this.lblCharOffset.Size = new System.Drawing.Size(82, 13);
            this.lblCharOffset.TabIndex = 14;
            this.lblCharOffset.Text = "Character offset";
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(449, 177);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(51, 13);
            this.lblFontSize.TabIndex = 16;
            this.lblFontSize.Text = "Font Size";
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(39, 77);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(73, 13);
            this.lblFont.TabIndex = 17;
            this.lblFont.Text = "System Fonts:";
            this.lblFont.Click += new System.EventHandler(this.LblFont_Click);
            // 
            // numDbs
            // 
            this.numDbs.Location = new System.Drawing.Point(631, 284);
            this.numDbs.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numDbs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDbs.Name = "numDbs";
            this.numDbs.Size = new System.Drawing.Size(53, 20);
            this.numDbs.TabIndex = 22;
            this.numDbs.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numDbs.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDbs.ValueChanged += new System.EventHandler(this.numDbs_ValueChanged);
            // 
            // sfd
            // 
            this.sfd.DefaultExt = "zi";
            this.sfd.Filter = "Nextion Font Files|*.zi";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(828, 25);
            this.toolStrip1.TabIndex = 24;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(51, 22);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbUseSingleCharacterMaxSize);
            this.panel1.Controls.Add(this.rbUseAllCharactersMaxSize);
            this.panel1.Controls.Add(this.rbManualFontSize);
            this.panel1.Controls.Add(this.numFontSize);
            this.panel1.Location = new System.Drawing.Point(452, 194);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 41);
            this.panel1.TabIndex = 25;
            // 
            // rbUseSingleCharacterMaxSize
            // 
            this.rbUseSingleCharacterMaxSize.AutoSize = true;
            this.rbUseSingleCharacterMaxSize.Location = new System.Drawing.Point(225, 9);
            this.rbUseSingleCharacterMaxSize.Margin = new System.Windows.Forms.Padding(4);
            this.rbUseSingleCharacterMaxSize.Name = "rbUseSingleCharacterMaxSize";
            this.rbUseSingleCharacterMaxSize.Size = new System.Drawing.Size(97, 17);
            this.rbUseSingleCharacterMaxSize.TabIndex = 22;
            this.rbUseSingleCharacterMaxSize.Text = "Single max size";
            this.rbUseSingleCharacterMaxSize.UseVisualStyleBackColor = true;
            this.rbUseSingleCharacterMaxSize.CheckedChanged += new System.EventHandler(this.rbUseSingleCharacterMaxSize_CheckedChanged);
            // 
            // rbUseAllCharactersMaxSize
            // 
            this.rbUseAllCharactersMaxSize.AutoSize = true;
            this.rbUseAllCharactersMaxSize.Checked = true;
            this.rbUseAllCharactersMaxSize.Location = new System.Drawing.Point(112, 9);
            this.rbUseAllCharactersMaxSize.Margin = new System.Windows.Forms.Padding(4);
            this.rbUseAllCharactersMaxSize.Name = "rbUseAllCharactersMaxSize";
            this.rbUseAllCharactersMaxSize.Size = new System.Drawing.Size(79, 17);
            this.rbUseAllCharactersMaxSize.TabIndex = 21;
            this.rbUseAllCharactersMaxSize.TabStop = true;
            this.rbUseAllCharactersMaxSize.Text = "All max size";
            this.rbUseAllCharactersMaxSize.UseVisualStyleBackColor = true;
            this.rbUseAllCharactersMaxSize.CheckedChanged += new System.EventHandler(this.rbUseAllCharactersMaxSize_CheckedChanged);
            // 
            // rbManualFontSize
            // 
            this.rbManualFontSize.AutoSize = true;
            this.rbManualFontSize.Location = new System.Drawing.Point(4, 11);
            this.rbManualFontSize.Margin = new System.Windows.Forms.Padding(4);
            this.rbManualFontSize.Name = "rbManualFontSize";
            this.rbManualFontSize.Size = new System.Drawing.Size(14, 13);
            this.rbManualFontSize.TabIndex = 20;
            this.rbManualFontSize.UseVisualStyleBackColor = true;
            this.rbManualFontSize.CheckedChanged += new System.EventHandler(this.rbManualFontSize_CheckedChanged);
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(31, 7);
            this.numFontSize.Margin = new System.Windows.Forms.Padding(4);
            this.numFontSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numFontSize.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(67, 20);
            this.numFontSize.TabIndex = 18;
            this.numFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numFontSize.ValueChanged += new System.EventHandler(this.numFontSize_ValueChanged);
            // 
            // PreviewTest
            // 
            this.PreviewTest.AutoSize = true;
            this.PreviewTest.Checked = true;
            this.PreviewTest.Location = new System.Drawing.Point(425, 440);
            this.PreviewTest.Name = "PreviewTest";
            this.PreviewTest.Size = new System.Drawing.Size(46, 17);
            this.PreviewTest.TabIndex = 26;
            this.PreviewTest.TabStop = true;
            this.PreviewTest.Text = "Test";
            this.PreviewTest.UseVisualStyleBackColor = true;
            this.PreviewTest.CheckedChanged += new System.EventHandler(this.PreviewTest_CheckedChanged);
            // 
            // PreviewBW
            // 
            this.PreviewBW.AutoSize = true;
            this.PreviewBW.Location = new System.Drawing.Point(492, 440);
            this.PreviewBW.Name = "PreviewBW";
            this.PreviewBW.Size = new System.Drawing.Size(48, 17);
            this.PreviewBW.TabIndex = 27;
            this.PreviewBW.Text = "B/W";
            this.PreviewBW.UseVisualStyleBackColor = true;
            this.PreviewBW.CheckedChanged += new System.EventHandler(this.PreviewTest_CheckedChanged);
            // 
            // PreviewWB
            // 
            this.PreviewWB.AutoSize = true;
            this.PreviewWB.Location = new System.Drawing.Point(561, 440);
            this.PreviewWB.Name = "PreviewWB";
            this.PreviewWB.Size = new System.Drawing.Size(48, 17);
            this.PreviewWB.TabIndex = 28;
            this.PreviewWB.Text = "W/B";
            this.PreviewWB.UseVisualStyleBackColor = true;
            this.PreviewWB.CheckedChanged += new System.EventHandler(this.PreviewTest_CheckedChanged);
            // 
            // lstFonts
            // 
            this.lstFonts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstFonts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.lstFonts.FontPreviewSize = 16;
            this.lstFonts.FormattingEnabled = true;
            this.lstFonts.IntegralHeight = false;
            this.lstFonts.Items.AddRange(new object[] {
            "[No name]",
            "Agency FB",
            "Algerian",
            "AR BERKLEY",
            "AR BLANCA",
            "AR BONNIE",
            "AR CARTER",
            "AR CENA",
            "AR CHRISTY",
            "AR DARLING",
            "AR DECODE",
            "AR DELANEY",
            "AR DESTINE",
            "AR ESSENCE",
            "AR HERMANN",
            "AR JULIAN",
            "Arial",
            "Arial Black",
            "Arial Narrow",
            "Arial Rounded MT Bold",
            "Arial Unicode MS",
            "Bahnschrift",
            "Bahnschrift Condensed",
            "Bahnschrift Light",
            "Bahnschrift Light Condensed",
            "Bahnschrift Light SemiCondensed",
            "Bahnschrift SemiBold",
            "Bahnschrift SemiBold Condensed",
            "Bahnschrift SemiCondensed",
            "Bahnschrift SemiLight",
            "Bahnschrift SemiLight Condensed",
            "Baskerville Old Face",
            "Bauhaus 93",
            "Bell MT",
            "Berlin Sans FB",
            "Berlin Sans FB Demi",
            "Bernard MT Condensed",
            "Blackadder ITC",
            "Bodoni MT",
            "Bodoni MT Black",
            "Bodoni MT Condensed",
            "Bodoni MT Poster Compressed",
            "Book Antiqua",
            "Bookman Old Style",
            "Bookshelf Symbol 7",
            "Bradley Hand ITC",
            "Britannic Bold",
            "Broadway",
            "Brush Script MT",
            "Calibri",
            "Calibri Light",
            "Californian FB",
            "Calisto MT",
            "Cambria",
            "Cambria Math",
            "Candara",
            "Candara Light",
            "Castellar",
            "Centaur",
            "Century",
            "Century Gothic",
            "Century Schoolbook",
            "Chiller",
            "Colonna MT",
            "Comic Sans MS",
            "Consolas",
            "Constantia",
            "Cooper Black",
            "Copperplate Gothic Bold",
            "Copperplate Gothic Light",
            "Corbel",
            "Corbel Light",
            "Courier New",
            "Curlz MT",
            "Dubai",
            "Dubai Light",
            "Dubai Medium",
            "DYMO Symbols",
            "DYMObvba",
            "Ebrima",
            "Edwardian Script ITC",
            "Elephant",
            "Engravers MT",
            "Eras Bold ITC",
            "Eras Demi ITC",
            "Eras Light ITC",
            "Eras Medium ITC",
            "Erbos Draco 1st NBP",
            "Erbos Draco 1st Open NBP",
            "Felix Titling",
            "Footlight MT Light",
            "Forte",
            "Franklin Gothic Book",
            "Franklin Gothic Demi",
            "Franklin Gothic Demi Cond",
            "Franklin Gothic Heavy",
            "Franklin Gothic Medium",
            "Franklin Gothic Medium Cond",
            "Freestyle Script",
            "French Script MT",
            "Gabriola",
            "Gadugi",
            "Garamond",
            "Georgia",
            "Gigi",
            "Gill Sans MT",
            "Gill Sans MT Condensed",
            "Gill Sans MT Ext Condensed Bold",
            "Gill Sans Ultra Bold",
            "Gill Sans Ultra Bold Condensed",
            "Gloucester MT Extra Condensed",
            "Goudy Old Style",
            "Goudy Stout",
            "Haettenschweiler",
            "Harlow Solid Italic",
            "Harrington",
            "High Tower Text",
            "HoloLens MDL2 Assets",
            "icomoon",
            "Impact",
            "Imprint MT Shadow",
            "Informal Roman",
            "Ink Free",
            "Javanese Text",
            "Jokerman",
            "Juice ITC",
            "Kristen ITC",
            "Kunstler Script",
            "Leelawadee UI",
            "Leelawadee UI Semilight",
            "Lucida Bright",
            "Lucida Calligraphy",
            "Lucida Console",
            "Lucida Fax",
            "Lucida Handwriting",
            "Lucida Sans",
            "Lucida Sans Typewriter",
            "Lucida Sans Unicode",
            "Magneto",
            "Maiandra GD",
            "Malgun Gothic",
            "Malgun Gothic Semilight",
            "Marlett",
            "Matura MT Script Capitals",
            "Microsoft Himalaya",
            "Microsoft JhengHei",
            "Microsoft JhengHei Light",
            "Microsoft JhengHei UI",
            "Microsoft JhengHei UI Light",
            "Microsoft New Tai Lue",
            "Microsoft PhagsPa",
            "Microsoft Sans Serif",
            "Microsoft Tai Le",
            "Microsoft YaHei",
            "Microsoft YaHei Light",
            "Microsoft YaHei UI",
            "Microsoft YaHei UI Light",
            "Microsoft Yi Baiti",
            "MingLiU_HKSCS-ExtB",
            "MingLiU-ExtB",
            "Mistral",
            "Modern No. 20",
            "Mongolian Baiti",
            "Monotype Corsiva",
            "Montserrat",
            "MS Gothic",
            "MS Outlook",
            "MS PGothic",
            "MS Reference Sans Serif",
            "MS Reference Specialty",
            "MS UI Gothic",
            "MT Extra",
            "MV Boli",
            "Myanmar Text",
            "Niagara Engraved",
            "Niagara Solid",
            "Nirmala UI",
            "Nirmala UI Semilight",
            "NSimSun",
            "OCR A Extended",
            "Old English Text MT",
            "Onyx",
            "Palace Script MT",
            "Palatino Linotype",
            "Papyrus",
            "Parchment",
            "Perpetua",
            "Perpetua Titling MT",
            "Playbill",
            "PMingLiU-ExtB",
            "Poor Richard",
            "Pristina",
            "Rage Italic",
            "Ravie",
            "Rockwell",
            "Rockwell Condensed",
            "Rockwell Extra Bold",
            "Script MT Bold",
            "Segoe MDL2 Assets",
            "Segoe Print",
            "Segoe Script",
            "Segoe UI",
            "Segoe UI Black",
            "Segoe UI Emoji",
            "Segoe UI Historic",
            "Segoe UI Light",
            "Segoe UI Semibold",
            "Segoe UI Semilight",
            "Segoe UI Symbol",
            "Showcard Gothic",
            "SimSun",
            "SimSun-ExtB",
            "Sitka Banner",
            "Sitka Display",
            "Sitka Heading",
            "Sitka Small",
            "Sitka Subheading",
            "Sitka Text",
            "Snap ITC",
            "Stencil",
            "Sylfaen",
            "Symbol",
            "Tahoma",
            "Tempus Sans ITC",
            "Times New Roman",
            "Trebuchet MS",
            "Tw Cen MT",
            "Tw Cen MT Condensed",
            "Tw Cen MT Condensed Extra Bold",
            "Verdana",
            "Viner Hand ITC",
            "Vivaldi",
            "Vladimir Script",
            "Webdings",
            "Wide Latin",
            "Wingdings",
            "Wingdings 2",
            "Wingdings 3",
            "Yu Gothic",
            "Yu Gothic Light",
            "Yu Gothic Medium",
            "Yu Gothic UI",
            "Yu Gothic UI Light",
            "Yu Gothic UI Semibold",
            "Yu Gothic UI Semilight",
            "[No name]",
            "Agency FB",
            "Algerian",
            "Arial",
            "Arial Black",
            "Arial Narrow",
            "Arial Rounded MT Bold",
            "Bahnschrift",
            "Bahnschrift Condensed",
            "Bahnschrift Light",
            "Bahnschrift Light Condensed",
            "Bahnschrift Light SemiCondensed",
            "Bahnschrift SemiBold",
            "Bahnschrift SemiBold Condensed",
            "Bahnschrift SemiCondensed",
            "Bahnschrift SemiLight",
            "Bahnschrift SemiLight Condensed",
            "Baskerville Old Face",
            "Bauhaus 93",
            "Bell MT",
            "Berlin Sans FB",
            "Berlin Sans FB Demi",
            "Bernard MT Condensed",
            "Blackadder ITC",
            "Bodoni MT",
            "Bodoni MT Black",
            "Bodoni MT Condensed",
            "Bodoni MT Poster Compressed",
            "Book Antiqua",
            "Bookman Old Style",
            "Bookshelf Symbol 7",
            "Bradley Hand ITC",
            "Britannic Bold",
            "Broadway",
            "Brush Script MT",
            "Calibri",
            "Calibri Light",
            "Californian FB",
            "Calisto MT",
            "Cambria",
            "Cambria Math",
            "Candara",
            "Castellar",
            "Centaur",
            "Century",
            "Century Gothic",
            "Century Schoolbook",
            "Chiller",
            "Colonna MT",
            "Comic Sans MS",
            "Consolas",
            "Constantia",
            "Cooper Black",
            "Copperplate Gothic Bold",
            "Copperplate Gothic Light",
            "Corbel",
            "Courier New",
            "Curlz MT",
            "Dubai",
            "Dubai Light",
            "Dubai Medium",
            "Ebrima",
            "Edwardian Script ITC",
            "Elephant",
            "Engravers MT",
            "Eras Bold ITC",
            "Eras Demi ITC",
            "Eras Light ITC",
            "Eras Medium ITC",
            "Felix Titling",
            "fontello",
            "Footlight MT Light",
            "Forte",
            "Franklin Gothic Book",
            "Franklin Gothic Demi",
            "Franklin Gothic Demi Cond",
            "Franklin Gothic Heavy",
            "Franklin Gothic Medium",
            "Franklin Gothic Medium Cond",
            "Freestyle Script",
            "French Script MT",
            "Gabriola",
            "Gadugi",
            "Garamond",
            "Georgia",
            "Gigi",
            "Gill Sans MT",
            "Gill Sans MT Condensed",
            "Gill Sans MT Ext Condensed Bold",
            "Gill Sans Ultra Bold",
            "Gill Sans Ultra Bold Condensed",
            "Gloucester MT Extra Condensed",
            "GLYPHICONS Halflings",
            "Goudy Old Style",
            "Goudy Stout",
            "Haettenschweiler",
            "Harlow Solid Italic",
            "Harrington",
            "High Tower Text",
            "HoloLens MDL2 Assets",
            "icon-brand",
            "icon-large",
            "icon-small",
            "icon-ui",
            "Impact",
            "Imprint MT Shadow",
            "Informal Roman",
            "Ink Free",
            "Javanese Text",
            "Jokerman",
            "Juice ITC",
            "Kristen ITC",
            "Kunstler Script",
            "Leelawadee",
            "Leelawadee UI",
            "Leelawadee UI Semilight",
            "Lucida Bright",
            "Lucida Calligraphy",
            "Lucida Console",
            "Lucida Fax",
            "Lucida Handwriting",
            "Lucida Sans",
            "Lucida Sans Typewriter",
            "Lucida Sans Unicode",
            "Magneto",
            "Maiandra GD",
            "Malgun Gothic",
            "Malgun Gothic Semilight",
            "Marlett",
            "Matura MT Script Capitals",
            "Microsoft Himalaya",
            "Microsoft JhengHei",
            "Microsoft JhengHei Light",
            "Microsoft JhengHei UI",
            "Microsoft JhengHei UI Light",
            "Microsoft New Tai Lue",
            "Microsoft PhagsPa",
            "Microsoft Sans Serif",
            "Microsoft Tai Le",
            "Microsoft Uighur",
            "Microsoft YaHei",
            "Microsoft YaHei Light",
            "Microsoft YaHei UI",
            "Microsoft YaHei UI Light",
            "Microsoft Yi Baiti",
            "MingLiU_HKSCS-ExtB",
            "MingLiU-ExtB",
            "Mistral",
            "Modern No. 20",
            "Mongolian Baiti",
            "Monotype Corsiva",
            "MS Gothic",
            "MS Outlook",
            "MS PGothic",
            "MS Reference Sans Serif",
            "MS Reference Specialty",
            "MS UI Gothic",
            "MT Extra",
            "MV Boli",
            "Myanmar Text",
            "Niagara Engraved",
            "Niagara Solid",
            "Nirmala UI",
            "Nirmala UI Semilight",
            "NSimSun",
            "OCR A Extended",
            "Old English Text MT",
            "Onyx",
            "Palace Script MT",
            "Palatino Linotype",
            "Papyrus",
            "Parchment",
            "Perpetua",
            "Perpetua Titling MT",
            "Playbill",
            "PMingLiU-ExtB",
            "Poor Richard",
            "Pristina",
            "Rage Italic",
            "Ravie",
            "Roboto",
            "Rockwell",
            "Rockwell Condensed",
            "Rockwell Extra Bold",
            "Script MT Bold",
            "Segoe MDL2 Assets",
            "Segoe Print",
            "Segoe Script",
            "Segoe UI",
            "Segoe UI Black",
            "Segoe UI Emoji",
            "Segoe UI Historic",
            "Segoe UI Light",
            "Segoe UI Semibold",
            "Segoe UI Semilight",
            "Segoe UI Symbol",
            "Showcard Gothic",
            "SimSun",
            "SimSun-ExtB",
            "Sitka Banner",
            "Sitka Display",
            "Sitka Heading",
            "Sitka Small",
            "Sitka Subheading",
            "Sitka Text",
            "Snap ITC",
            "Stencil",
            "Sylfaen",
            "Symbol",
            "Tahoma",
            "Tempus Sans ITC",
            "Times New Roman",
            "Trebuchet MS",
            "Tw Cen MT",
            "Tw Cen MT Condensed",
            "Tw Cen MT Condensed Extra Bold",
            "Verdana",
            "Viner Hand ITC",
            "Vivaldi",
            "Vladimir Script",
            "Webdings",
            "Wide Latin",
            "Wingdings",
            "Wingdings 2",
            "Wingdings 3",
            "Yu Gothic",
            "Yu Gothic Light",
            "Yu Gothic Medium",
            "Yu Gothic UI",
            "Yu Gothic UI Light",
            "Yu Gothic UI Semibold",
            "Yu Gothic UI Semilight",
            "[No name]",
            "Agency FB",
            "Algerian",
            "Arial",
            "Arial Black",
            "Arial Narrow",
            "Arial Nova",
            "Arial Nova Cond",
            "Arial Nova Cond Light",
            "Arial Nova Light",
            "Arial Rounded MT Bold",
            "Bahnschrift",
            "Bahnschrift Condensed",
            "Bahnschrift Light",
            "Bahnschrift Light Condensed",
            "Bahnschrift Light SemiCondensed",
            "Bahnschrift SemiBold",
            "Bahnschrift SemiBold Condensed",
            "Bahnschrift SemiCondensed",
            "Bahnschrift SemiLight",
            "Bahnschrift SemiLight Condensed",
            "Baskerville Old Face",
            "Bauhaus 93",
            "Bell MT",
            "Berlin Sans FB",
            "Berlin Sans FB Demi",
            "Bernard MT Condensed",
            "Blackadder ITC",
            "Bodoni MT",
            "Bodoni MT Black",
            "Bodoni MT Condensed",
            "Bodoni MT Poster Compressed",
            "Book Antiqua",
            "Bookman Old Style",
            "Bookshelf Symbol 7",
            "Bradley Hand ITC",
            "Britannic Bold",
            "Broadway",
            "Brush Script MT",
            "Calibri",
            "Calibri Light",
            "Californian FB",
            "Calisto MT",
            "Cambria",
            "Cambria Math",
            "Candara",
            "Castellar",
            "Centaur",
            "Century",
            "Century Gothic",
            "Century Schoolbook",
            "Chiller",
            "Colonna MT",
            "Comic Sans MS",
            "Consolas",
            "Constantia",
            "Cooper Black",
            "Copperplate Gothic Bold",
            "Copperplate Gothic Light",
            "Corbel",
            "Courier New",
            "Curlz MT",
            "Dubai",
            "Dubai Light",
            "Dubai Medium",
            "Ebrima",
            "Edwardian Script ITC",
            "Elephant",
            "Engravers MT",
            "Eras Bold ITC",
            "Eras Demi ITC",
            "Eras Light ITC",
            "Eras Medium ITC",
            "Felix Titling",
            "Footlight MT Light",
            "Forte",
            "Franklin Gothic Book",
            "Franklin Gothic Demi",
            "Franklin Gothic Demi Cond",
            "Franklin Gothic Heavy",
            "Franklin Gothic Medium",
            "Franklin Gothic Medium Cond",
            "Freestyle Script",
            "French Script MT",
            "Gabriola",
            "Gadugi",
            "Garamond",
            "Georgia",
            "Georgia Pro",
            "Georgia Pro Black",
            "Georgia Pro Cond",
            "Georgia Pro Cond Black",
            "Georgia Pro Cond Light",
            "Georgia Pro Cond Semibold",
            "Georgia Pro Light",
            "Georgia Pro Semibold",
            "Gigi",
            "Gill Sans MT",
            "Gill Sans MT Condensed",
            "Gill Sans MT Ext Condensed Bold",
            "Gill Sans Nova",
            "Gill Sans Nova Cond",
            "Gill Sans Nova Cond Lt",
            "Gill Sans Nova Cond Ultra Bold",
            "Gill Sans Nova Cond XBd",
            "Gill Sans Nova Light",
            "Gill Sans Nova Ultra Bold",
            "Gill Sans Ultra Bold",
            "Gill Sans Ultra Bold Condensed",
            "Gloucester MT Extra Condensed",
            "Goudy Old Style",
            "Goudy Stout",
            "Haettenschweiler",
            "Harlow Solid Italic",
            "Harrington",
            "HelvLight",
            "High Tower Text",
            "HoloLens MDL2 Assets",
            "Impact",
            "Imprint MT Shadow",
            "Informal Roman",
            "Ink Free",
            "Javanese Text",
            "Jokerman",
            "Juice ITC",
            "Kristen ITC",
            "Kunstler Script",
            "Lato",
            "Leelawadee",
            "Leelawadee UI",
            "Leelawadee UI Semilight",
            "Lucida Bright",
            "Lucida Calligraphy",
            "Lucida Console",
            "Lucida Fax",
            "Lucida Handwriting",
            "Lucida Sans",
            "Lucida Sans Typewriter",
            "Lucida Sans Unicode",
            "Magneto",
            "Maiandra GD",
            "Malgun Gothic",
            "Malgun Gothic Semilight",
            "Marlett",
            "Matura MT Script Capitals",
            "Microsoft Himalaya",
            "Microsoft JhengHei",
            "Microsoft JhengHei Light",
            "Microsoft JhengHei UI",
            "Microsoft JhengHei UI Light",
            "Microsoft New Tai Lue",
            "Microsoft PhagsPa",
            "Microsoft Sans Serif",
            "Microsoft Tai Le",
            "Microsoft Uighur",
            "Microsoft YaHei",
            "Microsoft YaHei Light",
            "Microsoft YaHei UI",
            "Microsoft YaHei UI Light",
            "Microsoft Yi Baiti",
            "MingLiU_HKSCS-ExtB",
            "MingLiU-ExtB",
            "Mistral",
            "Modern No. 20",
            "Mongolian Baiti",
            "Monotype Corsiva",
            "MS Gothic",
            "MS Outlook",
            "MS PGothic",
            "MS Reference Sans Serif",
            "MS Reference Specialty",
            "MS UI Gothic",
            "MT Extra",
            "MV Boli",
            "Myanmar Text",
            "Neue Haas Grotesk Text Pro",
            "Niagara Engraved",
            "Niagara Solid",
            "Nirmala UI",
            "Nirmala UI Semilight",
            "NSimSun",
            "OCR A Extended",
            "Old English Text MT",
            "Onyx",
            "Palace Script MT",
            "Palatino Linotype",
            "Papyrus",
            "Parchment",
            "Perpetua",
            "Perpetua Titling MT",
            "Playbill",
            "PMingLiU-ExtB",
            "Poor Richard",
            "Pristina",
            "Rage Italic",
            "Ravie",
            "Rockwell",
            "Rockwell Condensed",
            "Rockwell Extra Bold",
            "Rockwell Nova",
            "Rockwell Nova Cond",
            "Rockwell Nova Cond Light",
            "Rockwell Nova Extra Bold",
            "Rockwell Nova Light",
            "Script MT Bold",
            "Segoe MDL2 Assets",
            "Segoe Print",
            "Segoe Script",
            "Segoe UI",
            "Segoe UI Black",
            "Segoe UI Emoji",
            "Segoe UI Historic",
            "Segoe UI Light",
            "Segoe UI Semibold",
            "Segoe UI Semilight",
            "Segoe UI Symbol",
            "Showcard Gothic",
            "SimSun",
            "SimSun-ExtB",
            "Sitka Banner",
            "Sitka Display",
            "Sitka Heading",
            "Sitka Small",
            "Sitka Subheading",
            "Sitka Text",
            "Snap ITC",
            "Stencil",
            "Sylfaen",
            "Symbol",
            "Tahoma",
            "Tempus Sans ITC",
            "Times New Roman",
            "Trebuchet MS",
            "Tw Cen MT",
            "Tw Cen MT Condensed",
            "Tw Cen MT Condensed Extra Bold",
            "Webdings",
            "Verdana",
            "Verdana Pro",
            "Verdana Pro Black",
            "Verdana Pro Cond",
            "Verdana Pro Cond Black",
            "Verdana Pro Cond Light",
            "Verdana Pro Cond SemiBold",
            "Verdana Pro Light",
            "Verdana Pro SemiBold",
            "Wide Latin",
            "Viner Hand ITC",
            "Wingdings",
            "Wingdings 2",
            "Wingdings 3",
            "Vivaldi",
            "Vladimir Script",
            "Yu Gothic",
            "Yu Gothic Light",
            "Yu Gothic Medium",
            "Yu Gothic UI",
            "Yu Gothic UI Light",
            "Yu Gothic UI Semibold",
            "Yu Gothic UI Semilight",
            "[No name]",
            "Agency FB",
            "Algerian",
            "Arial",
            "Arial Black",
            "Arial Narrow",
            "Arial Nova",
            "Arial Nova Cond",
            "Arial Nova Cond Light",
            "Arial Nova Light",
            "Arial Rounded MT Bold",
            "Bahnschrift",
            "Bahnschrift Condensed",
            "Bahnschrift Light",
            "Bahnschrift Light Condensed",
            "Bahnschrift Light SemiCondensed",
            "Bahnschrift SemiBold",
            "Bahnschrift SemiBold Condensed",
            "Bahnschrift SemiCondensed",
            "Bahnschrift SemiLight",
            "Bahnschrift SemiLight Condensed",
            "Baskerville Old Face",
            "Bauhaus 93",
            "Bell MT",
            "Berlin Sans FB",
            "Berlin Sans FB Demi",
            "Bernard MT Condensed",
            "Blackadder ITC",
            "Bodoni MT",
            "Bodoni MT Black",
            "Bodoni MT Condensed",
            "Bodoni MT Poster Compressed",
            "Book Antiqua",
            "Bookman Old Style",
            "Bookshelf Symbol 7",
            "Bradley Hand ITC",
            "Britannic Bold",
            "Broadway",
            "Brush Script MT",
            "Calibri",
            "Calibri Light",
            "Californian FB",
            "Calisto MT",
            "Cambria",
            "Cambria Math",
            "Candara",
            "Castellar",
            "Centaur",
            "Century",
            "Century Gothic",
            "Century Schoolbook",
            "Chiller",
            "Colonna MT",
            "Comic Sans MS",
            "Consolas",
            "Constantia",
            "Cooper Black",
            "Copperplate Gothic Bold",
            "Copperplate Gothic Light",
            "Corbel",
            "Courier New",
            "Curlz MT",
            "Dubai",
            "Dubai Light",
            "Dubai Medium",
            "Ebrima",
            "Edwardian Script ITC",
            "Elephant",
            "Engravers MT",
            "Eras Bold ITC",
            "Eras Demi ITC",
            "Eras Light ITC",
            "Eras Medium ITC",
            "Felix Titling",
            "Footlight MT Light",
            "Forte",
            "Franklin Gothic Book",
            "Franklin Gothic Demi",
            "Franklin Gothic Demi Cond",
            "Franklin Gothic Heavy",
            "Franklin Gothic Medium",
            "Franklin Gothic Medium Cond",
            "Freestyle Script",
            "French Script MT",
            "Gabriola",
            "Gadugi",
            "Garamond",
            "Georgia",
            "Georgia Pro",
            "Georgia Pro Black",
            "Georgia Pro Cond",
            "Georgia Pro Cond Black",
            "Georgia Pro Cond Light",
            "Georgia Pro Cond Semibold",
            "Georgia Pro Light",
            "Georgia Pro Semibold",
            "Gigi",
            "Gill Sans MT",
            "Gill Sans MT Condensed",
            "Gill Sans MT Ext Condensed Bold",
            "Gill Sans Nova",
            "Gill Sans Nova Cond",
            "Gill Sans Nova Cond Lt",
            "Gill Sans Nova Cond Ultra Bold",
            "Gill Sans Nova Cond XBd",
            "Gill Sans Nova Light",
            "Gill Sans Nova Ultra Bold",
            "Gill Sans Ultra Bold",
            "Gill Sans Ultra Bold Condensed",
            "Gloucester MT Extra Condensed",
            "Goudy Old Style",
            "Goudy Stout",
            "Haettenschweiler",
            "Harlow Solid Italic",
            "Harrington",
            "HelvLight",
            "High Tower Text",
            "HoloLens MDL2 Assets",
            "Impact",
            "Imprint MT Shadow",
            "Informal Roman",
            "Ink Free",
            "Javanese Text",
            "Jokerman",
            "Juice ITC",
            "Kristen ITC",
            "Kunstler Script",
            "Lato",
            "Leelawadee",
            "Leelawadee UI",
            "Leelawadee UI Semilight",
            "Lucida Bright",
            "Lucida Calligraphy",
            "Lucida Console",
            "Lucida Fax",
            "Lucida Handwriting",
            "Lucida Sans",
            "Lucida Sans Typewriter",
            "Lucida Sans Unicode",
            "Magneto",
            "Maiandra GD",
            "Malgun Gothic",
            "Malgun Gothic Semilight",
            "Marlett",
            "Matura MT Script Capitals",
            "Microsoft Himalaya",
            "Microsoft JhengHei",
            "Microsoft JhengHei Light",
            "Microsoft JhengHei UI",
            "Microsoft JhengHei UI Light",
            "Microsoft New Tai Lue",
            "Microsoft PhagsPa",
            "Microsoft Sans Serif",
            "Microsoft Tai Le",
            "Microsoft Uighur",
            "Microsoft YaHei",
            "Microsoft YaHei Light",
            "Microsoft YaHei UI",
            "Microsoft YaHei UI Light",
            "Microsoft Yi Baiti",
            "MingLiU_HKSCS-ExtB",
            "MingLiU-ExtB",
            "Mistral",
            "Modern No. 20",
            "Mongolian Baiti",
            "Monotype Corsiva",
            "MS Gothic",
            "MS Outlook",
            "MS PGothic",
            "MS Reference Sans Serif",
            "MS Reference Specialty",
            "MS UI Gothic",
            "MT Extra",
            "MV Boli",
            "Myanmar Text",
            "Neue Haas Grotesk Text Pro",
            "Niagara Engraved",
            "Niagara Solid",
            "Nirmala UI",
            "Nirmala UI Semilight",
            "NSimSun",
            "OCR A Extended",
            "Old English Text MT",
            "Onyx",
            "Palace Script MT",
            "Palatino Linotype",
            "Papyrus",
            "Parchment",
            "Perpetua",
            "Perpetua Titling MT",
            "Playbill",
            "PMingLiU-ExtB",
            "Poor Richard",
            "Pristina",
            "Rage Italic",
            "Ravie",
            "Rockwell",
            "Rockwell Condensed",
            "Rockwell Extra Bold",
            "Rockwell Nova",
            "Rockwell Nova Cond",
            "Rockwell Nova Cond Light",
            "Rockwell Nova Extra Bold",
            "Rockwell Nova Light",
            "Script MT Bold",
            "Segoe MDL2 Assets",
            "Segoe Print",
            "Segoe Script",
            "Segoe UI",
            "Segoe UI Black",
            "Segoe UI Emoji",
            "Segoe UI Historic",
            "Segoe UI Light",
            "Segoe UI Semibold",
            "Segoe UI Semilight",
            "Segoe UI Symbol",
            "Showcard Gothic",
            "SimSun",
            "SimSun-ExtB",
            "Sitka Banner",
            "Sitka Display",
            "Sitka Heading",
            "Sitka Small",
            "Sitka Subheading",
            "Sitka Text",
            "Snap ITC",
            "Stencil",
            "Sylfaen",
            "Symbol",
            "Tahoma",
            "Tempus Sans ITC",
            "Times New Roman",
            "Trebuchet MS",
            "Tw Cen MT",
            "Tw Cen MT Condensed",
            "Tw Cen MT Condensed Extra Bold",
            "Webdings",
            "Verdana",
            "Verdana Pro",
            "Verdana Pro Black",
            "Verdana Pro Cond",
            "Verdana Pro Cond Black",
            "Verdana Pro Cond Light",
            "Verdana Pro Cond SemiBold",
            "Verdana Pro Light",
            "Verdana Pro SemiBold",
            "Wide Latin",
            "Viner Hand ITC",
            "Wingdings",
            "Wingdings 2",
            "Wingdings 3",
            "Vivaldi",
            "Vladimir Script",
            "Yu Gothic",
            "Yu Gothic Light",
            "Yu Gothic Medium",
            "Yu Gothic UI",
            "Yu Gothic UI Light",
            "Yu Gothic UI Semibold",
            "Yu Gothic UI Semilight",
            "[No name]",
            "Agency FB",
            "Algerian",
            "Arial",
            "Arial Black",
            "Arial Narrow",
            "Arial Nova",
            "Arial Nova Cond",
            "Arial Nova Cond Light",
            "Arial Nova Light",
            "Arial Rounded MT Bold",
            "Bahnschrift",
            "Bahnschrift Condensed",
            "Bahnschrift Light",
            "Bahnschrift Light Condensed",
            "Bahnschrift Light SemiCondensed",
            "Bahnschrift SemiBold",
            "Bahnschrift SemiBold Condensed",
            "Bahnschrift SemiCondensed",
            "Bahnschrift SemiLight",
            "Bahnschrift SemiLight Condensed",
            "Baskerville Old Face",
            "Bauhaus 93",
            "Bell MT",
            "Berlin Sans FB",
            "Berlin Sans FB Demi",
            "Bernard MT Condensed",
            "Blackadder ITC",
            "Bodoni MT",
            "Bodoni MT Black",
            "Bodoni MT Condensed",
            "Bodoni MT Poster Compressed",
            "Book Antiqua",
            "Bookman Old Style",
            "Bookshelf Symbol 7",
            "Bradley Hand ITC",
            "Britannic Bold",
            "Broadway",
            "Brush Script MT",
            "Calibri",
            "Calibri Light",
            "Californian FB",
            "Calisto MT",
            "Cambria",
            "Cambria Math",
            "Candara",
            "Castellar",
            "Centaur",
            "Century",
            "Century Gothic",
            "Century Schoolbook",
            "Chiller",
            "Colonna MT",
            "Comic Sans MS",
            "Consolas",
            "Constantia",
            "Cooper Black",
            "Copperplate Gothic Bold",
            "Copperplate Gothic Light",
            "Corbel",
            "Courier New",
            "Curlz MT",
            "Dubai",
            "Dubai Light",
            "Dubai Medium",
            "Ebrima",
            "Edwardian Script ITC",
            "Elephant",
            "Engravers MT",
            "Eras Bold ITC",
            "Eras Demi ITC",
            "Eras Light ITC",
            "Eras Medium ITC",
            "Felix Titling",
            "Footlight MT Light",
            "Forte",
            "Franklin Gothic Book",
            "Franklin Gothic Demi",
            "Franklin Gothic Demi Cond",
            "Franklin Gothic Heavy",
            "Franklin Gothic Medium",
            "Franklin Gothic Medium Cond",
            "Freestyle Script",
            "French Script MT",
            "Gabriola",
            "Gadugi",
            "Garamond",
            "Georgia",
            "Georgia Pro",
            "Georgia Pro Black",
            "Georgia Pro Cond",
            "Georgia Pro Cond Black",
            "Georgia Pro Cond Light",
            "Georgia Pro Cond Semibold",
            "Georgia Pro Light",
            "Georgia Pro Semibold",
            "Gigi",
            "Gill Sans MT",
            "Gill Sans MT Condensed",
            "Gill Sans MT Ext Condensed Bold",
            "Gill Sans Nova",
            "Gill Sans Nova Cond",
            "Gill Sans Nova Cond Lt",
            "Gill Sans Nova Cond Ultra Bold",
            "Gill Sans Nova Cond XBd",
            "Gill Sans Nova Light",
            "Gill Sans Nova Ultra Bold",
            "Gill Sans Ultra Bold",
            "Gill Sans Ultra Bold Condensed",
            "Gloucester MT Extra Condensed",
            "Goudy Old Style",
            "Goudy Stout",
            "Haettenschweiler",
            "Harlow Solid Italic",
            "Harrington",
            "HelvLight",
            "High Tower Text",
            "HoloLens MDL2 Assets",
            "Impact",
            "Imprint MT Shadow",
            "Informal Roman",
            "Ink Free",
            "Javanese Text",
            "Jokerman",
            "Juice ITC",
            "Kristen ITC",
            "Kunstler Script",
            "Lato",
            "Leelawadee",
            "Leelawadee UI",
            "Leelawadee UI Semilight",
            "Lucida Bright",
            "Lucida Calligraphy",
            "Lucida Console",
            "Lucida Fax",
            "Lucida Handwriting",
            "Lucida Sans",
            "Lucida Sans Typewriter",
            "Lucida Sans Unicode",
            "Magneto",
            "Maiandra GD",
            "Malgun Gothic",
            "Malgun Gothic Semilight",
            "Marlett",
            "Matura MT Script Capitals",
            "Microsoft Himalaya",
            "Microsoft JhengHei",
            "Microsoft JhengHei Light",
            "Microsoft JhengHei UI",
            "Microsoft JhengHei UI Light",
            "Microsoft New Tai Lue",
            "Microsoft PhagsPa",
            "Microsoft Sans Serif",
            "Microsoft Tai Le",
            "Microsoft Uighur",
            "Microsoft YaHei",
            "Microsoft YaHei Light",
            "Microsoft YaHei UI",
            "Microsoft YaHei UI Light",
            "Microsoft Yi Baiti",
            "MingLiU_HKSCS-ExtB",
            "MingLiU-ExtB",
            "Mistral",
            "Modern No. 20",
            "Mongolian Baiti",
            "Monotype Corsiva",
            "MS Gothic",
            "MS Outlook",
            "MS PGothic",
            "MS Reference Sans Serif",
            "MS Reference Specialty",
            "MS UI Gothic",
            "MT Extra",
            "MV Boli",
            "Myanmar Text",
            "Neue Haas Grotesk Text Pro",
            "Niagara Engraved",
            "Niagara Solid",
            "Nirmala UI",
            "Nirmala UI Semilight",
            "NSimSun",
            "OCR A Extended",
            "Old English Text MT",
            "Onyx",
            "Palace Script MT",
            "Palatino Linotype",
            "Papyrus",
            "Parchment",
            "Perpetua",
            "Perpetua Titling MT",
            "Playbill",
            "PMingLiU-ExtB",
            "Poor Richard",
            "Pristina",
            "Rage Italic",
            "Ravie",
            "Rockwell",
            "Rockwell Condensed",
            "Rockwell Extra Bold",
            "Rockwell Nova",
            "Rockwell Nova Cond",
            "Rockwell Nova Cond Light",
            "Rockwell Nova Extra Bold",
            "Rockwell Nova Light",
            "Script MT Bold",
            "Segoe MDL2 Assets",
            "Segoe Print",
            "Segoe Script",
            "Segoe UI",
            "Segoe UI Black",
            "Segoe UI Emoji",
            "Segoe UI Historic",
            "Segoe UI Light",
            "Segoe UI Semibold",
            "Segoe UI Semilight",
            "Segoe UI Symbol",
            "Showcard Gothic",
            "SimSun",
            "SimSun-ExtB",
            "Sitka Banner",
            "Sitka Display",
            "Sitka Heading",
            "Sitka Small",
            "Sitka Subheading",
            "Sitka Text",
            "Snap ITC",
            "Stencil",
            "Sylfaen",
            "Symbol",
            "Tahoma",
            "Tempus Sans ITC",
            "Times New Roman",
            "Trebuchet MS",
            "Tw Cen MT",
            "Tw Cen MT Condensed",
            "Tw Cen MT Condensed Extra Bold",
            "Webdings",
            "Verdana",
            "Verdana Pro",
            "Verdana Pro Black",
            "Verdana Pro Cond",
            "Verdana Pro Cond Black",
            "Verdana Pro Cond Light",
            "Verdana Pro Cond SemiBold",
            "Verdana Pro Light",
            "Verdana Pro SemiBold",
            "Wide Latin",
            "Viner Hand ITC",
            "Wingdings",
            "Wingdings 2",
            "Wingdings 3",
            "Vivaldi",
            "Vladimir Script",
            "Yu Gothic",
            "Yu Gothic Light",
            "Yu Gothic Medium",
            "Yu Gothic UI",
            "Yu Gothic UI Light",
            "Yu Gothic UI Semibold",
            "Yu Gothic UI Semilight"});
            this.lstFonts.Location = new System.Drawing.Point(42, 93);
            this.lstFonts.Name = "lstFonts";
            this.lstFonts.Size = new System.Drawing.Size(338, 254);
            this.lstFonts.TabIndex = 9;
            this.lstFonts.SelectedIndexChanged += new System.EventHandler(this.lstFonts_SelectedIndexChanged);
            // 
            // sldContrast
            // 
            this.sldContrast.LargeChange = 4;
            this.sldContrast.Location = new System.Drawing.Point(599, 142);
            this.sldContrast.Maximum = 12;
            this.sldContrast.Name = "sldContrast";
            this.sldContrast.Size = new System.Drawing.Size(191, 45);
            this.sldContrast.TabIndex = 0;
            this.sldContrast.Value = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(596, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 29;
            this.label1.Text = "Contrast";
            this.label1.Click += new System.EventHandler(this.Label1_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(42, 378);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(295, 212);
            this.treeView1.TabIndex = 30;
            // 
            // FormAddCharacters
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 602);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.sldContrast);
            this.Controls.Add(this.PreviewWB);
            this.Controls.Add(this.PreviewBW);
            this.Controls.Add(this.PreviewTest);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.numDbs);
            this.Controls.Add(this.lblFont);
            this.Controls.Add(this.lblFontSize);
            this.Controls.Add(this.lblCharOffset);
            this.Controls.Add(this.lblCharOffsetY);
            this.Controls.Add(this.lblCharOffsetX);
            this.Controls.Add(this.lblNextionFontSize);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.lstFonts);
            this.Controls.Add(this.numCharOffsetY);
            this.Controls.Add(this.numCharOffsetX);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.cmbNextionFontSize);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddCharacters";
            this.Text = "Add Characters";
            this.Load += new System.EventHandler(this.FormAddCharacters_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDbs)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sldContrast)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ComboBox cmbNextionFontSize;
        private System.Windows.Forms.FlowLayoutPanel panelPreview;
        private System.Windows.Forms.NumericUpDown numCharOffsetX;
        private System.Windows.Forms.NumericUpDown numCharOffsetY;
        private Controls.FontListBox lstFonts;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Label lblNextionFontSize;
        private System.Windows.Forms.Label lblCharOffsetX;
        private System.Windows.Forms.Label lblCharOffsetY;
        private System.Windows.Forms.Label lblCharOffset;
        private System.Windows.Forms.Label lblFontSize;
        private System.Windows.Forms.Label lblFont;
        private System.Windows.Forms.NumericUpDown numDbs;

        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbUseSingleCharacterMaxSize;
        private System.Windows.Forms.RadioButton rbUseAllCharactersMaxSize;
        private System.Windows.Forms.RadioButton rbManualFontSize;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.RadioButton PreviewTest;
        private System.Windows.Forms.RadioButton PreviewBW;
        private System.Windows.Forms.RadioButton PreviewWB;
        private System.Windows.Forms.TrackBar sldContrast;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeView1;
    }
}