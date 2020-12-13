using System;
using System.Linq;

namespace Brickwork
{
    /// <summary>
    /// <para>The main class of the program.</para>
    /// <para>Contains the <c>Main</c> method.</para>
    /// </summary>
    class Program
    {
        /// <summary>
        /// The entry point of the program.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //The input from the user for the number of rows and columns of the input.
            int[] input = Console.ReadLine()
                                 .TrimEnd()
                                 .Split(' ')
                                 .Select(int.Parse)
                                 .ToArray();
            int n = input[0];
            int m = input[1];

            //The first layer of bricks on which the next one will be based.
            BrickLayer firstLayer = new BrickLayer(n, m);

            //Input of the first layer from the user.
            firstLayer.EnterLayout();

            //Initializing of the BrickLayer object that will hold the second layer of bricks.
            BrickLayer secondLayer = new BrickLayer(firstLayer.Layout.GetLength(0), firstLayer.Layout.GetLength(1));

            //Checking if there is a possible second layer of bricks and printing it if it exists.
            //If no solution exists, an error message is displayed instead.
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
