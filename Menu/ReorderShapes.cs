using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalProject_1
{
    internal class ReorderShapes
    {
        private WorkingWithField Field;
        private List<Figure> ListFigures;
        private readonly FigurePainter FigurePainter;
        public ReorderShapes(WorkingWithField field, List<Figure> listFigures)
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
                Console.WriteLine("2.\tSort ascending Area");
                Console.WriteLine("3.\tSort ascending Perimeter");
                Console.WriteLine("4.\tSort descending Area");
                Console.WriteLine("5.\tSort descending Perimeter");
                Console.WriteLine("6.\tReturn to main menu");

                while (true)
                {
                    Console.Write("Enter the number: ");
                    if (!int.TryParse(Console.ReadLine(), out num)) Console.WriteLine("The only numbers can be entered. Try again");
                    else if (num < 1 || num > 6) Console.WriteLine("Incorrect number. Try again");
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
                        AscendingArea();
                        break;
                    case 3:
                        AscendingPerimeter();
                        break;
                    case 4:
                        DescendingArea();
                        break;
                    case 5:
                        DescendingPerimeter();
                        break;
                    case 6:
                        Console.WriteLine("Done!\n");
                        return;
                }
            }
        }

        private void DisplayScene() => Field.DisplayField();

        private void AscendingArea()
        {
            ListFigures = ListFigures
                .OrderBy(x => x.Area()).ToList();
            Field = FigurePainter.DrawAll(ListFigures);
            Console.WriteLine("Done!\n");
        }

        private void AscendingPerimeter()
        {
            ListFigures = ListFigures
                .OrderBy(x => x.Perimeter()).ToList();
            Field = FigurePainter.DrawAll(ListFigures);
            Console.WriteLine("Done!\n");
        }

        private void DescendingArea()
        {
            ListFigures = ListFigures
                .OrderByDescending(x => x.Area()).ToList();
            Field = FigurePainter.DrawAll(ListFigures);
            Console.WriteLine("Done!\n");
        }

        private void DescendingPerimeter()
        {
            ListFigures = ListFigures
                .OrderByDescending(x => x.Perimeter()).ToList();
            Field = FigurePainter.DrawAll(ListFigures);
            Console.WriteLine("Done!\n");
        }
    }
}
