using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FinalProject_1
{
    internal class WorkingWithFile
    {
        internal WorkingWithFile()
        {
           using (File.Open("figures.xml", FileMode.OpenOrCreate)) { }
        }
        internal string TakeFromFile() => File.ReadAllText("figures.xml");
        internal void PushToFile(List<Figure> listFigure)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(List<Figure>));
            using (FileStream fs = new FileStream("figures.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, listFigure);
            }           
        }
    }
}
