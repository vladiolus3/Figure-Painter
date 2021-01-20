using System;

namespace FinalProject_1
{
    internal class WorkingWithField
    {
        internal readonly int N = 25, M = 50;
        private readonly char[,] Field;

        internal WorkingWithField()
        {
            Field = new char[N, M];
            Init();
        }

        internal void Init()
        {
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++) Field[i, j] = '*';
        }

        internal void Set(int x, int y, char symb) => Field[x, y] = symb;
        internal char Get(int x, int y) => Field[x, y];

        internal void DisplayField()
        {
            Console.WriteLine("\nYour scene:");
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++) Console.Write(Field[i, j]);
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
