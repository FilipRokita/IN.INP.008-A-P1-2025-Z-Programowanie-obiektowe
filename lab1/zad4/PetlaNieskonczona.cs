using System;

namespace Lab01Zad4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CzytajLiczbyDopokiNieUjemna();
        }

        static void CzytajLiczbyDopokiNieUjemna()
        {
            // dopoki uzytkownik nie wpisze liczby ujemnej, jedziemy dalej
            while (true)
            {
                Console.Write("podaj liczbę całkowitą (ujemna kończy): ");
                int x = Convert.ToInt32(Console.ReadLine());

                if (x < 0)
                {
                    Console.WriteLine("podano liczbę ujemną – koniec pętli");
                    break;
                }

                Console.WriteLine($"podałeś: {x}");
            }
        }
    }
}
