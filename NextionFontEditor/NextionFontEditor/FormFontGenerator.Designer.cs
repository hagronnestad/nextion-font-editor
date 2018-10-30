namespace NextionFontEditor {
    partial class FormFontGenerator {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
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
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.rbManualFontSize = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbUseSingleCharacterMaxSize = new System.Windows.Forms.RadioButton();
            this.rbUseAllCharactersMaxSize = new System.Windows.Forms.RadioButton();
            this.numDbs = new System.Windows.Forms.NumericUpDown();
            this.lstFonts = new NextionFontEditor.Controls.FontListBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDbs)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbNextionFontSize
            // 
            this.cmbNextionFontSize.FormattingEnabled = true;
            this.cmbNextionFontSize.Location = new System.Drawing.Point(12, 25);
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
            this.panelPreview.Location = new System.Drawing.Point(356, 25);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(460, 565);
            this.panelPreview.TabIndex = 5;
            // 
            // numCharOffsetX
            // 
            this.numCharOffsetX.Location = new System.Drawing.Point(32, 165);
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
            this.numCharOffsetY.Location = new System.Drawing.Point(32, 191);
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
            this.lblPreview.Location = new System.Drawing.Point(353, 9);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.Size = new System.Drawing.Size(45, 13);
            this.lblPreview.TabIndex = 10;
            this.lblPreview.Text = "Preview";
            // 
            // lblNextionFontSize
            // 
            this.lblNextionFontSize.AutoSize = true;
            this.lblNextionFontSize.Location = new System.Drawing.Point(9, 9);
            this.lblNextionFontSize.Name = "lblNextionFontSize";
            this.lblNextionFontSize.Size = new System.Drawing.Size(90, 13);
            this.lblNextionFontSize.TabIndex = 11;
            this.lblNextionFontSize.Text = "Nextion Font Size";
            // 
            // lblCharOffsetX
            // 
            this.lblCharOffsetX.AutoSize = true;
            this.lblCharOffsetX.Location = new System.Drawing.Point(12, 167);
            this.lblCharOffsetX.Name = "lblCharOffsetX";
            this.lblCharOffsetX.Size = new System.Drawing.Size(14, 13);
            this.lblCharOffsetX.TabIndex = 12;
            this.lblCharOffsetX.Text = "X";
            // 
            // lblCharOffsetY
            // 
            this.lblCharOffsetY.AutoSize = true;
            this.lblCharOffsetY.Location = new System.Drawing.Point(12, 193);
            this.lblCharOffsetY.Name = "lblCharOffsetY";
            this.lblCharOffsetY.Size = new System.Drawing.Size(14, 13);
            this.lblCharOffsetY.TabIndex = 13;
            this.lblCharOffsetY.Text = "Y";
            // 
            // lblCharOffset
            // 
            this.lblCharOffset.AutoSize = true;
            this.lblCharOffset.Location = new System.Drawing.Point(12, 144);
            this.lblCharOffset.Name = "lblCharOffset";
            this.lblCharOffset.Size = new System.Drawing.Size(82, 13);
            this.lblCharOffset.TabIndex = 14;
            this.lblCharOffset.Text = "Character offset";
            // 
            // lblFontSize
            // 
            this.lblFontSize.AutoSize = true;
            this.lblFontSize.Location = new System.Drawing.Point(9, 58);
            this.lblFontSize.Name = "lblFontSize";
            this.lblFontSize.Size = new System.Drawing.Size(51, 13);
            this.lblFontSize.TabIndex = 16;
            this.lblFontSize.Text = "Font Size";
            // 
            // lblFont
            // 
            this.lblFont.AutoSize = true;
            this.lblFont.Location = new System.Drawing.Point(9, 222);
            this.lblFont.Name = "lblFont";
            this.lblFont.Size = new System.Drawing.Size(28, 13);
            this.lblFont.TabIndex = 17;
            this.lblFont.Text = "Font";
            // 
            // numFontSize
            // 
            this.numFontSize.Location = new System.Drawing.Point(23, 6);
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
            this.numFontSize.Size = new System.Drawing.Size(50, 20);
            this.numFontSize.TabIndex = 18;
            this.numFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numFontSize.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numFontSize.ValueChanged += new System.EventHandler(this.numFontSize_ValueChanged);
            // 
            // rbManualFontSize
            // 
            this.rbManualFontSize.AutoSize = true;
            this.rbManualFontSize.Location = new System.Drawing.Point(3, 9);
            this.rbManualFontSize.Name = "rbManualFontSize";
            this.rbManualFontSize.Size = new System.Drawing.Size(14, 13);
            this.rbManualFontSize.TabIndex = 20;
            this.rbManualFontSize.UseVisualStyleBackColor = true;
            this.rbManualFontSize.CheckedChanged += new System.EventHandler(this.rbManualFontSize_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbUseSingleCharacterMaxSize);
            this.panel1.Controls.Add(this.rbUseAllCharactersMaxSize);
            this.panel1.Controls.Add(this.rbManualFontSize);
            this.panel1.Controls.Add(this.numFontSize);
            this.panel1.Location = new System.Drawing.Point(12, 73);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(326, 33);
            this.panel1.TabIndex = 21;
            // 
            // rbUseSingleCharacterMaxSize
            // 
            this.rbUseSingleCharacterMaxSize.AutoSize = true;
            this.rbUseSingleCharacterMaxSize.Location = new System.Drawing.Point(169, 7);
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
            this.rbUseAllCharactersMaxSize.Location = new System.Drawing.Point(84, 7);
            this.rbUseAllCharactersMaxSize.Name = "rbUseAllCharactersMaxSize";
            this.rbUseAllCharactersMaxSize.Size = new System.Drawing.Size(79, 17);
            this.rbUseAllCharactersMaxSize.TabIndex = 21;
            this.rbUseAllCharactersMaxSize.TabStop = true;
            this.rbUseAllCharactersMaxSize.Text = "All max size";
            this.rbUseAllCharactersMaxSize.UseVisualStyleBackColor = true;
            this.rbUseAllCharactersMaxSize.CheckedChanged += new System.EventHandler(this.rbUseAllCharactersMaxSize_CheckedChanged);
            // 
            // numDbs
            // 
            this.numDbs.Location = new System.Drawing.Point(191, 165);
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
            "Castellar",
            "Centaur",
            "Century",
            "Century Gothic",
            "Century Schoolbook",
            "Chiller",
            "Colonna MT",
            "Comfortaa",
            "Comfortaa Light",
            "Comic Sans MS",
            "Consolas",
            "Constantia",
            "Cooper Black",
            "Copperplate Gothic Bold",
            "Copperplate Gothic Light",
            "Corbel",
            "Courier New",
            "Curlz MT",
            "DengXian",
            "Droid Sans",
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
            "Microsoft MHei",
            "Microsoft NeoGothic",
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
            "Orbitron",
            "Orbitron Black",
            "Orbitron Medium",
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
            "Roboto Black",
            "Roboto Condensed",
            "Roboto Condensed Light",
            "Roboto Light",
            "Roboto Medium",
            "Roboto Mono",
            "Roboto Mono Light",
            "Roboto Mono Medium",
            "Roboto Mono Thin",
            "Roboto Slab",
            "Roboto Thin",
            "Rockwell",
            "Rockwell Condensed",
            "Rockwell Extra Bold",
            "ROG Fonts v1.6",
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
            "Segoe WP",
            "Segoe WP Black",
            "Segoe WP Light",
            "Segoe WP Semibold",
            "Segoe WP SemiLight",
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
            "TeamViewer11",
            "Tempus Sans ITC",
            "Times New Roman",
            "Trebuchet MS",
            "Tw Cen MT",
            "Tw Cen MT Condensed",
            "Tw Cen MT Condensed Extra Bold",
            "Ubuntu",
            "Ubuntu Condensed",
            "Ubuntu Light",
            "Ubuntu Mono",
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
            "",
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
            "FontAwesome",
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
            "Museo Sans For Dell",
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
            "Yu Gothic UI Semilight"});
            this.lstFonts.Location = new System.Drawing.Point(12, 238);
            this.lstFonts.Name = "lstFonts";
            this.lstFonts.Size = new System.Drawing.Size(338, 352);
            this.lstFonts.TabIndex = 9;
            this.lstFonts.SelectedIndexChanged += new System.EventHandler(this.lstFonts_SelectedIndexChanged);
            // 
            // FormFontGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(828, 602);
            this.Controls.Add(this.numDbs);
            this.Controls.Add(this.panel1);
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
            this.Name = "FormFontGenerator";
            this.Text = "Font Generator";
            this.Load += new System.EventHandler(this.FormFontGenerator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCharOffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDbs)).EndInit();
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
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.RadioButton rbManualFontSize;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rbUseSingleCharacterMaxSize;
        private System.Windows.Forms.RadioButton rbUseAllCharactersMaxSize;
        private System.Windows.Forms.NumericUpDown numDbs;
    }
}