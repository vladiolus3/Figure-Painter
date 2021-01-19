using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject_1
{
    internal class ActionWithShapes
    {
        private WorkingWithField Field;
        private List<Figure> ListFigures;
        private readonly FigurePainter FigurePainter;
        public ActionWithShapes(WorkingWithField field, List<Figure> listFigures)
        {
            Field = field;
            ListFigures = listFigures;
            FigurePainter = new FigurePainter(field);
            Start();
        }

        private void Start()
        {
            while (true)
            {
                int num;
                Console.WriteLine("1.\tDisplay scene");
                Console.WriteLine("2.\tAdd shape");
                Console.WriteLine("3.\tDelete shape");
                Console.WriteLine("4.\tMove shape");
                Console.WriteLine("5.\tReturn to main menu");

                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 1 || num > 5) Console.WriteLine("Incorrect number. Try again");
                    else break;
                }
                Console.WriteLine();
                switch (num)
                {
                    case 1:
                        DisplayScene();
                        Console.WriteLine("Done!\n");
                        break;
                    case 2:
                        AddShape();
                        break;
                    case 3:
                        RemoveShape();
                        break;
                    case 4:
                        MoveShape();
                        break;
                    case 5:
                        Console.WriteLine("Done!\n");
                        return;
                }
            }
        }

        private void DisplayScene() => Field.DisplayField();

        private void AddShape()
        {
            if (ListFigures.Count > 10) Console.WriteLine("The maximum number of shapes has already been created!\n");
            else new AddShapeAction(Field, ListFigures);
        }

        private void RemoveShape()
        {
            if (ListFigures.Count == 0) 
            {
                Console.WriteLine("You have no figures!\n");
                return;
            }
            int index;
            while (true)
            {
                Console.Write("Enter shape index: ");
                if (!int.TryParse(Console.ReadLine(), out index)) Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (index < 0 || index > ListFigures.Count - 1) Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else
                {
                    ListFigures.RemoveAt(index);
                    Field = FigurePainter.DrawAll(ListFigures);
                    break;
                }
            }
            Console.WriteLine("Done!\n");
        }

        private void MoveShape()
        {
            if (ListFigures.Count == 0)
            {
                Console.WriteLine("You have no figures!\n");
                return;
            }
            int index;
            while (true)
            {
                Console.Write("Enter shape index: ");
                if (!int.TryParse(Console.ReadLine(), out index)) Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (index < 0 || index > ListFigures.Count - 1) Console.WriteLine("The number is not in the specified range. Try again\n\n");
                else break;
            }

            Console.WriteLine("How do you want to move the shape?\nW(w)\t—\tNorth;\nS(s)\t—\tSouth;\n" +
                "A(a)\t—\tWest;\nD(d)\t—\tEast;\n");
            Console.Write("Enter 1 of 4 ways: ");
            var tap = Console.ReadKey();
            while (tap.Key != ConsoleKey.W && tap.Key != ConsoleKey.S && tap.Key != ConsoleKey.A && tap.Key != ConsoleKey.D)
            {
                Console.Write("\nPlease choose 1 of 2 ways: ");
                tap = Console.ReadKey();
            }
            Console.WriteLine("\n");

            int distance;
            while (true)
            {
                Console.Write("Enter distance: ");
                if (!int.TryParse(Console.ReadLine(), out distance)) Console.WriteLine("The only numbers can be entered. Try again\n\n");
                else if (distance < 1 || distance > Field.N)
                    Console.WriteLine($"The number is not in the specified range (0(not including), {Field.N}). Try again\n\n");
                else break;
            }

            var points = new List<(int, int)>();
            if (tap.Key == ConsoleKey.W)
            {
                var figure = ListFigures[index];
                points = figure.Points
                    .Select(x => (x.Item1, x.Item2 + distance > 75 ? 75 : x.Item2 + distance)).ToList();
                ListFigures[index] = new Figure(figure.Title, points, figure.ContourOnly);
            }
            else if (tap.Key == ConsoleKey.S)
            {
                var figure = ListFigures[index];
                points = figure.Points
                    .Select(x => (x.Item1, x.Item2 - distance < -75 ? 075 : x.Item2 - distance)).ToList();
                ListFigures[index] = new Figure(figure.Title, points, figure.ContourOnly);
            }
            else if (tap.Key == ConsoleKey.A)
            {
                var figure = ListFigures[index];
                points = figure.Points
                    .Select(x => (x.Item1 - distance < -75 ? -75 : x.Item1 - distance, x.Item2)).ToList();
                ListFigures[index] = new Figure(figure.Title, points, figure.ContourOnly);
            }
            else if (tap.Key == ConsoleKey.D)
            {
                var figure = ListFigures[index];
                points = figure.Points
                    .Select(x => (x.Item1 + distance > 75 ? 75 : x.Item1 + distance, x.Item2)).ToList();
                ListFigures[index] = new Figure(figure.Title, points, figure.ContourOnly);
            }
            Field = FigurePainter.DrawAll(ListFigures);
            Console.WriteLine("Done!\n");
        }
    }
}
