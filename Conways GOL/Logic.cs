using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conways_GOL
{
    public class Logic
    {
        #region fields
        private readonly int fieldSize;
        private readonly int figSize;
        private bool placeFig;
        private bool[,] field;
        private bool[,] figure;
        #endregion

        #region Constructors
        public Logic() : this(100, 5)
        {
        }

        public Logic(int fieldSize, int figSize)
        {
            this.Generation = 0;
            this.fieldSize = fieldSize;
            this.figSize = figSize;
            this.placeFig = false;
            this.field = new bool[fieldSize, fieldSize];
            this.figure = new bool[figSize, figSize];
        }
        #endregion

        #region Properties
        public int FieldWidth
        {
            get { return field.GetLength(0); }
        }
        public int FieldHeight
        {
            get { return field.GetLength(1); }
        }
        public int FigureWidth
        {
            get { return figure.GetLength(0); }
        }
        public int FigureHeight
        {
            get { return figure.GetLength(1); }
        }
        public bool PlaceMode
        {
            get { return placeFig; }
            set { placeFig = value; }
        }
        public int Generation
        { get; set; }
        #endregion

        #region Functions
        /* Returns the status of the cell at a given position in the field
         */
        public bool GetCell(int x, int y)
        {
            if (x >= field.GetLength(0) || y >= field.GetLength(1) || x < 0 || y < 0)
            {
                return false;
            }
            return field[x, y];
        }
        public bool GetCell(double x, double y)
        {
            if (x >= field.GetLength(0)  || y >= field.GetLength(1) || x == double.NaN || y == double.NaN)
                return false;
            return field[(int) Math.Floor(x),(int) Math.Floor(y)];
        }

        /* Returns the status of the cell at a given position in the figure
         */
        public bool GetFigureCell(int x, int y)
        {
            if (x < 0 || y < 0 || x >= FigureWidth || y >= FigureHeight)
            {
                return false;
            }
            else
                return figure[x, y];
        }

        /* Manually changes the specified cell in the field
         */
        public void SwitchCell(int x, int y)
        {
            field[Torus(x, 0), Torus(y, 1)] = !field[Torus(x, 0), Torus(y, 1)];
        }

        /* Manually changes the specified cell in the figure
         */
        public void SwitchFigCell(int x, int y)
        {
            if (x < figure.GetLength(0)  && y < figure.GetLength(1))
            figure[x, y] = !figure[x, y];
        }

        /* Clears the figure
         * (kills all cells)
         * 
         * Params:
         * width, height - if apparent, uses an empty array with the given bounds as new figure
         */
        public void ClearFig()
        {
            figure = new bool[figure.GetLength(0),figure.GetLength(1)];
        }
        public void ClearFig(int width,  int height)
        {
            figure = new bool[width, height];
        }

        /* Spins the figure by 90 degree
         * [1][2][3]
         * [4][5][6]
         * ->
         * [4][1]
         * [5][2]
         * [6][3]
         */
        public void SpinFig()
        {
            bool[,] newFig = new bool[figure.GetLength(1), figure.GetLength(0)];
            for (int x = 0;  x < newFig.GetLength(0); x++)
            {
                for (int y = 0; y < newFig.GetLength(1); y++)
                {
                    newFig[x, y] = figure[y, figure.GetLength(1) - 1 - x];
                }
            }
            figure = newFig;
        }

        /* Mirrors the figure across the given Axis
         * 
         * Params:
         * dimension - 0 for X, 1 for Y
         */
        public void MirrorFigX(int dimension)
        {
            if (dimension < 0 || dimension > 1) return;
            bool[,] newFig = new bool[figure.GetLength(0), figure.GetLength(1)];
            for (int x = 0;x < newFig.GetLength(0);x++)
            {
                for (int y = 0;y < newFig.GetLength(1);y++)
                {
                    if (dimension == 0)
                        newFig[x, y] = figure[figure.GetLength(0) - 1 - x, y];
                    else
                        newFig[x, y] = figure[x, figure.GetLength(1) - 1 - y];
                }
            }
            figure = newFig;
        }

        /* Pastes the figure at the specified position in the field
         * 
         * Params:
         * xPos - x position in the field to start pasting
         * yPos - y position in the field to start pasting
         * pasteEmpty - true if empty cells should be copied to the field aswell
         */
        public void PasteFig(int xPos, int yPos, bool pasteEmpty)
        {
            for (int xFig = 0; xFig < figure.GetLength(0); xFig++)
            {
                for (int yFig = 0; yFig < figure.GetLength(1); yFig++)
                {
                    int pasteX = Torus(xPos + xFig, 0);
                    int pasteY = Torus(yPos + yFig, 1);

                    if (pasteEmpty)
                    {
                        field[pasteX, pasteY] = figure[xFig, yFig];
                    }
                    else if (figure[xFig, yFig])
                    {
                        field[pasteX, pasteY] = true;
                    }
                }
            }
        }

        /* Returns the correct position for any relative coordinates (e.g beyond the edges)
         * 
         * Params:
         * n - the position to find the correct Torus value for
         * dimension - the dimension of the field array the value is taken from ( either 0 for X or 1 for Y )
         */
        public int Torus(int n, int dimension)
        {
            if (n < 0)
            {
                return n + field.GetLength(dimension);
            }
            else if (n >= field.GetLength(dimension)) 
            {
                return n - field.GetLength(dimension);
            }
            return n;
        }

        /* Clears the field
         * (kills all cells)
         * 
         * Params:
         * width, height - if present, use as new field size
         */
        public void ClearField()
        {
            field = new bool[field.GetLength(0), field.GetLength(1)];
        }
        public void ClearField(int width, int height)
        {
            field = new bool[width, height];
        }

        /* Randomly generates a new field
         * 
         * Params:
         * percentAlive - the percentage of cells that should be alive after generation (recommended 50%)
         */
        public void GenerateField(double percentAlive = 50)
        {
            Random random = new Random();
            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    field[x, y] = (random.NextDouble() * 100 < percentAlive);
                }
            }

        }

        /* Call this in the GUI to simulate 1 Game Tick
         * Handles everything that needs to be processed to resolve a game loop
         */
        public void Tick()
        {
            TickCells();
            Generation++;
        }

        /* returns the amount of living direct neighbours for the cell at given coordinates
         * in this pattern:
         * [X][X][X]
         * [X][ ][X]
         * [X][X][X]
         *
         * Params:
         * X - X position in grid to check
         * Y - Y position in grid to check
         * 
         * Returns a value between 0 and 8
         */
        private int GetNeighbours(int x, int y)
        {
            int result = 0;
            
            for (int checkX  = x - 1; checkX <= x + 1; checkX++)
            {
                for (int checkY = y - 1; checkY <= y + 1; checkY++)
                {
                    // Add 1 if cell is alive
                    if (!(x == checkX && y == checkY) && field[Torus(checkX, 0), Torus(checkY, 1)])
                        result++;                
                }
            }
            return result;
        }

        /* Handles cells upon Tick (calculates the next generation) 
         * (Applies rules for Game Of Life here)
         * 
         * Rearanges the main grid after calculating at once
         */
        private void TickCells()
        {
            bool[,] newField = new bool[field.GetLength(0), field.GetLength(1)];
            // Multithreading for each row to boost performance:
            Parallel.For(0, field.GetLength(0), x =>
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    int n = GetNeighbours(x, y);
                    if (n == 2)
                    {
                        newField[x, y] = field[x, y];
                    }
                    else if (n == 3)
                    {
                        newField[x, y] = true;
                    }
                    else
                    {
                        newField[x, y] = false;
                    }
                }
            });
            // Replace the field with the next generation
            field = newField;
        }

        /* Saves the current field to a file
         * 
         * Using the following format:
         * Width, Height
         * Row 1 (comma separated)
         * Row 2 (comma separated)
         * Row 3 (comma separated)
         * ...
         * 
         * Can throw IOException
         * 
         * Params:
         * filename - full path to the document to save to
         */
        public void SaveFieldToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(FieldWidth + "," + FieldHeight);

                for (int y = 0; y < field.GetLength(1); y++)
                {
                    for (int x = 0; x < field.GetLength(0); x++)
                    {
                        writer.Write(field[x, y]);
                        if (x < field.GetLength(0) - 1)
                            writer.Write(",");
                    }
                    writer.WriteLine();
                }
            }
        }

        /* Loads the current field from a file
         * 
         * (see above)
         * Can also throw FormatException (if someone altered the .lvl file)
         */
        public void LoadFieldFromFile(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string? firstLine = sr.ReadLine();
                if (firstLine == null)
                    throw new FormatException("Could not read array size from file. Read line was null.");
                
                string[] boundstring = firstLine.Split(",");
                int[] bounds = new int[boundstring.Length];
                
                bounds[0] = int.Parse(boundstring[0]);
                bounds[1] = int.Parse(boundstring[1]);
                

                bool[,] newField = new bool[bounds[0], bounds[1]];
                for (int y = 0; y < bounds[1]; y++)
                {
                    string? line = sr.ReadLine();
                    if (line == null)
                        throw new FormatException("Could not read cell row from file. Read line was null.");
                    string[] row = line.Split(",");
                    for (int x = 0; x < row.Length; x++)
                    {
                        newField[x,y] = bool.Parse(row[x]);
                    }
                }
                field = newField;
            }
        }

        /* Saves the current figure to a file
         * 
         * Using the following format:
         * Width, Height
         * Row 1 (comma separated)
         * Row 2 (comma separated)
         * Row 3 (comma separated)
         * ...
         * 
         * Can throw IOException
         * 
         * Params:
         * filename - full path to the document to save to
         */
        public void SaveFigToFile(string filename)
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine(FigureWidth + "," + FigureHeight);

                for (int y = 0; y < figure.GetLength(1); y++)
                {
                    for (int x = 0; x < figure.GetLength(0); x++)
                    {
                        writer.Write(figure[x, y]);
                        if (x < figure.GetLength(0) - 1)
                            writer.Write(",");
                    }
                    writer.WriteLine();
                }
            }
        }

        /* Loads the current field from a file
         * 
         * (see above)
         * Can also throw FormatException (if someone altered the .fig file)
         */
        public void LoadFigFromFile(string filename)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                string? firstLine = sr.ReadLine();
                if (firstLine == null)
                    throw new FormatException("Could not read array size from file. Read line was null.");

                string[] boundstring = firstLine.Split(",");
                int[] bounds = new int[boundstring.Length];

                bounds[0] = int.Parse(boundstring[0]);
                bounds[1] = int.Parse(boundstring[1]);


                bool[,] newFig = new bool[bounds[0], bounds[1]];
                for (int y = 0; y < bounds[1]; y++)
                {
                    string? line = sr.ReadLine();
                    if (line == null)
                        throw new FormatException("Could not read cell row from file. Read line was null.");
                    string[] row = line.Split(",");
                    for (int x = 0; x < row.Length; x++)
                    {
                        newFig[x, y] = bool.Parse(row[x]);
                    }
                }
                figure = newFig;
            }
        }

        /* Creates a copy of the marked section in the playing field as the current figure
         * 
         * Params:
         * startPoint, endPoint - points to determine the edges of the rectangular copy section
         */
        public void FigFromCoords(Point startPoint, Point endPoint)
        {
            int sizeX = Math.Abs(endPoint.X - startPoint.X);
            int sizeY = Math.Abs(endPoint.Y - startPoint.Y);
            int fromX = Math.Min(endPoint.X, startPoint.X);
            int fromY = Math.Min(endPoint.Y, startPoint.Y);
            if (sizeX < 1 || sizeY < 1) return;
            figure = new bool[sizeX, sizeY];
            for (int x = 0; x < sizeX; x++)
            {
                for (int y = 0; y < sizeY; y++)
                {
                    figure[x, y] = field[Torus(x + fromX,0), Torus(y + fromY,1)];
                }
            }
        }

        /* Checks if a given point is inside the rect on the toroid field
         */
        public bool IsInsideRectOnField(Point p, int rX, int rY, int rW, int rH)
        {
            int x1 = Torus(rX, 0);
            int y1 = Torus(rY, 1);
            int x2 = Torus(rX + rW, 0);
            int y2 = Torus(rY + rH, 1);

            int px = Torus(p.X, 0);
            int py = Torus(p.Y, 1);

            // Check X-axis inclusion
            bool insideX;
            if (x1 <= x2)
                insideX = (px >= x1) && (px < x2);
            else
                insideX = (px >= x1) || (px < x2);

            // Check Y-axis inclusion
            bool insideY;
            if (y1 <= y2)
                insideY = (py >= y1) && (py < y2);
            else
                insideY = (py >= y1) || (py < y2);

            return insideX && insideY;
        }
        #endregion
    }
}
