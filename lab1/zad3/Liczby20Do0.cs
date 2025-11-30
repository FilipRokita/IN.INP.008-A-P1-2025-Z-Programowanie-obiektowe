using System;

namespace Lab01Zad3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WyswietlLiczby();
        }

        static void WyswietlLiczby()
        {
            // schodzimy od 20 do 0, omijamy kilka konkretnych wartosci
            for (int i = 20; i >= 0; i--)
            {
                if (i == 2 || i == 6 || i == 9 || i == 15 || i == 19)
                {
                    continue; // skok do kolejnej iteracji
                }

                Console.Write(i + " ");
            }

            Console.WriteLine();
        }
    }
}
