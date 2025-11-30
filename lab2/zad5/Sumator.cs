using System;

namespace Lab02Zad5
{
    internal class Sumator
    {
        // tablica liczb, po zmianie widocznosci na private
        private int[] Liczby;

        public Sumator(int[] liczby)
        {
            // na wszelki wypadek kopiujemy, zeby nam nikt z zewnatrz nie mieszał w tablicy
            Liczby = liczby ?? Array.Empty<int>();
        }

        public int Suma()
        {
            int suma = 0;
            foreach (int x in Liczby)
            {
                suma += x;
            }
            return suma;
        }

        public int SumaPodziel2()
        {
            int suma = 0;
            foreach (int x in Liczby)
            {
                if (x % 2 == 0)
                {
                    suma += x;
                }
            }
            return suma;
        }

        public int IleElementow()
        {
            return Liczby.Length;
        }

        public void WypiszWszystkie()
        {
            // takie proste wypisanie po kolei
            Console.WriteLine("wszystkie elementy tablicy:");
            for (int i = 0; i < Liczby.Length; i++)
            {
                Console.WriteLine($"[{i}] = {Liczby[i]}");
            }
        }

        public void WypiszZakres(int lowIndex, int highIndex)
        {
            // lekkie ogarnięcie indeksów, żeby nie wylecieć poza zakres
            if (Liczby.Length == 0)
            {
                Console.WriteLine("tablica jest pusta");
                return;
            }

            if (lowIndex < 0)
            {
                lowIndex = 0;
            }

            if (highIndex >= Liczby.Length)
            {
                highIndex = Liczby.Length - 1;
            }

            if (lowIndex > highIndex)
            {
                Console.WriteLine("zakres indeksów jest pusty");
                return;
            }

            Console.WriteLine($"elementy od {lowIndex} do {highIndex}:");
            for (int i = lowIndex; i <= highIndex; i++)
            {
                Console.WriteLine($"[{i}] = {Liczby[i]}");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            int[] dane = { 1, 2, 3, 4, 5, 6 };
            var s = new Sumator(dane);

            s.WypiszWszystkie();
            Console.WriteLine($"suma: {s.Suma()}");
            Console.WriteLine($"suma liczb podzielnych przez 2: {s.SumaPodziel2()}");
            Console.WriteLine($"liczba elementów: {s.IleElementow()}");

            s.WypiszZakres(1, 4);
            s.WypiszZakres(-2, 100);
        }
    }
}
