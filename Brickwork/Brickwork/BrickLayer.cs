using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brickwork
{
    public class BrickLayer
    {
        private int[,] layer;

        public int[,] Layer
        {
            get { return layer; }
            set { layer = value; }
        }

        public BrickLayer(int n, int m)
        {
            while (!this.ValidateSize(n) || !this.ValidateSize(m))
            {
                Console.WriteLine("Invalid values for N or M. The values must br less than 100, even and positive.");
                int[] input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                n = input[0];
                m = input[1];
            }
            this.Layer = new int[n, m];
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
    }
}
