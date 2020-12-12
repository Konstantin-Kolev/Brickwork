using System;
using System.Linq;

namespace Brickwork
{
    /// <summary>
    /// The basic class that stores the layout of the layer and holds all the logic for it.
    /// </summary>
    public class BrickLayer
    {
        //Properties that hold the size of the layer for easier to read and write code.
        private int rows;
        private int columns;

        //The array which holds the values that represent the layout of the bricks in the layer. 
        private int[,] layout;

        /// <summary>
        /// The positions of the bricks in the layer.
        /// </summary>
        public int[,] Layout
        {
            get { return layout; }
            set { layout = value; }
        }

        /// <summary>
        /// <para>The constructor for the class.</para>
        /// <para>Validates the values for the size.</para>
        /// </summary>
        /// <param name="n">The number of rows the layer will have.</param>
        /// <param name="m">The number of columns the layer wll have.</param>
        public BrickLayer(int n, int m)
        {
            while (!this.ValidateSize(n) || !this.ValidateSize(m))
            {
                Console.WriteLine("Invalid values for N or M. The values must br less than 100, even and positive.");
                int[] input = Console.ReadLine()
                                     .TrimEnd()
                                     .Split(' ')
                                     .Select(int.Parse)
                                     .ToArray();
                n = input[0];
                m = input[1];
            }

            this.Layout = new int[n, m];
            this.rows = n;
            this.columns = m;
        }

        /// <summary>
        /// Validates the given value for the size of the layer.
        /// </summary>
        /// <param name="size">An integer used for the size of the layer.</param>
        /// <returns><c>true</c> if <c>size</c> is valid; otheriwse <c>false</c></returns>
        private bool ValidateSize(int size)
        {
            if (size < 2 || size > 100)
            {
                return false;
            }
            else if (size % 2 != 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// <para>Input the layout of the layer from the console.</para>
        /// <para>Validates each row for the number of values.</para>
        /// <para>Validates the whole layout after input.</para>
        /// </summary>
        public void EnterLayout()
        {
            for (int i = 0; i < this.rows; i++)
            {
                int[] input = Console.ReadLine()
                                     .TrimEnd()
                                     .Split(' ')
                                     .Select(int.Parse)
                                     .ToArray();
                for (int j = 0; j < this.columns; j++)
                {
                    while (input.Length != this.columns)
                    {
                        Console.WriteLine("The input must be the same as the set size");
                        input = Console.ReadLine()
                                     .TrimEnd()
                                     .Split(' ')
                                     .Select(int.Parse)
                                     .ToArray();
                    }
                    this.Layout[i, j] = input[j];
                }
            }

            if (!this.ValidateLayout())
            {
                Console.WriteLine("This layout is invalid. You cannot have a single brick span more than 2 spaces in a row/column.");
                this.EnterLayout();
            }
        }

        /// <summary>
        /// Checks the layout of the layer for bricks that span 3 rows/columns.
        /// </summary>
        /// <returns><c>true</c> if the layout is valid; otherwise <c>false</c></returns>
        private bool ValidateLayout()
        {
            if (this.rows == 2)
            {
                for (int i = 0; i < this.rows; i++)
                {
                    for (int j = 0; j < this.columns - 2; j++)
                    {
                        if (this.Layout[i, j] == this.Layout[i, j + 2])
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < this.rows - 2; i++)
                {
                    for (int j = 0; j < this.columns - 2; j++)
                    {
                        if (this.Layout[i, j] == this.Layout[i + 2, j])
                        {
                            return false;
                        }
                        if (this.Layout[i, j] == this.Layout[i, j + 2])
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        //Method that prints the array Layout in the console.

        /// <summary>
        /// Outputs the layout of the layer to the console.
        /// </summary>
        public void PrintLayout()
        {
            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    Console.Write("{0} ", this.Layout[i, j]);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// <para>Attampts to generate the next layer of bricks.</para>
        /// </summary>
        /// <param name="layer">A 2D-array of integers that represents the layout of the new layer.</param>
        /// <param name="brickNumber">The number of the current brick.</param>
        /// <returns><c>true</c> if a solution is found; otherwise <c>false</c></returns>
        public bool GenerateNextLayer(int[,] layer, int brickNumber)
        {
            if (brickNumber > (this.rows * this.columns) / 2)
            {
                return true;
            }

            for (int i = 0; i < this.rows; i++)
            {
                for (int j = 0; j < this.columns; j++)
                {
                    if (layer[i, j] == 0)
                    {
                        if (CanPlaceRight(layer, i, j))
                        {
                            layer[i, j] = brickNumber;
                            layer[i, j + 1] = brickNumber;
                            if (GenerateNextLayer(layer, brickNumber + 1))
                            {
                                return true;
                            }
                            else
                            {
                                layer[i, j] = 0;
                                layer[i, j + 1] = 0;
                            }
                        }
                        else if (CanPlaceDown(layer, i, j))
                        {
                            layer[i, j] = brickNumber;
                            layer[i + 1, j] = brickNumber;
                            if (GenerateNextLayer(layer, brickNumber + 1))
                            {
                                return true;
                            }
                            else
                            {
                                layer[i, j] = 0;
                                layer[i + 1, j] = 0;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if it's possible to place the brick to the right of the current position
        /// </summary>
        /// <param name="layer">A 2D-arrray of integers that represents the layout of the bricks.</param>
        /// <param name="row">The number of the row of the current possition</param>
        /// <param name="column">The number of the column of the current position</param>
        /// <returns><c>true</c> if it's possible to place the brick to the right; otherwise <c>false</c></returns>
        private bool CanPlaceRight(int[,] layer, int row, int column)
        {
            if (column + 1 >= this.columns)
            {
                return false;
            }

            if (layer[row, column + 1] != 0)
            {
                return false;
            }

            if (this.Layout[row, column] == this.Layout[row, column + 1])
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if it's possible to place the brick below the current position
        /// </summary>
        /// <param name="layer">A 2D-arrray of integers that represents the layout of the bricks.</param>
        /// <param name="row">The number of the row of the current possition</param>
        /// <param name="column">The number of the column of the current position</param>
        /// <returns><c>true</c> if it's possible to place the brick below; otherwise <c>false</c></returns>
        private bool CanPlaceDown(int[,] layer, int row, int column)
        {
            if (row + 1 >= this.rows)
            {
                return false;
            }

            if (layer[row + 1, column] != 0)
            {
                return false;
            }

            if (this.Layout[row, column] == this.Layout[row + 1, column])
            {
                return false;
            }

            return true;
        }
    }
}
