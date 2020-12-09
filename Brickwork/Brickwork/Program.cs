using System;
using System.Linq;

namespace Brickwork
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine()
                                 .Split(' ')
                                 .Select(int.Parse)
                                 .ToArray();
            int n = input[0];
            int m = input[1];
            BrickLayer firstLayer = new BrickLayer(n, m);
            firstLayer.EnterLayout();
            firstLayer.PrintLayout();
        }
    }
}
