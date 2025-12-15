using System.Diagnostics;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Conways_GOL
{
    public partial class GameForm : Form
    {
        #region Fields
        private Logic logic;
        private static readonly int fieldSize = 120;
        private static readonly int figSize = 7;
        private static int scrollSpeed = 5;
        private int msSinceLastTick;
        private double lastTickTime;
        private static double zoomSpeed = 1.5;
        private static bool drawGrid;
        private static readonly string Levelpath = Path.GetFullPath(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Conway's Game Of Life\\levels\\");
        private static readonly string Figpath = Path.GetFullPath(Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "\\Conway's Game Of Life\\figs\\");
        private Point cursor;
        private bool paintFig;
        private Point copystartPoint = new Point(-1, -1);
        private Point dragstartPoint = new Point(-1, -1);
        private Point offset = new Point(0, 0);
        private double zoom = 1;
        private bool[] scroll = new bool[4];
        private TableLayoutPanel? last;
        private Localization loc;
        private Lang[] languages;
        private ImageList imageList;
        #endregion

        #region Constructor
        public GameForm()
        {
            InitializeComponent();
            logic = new Logic(fieldSize, figSize);

            //Copy prefabs to folders
            CopyDirectorySafe(Path.GetDirectoryName(Application.ExecutablePath) + "\\levels\\", Levelpath);
            CopyDirectorySafe(Path.GetDirectoryName(Application.ExecutablePath) + "\\figures\\", Figpath);
            optionsGrid.Hide();
            mainGrid.Hide();
            menuGrid.Show();
            buttonCampaign.Enabled = false;
            pictureBoxBanner.Image = ResizeImage(pictureBoxBanner.InitialImage, pictureBoxBanner.Width, pictureBoxBanner.Height);
            imageList = new ImageList();
            imageList.Images.Add(Properties.Resources.en);
            imageList.Images.Add(Properties.Resources.fr);
            imageList.Images.Add(Properties.Resources.de);
            imageList.Images.Add(Properties.Resources.es);
            loc = new Localization();
            languages = new Lang[]
            {
                new Lang("en", loc.GetText("lang_en")),
                new Lang("fr", loc.GetText("lang_fr")),
                new Lang("de", loc.GetText("lang_de")),
                new Lang("es", loc.GetText("lang_es"))
            };
            foreach (Lang lang in languages)
            {
                comboBoxLanguage.Items.Add(lang);
            }
            comboBoxLanguage.SelectedIndex = 2;
        }
        #endregion

        #region Properties
        /* Stops the DoubleBuffered Panels from drawing fragments outside their own bounds
         * 
         * by Hans Passant
         * found (https://stackoverflow.com/questions/3718380/winforms-double-buffering)
         */
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000; // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }
        #endregion

        #region Functions
        /* Starts or Stops the Game Timer (Game Loop)
         */
        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (timerGameTick.Enabled)
            {
                timerGameTick.Stop();
                buttonPause.Text = loc.GetText("ingame_resume");
                RefreshInfo();
            }
            else
            {
                timerGameTick.Start();
                buttonPause.Text = loc.GetText("ingame_pause");
                RefreshInfo();
            }
        }

        /* Returns the factor to multiply a cell with to get the position of that cell on the panel
         */
        private PointF CellScale()
        {
            if (panelField == null)
                return new Point(-1, -1);
            PointF p = new PointF();
            p.X = (float)((double)panelField.Width / logic.FieldWidth * zoom);
            p.Y = (float)((double)panelField.Height / logic.FieldHeight * zoom);
            return p;
        }

        /* Clears the playing field
         * by calling the corresponding logic function
         */
        private void buttonClearField_Click(object sender, EventArgs e)
        {
            logic.ClearField();
            panelField.Invalidate();
        }

        /* Generates a new playing field
         * by calling the corresponding logic function
         */
        private void buttonGenerateField_Click(Object sender, EventArgs e)
        {
            logic.GenerateField();
            panelField.Invalidate();
        }

        /* Acts as the main entry point for the generation changes. Each Gametick spawns here
         * Calls the Tick method in the game logic and forces the field to redraw (animation loop)
         * Also tracks some stats that can be displayed in an infobox or stored in a stats view
         */
        private void timerGameTick_Tick(object sender, EventArgs e)
        {
            if (timerGameTick.Enabled)
            {
                logic.Tick();
                panelField.Invalidate();

                double tickTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                if (lastTickTime == 0)
                {
                    msSinceLastTick = 0;
                    lastTickTime = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                }
                else
                {
                    msSinceLastTick = (int)(tickTime - lastTickTime);
                }
                lastTickTime = tickTime;

                RefreshInfo();
            }
        }

        /* Updates the info textBox
         */
        private void RefreshInfo()
        {
            string simulation = timerGameTick.Enabled ? loc.GetText("ingame_info_simulation_active") : loc.GetText("ingame_info_simulation_paused");
            textBoxInfo.Text =
                loc.GetText("ingame_info_1") + $"{simulation}\r\n" +
                loc.GetText("ingame_info_2") + $"{logic.Generation}\r\n" +
                loc.GetText("ingame_info_3") + $"[{cursor.X},{cursor.Y}]\r\n" +
                loc.GetText("ingame_info_4") + $"{msSinceLastTick}\r\n" +
                loc.GetText("ingame_info_5") + $"{zoom.ToString("n2")}\r\n" +
                loc.GetText("ingame_info_6") + $"{offset}";

        }

        /* Used to draw transparent pixels directly on an Image
         */
        private byte AlphaCompose(byte oldColor, byte newColor, int alpha)
        {
            double a = (double)alpha / 255;
            return (byte)(a * newColor + (1 - a) * oldColor);
        }

        /* Visualizes the current playing field
         * Scaling up the field as far as possible within bounds while keeping all cells at an equal size
         * Also draws an indicator for where to paste figures when hovering over the panel with the mouse cursor
         */
        private void panelField_Paint(object sender, PaintEventArgs e)
        {
            if (panelField.Width == 0 ||
                panelField.Height == 0)
                return;
            DateTime temp = DateTime.Now;

            Graphics g = e.Graphics;

            // Try using a Bitmap from an array to draw at once and multithread the graphics calculation

            Bitmap bmp = new Bitmap(panelField.Width, panelField.Height, PixelFormat.Format32bppArgb);
            Rectangle area = new Rectangle(0, 0, panelField.Width, panelField.Height);
            BitmapData bmpData = bmp.LockBits(area, ImageLockMode.WriteOnly, bmp.PixelFormat);

            int stride = bmpData.Stride;
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(stride) * panelField.Height;
            byte[] pixelBuffer = new byte[bytes];

            // Parallel filling of pixelBuffer
            Parallel.For(0, panelField.Height, y =>
            {
                int rowStart = y * stride;

                int verticalBorder = (int)(panelField.Width - logic.FieldWidth * CellScale().X);
                int horizontalBorder = (int)(panelField.Height - logic.FieldHeight * CellScale().Y);

                for (int x = 0; x < panelField.Width; x++)
                {
                    int pixelIndex = rowStart + x * 4;
                    int xPos = (int)((x - offset.X) / CellScale().X);
                    int yPos = (int)((y - offset.Y) / CellScale().Y);
                    Point pos = new Point(xPos, yPos);

                    if (drawGrid && ((int)((x - offset.X) % CellScale().X) == 0 || (int)((y - offset.Y) % CellScale().Y) == 0))
                    {
                        pixelBuffer[pixelIndex] = 0;    // Blue
                        pixelBuffer[pixelIndex + 1] = 0;    // Green
                        pixelBuffer[pixelIndex + 2] = 0;    // Red
                        pixelBuffer[pixelIndex + 3] = 255;  // Alpha
                    }
                    else if (logic.GetCell(xPos, yPos))
                    {
                        // Set pixel bytes: Blue, Green, Red, Alpha (fully opaque)
                        pixelBuffer[pixelIndex] = 0;    // Blue
                        pixelBuffer[pixelIndex + 1] = 0;    // Green
                        pixelBuffer[pixelIndex + 2] = 0;    // Red
                        pixelBuffer[pixelIndex + 3] = 255;  // Alpha
                    }
                    else if (!logic.GetCell(xPos, yPos))
                    {
                        //Color.Wheat = ARGB FFF5DEB3
                        pixelBuffer[pixelIndex] = 0xB3;     // Blue
                        pixelBuffer[pixelIndex + 1] = 0xDE;     // Green
                        pixelBuffer[pixelIndex + 2] = 0xF5;     // Red
                        pixelBuffer[pixelIndex + 3] = 0x00;     // Alpha
                    }

                    // Alpha Composing for drawing on top
                    if (copystartPoint.X == -1 && radioButtonPlaceCell.Checked && paintFig && xPos == cursor.X && yPos == cursor.Y)
                    {
                        //Color.Lime = FF00FF00
                        // Draw at cursor for single cell switching
                        pixelBuffer[pixelIndex] = AlphaCompose(pixelBuffer[pixelIndex], 0, 128);        // Blue
                        pixelBuffer[pixelIndex + 1] = AlphaCompose(pixelBuffer[pixelIndex + 1], 0xFF, 128); // Green
                        pixelBuffer[pixelIndex + 2] = AlphaCompose(pixelBuffer[pixelIndex + 2], 0, 128);    // Red
                        pixelBuffer[pixelIndex + 3] = 0xFF;                                                 // Alpha
                    }
                    else if (radioButtonPlaceFig.Checked && paintFig && copystartPoint.X == -1 &&
                    logic.IsInsideRectOnField(pos, cursor.X - logic.FigureWidth / 2, cursor.Y - logic.FigureHeight / 2, logic.FigureWidth, logic.FigureHeight))
                    {
                        if (logic.GetFigureCell(logic.Torus(xPos - cursor.X + logic.FigureWidth / 2, 0), logic.Torus(yPos - cursor.Y + logic.FigureHeight / 2, 1)))
                        {
                            // Draw Figure on field at cursor position
                            pixelBuffer[pixelIndex] = AlphaCompose(pixelBuffer[pixelIndex], 0, 128);        // Blue
                            pixelBuffer[pixelIndex + 1] = AlphaCompose(pixelBuffer[pixelIndex + 1], 0xFF, 128); // Green
                            pixelBuffer[pixelIndex + 2] = AlphaCompose(pixelBuffer[pixelIndex + 2], 0, 128);    // Red
                            pixelBuffer[pixelIndex + 3] = 0xFF;                                                 // Alpha
                        }
                        else if (checkBox_OverrideCells.Checked && ((int)((x - offset.X) % CellScale().X) == 0 || (int)((y - offset.Y) % CellScale().Y) == 0))
                        {
                            // Draw borders to include empty cells from figure
                            // TODO: Draw the last line too (next cell tho)
                            pixelBuffer[pixelIndex] = AlphaCompose(pixelBuffer[pixelIndex], 0, 128);        // Blue
                            pixelBuffer[pixelIndex + 1] = AlphaCompose(pixelBuffer[pixelIndex + 1], 0xFF, 128); // Green
                            pixelBuffer[pixelIndex + 2] = AlphaCompose(pixelBuffer[pixelIndex + 2], 0, 128);    // Red
                            pixelBuffer[pixelIndex + 3] = 0xFF;                                                 // Alpha
                        }
                    }
                    else if (copystartPoint.X != -1)
                    {
                        int fromX = Math.Min(cursor.X, copystartPoint.X);
                        int fromY = Math.Min(cursor.Y, copystartPoint.Y);
                        int sizeX = Math.Abs(cursor.X - copystartPoint.X);
                        int sizeY = Math.Abs(cursor.Y - copystartPoint.Y);

                        // check if inside the boundings
                        if (logic.IsInsideRectOnField(pos, fromX, fromY, sizeX, sizeY))
                        {
                            // Paint copy rect
                            //Color.Maroon = FF800000
                            pixelBuffer[pixelIndex] = AlphaCompose(pixelBuffer[pixelIndex], 0, 128);        // Blue
                            pixelBuffer[pixelIndex + 1] = AlphaCompose(pixelBuffer[pixelIndex + 1], 0, 128);    // Green
                            pixelBuffer[pixelIndex + 2] = AlphaCompose(pixelBuffer[pixelIndex + 2], 0x80, 128); // Red
                            pixelBuffer[pixelIndex + 3] = 0xFF;
                        }
                    }

                }
            });

            Marshal.Copy(pixelBuffer, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);

            // Only 1 Graphics operation instead of 10000+
            g.DrawImage(bmp, 0, 0);
        }

        /* Visualizes the figure-designer grid
         * Scaling up as far as possible within bounds while keeping all cells at an equal size
         */
        private void panelFigure_Paint(object sender, PaintEventArgs e)
        {
            if (panelFigure.Width == 0 ||
                panelFigure.Height == 0)
                return;
            

            Graphics g = e.Graphics;
            double cellScaleX = (double)panelFigure.Width / logic.FigureWidth;
            double cellScaleY = (double)panelFigure.Height / logic.FigureHeight;

            // Try using a Bitmap from an array to draw at once and multithread the graphics calculation

            Bitmap bmp = new Bitmap(panelFigure.Width, panelFigure.Height, PixelFormat.Format32bppArgb);
            Rectangle area = new Rectangle(0, 0, panelFigure.Width, panelFigure.Height);
            BitmapData bmpData = bmp.LockBits(area, ImageLockMode.WriteOnly, bmp.PixelFormat);

            int stride = bmpData.Stride;
            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(stride) * panelFigure.Height;
            byte[] pixelBuffer = new byte[bytes];

            // Parallel filling of pixelBuffer
            Parallel.For(0, panelFigure.Height, y =>
            {
                int rowStart = y * stride;

                for (int x = 0; x < panelFigure.Width; x++)
                {
                    int pixelIndex = rowStart + x * 4;
                    int xPos = (int)(x / cellScaleX);
                    int yPos = (int)(y / cellScaleY);
                    Point pos = new Point(xPos, yPos);

                    if ((int)(x % cellScaleX) == 0 || (int)(y % cellScaleY) == 0)
                    {
                        pixelBuffer[pixelIndex] = 0;    // Blue
                        pixelBuffer[pixelIndex + 1] = 0;    // Green
                        pixelBuffer[pixelIndex + 2] = 0;    // Red
                        pixelBuffer[pixelIndex + 3] = 255;  // Alpha
                    }
                    else if (logic.GetFigureCell(xPos, yPos))
                    {
                        pixelBuffer[pixelIndex] = 0;    // Blue
                        pixelBuffer[pixelIndex + 1] = 0;    // Green
                        pixelBuffer[pixelIndex + 2] = 0;    // Red
                        pixelBuffer[pixelIndex + 3] = 255;  // Alpha
                    }
                }
            });
            Marshal.Copy(pixelBuffer, 0, ptr, bytes);
            bmp.UnlockBits(bmpData);
            g.DrawImage(bmp, 0, 0);

        }

        /* Handles the interaction with the field
         * by either pasting the current model or switching the clicked cell using the corresponding logic functions
         */
        private void panelField_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (radioButtonPlaceCell.Checked)
                {
                    logic.SwitchCell(cursor.X, cursor.Y);
                }
                else if (radioButtonPlaceFig.Checked)
                {
                    logic.PasteFig(cursor.X - (logic.FigureWidth / 2), cursor.Y - (logic.FigureHeight / 2), checkBox_OverrideCells.Checked);
                }
                panelField.Invalidate();
            }
        }

        /* Handles the interaction with the figure designer grid
         * by calling the logic function to switch the cell at cursor position
         */
        private void panelFigure_MouseClick(object sender, MouseEventArgs e)
        {
            int cellScaleX = panelFigure.Width / logic.FigureWidth;
            int cellScaleY = panelFigure.Height / logic.FigureHeight;

            Point pos = e.Location;
            pos.X /= cellScaleX;
            pos.Y /= cellScaleY;

            logic.SwitchFigCell(pos.X, pos.Y);

            panelFigure.Invalidate();
        }

        /* Clears the figure
         * by calling the corresponding logic function
         */
        private void buttonClearFig_Click(object sender, EventArgs e)
        {
            logic.ClearFig();
            panelFigure.Invalidate();
        }

        /* Forces redrawal for the playing field upon rescale
         * 
         * Obsolete with Panel Snapping
         */
        private void panelField_SizeChanged(object sender, EventArgs e)
        {
            //panelField.Invalidate();
        }

        /* Forces redrawal for the figure designer grid upon rescale
         */
        private void panelFigure_SizeChanged(object sender, EventArgs e)
        {
            panelFigure.Invalidate();
        }

        /* Enables the drawing of the figure paste indicator
         */
        private void panelField_Enter(object sender, EventArgs e)
        {
            panelField.Focus();
            paintFig = true;
        }

        /* Disables the drawing of the figure paste indicator
         */
        private void panelField_Leave(object sender, EventArgs e)
        {
            paintFig = false;
            panelField.Invalidate();
        }

        /* Refreshes the stored cursor position when hovering over the panel
         */
        private void panelField_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragstartPoint.X != -1)
            {
                double contentWidth = logic.FieldWidth * CellScale().X;
                double contentHeight = logic.FieldHeight * CellScale().Y;

                double upperXLim = 0;
                double lowerXLim = (-1) * contentWidth + panelField.Width;
                double upperYLim = 0;
                double lowerYLim = (-1) * contentHeight + panelField.Height;

                Point oldOffset = offset;

                // Drag the startpoint with the cursor
                offset.X += (e.Location.X - dragstartPoint.X);
                if (offset.X > upperXLim)
                    offset.X = (int)upperXLim;
                else if (offset.X < lowerXLim)
                    offset.X = (int)lowerYLim;
                offset.Y += (e.Location.Y - dragstartPoint.Y);
                if (offset.Y > upperYLim)
                    offset.Y = (int)(upperYLim);
                else if (offset.Y < lowerYLim)
                    offset.Y = (int)(lowerYLim);

                paintFig = false;
                if (offset.X > oldOffset.X && offset.Y > oldOffset.Y)
                    panelField.Cursor = Cursors.PanSE;
                else if (offset.X < oldOffset.X && offset.Y < oldOffset.Y)
                    panelField.Cursor = Cursors.PanNW;
                else if (offset.X < oldOffset.X && offset.Y > oldOffset.Y)
                    panelField.Cursor = Cursors.PanSW;
                else if (offset.X > oldOffset.X && offset.Y < oldOffset.Y)
                    panelField.Cursor = Cursors.PanNE;
                else if (offset.X > oldOffset.X)
                    panelField.Cursor = Cursors.PanEast;
                else if (offset.X < oldOffset.X)
                    panelField.Cursor = Cursors.PanWest;
                else if (offset.Y > oldOffset.Y)
                    panelField.Cursor = Cursors.PanSouth;
                else if (offset.Y < oldOffset.Y)
                    panelField.Cursor = Cursors.PanNorth;

                dragstartPoint = e.Location;
                panelField.Invalidate();
            }
            RefreshCursor(e.Location);
        }

        /* RefreshCursor
         */
        private void RefreshCursor(Point p)
        {
            p.X -= offset.X;
            p.X = (int)(p.X / CellScale().X);
            p.Y -= offset.Y;
            p.Y = (int)(p.Y / CellScale().Y);
            if (p.X < 0) p.X = 0;
            if (p.Y < 0) p.Y = 0;

            if (p != cursor)
            {
                cursor = p;
                panelField.Invalidate();
                RefreshInfo();
            }
        }

        /* Handles the import of playing fields from a file
         * by using an OpenFileDialog and calling the corresponding logic function
         */
        private void buttonLoadField_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Levelpath;
                dialog.Filter = "level files (*.lvl)|*.lvl";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.LoadFieldFromFile(dialog.FileName);
                        zoom = 1;
                        offset = new Point(0, 0);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Error trying to load from file. \nIOException: {ex.Message}");
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show($"Error trying to load from file. \nFormatException: {ex.Message}");
                    }
                }
            }
            SnapPanels();
        }

        /* Handles the export of playing fields to a file
         * by using a SaveFileDialog and calling the corresponding logic function
         */
        private void buttonSaveField_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = Levelpath;
                dialog.Filter = "level files (*.lvl)|*.lvl";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveFieldToFile(dialog.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Error trying to save to file. \nIOException: {ex.Message}");
                    }
                }
            }
        }

        /* Handles the import of figures
         * (see above)
         */
        private void buttonLoadFig_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.InitialDirectory = Figpath;
                dialog.Filter = "figure files (*.fig)|*.fig";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.LoadFigFromFile(dialog.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Error trying to load from file. \nIOException: {ex.Message}");
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show($"Error trying to load from file. \nFormatException: {ex.Message}");
                    }
                }
                radioButtonPlaceFig.Checked = true;
                SnapPanels();
                panelFigure.Invalidate();
            }
        }

        /* Handles the export of figures
         * (see above)
         */
        private void buttonSaveFig_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = Figpath;
                dialog.Filter = "figure files (*.fig)|*.fig";
                dialog.FilterIndex = 1;
                dialog.RestoreDirectory = false;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveFigToFile(dialog.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show($"Error trying to save to file. \nIOException: {ex.Message}");
                    }
                }
            }
        }

        /* Helper function to copy files from one directory to another without overriding
         * 
         * Params:
         * sourceDir - source directory
         * targetDir - target directory
         */
        void CopyDirectorySafe(string sourceDir, string targetDir)
        {
            if (!Directory.Exists(targetDir))
            {
                Directory.CreateDirectory(targetDir);
            }

            foreach (var filePath in Directory.GetFiles(sourceDir))
            {
                string fileName = Path.GetFileName(filePath);
                string destFilePath = Path.Combine(targetDir, fileName);

                if (!File.Exists(destFilePath))
                {
                    File.Copy(filePath, destFilePath);
                }
            }

            foreach (var directoryPath in Directory.GetDirectories(sourceDir))
            {
                string dirName = Path.GetFileName(directoryPath);
                string destSubDir = Path.Combine(targetDir, dirName);

                CopyDirectorySafe(directoryPath, destSubDir);
            }
        }

        /* Snaps both grids to rescale in sizes that are multiples of the field size
         * Should become partially obsolete for playing field when zoom is introduced
         * TODO: Remove / Edit snapping for zoom
         * 
         * Params:
         * stop (false) - if called with true, the method is called once, if false, twice
         */
        private void SnapPanels()
        {
            if (logic == null || panelField == null || panelFigure == null)
                return;

            int fitX = (int)((mainGrid.Width * 0.6 * zoom) / (logic.FieldWidth));
            int fitY = (int)((mainGrid.Height * zoom) / (logic.FieldHeight));

            int snappedWidth = Math.Min((int)(mainGrid.Width * 0.6), (int)(logic.FieldWidth * fitX));
            int snappedHeight = Math.Min(mainGrid.Height, (int)(logic.FieldHeight * fitY));

            // aspect ratio
            double aspectRatio = (double)logic.FieldWidth / (double)logic.FieldHeight;
            if ((double)snappedWidth / snappedHeight > aspectRatio)
            {
                snappedWidth = (int)(snappedHeight * aspectRatio);
            }
            else if ((double)snappedWidth / snappedHeight < aspectRatio)
            {
                snappedHeight = (int)(snappedWidth / aspectRatio);
            }

            // Set new width and height, place panel in the center
            panelField.Height = snappedHeight;
            panelField.Width = snappedWidth;
            if (mainGrid.GetColumnWidths().Length > 0)
                panelFigure.Location = new Point((mainGrid.GetColumnWidths()[1] - snappedWidth) / 2, (mainGrid.GetRowHeights()[0] - snappedHeight) / 2);

            // figure designer grid

            fitX = rightGrid.GetColumnWidths()[0] / logic.FigureWidth;
            fitY = rightGrid.GetRowHeights()[0] / logic.FigureHeight;

            snappedWidth = Math.Min(rightGrid.GetColumnWidths()[0], logic.FigureWidth * fitX);
            snappedHeight = Math.Min(rightGrid.GetRowHeights()[0], logic.FigureHeight * fitY);

            // aspect ratio
            aspectRatio = (double)logic.FigureWidth / logic.FigureHeight;
            if ((double)snappedWidth / snappedHeight > aspectRatio)
            {
                snappedWidth = (int)Math.Round(snappedHeight * aspectRatio);
            }
            else if ((double)snappedWidth / snappedHeight < aspectRatio)
            {
                snappedHeight = (int)Math.Round(snappedWidth / aspectRatio);
            }

            panelFigure.Width = snappedWidth;
            panelFigure.Height = snappedHeight;
            panelFigure.Location = new Point((rightGrid.GetColumnWidths()[0] - snappedWidth) / 2, (rightGrid.GetRowHeights()[0] - snappedHeight) / 2);

            panelField.Invalidate();
            panelFigure.Invalidate();
        }

        /* Override the resize method to snap panels (and fix the aspect ratio)
         */
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            SnapPanels();
        }

        /* Handles the zoom changes and generates a new offset based on the cursor location
         */
        private void Zoom(double factor, PointF mouse)
        {
            double oldZoom = zoom;
            zoom *= factor;
            if (zoom < 1)
            {
                zoom = 1;
                offset.X = offset.Y = 0;
            }
            if (zoom > 500)
            {
                zoom = oldZoom;
            }
            if (zoom != oldZoom)
            {
                // Update offset so that content under cursor stays fixed
                offset.X = (int)Math.Round(mouse.X - ((zoom / oldZoom) * (mouse.X - offset.X)));
                offset.Y = (int)Math.Round(mouse.Y - ((zoom / oldZoom) * (mouse.Y - offset.Y)));

                double contentWidth = logic.FieldWidth * CellScale().X;
                double contentHeight = logic.FieldHeight * CellScale().Y;

                double upperXLim = 0;
                double lowerXLim = (-1) * contentWidth + panelField.Width;
                double upperYLim = 0;
                double lowerYLim = (-1) * contentHeight + panelField.Height;

                offset.X = (int)Math.Round(Math.Min(offset.X, upperXLim));
                offset.X = (int)Math.Round(Math.Max(offset.X, lowerXLim));
                offset.Y = (int)Math.Round(Math.Min(offset.Y, upperYLim));
                offset.Y = (int)Math.Round(Math.Max(offset.Y, lowerYLim));


                if (upperXLim < lowerXLim) offset.X = 0;
                if (upperYLim < lowerYLim) offset.Y = 0;

                SnapPanels();
                RefreshInfo();
            }
        }

        /* Zooms in or out when mousewheel is scrolled
         * by calling the Zoom method
         */
        private void mainGrid_MouseWheel(object sender, MouseEventArgs e)
        {
            double factor = zoomSpeed;
            if (e.Delta < 0)
                factor = 1 / factor;
            // Mouse Pos over fieldPanel or center of grid
            Zoom(factor, panelField.PointToClient(Cursor.Position));
        }

        /* Initiates the figure copying process upon holding down right click
         */
        private void panelField_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Startpoint to copy
                Point p = e.Location;
                p.X -= offset.X;
                p.Y -= offset.Y;
                p.X = (int)(p.X / CellScale().X);
                p.Y = (int)(p.Y / CellScale().Y);
                copystartPoint = p;
            }
            else if (e.Button == MouseButtons.Middle)
            {
                // Start Dragging
                dragstartPoint = e.Location;
            }
        }

        /* Handles the figure copying when right click is released
         * by calling the corresponding logic function
         */
        private void panelField_MouseUp(object sender, MouseEventArgs e)
        {
            // Endpoint to copy
            if (e.Button == MouseButtons.Right)
            {
                Point p = e.Location;
                p.X -= offset.X;
                p.Y -= offset.Y;
                p.X = (int)(p.X / CellScale().X);
                p.Y = (int)(p.Y / CellScale().Y);
                logic.FigFromCoords(copystartPoint, p);
                panelFigure.Invalidate();
                panelField.Invalidate();
                copystartPoint.X = copystartPoint.Y = -1;
                SnapPanels();
            }
            else if (e.Button == MouseButtons.Middle)
            {
                dragstartPoint.X = dragstartPoint.Y = -1;
                paintFig = true;
                panelField.Cursor = Cursors.Default;
            }
        }

        /* Opens the settings menu
         * by hiding the main panel and showing the options panel
         */
        private void buttonOptions_Click(object sender, EventArgs e)
        {
            timerGameTick.Enabled = false;
            mainGrid.Hide();
            menuGrid.Hide();
            textBoxFieldSize.Text = logic.FieldWidth + "," + logic.FieldHeight;
            textBoxFigSize.Text = logic.FigureWidth + "," + logic.FigureHeight;
            textBoxTimerSpeed.Text = timerGameTick.Interval + "";
            checkBoxUseGrid.Checked = drawGrid;

            switch (((Button)sender).Name)
            {
                case "buttonOptions": last = mainGrid; break;
                case "buttonOptionsMM": last = menuGrid; break;
                default: break;
            }
            optionsGrid.Show();
            RefreshInfo();
            buttonPause.Text = loc.GetText("ingame_resume");
        }

        /* Returns to the main game view
         * (see above)
         */
        private void buttonBack_Click(object sender, EventArgs e)
        {
            optionsGrid.Hide();

            last?.Show();
        }

        /* Changes the Field Size
         * by calling the corresponding logic function
         */
        private void buttonApplyFieldSize_Click(object sender, EventArgs e)
        {
            string source = textBoxFieldSize.Text;
            string[] boundstrings = source.Split(',');
            int Width = -1;
            int Height = -1;
            if (boundstrings.Length > 2 || boundstrings.Length == 0)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_dimensions"));
                textBoxFieldSize.Text = logic.FieldWidth + "," + logic.FieldHeight;
                return;
            }
            try
            {
                Width = Height = int.Parse(boundstrings[0]);
                if (boundstrings.Length == 2)
                    Height = int.Parse(boundstrings[1]);
            }
            catch
            {
                MessageBox.Show(loc.GetText("prompt_invalid_format"));
                textBoxFieldSize.Text = logic.FieldWidth + "," + logic.FieldHeight;
                return;
            }
            if (Width < 10 || Width > 10000 || Width < logic.FigureWidth)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_range"));
                textBoxFieldSize.Text = logic.FieldWidth + "," + logic.FieldHeight;
                return;
            }
            if (Height < 10 || Height > 10000 || Height < logic.FigureHeight)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_range"));
                textBoxFieldSize.Text = logic.FieldWidth + "," + logic.FieldHeight;
                return;
            }
            if (MessageBox.Show(loc.GetText("prompt_override_field"), loc.GetText("options_apply"), MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                logic.ClearField(Width, Height);
                SnapPanels();
                MessageBox.Show(loc.GetText("prompt_fieldsize_changed"));
            }
        }

        /* Changes the Figure Size
         * by calling the corresponding logic funtion
         */
        private void buttonApplyFigSize_Click(object sender, EventArgs e)
        {
            string source = textBoxFigSize.Text;
            string[] boundstrings = source.Split(',');
            int Width = -1;
            int Height = -1;
            if (boundstrings.Length > 2 || boundstrings.Length == 0)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_dimensions"));
                textBoxFigSize.Text = logic.FieldWidth + "," + logic.FieldHeight;
                return;
            }
            try
            {
                Width = Height = int.Parse(boundstrings[0]);
                if (boundstrings.Length == 2)
                    Height = int.Parse(boundstrings[1]);
            }
            catch
            {
                MessageBox.Show(loc.GetText("prompt_invalid_format"));
                textBoxFigSize.Text = logic.FigureWidth + "," + logic.FigureHeight;
                return;
            }
            if (Width < 2 || Width > 100 || Width > logic.FieldWidth)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_range"));
                textBoxFigSize.Text = logic.FigureWidth + "," + logic.FigureHeight;
                return;
            }
            if (Height < 2 || Height > 100 || Height > logic.FieldHeight)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_range"));
                textBoxFigSize.Text = logic.FigureWidth + "," + logic.FigureHeight;
                return;
            }
            logic.ClearFig(Width, Height);
            MessageBox.Show(loc.GetText("prompt_figsize_changed"));
            SnapPanels();
        }

        /* Changes the MS per Tick (timer interval)
         */
        private void buttonTimerSpeed_Click(object sender, EventArgs e)
        {
            string source = textBoxTimerSpeed.Text;
            int tps = -1;
            try
            {
                tps = int.Parse(source);
            }
            catch
            {
                MessageBox.Show(loc.GetText("prompt_invalid_format"));
                textBoxTimerSpeed.Text = "" + timerGameTick.Interval;
                return;
            }
            if (tps < 50 || tps > 2000)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_range"));
                textBoxTimerSpeed.Text = "" + timerGameTick.Interval;
                return;
            }
            timerGameTick.Interval = tps;
            MessageBox.Show(loc.GetText("prompt_mspertick_changed"));
        }

        /* Changes the Scroll Speed
         */
        private void buttonScrollSpeed_Click(object sender, EventArgs e)
        {
            string source = textBoxScrollSpeed.Text;
            int scs = -1;
            try
            {
                scs = int.Parse(source);
            }
            catch
            {
                MessageBox.Show(loc.GetText("prompt_invalid_format"));
                textBoxScrollSpeed.Text = "" + scrollSpeed;
                return;
            }
            if (scs < 1 || scs > 50)
            {
                MessageBox.Show(loc.GetText("prompt_invalid_range"));
                textBoxScrollSpeed.Text = "" + scrollSpeed;
                return;
            }
            scrollSpeed = scs;
            MessageBox.Show(loc.GetText("prompt_scrollspeed_changed"));
        }

        /* Alternatively allows applying settings by hitting enter
         */
        private void textBoxFieldSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonApplyFieldSize_Click(sender, e);
                e.Handled = true;
            }
        }

        /* Alternatively allows applying settings by hitting enter
         */
        private void textBoxFigSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonApplyFigSize_Click(sender, e);
                e.Handled = true;
            }
        }

        /* Alternatively allows applying settings by hitting enter
         */
        private void textBoxTimerSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonTimerSpeed_Click(sender, e);
                e.Handled = true;
            }
        }

        /* Alternatively allows applying settings by hitting enter
         */
        private void textBoxScrollSpeed_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonScrollSpeed_Click(sender, e);
                e.Handled = true;
            }
        }

        /* Changes the offset according to the scroll values
         */
        private void ScrollField()
        {
            if (panelField == null)
                return;
            Point oldOffset = offset;

            double contentWidth = logic.FieldWidth * CellScale().X;
            double contentHeight = logic.FieldHeight * CellScale().Y;

            double upperXLim = 0; // contentWidth - panelField.Width;
            double lowerXLim = (-1) * contentWidth + panelField.Width;
            double upperYLim = 0; // contentHeight - panelField.Height;
            double lowerYLim = (-1) * contentHeight + panelField.Height;

            if (scroll[1])
            {
                offset.Y -= (int)(scrollSpeed * zoom);
                offset.Y = (int)Math.Max(offset.Y, lowerYLim);
            }
            if (scroll[0])
            {
                offset.Y += (int)(scrollSpeed * zoom);
                offset.Y = (int)Math.Min(offset.Y, upperYLim);
            }
            if (scroll[3])
            {
                offset.X -= (int)(scrollSpeed * zoom);
                offset.X = (int)Math.Max(offset.X, lowerXLim);
            }
            if (scroll[2])
            {
                offset.X += (int)(scrollSpeed * zoom);
                offset.X = (int)(Math.Min(offset.X, upperXLim));
            }

            if (oldOffset != offset)
                panelField.Invalidate();
        }

        /* Sets Scroll directions based on keys
         */
        private void panelField_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: scroll[0] = true; break;
                case Keys.Down: scroll[1] = true; break;
                case Keys.Left: scroll[2] = true; break;
                case Keys.Right: scroll[3] = true; break;
                case Keys.Add: Zoom(zoomSpeed, new Point(panelField.Width / 2, panelField.Height / 2)); break;
                case Keys.Subtract: Zoom(1 / zoomSpeed, new Point(panelField.Width / 2, panelField.Height / 2)); break;
            }
        }

        /* Resets scroll directions based on keys
         */
        private void panelField_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up: scroll[0] = false; break;
                case Keys.Down: scroll[1] = false; break;
                case Keys.Left: scroll[2] = false; break;
                case Keys.Right: scroll[3] = false; break;
            }
        }

        /* Calls continuous UI functions every 100 ms
         * like ScrollField
         */
        private void timerUIFunctions_Tick(object sender, EventArgs e)
        {
            ScrollField();
        }

        /* Enables or disables the grid view
         */
        private void checkBoxUseGrid_CheckedChanged(object sender, EventArgs e)
        {
            drawGrid = checkBoxUseGrid.Checked;
        }

        /* Enables or disables the OverrideCells checkbox depending on the place type
         */
        private void radioButtonPlaceFig_CheckedChanged(object sender, EventArgs e)
        {
            checkBox_OverrideCells.Enabled = radioButtonPlaceFig.Checked;
            radioButtonPlaceCell.TabStop = true;
            radioButtonPlaceFig.TabStop = true;
        }

        /* Spins the figure by 90°
         * by calling the corresponing logic function
         */
        private void buttonSpin90_Click(object sender, EventArgs e)
        {
            logic.SpinFig();
            SnapPanels();

        }

        /* Mirrors the figure across the X-axis
         * by calling the corresponding logic function
         */
        private void buttonMirrorX_Click(object sender, EventArgs e)
        {
            logic.MirrorFigX(0);
            SnapPanels();
        }

        /* Mirrors the figure across the Y-axis
         * (see above)
         */
        private void buttonMirrorY_Click(object sender, EventArgs e)
        {
            logic.MirrorFigX(1);
            SnapPanels();
        }

        /* Changes view from Main Menu to Main Game
         */
        private void buttonPlay_Click(object sender, EventArgs e)
        {
            menuGrid.Hide();
            mainGrid.Show();
            SnapPanels();
            timerUIFunctions.Start();
        }

        /* Loads a game from Main Menu (currently just loading a map and changing view)
         */
        private void buttonLoadState_Click(object sender, EventArgs e)
        {
            buttonLoadField_Click(sender, e);
            menuGrid.Hide();
            mainGrid.Show();
            SnapPanels();
            timerUIFunctions.Start();
        }

        /* Ends the application
         */
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /* Changes view from Main Game to Main Menu
         */
        private void buttonMenu_Click(object sender, EventArgs e)
        {
            timerGameTick.Stop();
            buttonPause.Text = loc.GetText("ingame_resume");
            RefreshInfo();

            mainGrid.Hide();
            menuGrid.Show();
            timerUIFunctions.Stop();
        }

        /* Resizes a given Image using LockBits
         */
        private static Bitmap ResizeImage(Image sourceImage, int targetWidth, int targetHeight)
        {
            Bitmap source = new Bitmap(sourceImage);
            Bitmap result = new Bitmap(targetWidth, targetHeight, source.PixelFormat);

            // Lock source bitmap
            Rectangle srcRect = new Rectangle(0, 0, source.Width, source.Height);
            BitmapData srcData = source.LockBits(srcRect, ImageLockMode.ReadOnly, source.PixelFormat);

            // Lock result bitmap
            Rectangle dstRect = new Rectangle(0, 0, targetWidth, targetHeight);
            BitmapData dstData = result.LockBits(dstRect, ImageLockMode.WriteOnly, result.PixelFormat);

            double factorX = (double)source.Width / targetWidth;
            double factorY = (double)source.Height / targetHeight;

            IntPtr srcStart = srcData.Scan0;
            IntPtr dstStart = dstData.Scan0;

            int stride = dstData.Stride;
            int srcStride = srcData.Stride;

            int bytes = Math.Abs(stride) * dstData.Height;
            int srcBytes = Math.Abs(srcStride) * srcData.Height;
            byte[] pixelBuffer = new byte[bytes];
            byte[] sourceArr = new byte[srcBytes];

            Marshal.Copy(srcData.Scan0, sourceArr, 0, srcBytes);

            for (int y = 0; y < targetHeight; y++)
            {
                int srcY = (int)(y * factorY);
                int rowStart = y * stride;
                int rowStartSrc = srcY * srcStride;

                for (int x = 0; x < targetWidth; x++)
                {
                    int pixelIndex = rowStart + x * 4;
                    int srcX = rowStartSrc + ((int)(x * factorX)) * 4;

                    for (int i = 0; i < 4; i++)
                    {
                        pixelBuffer[pixelIndex + i] = sourceArr[srcX + i];
                    }
                }
            }

            Marshal.Copy(pixelBuffer, 0, dstStart, bytes);
            source.UnlockBits(srcData);
            result.UnlockBits(dstData);

            return result;
        }

        /* Resizes the Banner Image when the window size changes
         */
        private void pictureBoxBanner_SizeChanged(object sender, EventArgs e)
        {
            pictureBoxBanner.Image = ResizeImage(pictureBoxBanner.InitialImage, pictureBoxBanner.Width, pictureBoxBanner.Height);
        }

        /* Draws icons in front of the languages to select from
         */
        public void comboBoxLanguage_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index < 0) return;


            double factor = (double) Properties.Resources.en.Width / Properties.Resources.en.Height;
            int h = comboBoxLanguage.ItemHeight - 2;
            imageList.ImageSize = new Size((int)Math.Ceiling(h * factor), h);

            // Draw icon
            imageList.Draw(e.Graphics, e.Bounds.Left + 1, e.Bounds.Top + 1, e.Index);

            // Draw text shifted right by icon width
            object? obj = comboBoxLanguage.Items[e.Index];
            Font? font = e.Font;
            if (obj != null && font != null)
            {

                e.Graphics.DrawString(obj.ToString(), font, Brushes.Black,
                    e.Bounds.Left + imageList.ImageSize.Width + 2, e.Bounds.Top);

                e.DrawFocusRectangle();
            }
        }

        /* Handles the selection of a different language
         * by calling the corresponding function
         */
        private void comboBoxLanguage_SelectedIndexChanged(object? sender, EventArgs e)
        {
            Lang? l = (comboBoxLanguage.SelectedItem as Lang);
            if (l != null)
            {
                ChangeLanguage(l.Code);
            }
        }

        /* Changes the client language
         * manages the Localization object
         */
        private void ChangeLanguage(string language)
        {
            loc.SetLanguage(language);

            foreach (Lang ll in languages)
            {
                ll.Name = loc.GetText("lang_" + ll.Code);
            }
            comboBoxLanguage.SelectedIndexChanged -= comboBoxLanguage_SelectedIndexChanged;
            for (int i = 0; i < languages.Length; i++)
            {
                comboBoxLanguage.Items[i] = languages[i];
            }
            comboBoxLanguage.SelectedIndexChanged += comboBoxLanguage_SelectedIndexChanged;

            buttonPlay.Text = loc.GetText("menu_freeplay");
            buttonCampaign.Text = loc.GetText("menu_campaign");

            buttonLoadState.Text = loc.GetText("menu_load");
            buttonOptionsMM.Text = loc.GetText("menu_options");
            buttonQuit.Text = loc.GetText("menu_quit");

            if (logic.Generation == 0)
                buttonPause.Text = loc.GetText("ingame_start");
            else
                buttonPause.Text = loc.GetText("ingame_resume");

            buttonGenerateField.Text = loc.GetText("ingame_generate");
            buttonClearField.Text = loc.GetText("ingame_clear_field");
            buttonLoadField.Text = loc.GetText("ingame_load_field");
            buttonSaveField.Text = loc.GetText("ingame_save_field");
            buttonMenu.Text = loc.GetText("ingame_menu");
            buttonSpin90.Text = loc.GetText("ingame_rotate");
            buttonMirrorX.Text = loc.GetText("ingame_mirror_x");
            buttonMirrorY.Text = loc.GetText("ingame_mirror_y");
            radioButtonPlaceCell.Text = loc.GetText("ingame_place_cell");
            radioButtonPlaceFig.Text = loc.GetText("ingame_place_fig");
            checkBox_OverrideCells.Text = loc.GetText("ingame_pastedead");
            buttonClearFig.Text = loc.GetText("ingame_clear_fig");
            buttonLoadFig.Text = loc.GetText("ingame_load_fig");
            buttonSaveFig.Text = loc.GetText("ingame_save_fig");
            buttonOptions.Text = loc.GetText("ingame_options");
            labelLanguage.Text = loc.GetText("options_lang");
            labelFieldSize.Text = loc.GetText("options_fieldsize_label");
            labelFigSize.Text = loc.GetText("options_figsize_label");
            labelTimerSpeed.Text = loc.GetText("options_mspertick_label");
            labelScrollSpeed.Text = loc.GetText("options_scrollspeed_label");
            labelUseGrid.Text = loc.GetText("options_showgrid_label");
            checkBoxUseGrid.Text = loc.GetText("options_showgrid_checkbox");
            buttonBack.Text = loc.GetText("options_back");
            buttonApplyFieldSize.Text = loc.GetText("options_apply");
            buttonApplyFigSize.Text = loc.GetText("options_apply");
            buttonTimerSpeed.Text = loc.GetText("options_apply");
            buttonScrollSpeed.Text = loc.GetText("options_apply");

        }
        #endregion
    }
}