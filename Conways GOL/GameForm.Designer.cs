using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;

namespace Conways_GOL
{
    public class BufferedPanel : Panel
    {
        public BufferedPanel()
        {
            this.DoubleBuffered = true;
        }
    }
    partial class GameForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            panelField = new BufferedPanel();
            panelFigure = new BufferedPanel();
            buttonSaveField = new Button();
            buttonLoadField = new Button();
            buttonPause = new Button();
            buttonSaveFig = new Button();
            buttonLoadFig = new Button();
            gridChoseMode = new TableLayoutPanel();
            radioButtonPlaceCell = new RadioButton();
            radioButtonPlaceFig = new RadioButton();
            textBoxInfo = new Label();
            checkBox_OverrideCells = new CheckBox();
            buttonClearField = new Button();
            buttonClearFig = new Button();
            buttonGenerateField = new Button();
            timerGameTick = new System.Windows.Forms.Timer(components);
            mainGrid = new TableLayoutPanel();
            leftGrid = new TableLayoutPanel();
            buttonMenu = new Button();
            rightGrid = new TableLayoutPanel();
            buttonOptions = new Button();
            gridTransformFig = new TableLayoutPanel();
            buttonMirrorY = new Button();
            buttonMirrorX = new Button();
            buttonSpin90 = new Button();
            optionsGrid = new TableLayoutPanel();
            labelUseGrid = new Label();
            buttonScrollSpeed = new Button();
            textBoxScrollSpeed = new TextBox();
            labelScrollSpeed = new Label();
            buttonTimerSpeed = new Button();
            textBoxTimerSpeed = new TextBox();
            labelTimerSpeed = new Label();
            buttonApplyFigSize = new Button();
            textBoxFigSize = new TextBox();
            labelFigSize = new Label();
            buttonBack = new Button();
            labelFieldSize = new Label();
            labelLanguage = new Label();
            textBoxFieldSize = new TextBox();
            buttonApplyFieldSize = new Button();
            checkBoxUseGrid = new CheckBox();
            timerUIFunctions = new System.Windows.Forms.Timer(components);
            menuGrid = new TableLayoutPanel();
            buttonQuit = new Button();
            buttonOptionsMM = new Button();
            buttonLoadState = new Button();
            buttonCampaign = new Button();
            labelTitle = new Label();
            buttonPlay = new Button();
            comboBoxLanguage = new ComboBox();
            pictureBoxBanner = new PictureBox();
            gridChoseMode.SuspendLayout();
            mainGrid.SuspendLayout();
            leftGrid.SuspendLayout();
            rightGrid.SuspendLayout();
            gridTransformFig.SuspendLayout();
            optionsGrid.SuspendLayout();
            menuGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBanner).BeginInit();
            SuspendLayout();
            // 
            // panelField
            // 
            panelField.Anchor = AnchorStyles.None;
            panelField.BackColor = Color.Wheat;
            panelField.Location = new Point(791, 430);
            panelField.Margin = new Padding(0);
            panelField.Name = "panelField";
            panelField.Size = new Size(0, 0);
            panelField.TabIndex = 0;
            panelField.KeyUp += panelField_KeyUp;
            panelField.KeyDown += panelField_KeyDown;
            panelField.SizeChanged += panelField_SizeChanged;
            panelField.DragEnter += panelField_Enter;
            panelField.DragLeave += panelField_Leave;
            panelField.Paint += panelField_Paint;
            panelField.MouseClick += panelField_MouseClick;
            panelField.MouseDown += panelField_MouseDown;
            panelField.MouseEnter += panelField_Enter;
            panelField.MouseLeave += panelField_Leave;
            panelField.MouseMove += panelField_MouseMove;
            panelField.MouseUp += panelField_MouseUp;
            // 
            // panelFigure
            // 
            panelFigure.Anchor = AnchorStyles.None;
            panelFigure.BackColor = Color.Wheat;
            panelFigure.Cursor = Cursors.Cross;
            panelFigure.Location = new Point(3, 20);
            panelFigure.Name = "panelFigure";
            panelFigure.Size = new Size(302, 334);
            panelFigure.TabIndex = 4;
            panelFigure.SizeChanged += panelFigure_SizeChanged;
            panelFigure.Paint += panelFigure_Paint;
            panelFigure.MouseClick += panelFigure_MouseClick;
            // 
            // buttonSaveField
            // 
            buttonSaveField.AutoSize = true;
            buttonSaveField.Dock = DockStyle.Fill;
            buttonSaveField.Location = new Point(3, 608);
            buttonSaveField.Name = "buttonSaveField";
            buttonSaveField.Size = new Size(300, 115);
            buttonSaveField.TabIndex = 4;
            buttonSaveField.Text = "Spielfeld speichern";
            buttonSaveField.UseVisualStyleBackColor = true;
            buttonSaveField.Click += buttonSaveField_Click;
            // 
            // buttonLoadField
            // 
            buttonLoadField.AutoSize = true;
            buttonLoadField.Dock = DockStyle.Fill;
            buttonLoadField.Location = new Point(3, 487);
            buttonLoadField.Name = "buttonLoadField";
            buttonLoadField.Size = new Size(300, 115);
            buttonLoadField.TabIndex = 3;
            buttonLoadField.Text = "Spielfeld laden";
            buttonLoadField.UseVisualStyleBackColor = true;
            buttonLoadField.Click += buttonLoadField_Click;
            // 
            // buttonPause
            // 
            buttonPause.AutoSize = true;
            buttonPause.Dock = DockStyle.Fill;
            buttonPause.Location = new Point(3, 124);
            buttonPause.Name = "buttonPause";
            buttonPause.Size = new Size(300, 115);
            buttonPause.TabIndex = 0;
            buttonPause.Text = "Spiel starten";
            buttonPause.UseVisualStyleBackColor = true;
            buttonPause.Click += buttonPause_Click;
            // 
            // buttonSaveFig
            // 
            buttonSaveFig.AutoSize = true;
            buttonSaveFig.Dock = DockStyle.Fill;
            buttonSaveFig.Location = new Point(3, 717);
            buttonSaveFig.Name = "buttonSaveFig";
            buttonSaveFig.Size = new Size(302, 62);
            buttonSaveFig.TabIndex = 13;
            buttonSaveFig.Text = "Figur speichern";
            buttonSaveFig.UseVisualStyleBackColor = true;
            buttonSaveFig.Click += buttonSaveFig_Click;
            // 
            // buttonLoadFig
            // 
            buttonLoadFig.AutoSize = true;
            buttonLoadFig.Dock = DockStyle.Fill;
            buttonLoadFig.Location = new Point(3, 649);
            buttonLoadFig.Name = "buttonLoadFig";
            buttonLoadFig.Size = new Size(302, 62);
            buttonLoadFig.TabIndex = 12;
            buttonLoadFig.Text = "Figur laden";
            buttonLoadFig.UseVisualStyleBackColor = true;
            buttonLoadFig.Click += buttonLoadFig_Click;
            // 
            // gridChoseMode
            // 
            gridChoseMode.AutoSize = true;
            gridChoseMode.ColumnCount = 2;
            gridChoseMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            gridChoseMode.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            gridChoseMode.Controls.Add(radioButtonPlaceCell, 1, 0);
            gridChoseMode.Controls.Add(radioButtonPlaceFig, 0, 0);
            gridChoseMode.Dock = DockStyle.Fill;
            gridChoseMode.Location = new Point(3, 445);
            gridChoseMode.Name = "gridChoseMode";
            gridChoseMode.RowCount = 1;
            gridChoseMode.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            gridChoseMode.Size = new Size(302, 62);
            gridChoseMode.TabIndex = 7;
            // 
            // radioButtonPlaceCell
            // 
            radioButtonPlaceCell.AutoSize = true;
            radioButtonPlaceCell.Checked = true;
            radioButtonPlaceCell.Dock = DockStyle.Fill;
            radioButtonPlaceCell.Location = new Point(154, 3);
            radioButtonPlaceCell.Name = "radioButtonPlaceCell";
            radioButtonPlaceCell.Size = new Size(145, 56);
            radioButtonPlaceCell.TabIndex = 9;
            radioButtonPlaceCell.TabStop = true;
            radioButtonPlaceCell.Text = "Zelle ändern";
            radioButtonPlaceCell.UseVisualStyleBackColor = true;
            // 
            // radioButtonPlaceFig
            // 
            radioButtonPlaceFig.AutoSize = true;
            radioButtonPlaceFig.Dock = DockStyle.Fill;
            radioButtonPlaceFig.Location = new Point(3, 3);
            radioButtonPlaceFig.Name = "radioButtonPlaceFig";
            radioButtonPlaceFig.Size = new Size(145, 56);
            radioButtonPlaceFig.TabIndex = 8;
            radioButtonPlaceFig.TabStop = true;
            radioButtonPlaceFig.Text = "Figur einfügen";
            radioButtonPlaceFig.UseVisualStyleBackColor = true;
            radioButtonPlaceFig.CheckedChanged += radioButtonPlaceFig_CheckedChanged;
            // 
            // textBoxInfo
            // 
            textBoxInfo.Dock = DockStyle.Fill;
            textBoxInfo.Font = new Font("Courier New", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxInfo.Location = new Point(3, 0);
            textBoxInfo.Name = "textBoxInfo";
            textBoxInfo.Size = new Size(300, 121);
            textBoxInfo.TabIndex = 0;
            textBoxInfo.Text = "Status:\t\tSimulation pausiert\r\nGeneration:\t0\r\nZelle:\r\nMS / Tick:";
            // 
            // checkBox_OverrideCells
            // 
            checkBox_OverrideCells.AutoSize = true;
            checkBox_OverrideCells.Dock = DockStyle.Fill;
            checkBox_OverrideCells.Enabled = false;
            checkBox_OverrideCells.Location = new Point(3, 513);
            checkBox_OverrideCells.Name = "checkBox_OverrideCells";
            checkBox_OverrideCells.Size = new Size(302, 62);
            checkBox_OverrideCells.TabIndex = 10;
            checkBox_OverrideCells.Text = "inaktive Zellen einfügen";
            checkBox_OverrideCells.UseVisualStyleBackColor = true;
            // 
            // buttonClearField
            // 
            buttonClearField.AutoSize = true;
            buttonClearField.Dock = DockStyle.Fill;
            buttonClearField.Location = new Point(3, 366);
            buttonClearField.Name = "buttonClearField";
            buttonClearField.Size = new Size(300, 115);
            buttonClearField.TabIndex = 2;
            buttonClearField.Text = "Spielfeld löschen";
            buttonClearField.UseVisualStyleBackColor = true;
            buttonClearField.Click += buttonClearField_Click;
            // 
            // buttonClearFig
            // 
            buttonClearFig.AutoSize = true;
            buttonClearFig.Dock = DockStyle.Fill;
            buttonClearFig.Location = new Point(3, 581);
            buttonClearFig.Name = "buttonClearFig";
            buttonClearFig.Size = new Size(302, 62);
            buttonClearFig.TabIndex = 11;
            buttonClearFig.Text = "Figur löschen";
            buttonClearFig.UseVisualStyleBackColor = true;
            buttonClearFig.Click += buttonClearFig_Click;
            // 
            // buttonGenerateField
            // 
            buttonGenerateField.AutoSize = true;
            buttonGenerateField.Dock = DockStyle.Fill;
            buttonGenerateField.Location = new Point(3, 245);
            buttonGenerateField.Name = "buttonGenerateField";
            buttonGenerateField.Size = new Size(300, 115);
            buttonGenerateField.TabIndex = 1;
            buttonGenerateField.Text = "Spielfeld zufällig generieren";
            buttonGenerateField.UseVisualStyleBackColor = true;
            buttonGenerateField.Click += buttonGenerateField_Click;
            // 
            // timerGameTick
            // 
            timerGameTick.Interval = 500;
            timerGameTick.Tick += timerGameTick_Tick;
            // 
            // mainGrid
            // 
            mainGrid.ColumnCount = 3;
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            mainGrid.Controls.Add(panelField, 1, 0);
            mainGrid.Controls.Add(leftGrid, 0, 0);
            mainGrid.Controls.Add(rightGrid, 2, 0);
            mainGrid.Dock = DockStyle.Fill;
            mainGrid.Location = new Point(0, 0);
            mainGrid.Margin = new Padding(0);
            mainGrid.Name = "mainGrid";
            mainGrid.RowCount = 2;
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 0F));
            mainGrid.Size = new Size(1584, 861);
            mainGrid.TabIndex = 0;
            mainGrid.MouseWheel += mainGrid_MouseWheel;
            // 
            // leftGrid
            // 
            leftGrid.ColumnCount = 1;
            leftGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            leftGrid.Controls.Add(textBoxInfo, 0, 0);
            leftGrid.Controls.Add(buttonPause, 0, 1);
            leftGrid.Controls.Add(buttonGenerateField, 0, 2);
            leftGrid.Controls.Add(buttonClearField, 0, 3);
            leftGrid.Controls.Add(buttonLoadField, 0, 4);
            leftGrid.Controls.Add(buttonSaveField, 0, 5);
            leftGrid.Controls.Add(buttonMenu, 0, 6);
            leftGrid.Dock = DockStyle.Fill;
            leftGrid.Location = new Point(5, 5);
            leftGrid.Margin = new Padding(5);
            leftGrid.Name = "leftGrid";
            leftGrid.RowCount = 7;
            leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            leftGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            leftGrid.Size = new Size(306, 851);
            leftGrid.TabIndex = 0;
            // 
            // buttonMenu
            // 
            buttonMenu.AutoSize = true;
            buttonMenu.Dock = DockStyle.Fill;
            buttonMenu.Location = new Point(3, 729);
            buttonMenu.Name = "buttonMenu";
            buttonMenu.Size = new Size(300, 119);
            buttonMenu.TabIndex = 5;
            buttonMenu.Text = "Hauptmenü";
            buttonMenu.UseVisualStyleBackColor = true;
            buttonMenu.Click += buttonMenu_Click;
            // 
            // rightGrid
            // 
            rightGrid.ColumnCount = 1;
            rightGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            rightGrid.Controls.Add(panelFigure, 0, 0);
            rightGrid.Controls.Add(gridChoseMode, 0, 2);
            rightGrid.Controls.Add(checkBox_OverrideCells, 0, 3);
            rightGrid.Controls.Add(buttonClearFig, 0, 4);
            rightGrid.Controls.Add(buttonLoadFig, 0, 5);
            rightGrid.Controls.Add(buttonSaveFig, 0, 6);
            rightGrid.Controls.Add(buttonOptions, 0, 7);
            rightGrid.Controls.Add(gridTransformFig, 0, 1);
            rightGrid.Dock = DockStyle.Fill;
            rightGrid.Location = new Point(1271, 5);
            rightGrid.Margin = new Padding(5);
            rightGrid.Name = "rightGrid";
            rightGrid.RowCount = 8;
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 44F));
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            rightGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 8F));
            rightGrid.Size = new Size(308, 851);
            rightGrid.TabIndex = 1;
            // 
            // buttonOptions
            // 
            buttonOptions.AutoSize = true;
            buttonOptions.Dock = DockStyle.Fill;
            buttonOptions.Location = new Point(3, 785);
            buttonOptions.Name = "buttonOptions";
            buttonOptions.Size = new Size(302, 63);
            buttonOptions.TabIndex = 14;
            buttonOptions.Text = "Optionen";
            buttonOptions.UseVisualStyleBackColor = true;
            buttonOptions.Click += buttonOptions_Click;
            // 
            // gridTransformFig
            // 
            gridTransformFig.ColumnCount = 3;
            gridTransformFig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            gridTransformFig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            gridTransformFig.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            gridTransformFig.Controls.Add(buttonMirrorY, 2, 0);
            gridTransformFig.Controls.Add(buttonMirrorX, 1, 0);
            gridTransformFig.Controls.Add(buttonSpin90, 0, 0);
            gridTransformFig.Dock = DockStyle.Fill;
            gridTransformFig.Location = new Point(3, 377);
            gridTransformFig.Name = "gridTransformFig";
            gridTransformFig.RowCount = 1;
            gridTransformFig.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            gridTransformFig.Size = new Size(302, 62);
            gridTransformFig.TabIndex = 6;
            // 
            // buttonMirrorY
            // 
            buttonMirrorY.AutoSize = true;
            buttonMirrorY.Dock = DockStyle.Fill;
            buttonMirrorY.Location = new Point(203, 3);
            buttonMirrorY.Name = "buttonMirrorY";
            buttonMirrorY.Size = new Size(96, 56);
            buttonMirrorY.TabIndex = 8;
            buttonMirrorY.Text = "Auf Y Achse spiegeln";
            buttonMirrorY.UseVisualStyleBackColor = true;
            buttonMirrorY.Click += buttonMirrorY_Click;
            // 
            // buttonMirrorX
            // 
            buttonMirrorX.AutoSize = true;
            buttonMirrorX.Dock = DockStyle.Fill;
            buttonMirrorX.Location = new Point(103, 3);
            buttonMirrorX.Name = "buttonMirrorX";
            buttonMirrorX.Size = new Size(94, 56);
            buttonMirrorX.TabIndex = 7;
            buttonMirrorX.Text = "Auf X Achse spiegeln";
            buttonMirrorX.UseVisualStyleBackColor = true;
            buttonMirrorX.Click += buttonMirrorX_Click;
            // 
            // buttonSpin90
            // 
            buttonSpin90.AutoSize = true;
            buttonSpin90.Dock = DockStyle.Fill;
            buttonSpin90.Location = new Point(3, 3);
            buttonSpin90.Name = "buttonSpin90";
            buttonSpin90.Size = new Size(94, 56);
            buttonSpin90.TabIndex = 6;
            buttonSpin90.Text = "90° drehen";
            buttonSpin90.UseVisualStyleBackColor = true;
            buttonSpin90.Click += buttonSpin90_Click;
            // 
            // optionsGrid
            // 
            optionsGrid.ColumnCount = 5;
            optionsGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            optionsGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            optionsGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            optionsGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
            optionsGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 5F));
            optionsGrid.Controls.Add(labelUseGrid, 1, 4);
            optionsGrid.Controls.Add(buttonScrollSpeed, 3, 3);
            optionsGrid.Controls.Add(textBoxScrollSpeed, 2, 3);
            optionsGrid.Controls.Add(labelScrollSpeed, 1, 3);
            optionsGrid.Controls.Add(buttonTimerSpeed, 3, 2);
            optionsGrid.Controls.Add(textBoxTimerSpeed, 2, 2);
            optionsGrid.Controls.Add(labelTimerSpeed, 1, 2);
            optionsGrid.Controls.Add(buttonApplyFigSize, 3, 1);
            optionsGrid.Controls.Add(textBoxFigSize, 2, 1);
            optionsGrid.Controls.Add(labelFigSize, 1, 1);
            optionsGrid.Controls.Add(buttonBack, 2, 6);
            optionsGrid.Controls.Add(labelFieldSize, 1, 0);
            optionsGrid.Controls.Add(textBoxFieldSize, 2, 0);
            optionsGrid.Controls.Add(buttonApplyFieldSize, 3, 0);
            optionsGrid.Controls.Add(checkBoxUseGrid, 2, 4);
            optionsGrid.Controls.Add(labelLanguage, 1, 5);
            optionsGrid.Controls.Add(comboBoxLanguage, 2, 5);
            optionsGrid.Dock = DockStyle.Fill;
            optionsGrid.Location = new Point(0, 0);
            optionsGrid.Margin = new Padding(0);
            optionsGrid.Name = "optionsGrid";
            optionsGrid.RowCount = 7;
            optionsGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3F));
            optionsGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3F));
            optionsGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3F));
            optionsGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3F));
            optionsGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3F));
            optionsGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3F));
            optionsGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.3F));
            optionsGrid.Size = new Size(1584, 861);
            optionsGrid.TabIndex = 0;
            // 
            // labelLanguage
            // 
            labelLanguage.AutoSize = true;
            labelLanguage.Dock = DockStyle.Fill;
            labelLanguage.Location = new Point(82, 572);
            labelLanguage.Name = "labelLanguage";
            labelLanguage.Size = new Size(469, 143);
            labelLanguage.TabIndex = 22;
            labelLanguage.Text = "Sprache";
            labelLanguage.TextAlign = ContentAlignment.TopCenter;
            // 
            // comboBoxLanguage
            // 
            comboBoxLanguage.AutoSize = true;
            comboBoxLanguage.Dock = DockStyle.Fill;
            comboBoxLanguage.Name = "comboBoxLanguage";
            comboBoxLanguage.TabIndex = 9;
            comboBoxLanguage.SelectedIndexChanged += comboBoxLanguage_SelectedIndexChanged;
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.DrawMode = DrawMode.OwnerDrawFixed;
            comboBoxLanguage.DrawItem += comboBoxLanguage_DrawItem;
            // 
            // labelUseGrid
            // 
            labelUseGrid.AutoSize = true;
            labelUseGrid.Dock = DockStyle.Fill;
            labelUseGrid.Location = new Point(82, 572);
            labelUseGrid.Name = "labelUseGrid";
            labelUseGrid.Size = new Size(469, 143);
            labelUseGrid.TabIndex = 22;
            labelUseGrid.Text = "Grid anzeigen\r\n\r\nGibt an, ob jede Zelle auf dem Spielfeld mit einem Kästchen umranded dargestellt werden soll.";
            labelUseGrid.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonScrollSpeed
            // 
            buttonScrollSpeed.AutoSize = true;
            buttonScrollSpeed.Dock = DockStyle.Fill;
            buttonScrollSpeed.Location = new Point(1032, 432);
            buttonScrollSpeed.Name = "buttonScrollSpeed";
            buttonScrollSpeed.Size = new Size(469, 137);
            buttonScrollSpeed.TabIndex = 7;
            buttonScrollSpeed.Text = "Anwenden";
            buttonScrollSpeed.UseVisualStyleBackColor = true;
            buttonScrollSpeed.Click += buttonScrollSpeed_Click;
            // 
            // textBoxScrollSpeed
            // 
            textBoxScrollSpeed.Anchor = AnchorStyles.Left;
            textBoxScrollSpeed.Dock = DockStyle.Fill;
            textBoxScrollSpeed.Location = new Point(557, 489);
            textBoxScrollSpeed.Name = "textBoxScrollSpeed";
            textBoxScrollSpeed.Size = new Size(469, 23);
            textBoxScrollSpeed.TabIndex = 6;
            textBoxScrollSpeed.Text = "5";
            textBoxScrollSpeed.KeyDown += textBoxScrollSpeed_KeyDown;
            // 
            // labelScrollSpeed
            // 
            labelScrollSpeed.AutoSize = true;
            labelScrollSpeed.Dock = DockStyle.Fill;
            labelScrollSpeed.Location = new Point(82, 429);
            labelScrollSpeed.Name = "labelScrollSpeed";
            labelScrollSpeed.Size = new Size(469, 143);
            labelScrollSpeed.TabIndex = 21;
            labelScrollSpeed.Text = "Scrollgeschwindigkeit\r\n\r\nGib eine Zahl zwischen 1 und 20 ein.\r\nGibt die Anzahl der Pixel an, die mit jedem Scrollschritt verschoben werden.\r\nIst vom Zoom abhängig.";
            labelScrollSpeed.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonTimerSpeed
            // 
            buttonTimerSpeed.AutoSize = true;
            buttonTimerSpeed.Dock = DockStyle.Fill;
            buttonTimerSpeed.Location = new Point(1032, 289);
            buttonTimerSpeed.Name = "buttonTimerSpeed";
            buttonTimerSpeed.Size = new Size(469, 137);
            buttonTimerSpeed.TabIndex = 5;
            buttonTimerSpeed.Text = "Anwenden";
            buttonTimerSpeed.UseVisualStyleBackColor = true;
            buttonTimerSpeed.Click += buttonTimerSpeed_Click;
            // 
            // textBoxTimerSpeed
            // 
            textBoxTimerSpeed.Anchor = AnchorStyles.Left;
            textBoxTimerSpeed.Dock = DockStyle.Fill;
            textBoxTimerSpeed.Location = new Point(557, 346);
            textBoxTimerSpeed.Name = "textBoxTimerSpeed";
            textBoxTimerSpeed.Size = new Size(469, 23);
            textBoxTimerSpeed.TabIndex = 4;
            textBoxTimerSpeed.Text = "500";
            textBoxTimerSpeed.KeyDown += textBoxTimerSpeed_KeyDown;
            // 
            // labelTimerSpeed
            // 
            labelTimerSpeed.AutoSize = true;
            labelTimerSpeed.Dock = DockStyle.Fill;
            labelTimerSpeed.Location = new Point(82, 286);
            labelTimerSpeed.Name = "labelTimerSpeed";
            labelTimerSpeed.Size = new Size(469, 143);
            labelTimerSpeed.TabIndex = 18;
            labelTimerSpeed.Text = "MS / GameTick\n\nGib eine Zahl zwischen 50 und 5000 ein.\nKann die Performance erheblich beeinflussen.\nEs wird davon abgeraten weniger als 100 Millisekunden per Tick zu verwenden.";
            labelTimerSpeed.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonApplyFigSize
            // 
            buttonApplyFigSize.AutoSize = true;
            buttonApplyFigSize.Dock = DockStyle.Fill;
            buttonApplyFigSize.Location = new Point(1032, 146);
            buttonApplyFigSize.Name = "buttonApplyFigSize";
            buttonApplyFigSize.Size = new Size(469, 137);
            buttonApplyFigSize.TabIndex = 3;
            buttonApplyFigSize.Text = "Anwenden";
            buttonApplyFigSize.UseVisualStyleBackColor = true;
            buttonApplyFigSize.Click += buttonApplyFigSize_Click;
            // 
            // textBoxFigSize
            // 
            textBoxFigSize.Anchor = AnchorStyles.Left;
            textBoxFigSize.Dock = DockStyle.Fill;
            textBoxFigSize.Location = new Point(557, 203);
            textBoxFigSize.Name = "textBoxFigSize";
            textBoxFigSize.Size = new Size(469, 23);
            textBoxFigSize.TabIndex = 2;
            textBoxFigSize.Text = "7,7";
            textBoxFigSize.KeyDown += textBoxFigSize_KeyDown;
            // 
            // labelFigSize
            // 
            labelFigSize.AutoSize = true;
            labelFigSize.Dock = DockStyle.Fill;
            labelFigSize.Location = new Point(82, 143);
            labelFigSize.Name = "labelFigSize";
            labelFigSize.Size = new Size(469, 143);
            labelFigSize.TabIndex = 15;
            labelFigSize.Text = "Figurengröße\n\nGib eine Zahl zwischen 3 und 100 ein.\nMuss kleiner als Spielfeldgröße sein.Für ein nicht quadratisches Spielfeld, nutze ein Komma (',') um Breite und Höhe zu trennen.";
            labelFigSize.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonBack
            // 
            buttonBack.AutoSize = true;
            buttonBack.Dock = DockStyle.Fill;
            buttonBack.Location = new Point(557, 718);
            buttonBack.Name = "buttonBack";
            buttonBack.Size = new Size(469, 140);
            buttonBack.TabIndex = 10;
            buttonBack.Text = "Zurück";
            buttonBack.UseVisualStyleBackColor = true;
            buttonBack.Click += buttonBack_Click;
            // 
            // labelFieldSize
            // 
            labelFieldSize.AutoSize = true;
            labelFieldSize.Dock = DockStyle.Fill;
            labelFieldSize.Location = new Point(82, 0);
            labelFieldSize.Name = "labelFieldSize";
            labelFieldSize.Size = new Size(469, 143);
            labelFieldSize.TabIndex = 12;
            labelFieldSize.Text = "Spielfeldgröße\n\nGib eine Zahl zwischen 10 und 10000 ein.\nMuss größer als Figurengröße sein.Für ein nicht quadratisches Spielfeld, nutze ein Komma (',') um Breite und Höhe zu trennen.";
            labelFieldSize.TextAlign = ContentAlignment.TopCenter;
            // 
            // textBoxFieldSize
            // 
            textBoxFieldSize.Anchor = AnchorStyles.Left;
            textBoxFieldSize.Location = new Point(557, 60);
            textBoxFieldSize.Dock = DockStyle.Fill;
            textBoxFieldSize.Name = "textBoxFieldSize";
            textBoxFieldSize.Size = new Size(469, 23);
            textBoxFieldSize.TabIndex = 0;
            textBoxFieldSize.Text = "120,120";
            textBoxFieldSize.KeyDown += textBoxFieldSize_KeyDown;
            // 
            // buttonApplyFieldSize
            // 
            buttonApplyFieldSize.AutoSize = true;
            buttonApplyFieldSize.Dock = DockStyle.Fill;
            buttonApplyFieldSize.Location = new Point(1032, 3);
            buttonApplyFieldSize.Name = "buttonApplyFieldSize";
            buttonApplyFieldSize.Size = new Size(469, 137);
            buttonApplyFieldSize.TabIndex = 1;
            buttonApplyFieldSize.Text = "Anwenden";
            buttonApplyFieldSize.UseVisualStyleBackColor = true;
            buttonApplyFieldSize.Click += buttonApplyFieldSize_Click;
            // 
            // checkBoxUseGrid
            // 
            checkBoxUseGrid.AutoSize = true;
            checkBoxUseGrid.CheckAlign = ContentAlignment.TopRight;
            checkBoxUseGrid.Dock = DockStyle.Fill;
            checkBoxUseGrid.Location = new Point(557, 575);
            checkBoxUseGrid.Name = "checkBoxUseGrid";
            checkBoxUseGrid.Size = new Size(469, 137);
            checkBoxUseGrid.TabIndex = 8;
            checkBoxUseGrid.Text = "Grid aktivieren";
            checkBoxUseGrid.TextAlign = ContentAlignment.TopRight;
            checkBoxUseGrid.UseVisualStyleBackColor = true;
            checkBoxUseGrid.CheckedChanged += checkBoxUseGrid_CheckedChanged;
            // 
            // timerUIFunctions
            // 
            timerUIFunctions.Tick += timerUIFunctions_Tick;
            // 
            // menuGrid
            // 
            menuGrid.ColumnCount = 3;
            menuGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            menuGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            menuGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            menuGrid.Controls.Add(buttonQuit, 1, 5);
            menuGrid.Controls.Add(buttonOptionsMM, 1, 4);
            menuGrid.Controls.Add(buttonLoadState, 1, 3);
            menuGrid.Controls.Add(buttonCampaign, 1, 2);
            menuGrid.Controls.Add(labelTitle, 1, 0);
            menuGrid.Controls.Add(buttonPlay, 1, 1);
            menuGrid.Controls.Add(pictureBoxBanner, 1, 6);
            menuGrid.Dock = DockStyle.Fill;
            menuGrid.Location = new Point(0, 0);
            menuGrid.Name = "menuGrid";
            menuGrid.RowCount = 7;
            menuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            menuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            menuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            menuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            menuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            menuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            menuGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 14.29F));
            menuGrid.Size = new Size(1584, 861);
            menuGrid.TabIndex = 1;
            // 
            // buttonQuit
            // 
            buttonQuit.AutoSize = true;
            buttonQuit.Dock = DockStyle.Fill;
            buttonQuit.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonQuit.Location = new Point(161, 618);
            buttonQuit.Name = "buttonQuit";
            buttonQuit.Size = new Size(1261, 117);
            buttonQuit.TabIndex = 4;
            buttonQuit.Text = "Beenden";
            buttonQuit.UseVisualStyleBackColor = true;
            buttonQuit.Click += buttonQuit_Click;
            // 
            // buttonOptionsMM
            // 
            buttonOptionsMM.AutoSize = true;
            buttonOptionsMM.Dock = DockStyle.Fill;
            buttonOptionsMM.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonOptionsMM.Location = new Point(161, 495);
            buttonOptionsMM.Name = "buttonOptionsMM";
            buttonOptionsMM.Size = new Size(1261, 117);
            buttonOptionsMM.TabIndex = 3;
            buttonOptionsMM.Text = "Optionen";
            buttonOptionsMM.UseVisualStyleBackColor = true;
            buttonOptionsMM.Click += buttonOptions_Click;
            // 
            // buttonLoadState
            // 
            buttonLoadState.AutoSize = true;
            buttonLoadState.Dock = DockStyle.Fill;
            buttonLoadState.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonLoadState.Location = new Point(161, 372);
            buttonLoadState.Name = "buttonLoadState";
            buttonLoadState.Size = new Size(1261, 117);
            buttonLoadState.TabIndex = 2;
            buttonLoadState.Text = "Spiel laden";
            buttonLoadState.UseVisualStyleBackColor = true;
            buttonLoadState.Click += buttonLoadState_Click;
            // 
            // buttonCampaign
            // 
            buttonCampaign.AutoSize = true;
            buttonCampaign.Dock = DockStyle.Fill;
            buttonCampaign.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonCampaign.Location = new Point(161, 249);
            buttonCampaign.Name = "buttonCampaign";
            buttonCampaign.Size = new Size(1261, 117);
            buttonCampaign.TabIndex = 1;
            buttonCampaign.Text = "Kampagne starten";
            buttonCampaign.UseVisualStyleBackColor = true;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Dock = DockStyle.Fill;
            labelTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelTitle.Location = new Point(161, 0);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(1261, 123);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Conway's Game Of Life";
            labelTitle.TextAlign = ContentAlignment.TopCenter;
            // 
            // buttonPlay
            // 
            buttonPlay.AutoSize = true;
            buttonPlay.Dock = DockStyle.Fill;
            buttonPlay.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonPlay.Location = new Point(161, 126);
            buttonPlay.Name = "buttonPlay";
            buttonPlay.Size = new Size(1261, 117);
            buttonPlay.TabIndex = 0;
            buttonPlay.Text = "Freies Spiel";
            buttonPlay.UseVisualStyleBackColor = true;
            buttonPlay.Click += buttonPlay_Click;
            // 
            // pictureBoxBanner
            // 
            pictureBoxBanner.Dock = DockStyle.Fill;
            pictureBoxBanner.ErrorImage = Properties.Resources.Butterblume2;
            pictureBoxBanner.InitialImage = Properties.Resources.Butterblume2;
            pictureBoxBanner.Location = new Point(161, 741);
            pictureBoxBanner.Name = "pictureBoxBanner";
            pictureBoxBanner.Size = new Size(1261, 117);
            pictureBoxBanner.TabIndex = 5;
            pictureBoxBanner.TabStop = false;
            pictureBoxBanner.WaitOnLoad = true;
            pictureBoxBanner.SizeChanged += pictureBoxBanner_SizeChanged;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1584, 861);
            Controls.Add(menuGrid);
            Controls.Add(mainGrid);
            Controls.Add(optionsGrid);
            Name = "GameForm";
            Text = "A New Take On Conway's Game Of Life";
            gridChoseMode.ResumeLayout(false);
            gridChoseMode.PerformLayout();
            mainGrid.ResumeLayout(false);
            leftGrid.ResumeLayout(false);
            leftGrid.PerformLayout();
            rightGrid.ResumeLayout(false);
            rightGrid.PerformLayout();
            gridTransformFig.ResumeLayout(false);
            gridTransformFig.PerformLayout();
            optionsGrid.ResumeLayout(false);
            optionsGrid.PerformLayout();
            menuGrid.ResumeLayout(false);
            menuGrid.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxBanner).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private BufferedPanel panelField;
        private Button buttonSaveField;
        private Button buttonLoadField;
        private Button buttonPause;
        private BufferedPanel panelFigure;
        private Button buttonSaveFig;
        private Button buttonLoadFig;
        private TableLayoutPanel gridChoseMode;
        private Label textBoxInfo;
        private RadioButton radioButtonPlaceCell;
        private RadioButton radioButtonPlaceFig;
        private CheckBox checkBox_OverrideCells;
        private Button buttonClearField;
        private Button buttonClearFig;
        private Button buttonGenerateField;
        private System.Windows.Forms.Timer timerGameTick;
        private TableLayoutPanel mainGrid;
        private TableLayoutPanel leftGrid;
        private TableLayoutPanel rightGrid;
        private TableLayoutPanel optionsGrid;
        private Button buttonOptions;
        private Button buttonBack;
        private Label labelFieldSize;
        private TextBox textBoxFieldSize;
        private Button buttonApplyFieldSize;
        private TextBox textBoxTimerSpeed;
        private Label labelTimerSpeed;
        private Button buttonApplyFigSize;
        private TextBox textBoxFigSize;
        private Label labelFigSize;
        private Button buttonTimerSpeed;
        private System.Windows.Forms.Timer timerUIFunctions;
        private Label labelScrollSpeed;
        private Button buttonScrollSpeed;
        private TextBox textBoxScrollSpeed;
        private Label labelUseGrid;
        private CheckBox checkBoxUseGrid;
        private Label labelLanguage;
        private TableLayoutPanel gridTransformFig;
        private Button buttonSpin90;
        private Button buttonMirrorX;
        private Button buttonMirrorY;
        private TableLayoutPanel menuGrid;
        private Label labelTitle;
        private Button buttonLoadState;
        private Button buttonCampaign;
        private Button buttonPlay;
        private Button buttonOptionsMM;
        private Button buttonQuit;
        private Button buttonMenu;
        private ComboBox comboBoxLanguage;
        private PictureBox pictureBoxBanner;
    }
}