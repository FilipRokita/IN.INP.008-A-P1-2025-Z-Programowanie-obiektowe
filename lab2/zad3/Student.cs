using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab02Zad3
{
    internal class Student
    {
        // dane podstawowe
        public string Imie { get; set; }
        public string Nazwisko { get; set; }

        // tutaj trzymamy oceny, lista jest wygodna do dodawania
        private readonly List<int> oceny = new List<int>();

        public double SredniaOcen
        {
            get
            {
                if (oceny.Count == 0)
                {
                    // jak nie ma ocen, to sensowniej zwrocic 0.0
                    return 0.0;
                }

                return oceny.Average();
            }
        }

        public Student(string imie, string nazwisko)
        {
            Imie = imie;
            Nazwisko = nazwisko;
        }

        public void DodajOcene(int ocena)
        {
            // taka szybka walidacja klasycznego zakresu ocen 1–6
            if (ocena < 1 || ocena > 6)
            {
                throw new ArgumentException("Ocena powinna być w przedziale 1–6.");
            }

            oceny.Add(ocena);
        }

        public void WyswietlInformacje()
        {
            Console.WriteLine($"Student: {Imie} {Nazwisko}");
            Console.WriteLine($"Liczba ocen: {oceny.Count}, średnia: {SredniaOcen:F2}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var s = new Student("Anna", "Nowak");
            s.DodajOcene(5);
            s.DodajOcene(4);
            s.DodajOcene(3);
            s.WyswietlInformacje();
        }
    }
}
