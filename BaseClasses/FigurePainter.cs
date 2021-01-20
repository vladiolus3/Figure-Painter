using System;
using System.Collections.Generic;

namespace FinalProject_1
{
    internal class FigurePainter
    {
        private readonly WorkingWithField Field;
        internal FigurePainter(WorkingWithField field) => Field = field;
        internal WorkingWithField DrawAll(List<Figure> listFigures)
        {
            Field.Init();
            for (int i = 0; i < listFigures.Count; i++)
            {
                var figure = listFigures[i];
                if (figure.Title == "line") DrawLine(figure.Points, (char)(i + 48));
                else
                    if (figure.Title == "triangle") DrawTriangle(figure.Points, (char)(i + 48), figure.ContourOnly);
                else
                    if (figure.Title == "rect") DrawRect(figure.Points, (char)(i + 48), figure.ContourOnly);
                else
                    if (figure.Title == "circle") DrawCircle(figure.Points, (char)(i + 48), figure.ContourOnly);
            }
            return Field;
        }

        internal void DrawLine(List<(int, int)> points, char symb)
        {
            int x1 = points[0].Item1;
            int y1 = points[0].Item2;
            int x2 = points[1].Item1;
            int y2 = points[1].Item2;
            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int signX = x1 < x2 ? 1 : -1;
            int signY = y1 < y2 ? 1 : -1;
            int error = deltaX - deltaY;
            if (x2 > -1 && x2 < Field.N && y2 > -1 && y2 < Field.M) Field.Set(x2, y2, symb);
            while (x1 != x2 || y1 != y2)
            {
                if (x1 > -1 && x1 < Field.N && y1 > -1 && y1 < Field.M) Field.Set(x1, y1, symb);
                int error2 = error * 2;
                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += signX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += signY;
                }
            }
        }

        internal void DrawTriangle(List<(int, int)> points, char symb, bool contourOnly)
        {
            int x1 = points[0].Item1;
            int y1 = points[0].Item2;
            int x2 = points[1].Item1;
            int y2 = points[1].Item2;
            int x3 = points[2].Item1;
            int y3 = points[2].Item2;
            DrawLine(new List<(int, int)>() { (x1, y1), (x2, y2) }, symb);
            DrawLine(new List<(int, int)>() { (x2, y2), (x3, y3) }, symb);
            DrawLine(new List<(int, int)>() { (x1, y1), (x3, y3) }, symb);
            if (!contourOnly)
            {
                int minX = Math.Min(Math.Min(x1, x2), x3);
                int maxX = Math.Max(Math.Max(x1, x2), x3);
                maxX = maxX > Field.N ? Field.N : maxX;
                minX = minX < 0 ? 0 : minX;

                for (int i = minX; i < maxX; i++)
                {
                    for (int j = 0; j < Field.M; j++)
                    {
                        if (Field.Get(i, j) != symb) continue;
                        else
                        {
                            while (Field.Get(i, j) == symb)
                            {
                                if (j + 1 < Field.M) j++;
                                else break;
                            }
                            bool isBorder = false;
                            for (int k = j; k < Field.M; k++)
                                if (Field.Get(i, k) == symb)
                                {
                                    isBorder = true;
                                    break;
                                }
                            if (isBorder == true)
                                while (Field.Get(i, j) != symb)
                                {
                                    Field.Set(i, j, symb);
                                    if (j + 1 < Field.M) j++;
                                    else break;
                                }
                            break;
                        }
                    }
                }
            }
        }

        internal void DrawRect(List<(int, int)> points, char symb, bool contourOnly)
        {
            int x1 = points[0].Item1;
            int y1 = points[0].Item2;
            int x2 = points[1].Item1;
            int y2 = points[1].Item2;
            for (int i = x1; i < x2 + 1; i++)
            {
                if (i > -1 && i < Field.N)
                {
                    if (!contourOnly || i == x1 || i == x2)
                        for (int j = y2; j < y1 + 1; j++)
                        {
                            if (j > -1 && j < Field.M) Field.Set(i, j, symb);
                        }
                    if (y1 > -1 && y1 < Field.M) Field.Set(i, y1, symb);
                    if (y2 > -1 && y2 < Field.M) Field.Set(i, y2, symb);
                }
            }
        }

        internal void DrawCircle(List<(int, int)> points, char symb, bool contourOnly)
        {
            int x1 = points[0].Item1;
            int y1 = points[0].Item2;
            int radius = points[1].Item1 - points[0].Item1;
            int x = 0, y = radius;
            int delta = 1 - 2 * radius;
            int error;
            while (y >= 0)
            {
                if (x1 + x < Field.N && y1 + y < Field.M) Field.Set(x1 + x, y1 + y, symb);
                if (x1 + x < Field.N && y1 - y > -1) Field.Set(x1 + x, y1 - y, symb);

                if (!contourOnly) for (int i = y1 - y + 1; i < y1 + y; i++)
                        if (x1 + x < Field.N && i > -1 && i < Field.M) Field.Set(x1 + x, i, symb);

                if (x1 - x > -1 && y1 + y < Field.M) Field.Set(x1 - x, y1 + y, symb);
                if (x1 - x > -1 && y1 - y > -1) Field.Set(x1 - x, y1 - y, symb);

                if (!contourOnly) for (int i = y1 - y + 1; i < y1 + y; i++)
                        if (x1 - x > -1 && i > -1 && i < Field.M) Field.Set(x1 - x, i, symb);

                error = 2 * (delta + y) - 1;
                if ((delta < 0) && (error <= 0))
                {
                    delta += 2 * ++x + 1;
                    continue;
                }
                if ((delta > 0) && (error > 0))
                {
                    delta -= 2 * --y + 1;
                    continue;
                }
                delta += 2 * (++x - --y);
            }
        }
    }
}
