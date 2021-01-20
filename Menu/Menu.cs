using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace FinalProject_1
{
    class Menu
    {
        private readonly WorkingWithField Field;
        private List<Figure> ListFigures;
        private readonly WorkingWithFile WorkingWithFile;
        private readonly FigurePainter FigurePainter;

        public Menu()
        {
            Field = new WorkingWithField();
            ListFigures = new List<Figure>();
            WorkingWithFile = new WorkingWithFile();
            FigurePainter = new FigurePainter(Field);
            StartMenu();
        }

        private void StartMenu()
        {
            while (true)
            {
                int num;
                DisplaySceneMenu();       //Display scene
                Console.WriteLine("1.\tActions with shapes");
                Console.WriteLine("2.\tReorder shapes");
                Console.WriteLine("3.\tDownload from file");
                Console.WriteLine("4.\tUpload to file");
                Console.WriteLine("5.\tExit");

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
                        ActionWithShapesMenu();
                        break;
                    case 2:
                        ReorderShapesMenu();
                        break;
                    case 3:
                        DownloadFromFileMenu();
                        break;
                    case 4:
                        UploadToFileMenu();
                        break;
                    case 5:
                        ExitMenu();
                        break;
                }
            }
        }

        private void DisplaySceneMenu() => Field.DisplayField();

        private void ActionWithShapesMenu() => new ActionWithShapes(Field, ListFigures);

        private void ReorderShapesMenu()
        {
            if (ListFigures.Count > 1) new ReorderShapes(Field, ListFigures);
            else Console.WriteLine("Not enough figures!\n");
        }

        private void DownloadFromFileMenu()
        {
            var xml = WorkingWithFile.TakeFromFile();
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Figure>));
                if (!string.IsNullOrEmpty(xml))
                {                    
                    using (FileStream fs = new FileStream("figures.xml", FileMode.OpenOrCreate))
                    {
                        ListFigures = (List<Figure>)formatter.Deserialize(fs);
                    }
                    FigurePainter.DrawAll(ListFigures);
                }
                else throw new JsonException();
            }
            catch (JsonException)
            {
                Console.WriteLine("Failed to download data from file!\n");
                return;
            }
            Console.WriteLine("Done!\n");
        }

        private void UploadToFileMenu()
        {
            WorkingWithFile.PushToFile(ListFigures);
            Console.WriteLine("Done!\n");
        }

        private void ExitMenu()
        {
            Console.Write("See you soon! Press any key and the program will exit.");
            Console.ReadKey();
            Process.GetCurrentProcess().Kill();
        }
    }
}
