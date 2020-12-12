using System;
using System.Linq;

namespace Brickwork
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] input = Console.ReadLine()
                                 .TrimEnd()
                                 .Split(' ')
                                 .Select(int.Parse)
                                 .ToArray();
            int n = input[0];
            int m = input[1];
            BrickLayer firstLayer = new BrickLayer(n, m);
            firstLayer.EnterLayout();
            BrickLayer secondLayer = new BrickLayer(firstLayer.Layout.GetLength(0), firstLayer.Layout.GetLength(1));
            if (firstLayer.GenerateNextLayer(secondLayer.Layout, 1))
            {
                Console.WriteLine();
                secondLayer.PrintLayout();
            }
            else
            {
                Console.WriteLine("-1");
                Console.WriteLine("There is no solution");
            }
        }
    }
}
