using System;

namespace Lab01Zad2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PrzetworzTablice10();
        }

        static void PrzetworzTablice10()
        {
            // tu tylko klasyk: wczytanie 10 liczb i parę obliczen
            int[] liczby = new int[10];

            for (int i = 0; i < liczby.Length; i++)
            {
                Console.Write($"podaj liczbę #{i + 1}: ");
                liczby[i] = Convert.ToInt32(Console.ReadLine());
            }

            long iloczyn = 1;
            int suma = 0;
            int min = liczby[0];
            int max = liczby[0];

            foreach (int x in liczby)
            {
                suma += x;
                iloczyn *= x;

                if (x < min)
                {
                    min = x;
                }

                if (x > max)
                {
                    max = x;
                }
            }

            double srednia = (double)suma / liczby.Length;

            Console.WriteLine();
            Console.WriteLine($"suma: {suma}");
            Console.WriteLine($"iloczyn: {iloczyn}");
            Console.WriteLine($"średnia: {srednia}");
            Console.WriteLine($"minimum: {min}");
            Console.WriteLine($"maksimum: {max}");
        }
    }
}
