using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace FinalProject_1
{
    internal class AddShapeAction
    {
        private WorkingWithField Field;
        private List<Figure> ListFigures;
        private readonly FigurePainter FigurePainter;
        public AddShapeAction(WorkingWithField field, List<Figure> listFigures)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-en");
            Field = field;
            ListFigures = listFigures;
            FigurePainter = new FigurePainter(field);
            Start();
        }

        private void Start()
        {
            int num;
            Console.WriteLine("1.\tAdd line");
            Console.WriteLine("2.\tAdd triangle");
            Console.WriteLine("3.\tAdd rect");
            Console.WriteLine("4.\tAdd circle");
            Console.WriteLine("5.\tCancel adding");
            while (true)
            {
                Console.Write("Enter the number: ");
                if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (num < 1 || num > 5) Console.WriteLine("Incorrect number. Try again\n\n");
                else break;
            }
            Console.WriteLine();
            switch (num)
            {
                case 1:
                    AddShapeLine();
                    break;
                case 2:
                    AddShapeTriangle();
                    break;
                case 3:
                    AddShapeRect();
                    break;
                case 4:
                    AddShapeCircle();
                    break;
                case 5:
                    Console.WriteLine("Done!\n");
                    return;
            }
            Console.WriteLine("Done!\n");
        }

        private void AddShapeLine()
        {
            var Points = new List<(int, int)>();
            string[] input;
            int[] coords = new int[2];

            while (true)
            {
                Console.WriteLine("Enter the coordinates of the first point separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            }   //first point
            while (true)
            {
                Console.WriteLine("Enter the coordinates of the second point separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            }   //second point         

            var newFigure = new Figure("line", Points);
            ListFigures.Add(newFigure);

            FigurePainter.DrawLine(Points, (char)(ListFigures.Count + 47) );
        }

        private void AddShapeTriangle()
        {
            var Points = new List<(int, int)>();
            string[] input;
            int[] coords = new int[2];
            Console.WriteLine("Be careful: the program does not check if all points are on the same straight line!\n");

            while (true)
            {
                Console.WriteLine("Enter the coordinates of the first point separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            }   //first point
            while (true)
            {
                Console.WriteLine("Enter the coordinates of the second point separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            }   //second point         
            while (true)
            {
                Console.WriteLine("Enter the coordinates of the third point separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            }   //third point

            bool contourOnly = IsContourOnly();
            var newFigure = new Figure("triangle", Points, contourOnly);
            ListFigures.Add(newFigure);

            FigurePainter.DrawTriangle(Points, (char)(ListFigures.Count + 47), contourOnly);
        }

        private void AddShapeRect()
        {
            var Points = new List<(int, int)>();
            string[] input;
            int[] coords = new int[2];
            Console.WriteLine("Be careful: x2 must be greater than x1 and y2 must be less than y1!\n");

            while (true)
            {
                Console.WriteLine("Enter the coordinates of the top left point separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            }   //first point
            while (true)
            {
                Console.WriteLine("Enter the coordinates of the bottom right point separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else if (coords[0] <= Points[0].Item1 || coords[1] >= Points[0].Item2)
                    Console.WriteLine("x2 must be greater than x1 and y2 must be less than y1! Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            }   //second point         

            bool contourOnly = IsContourOnly();
            var newFigure = new Figure("rect", Points, contourOnly);
            ListFigures.Add(newFigure);

            FigurePainter.DrawRect(Points, (char)(ListFigures.Count + 47), contourOnly);
        }

        private void AddShapeCircle()
        {
            var Points = new List<(int, int)>();
            string[] input;
            int[] coords = new int[2];
            int radius;

            while (true)
            {
                Console.WriteLine("Enter the coordinates of the center of the circle separated by comma.\n" +
                    "Numbers must be between -75 and 75: ");
                input = Console.ReadLine().Trim(' ').Split(',');
                if (input.Length != 2) Console.WriteLine("There should only be 2 numbers separated by comma. Try again\n\n");
                else if (!int.TryParse(input[0], out coords[0]) || !int.TryParse(input[1], out coords[1]))
                    Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (coords[0] < -75 || coords[0] > 75 || coords[1] < -75 || coords[1] > 75)
                    Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0], coords[1]));
                    break;
                }
            } //center
            while (true)
            {
                Console.Write("Enter the radius of the circle.\n" +
                    "The number must be between 0 (not including) and 25: ");
                if (!int.TryParse(Console.ReadLine().Trim(' '), out radius)) Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (radius < 1 || radius > 25) Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    Points.Add((coords[0] + radius, coords[1]));
                    break;
                }
            } //radius

            bool contourOnly = IsContourOnly();
            var newFigure = new Figure("circle", Points, contourOnly);
            ListFigures.Add(newFigure);

            FigurePainter.DrawCircle(Points, (char)(ListFigures.Count + 47), contourOnly);
        }

        private bool IsContourOnly()
        {
            Console.WriteLine("Do you want to paint over the entire shape?\nY(y)\t—\tyes;\nN(n)\t—\tno;\n");
            Console.Write("Enter 1 of 2 ways: ");
            var tap = Console.ReadKey();
            while (tap.Key != ConsoleKey.Y && tap.Key != ConsoleKey.N)
            {
                Console.Write("\nPlease choose 1 of 2 ways: ");
                tap = Console.ReadKey();
            }
            Console.WriteLine("\n");
            if (tap.Key == ConsoleKey.N) return true;
            else return false;
        }
    }
}
