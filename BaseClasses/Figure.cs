using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace FinalProject_1
{
    [Serializable]
    [DataContract]
    public class Figure
    {
        [DataMember(Name = "title")]
        [JsonPropertyName("title")]
        public string Title {get; set;}
        [DataMember(Name = "points")]
        [JsonPropertyName("points")]
        //[JsonIgnore]
        public List<(int, int)> Points { get; set; } = new List<(int, int)>();
        [DataMember(Name = "isContourOnly")]
        [JsonPropertyName("isContourOnly")]
        public bool ContourOnly { get; set; } = true;
        public Figure() { }
        public Figure(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException();
            if (title != "line" && title != "triangle" && title != "rect" && title != "circle")
                throw new ArgumentException();
            Title = title;
        }
        public Figure(string title, bool contourOnly)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException();
            if (title != "line" && title != "triangle" && title != "rect" && title != "circle")
                throw new ArgumentException();
            Title = title;
            ContourOnly = contourOnly;
        }
        public Figure(string title, List<(int, int)> points)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException();
            if (title != "line" && title != "triangle" && title != "rect" && title != "circle")
                throw new ArgumentException();
            if ((title == "line" || title == "rect" || title == "circle") && points.Count != 2)
                throw new ArgumentOutOfRangeException();
            if (title == "triangle" && points.Count != 3)
                throw new ArgumentOutOfRangeException();
            Title = title;
            Points = points;
        }
        public Figure(string title, List<(int, int)> points, bool contourOnly)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException();
            if (title != "line" && title != "triangle" && title != "rect" && title != "circle")
                throw new ArgumentException();
            if ((title == "line" || title == "rect" || title == "circle") && points.Count != 2)
                throw new ArgumentOutOfRangeException();
            if (title == "triangle" && points.Count != 3)
                throw new ArgumentOutOfRangeException();
            Title = title;
            Points = points;
            ContourOnly = contourOnly;
        }

        internal double Area()
        {
            if (Title == "line")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int y1 = Points[0].Item2;
                int y2 = Points[1].Item2;
                return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            }
            else if (Title == "triangle")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int x3 = Points[2].Item1;
                int y1 = Points[0].Item2;
                int y2 = Points[1].Item2;
                int y3 = Points[2].Item2;
                return Math.Abs(0.5 * ((y2 - y3) * (x1 - x3) - (y1 - y3) * (x2 - x3)));
            }
            else if (Title == "rect")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int y1 = Points[0].Item2;
                int y2 = Points[1].Item2;
                return Math.Abs(y2 - y1) * Math.Abs(x2 - x1);
            }
            else if (Title == "circle")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int radius = x2 - x1;
                return Math.PI * Math.Pow(radius, 2);
            }
            else throw new ArgumentException("incorrect title");
        }

        internal double Perimeter()
        {
            if (Title == "line")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int y1 = Points[0].Item2;
                int y2 = Points[1].Item2;
                return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            }
            else if (Title == "triangle")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int x3 = Points[2].Item1;
                int y1 = Points[0].Item2;
                int y2 = Points[1].Item2;
                int y3 = Points[2].Item2;
                double a = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
                double b = Math.Sqrt(Math.Pow(x3 - x1, 2) + Math.Pow(y3 - y1, 2));
                double c = Math.Sqrt(Math.Pow(x3 - x2, 2) + Math.Pow(y3 - y2, 2));
                return a + b + c;
            }
            else if (Title == "rect")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int y1 = Points[0].Item2;
                int y2 = Points[1].Item2;
                return 2*(Math.Abs(y2 - y1) + Math.Abs(x2 - x1));
            }
            else if (Title == "circle")
            {
                int x1 = Points[0].Item1;
                int x2 = Points[1].Item1;
                int radius = x2 - x1;
                return 2* Math.PI * radius;
            }
            else throw new ArgumentException("incorrect title");
        }
    }
}
