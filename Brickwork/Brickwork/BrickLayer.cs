using System;
using System.Linq;

namespace Brickwork
{
    public class BrickLayer
    {
        private int[,] layout;

        public int[,] Layout
        {
            get { return layout; }
            set { layout = value; }
        }

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

        private bool ValidateSize(int size)
        {
            if (size < 2 || size > 100)
            {
                return false;
            }
            else if(size%2!=0)
            {
                return false;
            }

            return true;
        }

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
                    this.Layout[i, j] = input[j];
                }
            }
        }
    }
}
