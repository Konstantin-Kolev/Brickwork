using System;
using System.Linq;

namespace Brickwork
{
    public class BrickLayer
    {
        //The array which holds the values that represent the layout of the bricks in the layer. 
        private int[,] layout;

        public int[,] Layout
        {
            get { return layout; }
            set { layout = value; }
        }

        //Constructor for the class which implements validation for the size of the array Layout.
        //If the values are invalid it reqeusts new values from the user.
        public BrickLayer(int n, int m)
        {
            while (!this.ValidateSize(n) || !this.ValidateSize(m))
            {
                Console.WriteLine("Invalid values for N or M. The values must br less than 100, even and positive.");
                int[] input = Console.ReadLine()
                                     .Split(' ')
                                     .Select(int.Parse)
                                     .ToArray();
                n = input[0];
                m = input[1];
            }
            this.Layout = new int[n, m];
        }

        //Method, used for validating the values used for the size of the array Layout.
        //The values are checked to be in bounds for the given size and to be even.
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

        //Method that takes the input from the user and writes in the array Layout.
        //Validation checks if the input is the same size as the given values for N and M and if there are bricks spanning 3 rows/columns.
        //If the input is invalid the method requesta new one from the user.
        public void EnterLayout()
        {
            for (int i = 0; i < this.Layout.GetLength(0); i++)
            {
                int[] input = Console.ReadLine()
                                     .Split(' ')
                                     .Select(int.Parse)
                                     .ToArray();
                for (int j = 0; j < this.Layout.GetLength(1); j++)
                {
                    while (input.Length != this.Layout.GetLength(1))
                    {
                        Console.WriteLine("The input must be the same as the set size");
                        input = Console.ReadLine()
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

        //Method that validates the layout of the bricks
        //It checks to see if any value is the same as the one 2 spaces from it in its row or column.
        //If any values are found to be equal the whole layout is marked as invalid.
        private bool ValidateLayout()
        {
            if (this.Layout.GetLength(0) == 2)
            {
                for (int i = 0; i < this.Layout.GetLength(0); i++)
                {
                    for (int j = 0; j < this.Layout.GetLength(1) - 2; j++)
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
                for (int i = 0; i < this.Layout.GetLength(0) - 2; i++)
                {
                    for (int j = 0; j < this.Layout.GetLength(1) - 2; j++)
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
        public void PrintLayout()
        {
            for (int i = 0; i < this.Layout.GetLength(0); i++)
            {
                for (int j = 0; j < this.Layout.GetLength(1); j++)
                {
                    Console.Write("{0} ", this.Layout[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
