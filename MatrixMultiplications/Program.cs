using System;

namespace MatrixMultiplications
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var matrix = Matrix2DMapping.Create2DMatrix((0,0,3.1), (2,0,2.4), (0,2,2));
            var coordinate = Matrix2DMapping.CalculateCoordinate(matrix);
            Console.WriteLine($"X: {Math.Round(coordinate.X, 2)} Y: {Math.Round(coordinate.Y, 2)}");
            //Console.ReadLine();
        }
    }
}