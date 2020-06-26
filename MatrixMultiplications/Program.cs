using System;

namespace MatrixMultiplications
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var matrix = MatrixCalculations.Create3DMatrix((0,0,3.1), (2,0,2.4), (0,2,2));
            var coordinate = MatrixCalculations.CalculateCoordinate(matrix);
            Console.WriteLine($"X: {Math.Round(coordinate.X, 2)} Y: {Math.Round(coordinate.Y, 2)}");
            //Console.ReadLine();
        }
        
    }

    class MatrixCalculations
    {
        public static double[,] Create3DMatrix(
            (double x, double y, double beaconDistance) gatewayA, 
            (double x, double y, double beaconDistance) gatewayB, 
            (double x, double y, double beaconDistance) gatewayC) 
        {
            return new double[,]
            {
                { gatewayA.x, gatewayA.y, gatewayA.beaconDistance }, //Gateway A
                { gatewayB.x, gatewayB.y, gatewayB.beaconDistance }, //Gateway B
                { gatewayC.x, gatewayC.y, gatewayC.beaconDistance } //Gateway C
            };
        }
        //
        public static Coordinate CalculateCoordinate(double[,] gatewayMatrix)
        {
            var finalMatrix = MultiplyGatewayCoordinatesByTwo(gatewayMatrix);
            var determinantMatrix = finalMatrix;
            finalMatrix = SwapAndInverse(finalMatrix);
            finalMatrix = CalculateDeterminant(determinantMatrix);
            
            //Calculate K Values;
            double[] kValues = new double[3];
            kValues[0] = AddAndSquare(gatewayMatrix[0, 0], gatewayMatrix[0, 1]);
            kValues[1] = AddAndSquare(gatewayMatrix[1, 0], gatewayMatrix[1, 1]);
            kValues[2] = AddAndSquare(gatewayMatrix[2, 0], gatewayMatrix[2, 1]);
            //
            var bOne = SubtractAndSquare(gatewayMatrix[0, 2], gatewayMatrix[1, 2]) - kValues[0] + kValues[1];
            var bTwo = SubtractAndSquare(gatewayMatrix[0, 2], gatewayMatrix[2, 2]) - kValues[0] + kValues[2];
            //
            var XY = MultiplyBIntoMatrix(finalMatrix, bOne, bTwo);
            //-
            Coordinate coordinate = new Coordinate(XY[0,0], XY[0,1]);
            return coordinate;
        }
        
        private static double[,] SwapAndInverse(double[,] matrixMultiple)
        {
            var swap = matrixMultiple[0, 0];
            matrixMultiple[0, 0] = matrixMultiple[1, 1];
            matrixMultiple[1, 1] = swap;
            matrixMultiple[0, 1] = matrixMultiple[0, 1] * -1;
            matrixMultiple[1, 0] = matrixMultiple[1, 0] * -1;
            return matrixMultiple;
        }

        private static double[,] CalculateDeterminant(double[,] matrix)
        {
            try
            {
                var determinantA = CalculateDeterminantA(matrix);
                matrix = CalculateMatrixInverseDeterminants(determinantA, matrix);
                return matrix;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
                //Do something.
            }
        }

        private static double[,] MultiplyGatewayCoordinatesByTwo(double[,] gatewayMatrix)
        {
            double[,] matrixOutput = new double[2, 2];
            matrixOutput[0, 0] = DoubleAndSubtract(gatewayMatrix[1, 0], gatewayMatrix[0, 0]);
            matrixOutput[0, 1] = DoubleAndSubtract(gatewayMatrix[1, 1], gatewayMatrix[0, 1]);
            //
            matrixOutput[1, 0] = DoubleAndSubtract(gatewayMatrix[2, 0], gatewayMatrix[0, 0]);
            matrixOutput[1, 1] = DoubleAndSubtract(gatewayMatrix[2, 1], gatewayMatrix[0, 1]);
            //
            return matrixOutput;
        }
        

        private static double[,] MultiplyBIntoMatrix(double[,] matrix, double b1, double b2)
        {
            var x = matrix[0, 0] * b1 + matrix[0, 1] * b2;
            var y = matrix[1, 0] * b1 + matrix[1, 1] * b2;

            return new double[,] {{x, y}};
        }

        private static double[,] CalculateMatrixInverseDeterminants(double determinant, double[,] matrix)
        {
            var inverse = 1 / determinant;
            var returnMatrix = new double[2, 2];
            //
            returnMatrix[0, 0] = matrix[0, 0] * inverse;
            returnMatrix[0, 1] = matrix[0, 1] * inverse;
            returnMatrix[1, 0] = matrix[1, 0] * inverse;
            returnMatrix[1, 1] = matrix[1, 1] * inverse;

            return returnMatrix;
        }

        private static double CalculateDeterminantA(double[,] matrix)
        {
            var determinantA = ((matrix[0, 0] * matrix[1, 1]) - (matrix[1, 0] * matrix[0, 1]));
            //
            if (determinantA == 0)
            {
                throw new DivideByZeroException();
            }

            return determinantA;
        }

        private static double SubtractAndSquare(double value1, double value2)
        {
            return Math.Pow(value1, 2) - Math.Pow(value2, 2);
        }

        private static double AddAndSquare(double x, double y)
        {
            return Math.Pow(x, 2) + Math.Pow(y, 2);
        }
        
        private static double DoubleAndSubtract(double value, double subtract)
        {
            return 2 * (value - subtract);
        }
    }

    class GatewayCoordinates
    {
        public GatewayCoordinates(Coordinate[] gateways)
        {
            this._gateways = gateways;
        }
        //
        private Coordinate[] _gateways;
        //
        public Coordinate[] GetGatewayCoordinates()
        {
            return _gateways;
        }
    }

    class Coordinate
    {
        public Coordinate(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
        public double X { get; set; }
        public double Y { get; set; }
    }
}